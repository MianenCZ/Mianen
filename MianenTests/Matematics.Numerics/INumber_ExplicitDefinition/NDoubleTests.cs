using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mianen.Matematics.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics.Tests
{
	[TestClass()]
	public class NDoubleTests
	{
		[TestMethod()]
		public void CollectionTest()
		{
			List<NDouble> l = new List<NDouble>() { .1, .2, .3 };
			NDouble sum = (NDouble)l.Sum();
			Assert.AreEqual(sum.Value, .6);
		}

		[TestMethod()]
		public void NumberDoubleTest()
		{
			NDouble d1 = new NDouble(4.13);
			NDouble d2 = 4.13;
			NDouble d3 = 4;
		}

		[TestMethod()]
		public void CompareToTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void DivideTest()
		{
			NDouble d1 = 1.618;
			NDouble d2 = 2;
			var res = d1.Divide(d2);
		}

		[TestMethod()]
		public void EqualsTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void IsEqualTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void IsGreaterOrEqualThanTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void IsGreaterThanTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void IsLowerOrEqualThanTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void IsLowerThanTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void IsNotEqualTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void MultiplyTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void SubtractTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void ToStringTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void ToStringTest1()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void ToStringTest2()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void ToStringTest3()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void GetZeroTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void GetOneTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void NegativeTest()
		{
			throw new NotImplementedException();
		}

		[TestMethod()]
		public void PowerTest()
		{
			throw new NotImplementedException();
		}
	}
}