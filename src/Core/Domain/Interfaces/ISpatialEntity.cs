using GeoAPI.Geometries;
using System.Collections.Generic;

namespace Core.Domain.Interfaces
{
	public interface ISpatialEntity
	{
		IGeometry Geography { get; set; }
		IDictionary<string, string> Properties { get; }
	}
}
