using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using System;

namespace Core.Domain.Model
{
	public class Viewport
	{
		public double North { get; set; }
		public double East { get; set; }
		public double South { get; set; }
		public double West { get; set; }


		public IPolygon Box
		{
			get
			{
				var ring = GeometryFactory.Default.CreateLinearRing(new[] {
						new Coordinate(Math.Round(West, 3), Math.Round(North, 3)),
						new Coordinate(Math.Round(West, 3), Math.Round(South, 3)),
						new Coordinate(Math.Round(East, 3), Math.Round(South, 3)),
						new Coordinate(Math.Round(East, 3), Math.Round(North, 3)),
						new Coordinate(Math.Round(West, 3), Math.Round(North, 3))
				});
				ring.SRID = 4326;
				var poly = GeometryFactory.Default.CreatePolygon(ring, null);
				poly.SRID = 4326;
				return poly;
			}
		}
	}
}
