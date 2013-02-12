using Core.Domain.Interfaces;
using Core.Domain.Model;
using GeoAPI.Geometries;
using Infrastructure.DependencyResolution;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using System.Linq;

namespace IntegationTests
{
	public abstract class Behaves_like_repository_spec : Specification
	{
		protected IRepository repo;
		protected override void Arrange()
		{
			repo = IoC.Resolve<IRepository>();
		}
	}

	public class When_getting_all_states : Behaves_like_repository_spec
	{
		private IQueryable<State> _result;

		protected override void Act()
		{
			_result = repo.Find<State>();
		}

		[Test]
		public void It_should_return_the_correct_count()
		{
			Assert.That(_result.Count(), Is.EqualTo(51));
		}

		[Test]
		public void It_should_return_a_valid_geometry_for_the_geography()
		{
			var mo = _result.Where(x => x.Name == "Missouri").First();
			Assert.That(mo.Geography, Is.Not.Null);
			Assert.That(mo.Geography, Is.InstanceOf<IGeometry>());
		}
	}

	public class When_getting_bentonville : Behaves_like_repository_spec
	{
		private City _result;

		protected override void Act()
		{
			_result = repo.Get<City>(x => x.Id == 1068);
		}

		[Test]
		public void It_should_return_a_valid_geometry_for_the_geography()
		{
			Assert.That(_result.Geography, Is.Not.Null);
			Assert.That(_result.Geography, Is.InstanceOf<IGeometry>());
			Assert.That(_result.Geography, Is.TypeOf<Point>());
		}
	}

	public class When_getting_all_cities : Behaves_like_repository_spec
	{
		private IQueryable<City> _result;

		protected override void Act()
		{
			_result = repo.Find<City>();
		}

		[Test]
		public void It_should_return_a_point_as_geometry_for_all()
		{
			var list = _result.ToList();
			Assert.That(list.Where(x => x.Geography.GetType() == typeof(Point)).Count(), Is.EqualTo(3557));
			Assert.That(list.Where(x => x.Geography.GetType() == typeof(MultiPoint)).Count(), Is.EqualTo(0));
		}
	}


}
