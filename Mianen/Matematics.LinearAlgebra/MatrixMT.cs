using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Mianen.Matematics;
using Mianen.Matematics.Numerics;
using System.Threading;

namespace Mianen.Matematics.LinearAlgebra
{
	public static partial class MatrixMT
	{
		/// <summary>
		/// Create non-refence copy of Matrix.
		/// Using 4 threads.
		/// </summary>
		/// <typeparam name="T">Precision</typeparam>
		/// <param name="Source">Source Matrix</param>
		/// <exception cref="ArgumentNullException">Source is null</exception> 
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New DoubleMatrix</returns>
		public static Matrix<T> GetCopy<T>(Matrix<T> Source)
		{
			if (object.Equals(Source, null))
				throw new ArgumentNullException();
			Matrix<T> Res = new Matrix<T>(Source.RowCount, Source.ColumnCount);


			VirtualSubMatrix<T> s_up_left = new VirtualSubMatrix<T>(Source, 0, 0, Source.RowCount / 2, Source.ColumnCount / 2);
			VirtualSubMatrix<T> s_up_right = new VirtualSubMatrix<T>(Source, 0, Source.ColumnCount / 2, Source.RowCount / 2, Source.ColumnCount - (Source.ColumnCount / 2));
			VirtualSubMatrix<T> s_down_left = new VirtualSubMatrix<T>(Source, Source.RowCount / 2, 0, Source.RowCount - (Source.RowCount / 2), Source.ColumnCount / 2);
			VirtualSubMatrix<T> s_down_right = new VirtualSubMatrix<T>(Source, Source.RowCount / 2, Source.ColumnCount / 2, Source.RowCount - (Source.RowCount / 2), Source.ColumnCount - (Source.ColumnCount / 2));

			VirtualSubMatrix<T> res_up_left = new VirtualSubMatrix<T>(Res, 0, 0, Res.RowCount / 2, Res.ColumnCount / 2);
			VirtualSubMatrix<T> res_up_right = new VirtualSubMatrix<T>(Res, 0, Res.ColumnCount / 2, Res.RowCount / 2, Res.ColumnCount - (Res.ColumnCount / 2));
			VirtualSubMatrix<T> res_down_left = new VirtualSubMatrix<T>(Res, Res.RowCount / 2, 0, Res.RowCount - (Res.RowCount / 2), Res.ColumnCount / 2);
			VirtualSubMatrix<T> res_down_right = new VirtualSubMatrix<T>(Res, Res.RowCount / 2, Res.ColumnCount / 2, Res.RowCount - (Res.RowCount / 2), Res.ColumnCount - (Res.ColumnCount / 2));

			Quarter<T> q1 = new Quarter<T>(s_up_left, null, res_up_left);
			Quarter<T> q2 = new Quarter<T>(s_up_right, null, res_up_right);
			Quarter<T> q3 = new Quarter<T>(s_down_left, null, res_down_left);
			Quarter<T> q4 = new Quarter<T>(s_down_right, null, res_down_right);

			Thread t1 = new Thread(q1.getCopy);
			Thread t2 = new Thread(q2.getCopy);
			Thread t3 = new Thread(q3.getCopy);
			Thread t4 = new Thread(q4.getCopy);

			t1.Start();
			t2.Start();
			t3.Start();
			t4.Start();

			t1.Join();
			t2.Join();
			t3.Join();
			t4.Join();

			return Res;

		}

		/// <summary>
		/// Create transposition of Source Matrix.
		/// Using 3 threads
		/// </summary>
		/// <typeparam name="T">Precision</typeparam>
		/// <param name="Source">Source Matrix</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New Matrix</returns>
		public static Matrix<T> GetTranspose<T>(Matrix<T> Source)
		{
			if (object.Equals(Source, null))
				throw new ArgumentNullException();
			Matrix<T> Res = new Matrix<T>(Source.RowCount, Source.ColumnCount);



			VirtualSubMatrix<T> s_up_left = new VirtualSubMatrix<T>
				(Source, 0, 0, (Source.RowCount / 2) + (Source.RowCount % 2), (Source.ColumnCount / 2)+(Source.ColumnCount % 2));
			VirtualSubMatrix<T> s_up_right = new VirtualSubMatrix<T>
				(Source, 0, (Source.ColumnCount / 2) + (Source.ColumnCount % 2), Source.RowCount / 2, Source.ColumnCount / 2);
			VirtualSubMatrix<T> s_down_left = new VirtualSubMatrix<T>
				(Source, (Source.RowCount / 2) + (Source.RowCount % 2), 0, Source.RowCount / 2, Source.ColumnCount / 2);
			VirtualSubMatrix<T> s_down_right = new VirtualSubMatrix<T>
				(Source, Source.RowCount / 2, Source.ColumnCount / 2, (Source.RowCount / 2) + (Source.RowCount % 2), (Source.ColumnCount / 2) + (Source.ColumnCount % 2));

			VirtualSubMatrix<T> res_up_left = new VirtualSubMatrix<T>
				(Res, 0, 0, (Res.RowCount / 2) + (Res.RowCount % 2), (Res.ColumnCount / 2) + (Res.ColumnCount % 2));
			VirtualSubMatrix<T> res_up_right = new VirtualSubMatrix<T>
				(Res, 0, (Res.ColumnCount / 2) + (Res.ColumnCount % 2), Res.RowCount / 2, Res.ColumnCount / 2);
			VirtualSubMatrix<T> res_down_left = new VirtualSubMatrix<T>
				(Res, (Res.RowCount / 2) + (Res.RowCount % 2), 0, Res.RowCount / 2, Res.ColumnCount / 2);
			VirtualSubMatrix<T> res_down_right = new VirtualSubMatrix<T>
				(Res, Res.RowCount / 2, Res.ColumnCount / 2, (Res.RowCount / 2) + (Res.RowCount % 2), (Res.ColumnCount / 2) + (Res.ColumnCount % 2));

			Quarter<T> q1 = new Quarter<T>(s_up_left, null, res_up_left);
			Quarter<T> q2 = new Quarter<T>(s_up_right, null, res_down_left);
			Quarter<T> q3 = new Quarter<T>(s_down_left, null, res_up_right);
			Quarter<T> q4 = new Quarter<T>(s_down_right, null, res_down_right);

			Thread t1 = new Thread(q1.transpose);
			Thread t2 = new Thread(q2.transpose);
			Thread t3 = new Thread(q3.transpose);
			Thread t4 = new Thread(q4.transpose);

			t1.Start();
			t2.Start();
			t3.Start();
			t4.Start();

			t1.Join();
			t2.Join();
			t3.Join();
			t4.Join();

			return Res;
		}

		/// <summary>
		/// Make matrix multyplying
		/// Using 4 threads
		/// </summary>
		/// <param name="A">Left Martix</param>
		/// <param name="B">Right Matrix</param>
		/// <exception cref="ArgumentNullException">A or B is null</exception>
		/// <exception cref="ArgumentException">Column count of left matrix and row count of right has to be equal</exception>
		/// <returns>New Matrix</returns>
		public static Matrix<T> MultyplyAB<T>(Matrix<T> A, Matrix<T> B)
		{
			Matrix<T> Res = new Matrix<T>(A.RowCount, B.ColumnCount);


			VirtualSubMatrix<T> a_up = new VirtualSubMatrix<T>(A, 0, 0, A.RowCount / 2, A.ColumnCount);
			VirtualSubMatrix<T> a_down = new VirtualSubMatrix<T>(A, A.RowCount / 2, 0, (A.RowCount - (A.RowCount / 2)), A.ColumnCount);
			VirtualSubMatrix<T> b_left = new VirtualSubMatrix<T>(B, 0,0,B.RowCount, B.ColumnCount/2);
			VirtualSubMatrix<T> b_right = new VirtualSubMatrix<T>(B, 0, B.ColumnCount / 2, B.RowCount, (B.ColumnCount - (B.ColumnCount / 2)));

			VirtualSubMatrix<T> res_1 = new VirtualSubMatrix<T>(Res, 0, 0, Res.RowCount / 2, Res.ColumnCount / 2);
			VirtualSubMatrix<T> res_2 = new VirtualSubMatrix<T>(Res, 0, Res.ColumnCount / 2, Res.RowCount / 2, Res.ColumnCount - (Res.ColumnCount / 2));
			VirtualSubMatrix<T> res_3 = new VirtualSubMatrix<T>(Res, Res.RowCount / 2, 0, Res.RowCount- (Res.RowCount / 2), Res.ColumnCount / 2);
			VirtualSubMatrix<T> res_4 = new VirtualSubMatrix<T>(Res, Res.RowCount / 2, Res.ColumnCount / 2, Res.RowCount - (Res.RowCount / 2), Res.ColumnCount - (Res.ColumnCount / 2));
					   			 
			Quarter<T> q1 = new Quarter<T>(a_up, b_left, res_1);
			Quarter<T> q2 = new Quarter<T>(a_up, b_right, res_2);
			Quarter<T> q3 = new Quarter<T>(a_down, b_left, res_3);
			Quarter<T> q4 = new Quarter<T>(a_down, b_right, res_4);

			Thread t1 = new Thread(q1.mult);
			Thread t2 = new Thread(q2.mult);
			Thread t3 = new Thread(q3.mult);
			Thread t4 = new Thread(q4.mult);

			t1.Start();
			t2.Start();
			t3.Start();
			t4.Start();

			t1.Join();
			t2.Join();
			t3.Join();
			t4.Join();

			return Res;
		}

		/// <summary>
		/// Make addition of two Matrices
		/// </summary>
		/// <param name="A">Left Matrix</param>
		/// <param name="B">Right Matrix</param>
		/// <exception cref="ArgumentNullException">A or B is null</exception>
		/// <exception cref="ArgumentException">Size of Matrices is not same</exception>
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New Matrix</returns>
		public static Matrix<T> SumAB<T>(Matrix<T> A, Matrix<T> B)
		{
			if (object.Equals(A, null) || object.Equals(B, null))
				throw new ArgumentNullException();
			if (A.RowCount != B.RowCount || A.ColumnCount != B.ColumnCount)
				throw new ArgumentException();

			Matrix<T> Res = new Matrix<T>(A.RowCount, B.ColumnCount);

			VirtualSubMatrix<T> a_up_left = new VirtualSubMatrix<T>(A, 0, 0, A.RowCount / 2, A.ColumnCount / 2);
			VirtualSubMatrix<T> a_up_right = new VirtualSubMatrix<T>(A, 0, A.ColumnCount / 2, A.RowCount / 2, A.ColumnCount - (A.ColumnCount / 2));
			VirtualSubMatrix<T> a_down_left = new VirtualSubMatrix<T>(A, A.RowCount / 2, 0, A.RowCount-(A.RowCount / 2), A.ColumnCount / 2);
			VirtualSubMatrix<T> a_down_right = new VirtualSubMatrix<T>(A, A.RowCount / 2, A.ColumnCount / 2, A.RowCount - (A.RowCount / 2), A.ColumnCount - (A.ColumnCount / 2));
			

			VirtualSubMatrix<T> b_up_left = new VirtualSubMatrix<T>(B, 0, 0, B.RowCount / 2, B.ColumnCount / 2);
			VirtualSubMatrix<T> b_up_right = new VirtualSubMatrix<T>(B, 0, B.ColumnCount / 2, B.RowCount / 2, B.ColumnCount - (B.ColumnCount / 2));
			VirtualSubMatrix<T> b_down_left = new VirtualSubMatrix<T>(B, B.RowCount / 2, 0, B.RowCount - (B.RowCount / 2), B.ColumnCount / 2);
			VirtualSubMatrix<T> b_down_right = new VirtualSubMatrix<T>(B, B.RowCount / 2, B.ColumnCount / 2, B.RowCount - (B.RowCount / 2), B.ColumnCount - (B.ColumnCount / 2));


			VirtualSubMatrix<T> res_up_left = new VirtualSubMatrix<T>(Res, 0, 0, Res.RowCount / 2, Res.ColumnCount / 2);
			VirtualSubMatrix<T> res_up_right = new VirtualSubMatrix<T>(Res, 0, Res.ColumnCount / 2, Res.RowCount / 2, Res.ColumnCount - (Res.ColumnCount / 2));
			VirtualSubMatrix<T> res_down_left = new VirtualSubMatrix<T>(Res, Res.RowCount / 2, 0, Res.RowCount - (Res.RowCount / 2), Res.ColumnCount / 2);
			VirtualSubMatrix<T> res_down_right = new VirtualSubMatrix<T>(Res, Res.RowCount / 2, Res.ColumnCount / 2, Res.RowCount - (Res.RowCount / 2), Res.ColumnCount - (Res.ColumnCount / 2));

			Quarter<T> q1 = new Quarter<T>(a_up_left, b_up_left, res_up_left);
			Quarter<T> q2 = new Quarter<T>(a_up_right, b_up_right, res_up_right);
			Quarter<T> q3 = new Quarter<T>(a_down_left, b_down_left, res_down_left);
			Quarter<T> q4 = new Quarter<T>(a_down_right, b_down_right, res_down_right);
			
			Thread t1 = new Thread(q1.sum);
			Thread t2 = new Thread(q2.sum);
			Thread t3 = new Thread(q3.sum);
			Thread t4 = new Thread(q4.sum);

			t1.Start();
			t2.Start();
			t3.Start();
			t4.Start();

			t1.Join();
			t2.Join();
			t3.Join();
			t4.Join();

			return Res;
		}

		/// <summary>
		/// Make subtraction of two Matrices
		/// </summary>
		/// <param name="A">Left Matrix</param>
		/// <param name="B">Right Matrix</param>
		/// <exception cref="ArgumentNullException">A or B is null</exception>
		/// <exception cref="ArgumentException">Size of Matrices is not same</exception>
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New Matrix</returns>
		public static Matrix<T> SubtractAB<T>(Matrix<T> A, Matrix<T> B)
		{
			if (object.Equals(A, null) || object.Equals(B, null))
				throw new ArgumentNullException();
			if (A.RowCount != B.RowCount || A.ColumnCount != B.ColumnCount)
				throw new ArgumentException();

			Matrix<T> Res = new Matrix<T>(A.RowCount, B.ColumnCount);

			VirtualSubMatrix<T> a_up_left = new VirtualSubMatrix<T>(A, 0, 0, A.RowCount / 2, A.ColumnCount / 2);
			VirtualSubMatrix<T> a_up_right = new VirtualSubMatrix<T>(A, 0, A.ColumnCount / 2, A.RowCount / 2, A.ColumnCount - (A.ColumnCount / 2));
			VirtualSubMatrix<T> a_down_left = new VirtualSubMatrix<T>(A, A.RowCount / 2, 0, A.RowCount - (A.RowCount / 2), A.ColumnCount / 2);
			VirtualSubMatrix<T> a_down_right = new VirtualSubMatrix<T>(A, A.RowCount / 2, A.ColumnCount / 2, A.RowCount - (A.RowCount / 2), A.ColumnCount - (A.ColumnCount / 2));


			VirtualSubMatrix<T> b_up_left = new VirtualSubMatrix<T>(B, 0, 0, B.RowCount / 2, B.ColumnCount / 2);
			VirtualSubMatrix<T> b_up_right = new VirtualSubMatrix<T>(B, 0, B.ColumnCount / 2, B.RowCount / 2, B.ColumnCount - (B.ColumnCount / 2));
			VirtualSubMatrix<T> b_down_left = new VirtualSubMatrix<T>(B, B.RowCount / 2, 0, B.RowCount - (B.RowCount / 2), B.ColumnCount / 2);
			VirtualSubMatrix<T> b_down_right = new VirtualSubMatrix<T>(B, B.RowCount / 2, B.ColumnCount / 2, B.RowCount - (B.RowCount / 2), B.ColumnCount - (B.ColumnCount / 2));


			VirtualSubMatrix<T> res_up_left = new VirtualSubMatrix<T>(Res, 0, 0, Res.RowCount / 2, Res.ColumnCount / 2);
			VirtualSubMatrix<T> res_up_right = new VirtualSubMatrix<T>(Res, 0, Res.ColumnCount / 2, Res.RowCount / 2, Res.ColumnCount - (Res.ColumnCount / 2));
			VirtualSubMatrix<T> res_down_left = new VirtualSubMatrix<T>(Res, Res.RowCount / 2, 0, Res.RowCount - (Res.RowCount / 2), Res.ColumnCount / 2);
			VirtualSubMatrix<T> res_down_right = new VirtualSubMatrix<T>(Res, Res.RowCount / 2, Res.ColumnCount / 2, Res.RowCount - (Res.RowCount / 2), Res.ColumnCount - (Res.ColumnCount / 2));

			Quarter<T> q1 = new Quarter<T>(a_up_left, b_up_left, res_up_left);
			Quarter<T> q2 = new Quarter<T>(a_up_right, b_up_right, res_up_right);
			Quarter<T> q3 = new Quarter<T>(a_down_left, b_down_left, res_down_left);
			Quarter<T> q4 = new Quarter<T>(a_down_right, b_down_right, res_down_right);

			Thread t1 = new Thread(q1.sub);
			Thread t2 = new Thread(q2.sub);
			Thread t3 = new Thread(q3.sub);
			Thread t4 = new Thread(q4.sub);

			t1.Start();
			t2.Start();
			t3.Start();
			t4.Start();

			t1.Join();
			t2.Join();
			t3.Join();
			t4.Join();

			return Res;
		}
		

			   
		public static Matrix<T> MultyplyAx<T>(Matrix<T> A, Vector<T> x)
		{
			throw new NotImplementedException();
		}

	}
}
