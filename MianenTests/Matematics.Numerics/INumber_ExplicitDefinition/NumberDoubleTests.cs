using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mianen.Matematics.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics.Test
{
	[TestClass()]
	public class NDoubleTests
	{
		[TestMethod()]
		public void CollectionTest()
		{
			List<NDouble> l = new List<NDouble>() {.1, .2, .3};
			NDouble sum = (NDouble)l.Sum();
			Assert.AreEqual(sum.Value, .6);
		}

		[TestMethod()]
		public void NumberDoubleTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void AddTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void CompareToTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void DivideTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void EqualsTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void IsEqualTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void IsGreaterOrEqualThanTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void IsGreaterThanTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void IsLowerOrEqualThanTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void IsLowerThanTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void IsNotEqualTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void MultiplyTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void SubtractTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void ToStringTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void ToStringTest1()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void ToStringTest2()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void ToStringTest3()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetZeroTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetOneTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void NegativeTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void PowerTest()
		{
			Assert.Fail();
		}
	}
}