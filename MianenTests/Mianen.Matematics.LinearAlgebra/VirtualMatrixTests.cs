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

			VirtualSubMatrix<double> vA = new VirtualSubMatrix<double>(A, 1, 1, 1, 1);
			VirtualSubMatrix<double> vB = new VirtualSubMatrix<double>(B, 1, 1, 1, 1);

			vA.ToString();

			Console.WriteLine(A);
			Console.WriteLine(vA);
			Console.WriteLine(B);
			Console.WriteLine(vB);

			var res = vA * vB;

			Console.WriteLine(res);


		}


	}
}
