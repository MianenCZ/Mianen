using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mianen.Matematics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.LinearAlgebra.Tests
{
	[TestClass()]
	public class FunctionLibTests
	{
		[TestMethod()]
		public void LeastSquersTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetDeterminantTest01()
		{
			Matrix<double> mat = Matrix<double>.Create(4, 4, new double[] {1,1,1,0,1,1,0,1,1,0,1,1,0,1,1,1 }, MatrixElementOrder.RowMajor);
			double det = Matrix<double>.GetDeterminant(mat);
			Assert.AreEqual(-3, det);
		}

		[TestMethod()]
		public void GetDeterminantTest02()
		{
			long DetCount = 0;
			long ValCount = 0;
			for (int a = 0; a < 5; a++)
			{
				for (int b = 0; b < 5; b++)
				{
					for (int c = 0; c < 5; c++)
					{
						for (int d = 0; d < 5; d++)
						{
							for (int e = 0; e < 5; e++)
							{
								for (int f = 0; f < 5; f++)
								{
									for (int g = 0; g < 5; g++)
									{
										for (int h = 0; h < 5; h++)
										{
											for (int i = 0; i < 5; i++)
											{
												double det = a * e * i + d * h * c + b * f * g - c * e * g - d * b * i -
													  f * h * a;
												det += 1953125;
												if (det % 5 == 0)
													DetCount++;
												ValCount++;
											}
										}
									}
								}
							}
						}
					}
				}
			}

			Console.WriteLine((double)DetCount / (double)ValCount);

		}
	}
}