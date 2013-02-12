using Core.Domain.Interfaces;
using GeoAPI.Geometries;
using System;
using System.Collections.Generic;

namespace Core.GeoJson
{
	[Serializable]
	public class Feature
	{
		public string Type { get { return GetType().Name; } }
		public Geometry Geometry { get; set; }
		public Dictionary<string, string> Properties { get; set; }

		public Feature(ISpatialEntity entity)
		{
			switch (entity.Geography.GeometryType)
			{
				case "MultiPolygon":
					Geometry = new MultiPolygon(entity.Geography as IMultiPolygon);
					break;
				case "Polygon":
					Geometry = new Polygon(entity.Geography as IPolygon);
					break;
				case "Point":
					Geometry = new Point(entity.Geography as IPoint);
					break;
				default:
					break;
			}
			Properties = entity.Properties as Dictionary<string, string>;
		}
	}
}
