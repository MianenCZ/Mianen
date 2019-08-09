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
	public static class MatrixMT
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
			return null;
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
			return null;
		}

		/// <summary>
		/// Create transposition of Source Matrix.
		/// Using 4 Threads
		/// </summary>
		/// <typeparam name="T">Precision</typeparam>
		/// <param name="Source">Source Matrix</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New Matrix</returns>
		public static Matrix<T> SwapRows<T>(Matrix<T> Source, int Row1, int Row2, bool IndexFromZero = true)
		{
			return null;
		}

		/// <summary>
		/// Concatenate two matrices horizontaly.
		/// Using 4 Threads
		/// </summary>
		/// <typeparam name="T">Precision</typeparam>
		/// <param name="Left">Source Matrix on the left side</param>
		/// <param name="Rigth">Source Matrix on the right side</param>
		/// <exception cref="ArgumentNullException">Matrix Left or Right is null</exception>
		/// <exception cref="ArgumentException">Matrices RowCount is not same</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> JoinHorizontaly<T>(Matrix<T> Left, Matrix<T> Rigth)
		{
			return null;
		}

		/// <summary>
		/// Get part of matrix formed by taking a block of the entries from the Source Matrix.
		/// Using 4 threads
		/// </summary>
		/// <typeparam name="T">Precision</typeparam>
		/// <param name="Source">Source Matrix</param>
		/// <param name="FirstRow">Index of first row of submatrix in Source Matrix</param>
		/// <param name="FirstColumn">Index of first Column of submatrix in Source Matrix</param>
		/// <param name="RowCount">Submatrix rowCount</param>
		/// <param name="ColumnCount">Submatrix ColumnCount</param>
		/// <param name="IndexFromZero">Flag for specifying row indexing</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <exception cref="ArgumentException">define block of submatrix is not fit into a Source Matrix</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> GetSubMatrix<T>(Matrix<T> Source, int FirstRow, int FirstColumn, int RowCount,
											 int ColumnCount, bool IndexFromZero = true)
		{
			return null;
		}

		//TODO MultyplyAAT
		public static Matrix<T> MultyplyAAT<T>(Matrix<T> Source)
		{
			throw new NotImplementedException();
		}

		//TODO MultyplyATA
		public static Matrix<T> MultyplyATA<T>(Matrix<T> Source)
		{
			throw new NotImplementedException();
		}

		public static Matrix<T> MultyplyAB<T>(Matrix<T> A, Matrix<T> B)
		{
			throw new NotImplementedException();
		}

		public static Matrix<T> MultyplyAx<T>(Matrix<T> A, Vector<T> x)
		{
			throw new NotImplementedException();
		}
	}
}
