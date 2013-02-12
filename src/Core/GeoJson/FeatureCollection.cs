using Core.Domain.Interfaces;
using GeoAPI.Geometries;
using System;
using System.Collections.Generic;

namespace Core.GeoJson
{
	[Serializable]
	public class FeatureCollection
	{
		public string Type { get { return GetType().Name; } }
		public IEnumerable<Feature> Features { get; set; }

		public FeatureCollection(IEnumerable<ISpatialEntity> entities)
		{
			var features = new List<Feature>();
			foreach (var entity in entities)
			{
				features.Add(new Feature(entity));
			}
			Features = features;
		}
	}
}
