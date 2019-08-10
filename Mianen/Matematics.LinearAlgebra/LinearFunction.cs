using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.LinearAlgebra
{
	public static class LinearMath<T>
	{
		public static Vector<T> LeastSquers(Matrix<T> A, Vector<T> y)
		{
#if TRACE
				Console.WriteLine("A");
				Console.WriteLine(A);
#endif
			Matrix<T> A_t = Matrix.GetTranspose(A);
#if TRACE
				Console.WriteLine("A^T");
				Console.WriteLine(A_t);
#endif
			Matrix<T> A_tA = A_t * A;
#if TRACE
				Console.WriteLine("A^T * A");
				Console.WriteLine(A_tA);
#endif
			Matrix<T> I__A_tA__ = Matrix.GetInvert(A_tA);
#if TRACE
				Console.WriteLine("I__A_tA__");
				Console.WriteLine(I__A_tA__);
#endif
			Vector<T> A_ty = A_t * y;
#if TRACE
				Console.WriteLine("A_ty");
				Console.WriteLine(A_ty);
#endif
			Vector<T> res = I__A_tA__ * A_ty;
#if TRACE
				Console.WriteLine("res:");
				Console.WriteLine(res);
#endif
			return res;
		}

		/*
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Input"></param>
		/// <exception cref="ArgumentNullException">Input vector array is null</exception>
		/// <exception cref="ArgumentException">Vector in input are not same dimension</exception>
		/// <returns></returns>
		public static Vector<T>[] GrammSchmitOrtogonalization(Vector<T>[] Input)
		{
			if(Input == null)
				throw new ArgumentNullException();
			if(Input.Length == 0)
				return new Vector<T>[0];
			try
			{
				for (int i = 0; i < Input[0].Dimension; i++)
				{
					Vector<T> y = 
				}
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
			{
				throw new NotAllowedOperationException($"Operation * is not define on {typeof(T)}", ex);
			}
		}
		*/

	}

}
