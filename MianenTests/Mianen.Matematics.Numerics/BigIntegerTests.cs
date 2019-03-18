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
	public class BigIntegerTests
	{
		[TestMethod()]
		public void BigIntegerTest()
		{
			BigInteger b = new BigInteger();
			b = (BigInteger)10;
		}

		[TestMethod()]
		public void BigIntegerSignumTest()
		{
			BigInteger b = new BigInteger(-5);
		}

		[TestMethod()]
		public void BigIntegerTest1()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void ToStringTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetDefaultTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void TryParseTest()
		{
			Assert.Fail();
		}

		public bool IListContend<T>(IList<T> a, IList<T> b)
		{
			if (a.Count != b.Count)
				return false;
			for (int i = 0; i < a.Count; i++)
			{
				if ((a[i] == null && b[i] != null) || a[i] != null && b[i] == null)
					return false;

				if (!a[i].Equals(b[i]))
					return false;
			}
			return true;
		}
	}
}