using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.DataAccess.Impl;
using NHibernate;
using NHibernate.Spatial.Dialect;

namespace Infrastructure.DataAccess
{
	internal class NHibernateHelper
	{
		internal ISessionFactory BuildSessionFactory()
		{
			return Fluently.Configure()
					.Database(MsSqlConfiguration
						.MsSql2008
						.Dialect<MsSql2008GeographyDialect>()
						.ConnectionString(c => c.FromConnectionStringWithKey("connectionString"))
					.ShowSql())
					.Mappings(mapping => mapping.FluentMappings.AddFromAssemblyOf<Repository>())
					.BuildSessionFactory();
		}
	}
}
