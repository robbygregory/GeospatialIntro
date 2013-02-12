using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
	public interface IRepository
	{
		T Get<T>(Expression<Func<T,bool>> predicate) where T : IEntity;
		IQueryable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : IEntity;
		IQueryable<T> Find<T>() where T : IEntity;
		void Save<T>(T entity) where T : IEntity;
		void SubmitChanges();
	}
}
