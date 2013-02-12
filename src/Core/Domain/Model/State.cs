
using Core.Domain.Interfaces;
using GeoAPI.Geometries;
using System.Collections.Generic;
namespace Core.Domain.Model
{
	public class State : IEntity, ISpatialEntity
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual IGeometry Geography { get; set; }
		public virtual IDictionary<string, string> Properties
		{
			get
			{
				var dictionary = new Dictionary<string, string>();
				dictionary.Add("Id", Id.ToString());
				dictionary.Add("Name", Name);
				return dictionary;
			}
		}
	}
}
