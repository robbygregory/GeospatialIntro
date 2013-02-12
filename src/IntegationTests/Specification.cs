using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DependencyResolution;
using NUnit.Framework;

namespace IntegationTests
{
	[TestFixture]
	public abstract class Specification
	{
		[SetUp]
		public void Setup()
		{
			IoC.Register();
			Arrange();
			Act();
		}

		protected abstract void Arrange();
		protected abstract void Act();

		[TearDown]
		public virtual void TearDown()
		{
		}
	}
}
