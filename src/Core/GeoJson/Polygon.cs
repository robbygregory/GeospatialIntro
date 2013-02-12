using GeoAPI.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.GeoJson
{
	[Serializable]
	public class Polygon : Geometry
	{
		public Polygon(IPolygon polygon)
		{
			Coordinates = polygon.Coordinates.Select(c => new List<double> { c.Y, c.X });
		}
	}
}
