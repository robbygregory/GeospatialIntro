using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace Core.Domain.Interfaces
{
	public interface ISpatialRepository : IRepository
	{
		IList<T> IntersectsWith<T>(IPolygon boundry) where T : class, ISpatialEntity;
	}
}
