using Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace Infrastructure.DataAccess.Mappings
{
	public class CityMap : ClassMap<City>
	{
		public CityMap()
		{
			Table("spatial..city");
			Id(x => x.Id, "id");
			Map(x => x.Name, "name");
			Map(x => x.Geography, "geometry").CustomType<Wgs84GeographyType>();
		}
	}
}
