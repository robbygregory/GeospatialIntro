using Core.Domain.Interfaces;
using Core.Domain.Model;
using GeoAPI.Geometries;
using Infrastructure.DependencyResolution;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace IntegationTests
{
	public abstract class Behaves_like_spatialrepository_spec : Specification
	{
		protected ISpatialRepository repo;
		protected override void Arrange()
		{
			repo = IoC.Resolve<ISpatialRepository>();
		}
	}

	public class When_getting_the_state_for_a_given_viewport : Behaves_like_spatialrepository_spec
	{
		private IList<State> _result;
		protected override void Act()
		{
			var viewport = new Viewport { North = 36.3678, East = -94.1631, South = 36.3643, West = -94.170014 };
			_result = repo.IntersectsWith<State>(viewport.Box);
		}

		[Test]
		public void It_should_return_the_correct_count()
		{
			Assert.That(_result.Count(), Is.EqualTo(1));
		}

		[Test]
		public void It_should_return_the_correct_state()
		{
			Assert.That(_result.First().Name, Is.EqualTo("Arkansas"));
		}
	}
}
