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

		[TestMethod()]
		public void CreateDualImpTest()
		{
			Matrix<double> t = new Matrix<double>(2,2);
			NumDouble1 a = new NumDouble1(1);
			NumDouble2 b = new NumDouble2(1);
			t[1, 1] = a;
			t[2, 2] = b;


			var res = a.Add(b);
		}




		public static Matrix<double> GetRandom(int RowCount, int ColumnCount)
		{
			Random rnd = new Random();

			Matrix<double> res = new Matrix<double>(RowCount, ColumnCount);

			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColumnCount; j++)
				{
					res[i, j] = new NDouble(rnd.Next(-1000,1000));
				}
			}
			return res;
		}


		private class NumDouble1 : INumber<double>
		{

			public INumber<double> GetCopy() => new NumDouble1(this.Value);

			public NumDouble1(double v) { this.Value = v; }
			public double Value { get; set; }

			public INumber<double> Add(INumber<double> Number)
			{
				return new NumDouble1(Number.Value + this.Value);
			}

			public int CompareTo(INumber<double> other)
			{
				throw new NotImplementedException();
			}

			public INumber<double> Divide(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool Equals(INumber<double> other)
			{
				throw new NotImplementedException();
			}

			public INumber<double> GetOne()
			{
				throw new NotImplementedException();
			}

			public INumber<double> GetZero()
			{
				throw new NotImplementedException();
			}

			public bool IsEqual(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsGreaterOrEqualThan(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsGreaterThan(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsLowerOrEqualThan(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsLowerThan(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsNotEqual(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public INumber<double> Multiply(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public INumber<double> Negative()
			{
				throw new NotImplementedException();
			}

			public INumber<double> Power(INumber<double> Exponent)
			{
				throw new NotImplementedException();
			}

			public INumber<double> Subtract(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public string ToString(IFormatProvider IformatProvider)
			{
				throw new NotImplementedException();
			}

			public string ToString(string Format)
			{
				throw new NotImplementedException();
			}

			public string ToString(string Format, IFormatProvider IformatProvider)
			{
				throw new NotImplementedException();
			}
		}

		private class NumDouble2 : INumber<double>
		{
			public NumDouble2(double v) { this.Value = v; }
			public double Value { get; set; }

			public INumber<double> Add(INumber<double> Number)
			{
				throw new NotImplementedException();
			}
			public INumber<double> GetCopy() => new NumDouble2(this.Value);

			public int CompareTo(INumber<double> other)
			{
				throw new NotImplementedException();
			}

			public INumber<double> Divide(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool Equals(INumber<double> other)
			{
				throw new NotImplementedException();
			}

			public INumber<double> GetOne()
			{
				throw new NotImplementedException();
			}

			public INumber<double> GetZero()
			{
				throw new NotImplementedException();
			}

			public bool IsEqual(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsGreaterOrEqualThan(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsGreaterThan(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsLowerOrEqualThan(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsLowerThan(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsNotEqual(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public INumber<double> Multiply(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public INumber<double> Negative()
			{
				throw new NotImplementedException();
			}

			public INumber<double> Power(INumber<double> Exponent)
			{
				throw new NotImplementedException();
			}

			public INumber<double> Subtract(INumber<double> Number)
			{
				throw new NotImplementedException();
			}

			public string ToString(IFormatProvider IformatProvider)
			{
				throw new NotImplementedException();
			}

			public string ToString(string Format)
			{
				throw new NotImplementedException();
			}

			public string ToString(string Format, IFormatProvider IformatProvider)
			{
				throw new NotImplementedException();
			}
		}



	}
}