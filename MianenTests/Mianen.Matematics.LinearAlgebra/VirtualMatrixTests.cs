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
	public class VirtualMatrixTests
	{
		[TestMethod()]
		public void CreateTest()
		{
			Matrix<double> A = Matrix.Create(2, 2, new NDouble[] { 1d, 2d, 3d, 4d }, MatrixElementOrder.RowMajor);
			Matrix<double> B = Matrix.Create(2, 2, new NDouble[] { 1.2, 3.14159, -3d, -123456789123456789d }, MatrixElementOrder.RowMajor);

			VirtualSubMatrix<double> vA = new VirtualSubMatrix<double>(A, 0,0, 1, 1);
			VirtualSubMatrix<double> vB = new VirtualSubMatrix<double>(B, 0, 0, 1, 1);
			Matrix<double> vvB = vB;
			//vA.ToString();

			Console.WriteLine("A:");
			Console.WriteLine(A);
			Console.WriteLine(vA);
			Console.WriteLine("B:");
			Console.WriteLine(B);
			Console.WriteLine(vB);

			vvB.ToString();

			Console.WriteLine(vvB);
			var res = vA * vB;

			Console.WriteLine(res);
		}

		[TestMethod()]
		public void MultyplyTest01()
		{
			Matrix<double> a = MatrixTests.GetRandom(4,4);
			Matrix<double> b = MatrixTests.GetRandom(4,4);

			//Console.WriteLine(a);
			//Console.WriteLine(b);

			Matrix<double> res = a * b;
			Console.WriteLine(res);

			Matrix<double> tres = MatrixMT.MultyplyAB(a, b);
			Console.WriteLine(tres);

			Assert.IsTrue(res == tres);
		}

		[TestMethod()]
		public void MultyplyTest02()
		{
			Matrix<double> a = MatrixTests.GetRandom(500000, 10);
			Matrix<double> b = MatrixTests.GetRandom(10, 500000);

			//Console.WriteLine(a);
			//Console.WriteLine(b);
			DateTime begin = DateTime.Now;
			Matrix<double> res = b*a;
			TimeSpan oneT = DateTime.Now - begin;


			//Console.WriteLine(res);

			begin = DateTime.Now;
			Matrix<double> tres = MatrixMT.MultyplyAB(b,a);
			TimeSpan forT = DateTime.Now - begin;

			Console.WriteLine($"1: {oneT.TotalMilliseconds} VS 4: {forT.TotalMilliseconds}");

			//Console.WriteLine(tres);

			Assert.IsTrue(res == tres);
		}

		[TestMethod()]
		public void SumTest01()
		{
			Matrix<double> a = MatrixTests.GetRandom(4, 4);
			Matrix<double> b = MatrixTests.GetRandom(4, 4);

			//Console.WriteLine(a);
			//Console.WriteLine(b);

			Matrix<double> res = a + b;
			Console.WriteLine(res);

			Matrix<double> tres = MatrixMT.SumAB(a, b);
			Console.WriteLine(tres);

			Assert.IsTrue(res == tres);
		}

		[TestMethod()]
		public void SumTest02()
		{
			Matrix<double> a = MatrixTests.GetRandom(8, 4);
			Matrix<double> b = MatrixTests.GetRandom(8, 4);

			Console.WriteLine(a);
			Console.WriteLine(b);

			Matrix<double> res = a + b;
			Console.WriteLine(res);

			Matrix<double> tres = MatrixMT.SumAB(a, b);
			Console.WriteLine(tres);

			Assert.IsTrue(res == tres);
		}

		[TestMethod()]
		public void SumTest03()
		{
			Matrix<double> a = MatrixTests.GetRandom(500000, 3);
			Matrix<double> b = MatrixTests.GetRandom(500000, 3);

			//Console.WriteLine(a);
			//Console.WriteLine(b);
			DateTime begin = DateTime.Now;
			Matrix<double> res = a + b;
			TimeSpan oneT = DateTime.Now - begin;


			//Console.WriteLine(res);

			begin = DateTime.Now;
			Matrix<double> tres = MatrixMT.SumAB(a, b);
			TimeSpan forT = DateTime.Now - begin;

			Console.WriteLine($"1: {oneT.TotalMilliseconds} VS 4: {forT.TotalMilliseconds}");
			
			//Console.WriteLine(tres);

			Assert.IsTrue(res == tres);
		}

		[TestMethod()]
		public void SubTest01()
		{
			Matrix<double> a = MatrixTests.GetRandom(4, 4);
			Matrix<double> b = MatrixTests.GetRandom(4, 4);

			//Console.WriteLine(a);
			//Console.WriteLine(b);

			Matrix<double> res = a - b;
			Console.WriteLine(res);

			Matrix<double> tres = MatrixMT.SubtractAB(a, b);
			Console.WriteLine(tres);

			Assert.IsTrue(res == tres);
		}

		[TestMethod()]
		public void SubTest02()
		{
			Matrix<double> a = MatrixTests.GetRandom(8, 4);
			Matrix<double> b = MatrixTests.GetRandom(8, 4);

			Console.WriteLine(a);
			Console.WriteLine(b);

			Matrix<double> res = a - b;
			Console.WriteLine(res);

			Matrix<double> tres = MatrixMT.SubtractAB(a, b);
			Console.WriteLine(tres);

			Assert.IsTrue(res == tres);
		}

		[TestMethod()]
		public void GetCopyTest01()
		{
			Matrix<double> a = MatrixTests.GetRandom(153, 294);


			Matrix<double> res = Matrix.GetCopy(a);
			//Console.WriteLine(res);

			Matrix<double> tres = MatrixMT.GetCopy(a);
			//Console.WriteLine(tres);

			Assert.IsTrue(res == tres);
			Assert.IsTrue(res == a);
			Assert.IsTrue(tres == a);
		}

		[TestMethod()]
		public void GetCopyTest02()
		{
			Matrix<double> a = MatrixTests.GetRandom(153, 294);
			var N = new NDouble(150);
			a[1, 1] = N;

			Matrix<double> res = Matrix.GetCopy(a);
			//Console.WriteLine(res);

			Matrix<double> tres = MatrixMT.GetCopy(a);
			//Console.WriteLine(tres);
			N.Value = 170;
			//N = new NDouble(175);

			Assert.IsTrue(res == tres);
			Assert.IsTrue(res == a);
			Assert.IsTrue(tres == a);
			Assert.AreEqual(150, a[1,1].Value);
		}

		[TestMethod()]
		public void GetTranspose01()
		{
			Matrix<double> a = MatrixTests.GetRandom(150,150);

			//Console.WriteLine(a);
			DateTime begin = DateTime.Now;
			Matrix<double> res = Matrix.GetTranspose(a);
			TimeSpan oneT = DateTime.Now - begin;


			//Console.WriteLine(res);

			begin = DateTime.Now;
			Matrix<double> tres = MatrixMT.GetTranspose(a);
			TimeSpan forT = DateTime.Now - begin;

			Console.WriteLine($"1: {oneT.TotalMilliseconds} VS 4: {forT.TotalMilliseconds}");

			//Console.WriteLine(tres);

			Assert.IsTrue(res == tres);
		}

		[TestMethod()]
		public void ToString()
		{
			Matrix<double> var = MatrixTests.GetRandom(10, 10);
			Console.WriteLine(var);
		}

		[TestMethod()]
		public void GetTranspose02()
		{
			Matrix<double> a = MatrixTests.GetRandom(159, 159);

			//Console.WriteLine(a);
			DateTime begin = DateTime.Now;
			Matrix<double> res = Matrix.GetTranspose(a);
			TimeSpan oneT = DateTime.Now - begin;


			//Console.WriteLine(res);

			begin = DateTime.Now;
			Matrix<double> tres = MatrixMT.GetTranspose(a);
			TimeSpan forT = DateTime.Now - begin;

			Console.WriteLine($"1: {oneT.TotalMilliseconds} VS 4: {forT.TotalMilliseconds}");

			//Console.WriteLine(tres);

			Assert.IsTrue(res == tres);
		}




	}
}
