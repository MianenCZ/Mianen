using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mianen.Matematics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.LinearAlgebra.Tests
{
	[TestClass()]
	public class MatrixTests
	{
		[TestMethod()]
		public void MatrixTest()
		{
			Matrix<NumberDouble> a = new Matrix<NumberDouble>(2, 2);

			
			Console.WriteLine(a);

			//Matrix<byte> b = new Matrix<byte>(5, 45);
			//Matrix<string> s = new Matrix<string>(5, 45);

		}

		[TestMethod()]
		public void CreateTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void CreateTest1()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void CreateTest2()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetCopyTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetTransposeTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void SwapRowsTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void JoinHorizontalyTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetSubMatrixTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetREFTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetRREFTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetInvertTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void ToStringTest()
		{
			Assert.Fail();
		}
	}
}