using GeoAPI.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.GeoJson
{
	[Serializable]
	public class MultiPolygon : Geometry
	{
		public MultiPolygon(IMultiPolygon multipolygon)
		{
			Coordinates = multipolygon.Geometries.Select(p => p.Coordinates.Select(c => new List<double> { c.Y, c.X }));
		}
	}
}
