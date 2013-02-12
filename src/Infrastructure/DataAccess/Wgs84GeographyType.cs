using GeoAPI.Geometries;
using NHibernate.Spatial.Type;
using System;

namespace Infrastructure.DataAccess
{
	[Serializable]
	public class Wgs84GeographyType : MsSql2008GeographyType
	{
		protected override void SetDefaultSRID(IGeometry geometry)
		{
			geometry.SRID = 4326;
		}
	}
}
