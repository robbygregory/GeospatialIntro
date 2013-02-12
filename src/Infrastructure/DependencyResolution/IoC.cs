using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Interfaces;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Impl;
using NHibernate;

using StructureMap;namespace Infrastructure.DependencyResolution
{
	public class IoC
	{
		public static void Register()
		{
			ObjectFactory.Initialize(x =>
			{
				x.For<ISessionFactory>()
					.Singleton()
					.Use(() => new NHibernateHelper().BuildSessionFactory());
				x.For<ISession>()
					.HybridHttpOrThreadLocalScoped()
					.Use(ctx =>
					{
						var session = ctx.GetInstance<ISessionFactory>().OpenSession();
						session.FlushMode = FlushMode.Commit;
						session.BeginTransaction();
						return session;
					});
				x.For<IRepository>().Use<Repository>();
				x.For<ISpatialRepository>().Use<Repository>();
			});
		}

		public static T Resolve<T>() {
			return ObjectFactory.GetInstance<T>();
		}
	}
}
