using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Mianen.Matematics;


[assembly: InternalsVisibleTo("MianenTests")]
namespace Mianen.Matematics.LinearAlgebra
{
	public class Matrix<T>
	{
		/// <summary>
		/// Column count. Represents M in standart description
		/// </summary>
		public int RowCount { get; private set; }
		/// <summary>
		/// Row count. Represents N in standart description
		/// </summary>
		public int ColumnCount { get; private set; }
		private T[,] Data;
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i">represents row in Matrix default from 0</param>
		/// <param name="j">represents column in Matrix default form 0</param>
		/// <param name="IndexFromZero">define index base True - 0, False - 1</param>
		/// <exception cref="ArgumentOutOfRangeException">i or j is not inside Matrix</exception>
		/// <returns></returns>
		public T this[int i, int j, bool IndexFromZero = true]
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
			this.Data = new T[RowCount,ColumnCount];
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
		public static Matrix<T> Create(int RowCount, int ColumnCount, T[] Values, MatrixElementOrder ElementOrder)
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
						newMatrix[i, j] = Values[i *ColumnCount + j];
					else if (ElementOrder == MatrixElementOrder.ColumnMajor)
						newMatrix[i, j] = Values[j * RowCount + i];
				}
			}

			return newMatrix;
		}

		/// <summary>
		/// Constructs a new matrix of specified aray uning specified vector
		/// </summary>
		/// <param name="Source">Input vector</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>  
		/// <returns>New Matrix</returns>
		public static Matrix<T> Create(Vector<T> Source)
		{
			Matrix<T> newMatrix = new Matrix<T>(Source.Dimension, 1);
			for (int i = 0; i < Source.Dimension; i++)
			{
				newMatrix[i, 0] = Source[i];
			}
			return newMatrix;
		}

		public static Matrix<T> Create(ICollection<Vector<T>> Source, MatrixElementOrder ElementOrder)
		{
			if (Source == null)
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
		/// Create non-refence copy of Matrix
		/// </summary>
		/// <param name="Source">Source Matrix</param>
		/// <exception cref="ArgumentNullException">Source is null</exception> 
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New DoubleMatrix</returns>
		public static Matrix<T> GetCopy(Matrix<T> Source)
		{
			if (Source == null)
				throw new ArgumentNullException();
			Matrix<T> newMatrix = new Matrix<T>(Source.RowCount, Source.ColumnCount);
			Array.Copy(Source.Data, newMatrix.Data, Source.ColumnCount * Source.RowCount);
			return newMatrix;
		}

		/// <summary>
		/// Create transposition of Source Matrix
		/// </summary>
		/// <param name="Source">Source Matrix</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New Matrix</returns>
		public static Matrix<T> GetTranspose(Matrix<T> Source)
		{
			if (Source == null)
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
		/// <param name="Source">Source Matrix</param>
		/// <param name="Row1">Index of row to swap</param>
		/// <param name="Row2">Index of row to swap</param>
		/// <param name="IndexFromZero">Flag for specifying row indexing</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <exception cref="ArgumentException">Row1 or Row2 is not inside a Matrix</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> SwapRows(Matrix<T> Source, int Row1, int Row2, bool IndexFromZero = true)
		{
			int real1 = (IndexFromZero) ? Row1 : Row1 - 1;
			int real2 = (IndexFromZero) ? Row2 : Row2 - 1;

			if (Source == null)
				throw new ArgumentNullException();
			if (real1 > Source.RowCount || real2 > Source.RowCount)
				throw new IndexOutOfRangeException();
			if (real1 == real2)
				return Matrix<T>.GetCopy(Source);

			Matrix<T> newMatrix = Matrix<T>.GetCopy(Source);

			for (int i = 0; i < newMatrix.ColumnCount; i++)
			{
				T tmp = newMatrix[real1, i];
				newMatrix[real1, i] = newMatrix[real2, i];
				newMatrix[real2, i] = tmp;
			}

			return newMatrix;
		}

		/// <summary>
		/// Concatenate two matrices horizontaly
		/// </summary>
		/// <param name="Left">Source Matrix on the left side</param>
		/// <param name="Rigth">Source Matrix on the right side</param>
		/// <exception cref="ArgumentNullException">Matrix Left or Right is null</exception>
		/// <exception cref="ArgumentException">Matrices RowCount is not same</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> JoinHorizontaly(Matrix<T> Left, Matrix<T> Rigth)
		{
			if (Left == null || Rigth == null)
				throw new ArgumentNullException();
			if (Left.RowCount != Rigth.RowCount)
				throw new ArgumentException();
			Matrix<T> newMatrix = new Matrix<T>(Left.RowCount, Left.ColumnCount + Rigth.ColumnCount);

			for (int i = 0; i < Left.RowCount; i++)
			{
				for (int j = 0; j < Left.ColumnCount; j++)
				{
					newMatrix[i,j] = Left[i, j];
				}
				for (int j = 0; j < Rigth.ColumnCount; j++)
				{
					newMatrix[i, j + Left.ColumnCount] = Rigth[i, j];
				}
			}
			return newMatrix;
		}

		/// <summary>
		/// Get part of matrix formed by taking a block of the entries from the Source Matrix
		/// </summary>
		/// <param name="Source">Source Matrix</param>
		/// <param name="FirstRow">Index of first row of submatrix in Source Matrix</param>
		/// <param name="FirstColumn">Index of first Column of submatrix in Source Matrix</param>
		/// <param name="RowCount">Submatrix rowCount</param>
		/// <param name="ColumnCount">Submatrix ColumnCount</param>
		/// <param name="IndexFromZero">Flag for specifying row indexing</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <exception cref="ArgumentException">define block of submatrix is not fit into a Source Matrix</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> GetSubMatrix(Matrix<T> Source, int FirstRow, int FirstColumn, int RowCount, int ColumnCount, bool IndexFromZero = true)
		{
			if (Source == null)
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
		/// <param name="Source">Source Matrix</param>
		/// <param name="Zero">Identity element for addition</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <exception cref="NotAllowedOperationException">Operation +, -, * or /  is not defined for this object</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> GetREF(Matrix<T> Source)
		{
			if (Source == null)
				throw new ArgumentNullException();
			Matrix<T> newMatrix = Matrix<T>.GetCopy(Source);
#if DEBUG
			Console.WriteLine("Begin of REF:");
			Console.WriteLine(newMatrix);
			Console.WriteLine("...");
#endif

			dynamic z = Source[0, 0];
			T Zero = z - z;
			try
			{

				int RankREF = 0;
				for (int j = 0; j < newMatrix.ColumnCount; j++)
				{
					//Find first nonzero value in j-column and swith that row on RankRef level (pivot level)
					for (int i = RankREF; i < newMatrix.RowCount; i++)
					{
						dynamic a = newMatrix[i, j];
						dynamic b = Zero;
						if (a != b)
						{
#if DEBUG
							Console.WriteLine("Pivot Found: {0}:{1} - {2}", i, j, newMatrix[i, j]);
#endif
							//We find nonzero value
							if (i != RankREF)
							{
								//RankREF row in zero in column
								//Swap two rows to get nonzero value to RankREF level
#if DEBUG
								Console.WriteLine("Swaping");
#endif
								newMatrix = Matrix<T>.SwapRows(newMatrix, i, RankREF);
#if DEBUG
								Console.WriteLine(newMatrix);
#endif
							}
							//Now we have pivot in RankREF row in column

							//Subtraction from below to get pivot column
#if DEBUG
							Console.WriteLine("Subtraction");
#endif
							for (int sub = RankREF + 1; sub < newMatrix.RowCount; sub++)
							{
								dynamic c = newMatrix[sub, j];
								dynamic d = newMatrix[RankREF, j];
								dynamic constant = c / d;

								//* Change
								newMatrix[sub, j] = Zero;
								for (int q = j + 1; q < newMatrix.ColumnCount; q++)
								{
									dynamic e = newMatrix[sub, q];
									dynamic f = newMatrix[RankREF, q];
									newMatrix[sub, q] = e - constant * f;
								}
							}
#if DEBUG
							Console.WriteLine("After Subtraction");
							Console.WriteLine(newMatrix);
#endif
							RankREF++;
							break;
						}
					}
				}
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
			{
				throw new NotAllowedOperationException($"Operation != is not define for {Zero.GetType()} class", ex);
			}


			return newMatrix;
		}

		/// <summary>
		/// Get Reduced Row Echelon Form of a Matrix (REF)
		/// </summary>
		/// <param name="Source">Source Matrix</param>
		/// <param name="Zero">Identity element for addition</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <exception cref="NotAllowedOperationException">Operation +, -, * or /  is not defined for this object</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> GetRREF(Matrix<T> Source)
		{
#if DEBUG
			Console.WriteLine("Begin of RREF:");
#endif
			if (Source == null)
				throw new ArgumentNullException();

			dynamic z = Source[0, 0];
			T Zero = z - z;

			Matrix<T> newMatrix = Matrix<T>.GetREF(Source);
#if DEBUG
			Console.WriteLine(newMatrix);
#endif

			for (int i = newMatrix.RowCount - 1; i >= 0; i--)
			{
				for (int j = 0; j < newMatrix.ColumnCount; j++)
				{
					dynamic a = newMatrix[i, j];
					dynamic b = Zero;
					if (a != b)
					{
#if DEBUG
						Console.WriteLine("Pivo found {0}:{1} - {2}", i, j, newMatrix[i,j]);
						Console.WriteLine(newMatrix);
#endif
						//Pivot found
						dynamic pivot = newMatrix[i, j];
						for (int q = j; q < newMatrix.ColumnCount; q++)
						{
							dynamic c = newMatrix[i, q];
							newMatrix[i, q] = c / pivot;
						}
#if DEBUG
						Console.WriteLine("Norm line");
						Console.WriteLine(newMatrix);
#endif
						//Pivot is 1 (multyplication inert item)
						//Subtract upper rows

						for (int Sub = i - 1; Sub >= 0; Sub--)
						{
							dynamic con = newMatrix[Sub, j];
							if(con != Zero)
							{
								//* Change
								newMatrix[Sub, j] = Zero;
								for (int q = j + 1; q < newMatrix.ColumnCount; q++)
								{
									dynamic d = newMatrix[i, q];
									dynamic e = newMatrix[Sub, q];
									newMatrix[Sub, q] = e - d * con;
								}

								//*/


								/* Mathematicaly correct
								for (int q = j; q < newMatrix.ColumnCount; q++)
								{
									dynamic d = newMatrix[i, q];
									dynamic e = newMatrix[Sub, q];
									newMatrix[Sub, q] = e - d * con;
								}
								//*/
							}
						}
#if DEBUG
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
		/// <param name="Source">Source MAtrix</param>
		/// <param name="Zero">Identity element for addition</param>
		/// <param name="One">Identity element for multyplication</param>
		/// <exception cref="ArgumentNullException">Source is null</exception>
		/// <exception cref="ArgumentException">Source is not regular</exception>
		/// <exception cref="NotAllowedOperationException">Operation +, -, * or /  is not defined for this object</exception>
		/// <returns>new Matrix</returns>
		public static Matrix<T> GetInvert(Matrix<T> Source)
		{
			if (Source == null)
				throw new ArgumentNullException();
			if (Source.ColumnCount != Source.RowCount)
				throw new ArgumentException();

			dynamic z = Source[0, 0];
			T Zero = z - z;
			T One = z / z;
#if DEBUG
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
#if DEBUG
			Console.WriteLine(In_k);
#endif
			Matrix<T> newMatrix = Matrix<T>.JoinHorizontaly(Source, In_k);
#if DEBUG
			Console.WriteLine(newMatrix);
#endif
			Matrix<T> red = Matrix<T>.GetRREF(newMatrix);
#if DEBUG
			Console.WriteLine(red);
#endif
			for (int i = 0; i < red.RowCount; i++)
			{
				for (int j = 0; j < red.RowCount; j++)
				{
					dynamic a = red[i, j];
					dynamic b = One;
					dynamic C = Zero;
					if (i == j)
					{
						if (a != One)
							throw new ArgumentException();
					}
					else
					{
						if (a != Zero)
							throw new ArgumentException();
					}
				}
			}

			return Matrix<T>.GetSubMatrix(red, 0, Source.ColumnCount, Source.RowCount, Source.ColumnCount);
		}

		public static T GetDeterminant(Matrix<T> Source)
		{
			if (Source == null)
				throw new ArgumentNullException();
			Matrix<T> newMatrix = Matrix<T>.GetCopy(Source);
		#if DEBUG
			Console.WriteLine("Begin of GetDeterminant:");
			Console.WriteLine(newMatrix);
			Console.WriteLine("...");
		#endif
			int One = 1;
			dynamic z = Source[0, 0];
			T Zero = z - z;
			try
			{

				int RankREF = 0;
				for (int j = 0; j < newMatrix.ColumnCount; j++)
				{
					//Find first nonzero value in j-column and swith that row on RankRef level (pivot level)
					for (int i = RankREF; i < newMatrix.RowCount; i++)
					{
						dynamic a = newMatrix[i, j];
						dynamic b = Zero;
						if (a != b)
						{
						#if DEBUG
							Console.WriteLine("Pivot Found: {0}:{1} - {2}", i, j, newMatrix[i, j]);
						#endif
							//We find nonzero value
							if (i != RankREF)
							{
								//RankREF row in zero in column
								//Swap two rows to get nonzero value to RankREF level
							#if DEBUG
								One *= -1;
								Console.WriteLine("Swaping");
							#endif
								newMatrix = Matrix<T>.SwapRows(newMatrix, i, RankREF);
							#if DEBUG
								Console.WriteLine(newMatrix);
							#endif
							}
							//Now we have pivot in RankREF row in column

							//Subtraction from below to get pivot column
						#if DEBUG
							Console.WriteLine("Subtraction");
						#endif
							for (int sub = RankREF + 1; sub < newMatrix.RowCount; sub++)
							{
								dynamic c = newMatrix[sub, j];
								dynamic d = newMatrix[RankREF, j];
								dynamic constant = c / d;

								//* Change
								newMatrix[sub, j] = Zero;
								for (int q = j + 1; q < newMatrix.ColumnCount; q++)
								{
									dynamic e = newMatrix[sub, q];
									dynamic f = newMatrix[RankREF, q];
									newMatrix[sub, q] = e - constant * f;
								}
							}
						#if DEBUG
							Console.WriteLine("After Subtraction");
							Console.WriteLine(newMatrix);
						#endif
							RankREF++;
							break;
						}
					}
				}

				T det = (One == -1) ? -(dynamic) newMatrix[0, 0] : newMatrix[0, 0];
				for (int i = 0; i < newMatrix.ColumnCount; i++)
				{
					det *= (dynamic)newMatrix[i, i];
				}

				return det;
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
			{
				throw new NotAllowedOperationException($"Operation != is not define for {Zero.GetType()} class", ex);
			}
		}

		#region Aritmetic operations
		/// <summary>
		/// Make addition of two Matrices
		/// </summary>
		/// <param name="A">Left Matrix</param>
		/// <param name="B">Right Matrix</param>
		/// <exception cref="ArgumentNullException">A or B is null</exception>
		/// <exception cref="ArgumentException">Size of Matrices is not same</exception>
		/// <exception cref="NotAllowedOperationException">Operation + is not defined for this object</exception>
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
				try
				{
					for (int j = 0; j < newMatrix.ColumnCount; j++)
					{
						dynamic a = A[i, j];
						dynamic b = B[i, j];
						newMatrix[i, j] = a + b;
					}
				}
				catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
				{
					throw new NotAllowedOperationException($"Operation + is not define on {A[0, 0].GetType()} class", ex);
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
		/// <exception cref="NotAllowedOperationException">Operation - is not defined for this object</exception>
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
				try
				{ 
					for (int j = 0; j < newMatrix.ColumnCount; j++)
					{
						dynamic a = A[i, j];
						dynamic b = B[i, j];
						newMatrix[i, j] = a + b;
					}
				}
				catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
				{
					throw new NotAllowedOperationException($"Operation - is not define on {A[0, 0].GetType()} class", ex);
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
		/// <exception cref="NotAllowedOperationException">Operation * is not defined for this object</exception>
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New Matrix</returns>
		public static Matrix<T> operator *(T A, Matrix<T> B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();

			Matrix<T> newMatrix = Matrix<T>.GetCopy(B);
			for (int i = 0; i < newMatrix.RowCount; i++)
			{
				for (int j = 0; j < newMatrix.ColumnCount; j++)
				{
					try
					{
						dynamic q = A;
						dynamic p = B[i, j];
						dynamic res = q * p;
						newMatrix[i, j] = res;
					}
					catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
					{
						throw new NotAllowedOperationException($"Operation * is not define on {B[0, 0].GetType()} class", ex);
					}
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
		/// <exception cref="NotAllowedOperationException">Operation * is not defined for this object</exception>
		/// <remarks>Time: O(nm)</remarks>
		/// <returns>New Matrix</returns>
		public static Matrix<T> operator *(Matrix<T> A, T B)
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
		/// <exception cref="NotAllowedOperationException">Operation * is not defined for this object</exception>
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
					try
					{
						dynamic a = A[i, 0];
						dynamic b = B[0, j];
						dynamic Sum = a * b;

						for (int k = 1; k < A.ColumnCount; k++)
						{
							dynamic a1 = A[i, k];
							dynamic b1 = B[k, j];
							Sum += a1 * b1;
						}
						newMatrix[i, j] = Sum;
					}
					catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
					{
						throw new NotAllowedOperationException($"Operation * or + is not define on {B[0, 0].GetType()} class", ex);
					}
				}
			}
			return newMatrix;
		}

		public static Matrix<T> operator *(Vector<T> A, Matrix<T> B)
		{
			Matrix<T> A_ = Matrix<T>.Create(A);
			return A_ * B;
		}
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
