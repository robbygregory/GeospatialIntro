using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.GeoJson
{
	[Serializable]
	public class Geometry
	{
		public string Type { get { return GetType().Name; } }
		public object Coordinates { get; set; }
	}
}
