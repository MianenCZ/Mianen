using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mianen.Matematics.LinearAlgebra;
using Mianen.Matematics.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.LinearAlgebra.Tests
{
	[TestClass()]
	public class MatrixTests
	{
		[TestMethod()]
		public void MatrixTest()
		{
			Matrix<double> A = Matrix.Create(2, 2, new NDouble[] { 1d, 2d, 3d, 4d }, MatrixElementOrder.RowMajor);
			Matrix<double> B = Matrix.Create(2, 2, new NDouble[] { 1.2, 3.14159, -3d, -123456789123456789d }, MatrixElementOrder.RowMajor);
			
			Console.WriteLine(A);
			Console.WriteLine(B);
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

		[TestMethod()]
		public void CreateTest3()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void CreateTest4()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void CreateTest5()
		{
			Assert.Fail();
		}
	}
}