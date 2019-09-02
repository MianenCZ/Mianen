using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Mianen.Matematics;
using Mianen.Matematics.Numerics;


[assembly: InternalsVisibleTo("MianenTests")]

namespace Mianen.Matematics.LinearAlgebra
{
	public static class Matrix
	{
		/// <summary>
		/// Constructs a new matrix of specified aray uning specified vector
		/// </summary>
		/// <param name="Source">Input vector</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>  
		/// <returns>New Matrix</returns>
		public static Matrix<T> Create<T>(Vector<T> Source)
		{
			Matrix<T> newMatrix = new Matrix<T>(Source.Dimension, 1);
			for (int i = 0; i < Source.Dimension; i++)
			{
				newMatrix[i, 0] = Source[i];
			}

			return newMatrix;
		}

		public static Matrix<T> Create<T>(ICollection<Vector<T>> Source, MatrixElementOrder ElementOrder)
		{
			if (object.Equals(Source, null))
				throw new ArgumentNullException();
			if (Source.Count == 0)
				throw new ArgumentException();

			int rCount = (ElementOrder == MatrixElementOrder.ColumnMajor) ? Source.First().Dimension : Source.Count;
			int cCount = (ElementOrder != MatrixElementOrder.ColumnMajor) ? Source.First().Dimension : Source.Count;

			Matrix<T> newMatrix = new Matrix<T>(rCount, cCount);

			int i = 0;
			foreach (var item in Source)
			{
				for (int q = 0; q < item.Dimension; q++)
				{
					if (ElementOrder == MatrixElementOrder.RowMajor)
						newMatrix[i, q] = item[q];
					else
						newMatrix[q, i] = item[q];
				}

				i++;
			}

			return newMatrix;

		}

		/// <summary>
		/// Constructs a new matrix of the specified dimensions and using the specified values array.
		/// </summary>
		/// <param name="RowCount">The number of rows.</param>
		/// <param name="ColumnCount">The number of columns.</param>
		/// <param name="Values">An array of values that contain the elements of the matrix in the order specified by elementOrder</param>
		/// <param name="ElementOrder">A MatrixElementOrder value that specifies the order in which the matrix components are stored in the storage array values.</param>
		/// <exception cref="ArgumentNullException">value is null</exception>  
		/// <exception cref="ArgumentOutOfRangeException">rowCount is less than one. OR columnCount is less than one.</exception>  
		/// <exception cref="ArgumentException">values does not presize fit into a Matrix.</exception> 
		/// <remarks>O(nm)</remarks>
		/// <returns>A Matrix</returns>
		public static Matrix<T> Create<T>(int RowCount, int ColumnCount, INumber<T>[] Values,
										  MatrixElementOrder ElementOrder)
		{
			if (Values == null)
				throw new NullReferenceException();
			if (RowCount < 1 || ColumnCount < 1)
				throw new ArgumentOutOfRangeException();
			if (RowCount * ColumnCount != Values.Length)
				throw new ArgumentException();

			Matrix<T> newMatrix = new Matrix<T>(RowCount, ColumnCount);


			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColumnCount; j++)
				{
					if (ElementOrder == MatrixElementOrder.RowMajor)
						newMatrix[i, j] = Values[i * ColumnCount + j];
					else if (ElementOrder == MatrixElementOrder.ColumnMajor)
						newMatrix[i, j] = Values[j * RowCount + i];
				}
			}

			return newMatrix;
		}

		/// <summary>
		/// Create non-refence copy of Matrix
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
			Matrix<T> newMatrix = new Matrix<T>(Source.RowCount, Source.ColumnCount);

			for (int i = 0; i < Source.RowCount; i++)
			{
				for (int j = 0; j < Source.ColumnCount; j++)
				{
					newMatrix[i, j] = Source[i, j].GetCopy();
				}
			}
			return newMatrix;
		}

		//TODO: Description
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="Source"></param>
		/// <returns></returns>
		public static INumber<T> GetDeterminant<T>(Matrix<T> Source)
		{
			if (object.Equals(Source, null))
				throw new ArgumentNullException();
			Matrix<T> newMatrix = Matrix.GetCopy(Source);
#if TRACE
			Console.WriteLine("Begin of GetDeterminant:");
			Console.WriteLine(newMatrix);
			Console.WriteLine("...");
#endif
			int One = 1;
			INumber<T> Zero = Source[0, 0].GetZero();


			int RankREF = 0;
			for (int j = 0; j < newMatrix.ColumnCount; j++)
			{
				//Find first nonzero value in j-column and swith that row on RankRef level (pivot level)
				for (int i = RankREF; i < newMatrix.RowCount; i++)
				{
					INumber<T> a = newMatrix[i, j];
					INumber<T> b = newMatrix[i, j].GetZero();
					if (a != b)
					{
#if TRACE
						Console.WriteLine("Pivot Found: {0}:{1} - {2}", i, j, newMatrix[i, j]);
#endif
						//We find nonzero value
						if (i != RankREF)
						{
							//RankREF row in zero in column
							//Swap two rows to get nonzero value to RankREF level
#if TRACE
							One *= -1;
							Console.WriteLine("Swaping");
#endif
							newMatrix = Matrix.SwapRows(newMatrix, i, RankREF);
#if TRACE
							Console.WriteLine(newMatrix);
#endif
						}
						//Now we have pivot in RankREF row in column

						//Subtraction from below to get pivot column
#if TRACE
						Console.WriteLine("Subtraction");
#endif
						for (int sub = RankREF + 1; sub < newMatrix.RowCount; sub++)
						{
							INumber<T> constant = newMatrix[sub, j].Divide(newMatrix[RankREF, j]);

							//* Change
							newMatrix[sub, j] = Zero;
							for (int q = j + 1; q < newMatrix.ColumnCount; q++)
							{
								INumber<T> e = newMatrix[sub, q];
								INumber<T> f = newMatrix[RankREF, q];
								newMatrix[sub, q] = e.Subtract(f.Multiply(constant));
							}
						}
#if TRACE
						Console.WriteLine("After Subtraction");
						Console.WriteLine(newMatrix);
#endif
						RankREF++;
						break;
					}
				}
			}

			//ToDo
			INumber<T> det = (One == -1) ? newMatrix[0, 0].Negative() : newMatrix[0, 0];
			for (int i = 0; i < newMatrix.ColumnCount; i++)
			{
				det = det.Multiply(newMatrix[i, i]);
			}

			return det;
		}

		/// <summary>
		/// Create transposition of Source Matrix
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

			Matrix<T> newMatrix = new Matrix<T>(Source.ColumnCount, Source.RowCount);

			for (int i = 0; i < Source.RowCount; i++)
			{
				for (int j = 0; j < Source.ColumnCount; j++)
				{
					newMatrix[j, i] = Source[i, j];
				}
			}

			return newMatrix;
		}

		/// <summary>
		/// Swap two rows in Source Matrix
		/// </summary>
		/// <typeparam name="T">Precision</typeparam>
		/// <param name="Source">Source Matrix</param>
		/// <param name="Row1">Index of row to swap</param>
		/// <param name="Row2">Index of row to swap</param>
		/// <param name="IndexFromZero">Flag for specifying row indexing</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <exception cref="ArgumentException">Row1 or Row2 is not inside a Matrix</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> SwapRows<T>(Matrix<T> Source, int Row1, int Row2, bool IndexFromZero = true)
		{
			int real1 = (IndexFromZero) ? Row1 : Row1 - 1;
			int real2 = (IndexFromZero) ? Row2 : Row2 - 1;

			if (object.Equals(Source, null))
				throw new ArgumentNullException();
			if (real1 > Source.RowCount || real2 > Source.RowCount)
				throw new IndexOutOfRangeException();
			if (real1 == real2)
				return Matrix.GetCopy(Source);

			Matrix<T> newMatrix = Matrix.GetCopy(Source);

			for (int i = 0; i < newMatrix.ColumnCount; i++)
			{
				INumber<T> tmp = newMatrix[real1, i];
				newMatrix[real1, i] = newMatrix[real2, i];
				newMatrix[real2, i] = tmp;
			}

			return newMatrix;
		}

		/// <summary>
		/// Concatenate two matrices horizontaly
		/// </summary>
		/// <typeparam name="T">Precision</typeparam>
		/// <param name="Left">Source Matrix on the left side</param>
		/// <param name="Right">Source Matrix on the right side</param>
		/// <exception cref="ArgumentNullException">Matrix Left or Right is null</exception>
		/// <exception cref="ArgumentException">Matrices RowCount is not same</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> JoinHorizontaly<T>(Matrix<T> Left, Matrix<T> Right)
		{
			if (object.Equals(Left, null) || object.Equals(Right, null))
				throw new ArgumentNullException();
			if (Left.RowCount != Right.RowCount)
				throw new ArgumentException();
			Matrix<T> newMatrix = new Matrix<T>(Left.RowCount, Left.ColumnCount + Right.ColumnCount);

			for (int i = 0; i < Left.RowCount; i++)
			{
				for (int j = 0; j < Left.ColumnCount; j++)
				{
					newMatrix[i, j] = Left[i, j];
				}

				for (int j = 0; j < Right.ColumnCount; j++)
				{
					newMatrix[i, j + Left.ColumnCount] = Right[i, j];
				}
			}

			return newMatrix;
		}

		/// <summary>
		/// Get part of matrix formed by taking a block of the entries from the Source Matrix
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
			if (object.Equals(Source, null))
				throw new ArgumentNullException();

			if (!IndexFromZero)
			{
				FirstColumn--;
				FirstRow--;
			}


			if (FirstColumn + ColumnCount > Source.ColumnCount || FirstRow + RowCount > Source.RowCount)
				throw new ArgumentException();


			Matrix<T> newMatrix = new Matrix<T>(RowCount, ColumnCount);

			for (int i = 0; i < newMatrix.RowCount; i++)
			{
				for (int j = 0; j < newMatrix.ColumnCount; j++)
				{
					newMatrix[i, j] = Source[i + FirstRow, j + FirstColumn];
				}
			}

			return newMatrix;
		}
		/// <summary>
		/// Get Row Echelon Form of a Matrix (REF)
		/// </summary>
		/// <typeparam name="T">Precision</typeparam>
		/// <param name="Source">Source Matrix</param>
		/// <param name="Zero">Identity element for addition</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> GetREF<T>(Matrix<T> Source)
		{
			if (object.Equals(Source, null))
				throw new ArgumentNullException();
			Matrix<T> newMatrix = Matrix.GetCopy(Source);
#if TRACE
			Console.WriteLine("Begin of REF:");
			Console.WriteLine(newMatrix);
			Console.WriteLine("...");
#endif

			INumber<T> Zero = Source[0, 0].GetZero();


			int RankREF = 0;
			for (int j = 0; j < newMatrix.ColumnCount; j++)
			{
				//Find first nonzero value in j-column and swith that row on RankRef level (pivot level)
				for (int i = RankREF; i < newMatrix.RowCount; i++)
				{
					if (newMatrix[i, j].IsNotEqual(newMatrix[0, 0].GetZero()))
					{
#if TRACE
						Console.WriteLine("Pivot Found: {0}:{1} - {2}", i, j, newMatrix[i, j]);
#endif
						//We find nonzero value
						if (i != RankREF)
						{
							//RankREF row in zero in column
							//Swap two rows to get nonzero value to RankREF level
#if TRACE
							Console.WriteLine("Swaping");
#endif
							newMatrix = Matrix.SwapRows(newMatrix, i, RankREF);
#if TRACE
							Console.WriteLine(newMatrix);
#endif
						}
						//Now we have pivot in RankREF row in column

						//Subtraction from below to get pivot column
#if TRACE
						Console.WriteLine("Subtraction");
#endif
						for (int sub = RankREF + 1; sub < newMatrix.RowCount; sub++)
						{
							INumber<T> constant = newMatrix[sub, j].Divide(newMatrix[RankREF, j]);

							//* Change
							newMatrix[sub, j] = Zero;
							for (int q = j + 1; q < newMatrix.ColumnCount; q++)
							{
								newMatrix[sub, q] =
									newMatrix[sub, q].Subtract(constant.Multiply(newMatrix[RankREF, q]));
							}
						}
#if TRACE
						Console.WriteLine("After Subtraction");
						Console.WriteLine(newMatrix);
#endif
						RankREF++;
						break;
					}
				}
			}


			return newMatrix;
		}

		/// <summary>
		/// Get Reduced Row Echelon Form of a Matrix (REF)
		/// </summary>
		/// <typeparam name="T">Precision</typeparam>
		/// <param name="Source">Source Matrix</param>
		/// <param name="Zero">Identity element for addition</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> GetRREF<T>(Matrix<T> Source)
		{
#if TRACE
			Console.WriteLine("Begin of RREF:");
#endif
			if (object.Equals(Source, null))
				throw new ArgumentNullException();

			INumber<T> Zero = Source[0, 0].GetZero();

			Matrix<T> newMatrix = Matrix.GetREF(Source);
#if TRACE
			Console.WriteLine(newMatrix);
#endif

			for (int i = newMatrix.RowCount - 1; i >= 0; i--)
			{
				for (int j = 0; j < newMatrix.ColumnCount; j++)
				{
					if (newMatrix[i, j].IsNotEqual(newMatrix[0, 0].GetZero()))
					{
#if TRACE
						Console.WriteLine("Pivo found {0}:{1} - {2}", i, j, newMatrix[i, j]);
						Console.WriteLine(newMatrix);
#endif
						//Pivot found
						INumber<T> pivot = newMatrix[i, j];
						for (int q = j; q < newMatrix.ColumnCount; q++)
						{
							newMatrix[i, q] = newMatrix[i, q].Divide(pivot);
						}
#if TRACE
						Console.WriteLine("Norm line");
						Console.WriteLine(newMatrix);
#endif
						//Pivot is 1 (multyplication inert item)
						//Subtract upper rows

						for (int Sub = i - 1; Sub >= 0; Sub--)
						{
							INumber<T> constant = newMatrix[Sub, j];
							if (constant.IsNotEqual(Zero))
							{
								//* Change
								newMatrix[Sub, j] = Zero;
								for (int q = j + 1; q < newMatrix.ColumnCount; q++)
								{
									newMatrix[Sub, q] = newMatrix[Sub, q].Subtract(newMatrix[i, q].Multiply(constant));
								}
							}
						}
#if TRACE
						Console.WriteLine("Sub upper");
						Console.WriteLine(newMatrix);
#endif
						break;



					}
				}
			}


			return newMatrix;
		}

		/// <summary>
		/// Get inverse of Source Matrix
		/// </summary>
		/// <typeparam name="T">Precision</typeparam>
		/// <param name="Source">Source MAtrix</param>
		/// <param name="Zero">Identity element for addition</param>
		/// <param name="One">Identity element for multiplication</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <exception cref="ArgumentException">Source is not regular</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> GetInvert<T>(Matrix<T> Source)
		{
			if (object.Equals(Source, null))
				throw new ArgumentNullException();
			if (Source.ColumnCount != Source.RowCount)
				throw new ArgumentException();

			INumber<T> Zero = Source[0, 0].GetZero();
			INumber<T> One = Source[0, 0].GetOne();
#if TRACE
			Console.WriteLine("Begin of Inverting");
#endif
			Matrix<T> In_k = new Matrix<T>(Source.RowCount, Source.ColumnCount);
			for (int i = 0; i < In_k.RowCount; i++)
			{
				for (int j = 0; j < In_k.ColumnCount; j++)
				{
					if (i == j)
						In_k[i, j] = One;
					else
						In_k[i, j] = Zero;
				}
			}
#if TRACE
			Console.WriteLine(In_k);
#endif
			Matrix<T> newMatrix = Matrix.JoinHorizontaly(Source, In_k);
#if TRACE
			Console.WriteLine(newMatrix);
#endif
			Matrix<T> red = Matrix.GetRREF(newMatrix);
#if TRACE
			Console.WriteLine(red);
#endif
			for (int i = 0; i < red.RowCount; i++)
			{
				for (int j = 0; j < red.RowCount; j++)
				{
					if (i == j)
					{
						if (red[i, j].IsNotEqual(One))
							throw new ArgumentException();
					}
					else
					{
						if (red[i, j].IsNotEqual(Zero))
							throw new ArgumentException();
					}
				}
			}

			return Matrix.GetSubMatrix(red, 0, Source.ColumnCount, Source.RowCount, Source.ColumnCount);
		}
		
		/// <summary>
		/// Multyply matrix using pattern A*(A^T)
		/// </summary>
		/// <param name="A">Martix to multyply</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <returns>New Matrix</returns>
		public static Matrix<T> MultyplyAAT<T>(Matrix<T> A)
		{
			if (object.Equals(A, null))
				throw new ArgumentNullException();
			
			Matrix<T> NewMatrix = new Matrix<T>(A.RowCount, A.RowCount);

			for (int i = 0; i < NewMatrix.RowCount; i++)
			{
				for (int j = 0; j < NewMatrix.ColumnCount; j++)
				{
					INumber<T> Sum = A[i, 0].Multiply(A[j, 0]);
					for (int q = 0; q < A.ColumnCount; q++)
					{
						INumber<T> tmp = A[i, q].Multiply(A[j, q]);
						Sum = Sum.Add(tmp);
					}
				}
			}

			return NewMatrix;
		}

		/// <summary>
		/// Multyply matrix using pattern (A^T)*A
		/// </summary>
		/// <param name="A">Martix to multyply</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <returns>New Matrix</returns>
		public static Matrix<T> MultyplyATA<T>(Matrix<T> A)
		{
			if (object.Equals(A, null))
				throw new ArgumentNullException();

			Matrix<T> NewMatrix = new Matrix<T>(A.ColumnCount, A.ColumnCount);
			for (int i = 0; i < NewMatrix.RowCount; i++)
			{
				for (int j = 0; j < NewMatrix.ColumnCount; j++)
				{
					INumber<T> Sum = A[0,i].Multiply(A[0,j]);
					for (int q = 0; q < A.ColumnCount; q++)
					{
						INumber<T> tmp = A[q,i].Multiply(A[q,j]);
						Sum = Sum.Add(tmp);
					}
				}
			}

			return NewMatrix;
		}

		internal static Matrix<T> InternalREF<T>(Matrix<T> Source, out INumber<T> DeterminantCoef, out int Rank)
		{
			int DetCoef = 1;
			Matrix<T> newMatrix = Matrix.GetCopy(Source);
			INumber<T> Zero = Source[0, 0].GetZero();


			int RankREF = 0;
			for (int j = 0; j < newMatrix.ColumnCount; j++)
			{
				//Find first nonzero value in j-column and swap that row on RankRef level (pivot level)
				for (int i = RankREF; i < newMatrix.RowCount; i++)
				{
					if (newMatrix[i, j].IsNotEqual(newMatrix[0, 0].GetZero()))
					{
						//We find nonzero value
						if (i != RankREF)
						{
							//RankREF row in zero in column
							//Swap two rows to get nonzero value to RankREF level
							DetCoef *= -1;
							newMatrix = Matrix.SwapRows(newMatrix, i, RankREF);
						}
						//Now we have pivot in RankREF row in column

						//Subtraction from below to get pivot column
						for (int sub = RankREF + 1; sub < newMatrix.RowCount; sub++)
						{
							INumber<T> constant = newMatrix[sub, j].Divide(newMatrix[RankREF, j]);

							//* Change
							newMatrix[sub, j] = Zero;
							for (int q = j + 1; q < newMatrix.ColumnCount; q++)
							{
								newMatrix[sub, q] =
									newMatrix[sub, q].Subtract(constant.Multiply(newMatrix[RankREF, q]));
							}
						}
						RankREF++;
						break;
					}
				}
			}

			Rank = RankREF;
			DeterminantCoef = (DetCoef == 1) ? Source[0, 0].GetOne() : Source[0, 0].GetOne().Negative();
			return newMatrix;
		}
			   
	}

	/// <summary>
	/// Represents the possible values for the order in which elements of a matrix are stored. 
	/// The matrix elements must be contiguous in the storage array in the direction specified by this value.
	/// </summary>
	public enum MatrixElementOrder
	{
		/// <summary>
		/// The elements are stored in row major order. Elements in the same row are stored in one contiguous block.
		/// </summary>
		RowMajor = 101,

		/// <summary>
		/// The elements are stored in column major order. Elements in the same column are stored in one contiguous block.
		/// </summary>
		ColumnMajor = 102,

	}
}
