using GeoAPI.Geometries;
using System;
using System.Collections.Generic;

namespace Core.GeoJson
{
	[Serializable]
	public class Point : Geometry
	{
		public Point(IPoint point)
		{
			Coordinates = new List<double> { point.Coordinate.Y, point.Coordinate.X };
		}
	}
}
