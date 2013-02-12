using Core.Domain.Interfaces;
using Core.Domain.Model;
using Infrastructure.DependencyResolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
	class Program
	{
		private static IRepository _repo;
		static void Main(string[] args)
		{
			IoC.Register();
			_repo = IoC.Resolve<IRepository>();
			
			var geos = _repo.Find<City>().ToList();
			Console.WriteLine(string.Format("{0} total cities...", geos.Count()));
			
			int i =0;
			foreach (var geo in geos)
			{
				if (geo.Geography.GetType() == typeof(NetTopologySuite.Geometries.MultiPoint))
				{
					Console.WriteLine(string.Format("{0} is a MultiPoint...", geo.Name));
					var point = new NetTopologySuite.Geometries.Point(geo.Geography.Coordinates.First());
					geo.Geography = point;
					_repo.Save<City>(geo);
					_repo.SubmitChanges();
					Console.WriteLine(string.Format("{0} updated to Point...", geo.Name));
					i++;
				}
			}
			Console.WriteLine(string.Format("{0} total Cities updated...", i));
			Console.Read();
		}
	}
}
