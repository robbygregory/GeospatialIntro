using Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace Infrastructure.DataAccess.Mappings
{
	public class StateMap : ClassMap<State>
	{
		public StateMap()
		{
			Table("spatial..state");
			Id(x => x.Id, "id");
			Map(x => x.Name, "state_name");
			Map(x => x.Geography, "geometry").CustomType<Wgs84GeographyType>();
		}
	}
}
