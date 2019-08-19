using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Mianen.Matematics;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.LinearAlgebra
{
	static partial class MatrixMT
	{
		private class Quarter<T>
		{
			public Matrix<T> A, B, Res;
			public INumber<T> Const;

			public void mult()
			{
				var r = A * B;
				CopyToRes(r);
			}

			public void sum()
			{
				var r = A + B;
				CopyToRes(r);
			}

			public void sub()
			{
				var r = A - B;
				CopyToRes(r);
			}

			public void constMult()
			{
				var r = A * Const;
				CopyToRes(r);
			}

			public void getCopy()
			{
				var r = Matrix.GetCopy(A);
				CopyToRes(r);
			}

			public void transpose()
			{
				var r = Matrix.GetTranspose(A);
				CopyToRes(r);
			}

			public Quarter(Matrix<T> A, Matrix<T> B, Matrix<T> Res)
			{
				this.A = A;
				this.B = B;
				this.Res = Res;
			}

			private void CopyToRes(Matrix<T> r)
			{
				for (int i = 0; i < r.RowCount; i++)
				{
					for (int j = 0; j < r.ColumnCount; j++)
					{
						Res[i, j] = r[i, j];
					}
				}
			}



		}
	}
}
