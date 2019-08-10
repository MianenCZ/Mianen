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
	public class Matrix<T>
	{
		/// <summary>
		/// Column count. Represents M in standart description
		/// </summary>
		public int RowCount { get; internal set; }

		/// <summary>
		/// Row count. Represents N in standart description
		/// </summary>
		public int ColumnCount { get; internal set; }

		internal INumber<T>[,] Data;
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i">represents row in Matrix default from 0</param>
		/// <param name="j">represents column in Matrix default form 0</param>
		/// <param name="IndexFromZero">define index base True - 0, False - 1</param>
		/// <exception cref="ArgumentOutOfRangeException">i or j is not inside Matrix</exception>
		/// <returns></returns>
		public INumber<T> this[int i, int j, bool IndexFromZero = true]
		{
			get => Data[(IndexFromZero) ? i : i - 1, (IndexFromZero) ? j : j - 1];
			set => Data[(IndexFromZero) ? i : i - 1, (IndexFromZero) ? j : j - 1] = value;
		}

		/// <summary>
		/// Constructs a new matrix of the specified dimensions.
		/// </summary>
		/// <param name="RowCount">The number of rows.</param>
		/// <param name="ColumnCount">The number of columns.</param>
		/// <exception cref="IndexOutOfRangeException">RowCount is less than one. OR ColumnCount is less than one.</exception>
		public Matrix(int RowCount, int ColumnCount)
		{
			if (RowCount < 1 || ColumnCount < 1)
				throw new IndexOutOfRangeException();

			this.RowCount = RowCount;
			this.ColumnCount = ColumnCount;
			this.Data = new INumber<T>[RowCount, ColumnCount];
		}

		#region Aritmetic operations

		/// <summary>
		/// Make addition of two Matrices
		/// </summary>
		/// <param name="A">Left Matrix</param>
		/// <param name="B">Right Matrix</param>
		/// <exception cref="ArgumentNullException">A or B is null</exception>
		/// <exception cref="ArgumentException">Size of Matrices is not same</exception>
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New Matrix</returns>
		public static Matrix<T> operator +(Matrix<T> A, Matrix<T> B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();
			if (A.RowCount != B.RowCount || A.ColumnCount != B.ColumnCount)
				throw new ArgumentException();

			Matrix<T> newMatrix = new Matrix<T>(A.RowCount, B.RowCount);
			for (int i = 0; i < newMatrix.RowCount; i++)
			{
				for (int j = 0; j < newMatrix.ColumnCount; j++)
				{
					newMatrix[i, j] = A[i, j].Add(B[i, j]);
				}
			}

			return newMatrix;
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
		public static Matrix<T> operator -(Matrix<T> A, Matrix<T> B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();
			if (A.RowCount != B.RowCount || A.ColumnCount != B.ColumnCount)
				throw new ArgumentException();

			Matrix<T> newMatrix = new Matrix<T>(A.RowCount, B.RowCount);
			for (int i = 0; i < newMatrix.RowCount; i++)
			{
				for (int j = 0; j < newMatrix.ColumnCount; j++)
				{
					newMatrix[i, j] = A[i, j].Subtract(B[i, j]);
				}
			}

			return newMatrix;
		}

		/// <summary>
		/// Make scalar multyplying of Matrix and constant
		/// </summary>
		/// <param name="A">Constant</param>
		/// <param name="B">Matrix</param>
		/// <exception cref="ArgumentNullException">A or B is null</exception>
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New Matrix</returns>
		public static Matrix<T> operator *(INumber<T> A, Matrix<T> B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();

			Matrix<T> newMatrix = Matrix.GetCopy(B);
			for (int i = 0; i < newMatrix.RowCount; i++)
			{
				for (int j = 0; j < newMatrix.ColumnCount; j++)
				{
					newMatrix[i, j] = A.Multiply(B[i, j]);
				}
			}

			return newMatrix;
		}

		/// <summary>
		/// Make scalar multyplying of Matrix and constant
		/// </summary>
		/// <param name="A">Matrix</param>
		/// <param name="B">Constant</param>
		/// <exception cref="ArgumentNullException">A or B is null</exception>
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New Matrix</returns>
		public static Matrix<T> operator *(Matrix<T> A, INumber<T> B)
		{
			return B * A;
		}

		/// <summary>
		/// Make matrix multyplying
		/// </summary>
		/// <param name="A">Left Martix</param>
		/// <param name="B">Right Matrix</param>
		/// <exception cref="ArgumentNullException">A or B is null</exception>
		/// <exception cref="ArgumentException">Column count of left matrix and row count of right has to be equal</exception>
		/// <returns>New Matrix</returns>
		public static Matrix<T> operator *(Matrix<T> A, Matrix<T> B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();
			if (A.ColumnCount != B.RowCount)
				throw new ArgumentException();

			Matrix<T> newMatrix = new Matrix<T>(A.RowCount, B.ColumnCount);

			for (int i = 0; i < newMatrix.RowCount; i++)
			{
				for (int j = 0; j < newMatrix.ColumnCount; j++)
				{
					INumber<T> Sum = A[i, 0].Multiply(B[0, j]);

					for (int k = 1; k < A.ColumnCount; k++)
					{
						Sum = Sum.Add(A[i, k].Multiply(B[k, j]));
					}

					newMatrix[i, j] = Sum;
				}
			}

			return newMatrix;
		}

		//TODO: check math
		/*
		public static Matrix<T> operator *(Matrix<T> A, Vector<T> x)
		{
			var x_ = Matrix.Create(x);
			return A * x_;
		}
		*/

		#endregion

		public override string ToString()
		{
			int[] MaxLenghts = new int[this.ColumnCount];

			for (int i = 0; i < this.RowCount; i++)
			{
				for (int j = 0; j < this.ColumnCount; j++)
				{
					if (this[i, j].ToString().Length > MaxLenghts[j])
						MaxLenghts[j] = this[i, j].ToString().Length;
				}
			}

			StringBuilder bld = new StringBuilder();

			for (int i = 0; i < this.RowCount; i++)
			{
				for (int j = 0; j < this.ColumnCount; j++)
				{
					bld.Append("   ");

					for (int q = 0; q < MaxLenghts[j] - this[i, j].ToString().Length; q++)
						bld.Append(" ");
					bld.Append(this[i, j]);
				}

				bld.AppendLine();
			}

			return bld.ToString();
		}

	}

}
