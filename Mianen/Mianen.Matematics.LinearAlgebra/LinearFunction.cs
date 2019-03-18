using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.LinearAlgebra
{
	public static class FunctionLib<T>
	{
		public static Vector<T> LeastSquers(Matrix<T> A, Vector<T> y, T Zero, T One)
		{
#if DEBUG
				Console.WriteLine("A");
				Console.WriteLine(A);
#endif
			Matrix<T> A_t = Matrix<T>.GetTranspose(A);
#if DEBUG
				Console.WriteLine("A^T");
				Console.WriteLine(A_t);
#endif
			Matrix<T> A_tA = A_t * A;
#if DEBUG
				Console.WriteLine("A^T * A");
				Console.WriteLine(A_tA);
#endif
			Matrix<T> I__A_tA__ = Matrix<T>.GetInvert(A_tA);
#if DEBUG
				Console.WriteLine("I__A_tA__");
				Console.WriteLine(I__A_tA__);
#endif
			Vector<T> A_ty = A_t * y;
#if DEBUG
				Console.WriteLine("A_ty");
				Console.WriteLine(A_ty);
#endif
			Vector<T> res = I__A_tA__ * A_ty;
#if DEBUG
				Console.WriteLine("res:");
				Console.WriteLine(res);
#endif
			return res;
		}

		public static T GetDeterminant(Matrix<T> Sourse, T Zero)
		{
			if (Sourse == null)
				throw new ArgumentNullException();
			if (Sourse.ColumnCount != Sourse.RowCount)
				throw new ArgumentException();

			Matrix<T> newMatrix = Matrix<T>.GetREF(Sourse);

			dynamic a = newMatrix[0, 0];
			for (int i = 1; i < Sourse.ColumnCount; i++)
			{
				dynamic b = newMatrix[i, i];
				a *= b;
			}

			return a;
		}

	}
}
