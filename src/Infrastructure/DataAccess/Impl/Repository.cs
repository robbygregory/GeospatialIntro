using Core.Domain.Interfaces;
using GeoAPI.Geometries;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Spatial.Criterion.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Impl
{
	public class Repository : ISpatialRepository
	{
		private ISession _session;
		public Repository(ISession session) { _session = session; }

		public T Get<T>(Expression<Func<T, bool>> predicate) where T : IEntity
		{
			return Find<T>(predicate).FirstOrDefault();
		}

		public IQueryable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : IEntity
		{
			return Find<T>().Where(predicate);
		}

		public IQueryable<T> Find<T>() where T : IEntity
		{
			return _session.Query<T>();
		}

		public void Save<T>(T entity) where T : IEntity
		{
			_session.SaveOrUpdate(entity);
		}


		public void SubmitChanges()
		{
			try
			{
				_session.Transaction.Commit();
				_session.BeginTransaction();
			}
			catch
			{
				_session.Transaction.Rollback();
				throw;
			}
		}


		public IList<T> IntersectsWith<T>(IPolygon boundry) where T : class, ISpatialEntity
		{
			return _session.QueryOver<T>()
				.WhereSpatialRestrictionOn(x => x.Geography)
				.Intersects(boundry)
				.List<T>();
		}
	}
}
