using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.LinearAlgebra
{
	public class Vector<T>
	{
		public int Dimension { get; private set; }

		private T[] Data;

		public T this[int i, bool IndexFromZero = true]
		{
			get => Data[(IndexFromZero) ? i : i - 1];
			set => Data[(IndexFromZero) ? i : i - 1] = value;
		}

		public Vector(int Dimension)
		{
			this.Dimension = Dimension;
			this.Data = new T[Dimension];
		}

		public static Vector<T> Create(T[] Values)
		{
			Vector<T> newVector = new Vector<T>(Values.Length);
			Array.Copy(Values, newVector.Data, Values.Length);
			return newVector;
		}

		public static Vector<T> operator *(Matrix<T> A, Vector<T> B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();
			if (A.ColumnCount != B.Dimension)
				throw new ArgumentException();
			Vector<T> newVector = new Vector<T>(A.RowCount);


			for (int i = 0; i < A.RowCount; i++)
			{
				dynamic a = A[i, 0];
				dynamic b = B[0];
				dynamic Sum = a * b;

				for (int k = 1; k < B.Dimension; k++)
				{
					dynamic a1 = A[i, k];
					dynamic b1 = B[k];
					Sum += a1 * b1;
				}

				newVector[i] = Sum;
			}

			return newVector;
		}

		public static Vector<T> operator +(Vector<T> A, Vector<T> B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();
			if (A.Dimension != B.Dimension)
				throw new ArgumentException();

			Vector<T> newVector = new Vector<T>(A.Dimension);

			for (int i = 0; i < newVector.Dimension; i++)
			{
				dynamic a = A[i];
				dynamic b = B[i];
				newVector[i] = a + b;
			}

			return newVector;
		}

		public static Vector<T> operator -(Vector<T> A, Vector<T> B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();
			if (A.Dimension != B.Dimension)
				throw new ArgumentException();

			Vector<T> newVector = new Vector<T>(A.Dimension);

			for (int i = 0; i < newVector.Dimension; i++)
			{
				dynamic a = A[i];
				dynamic b = B[i];
				newVector[i] = a - b;
			}

			return newVector;
		}

		public override string ToString()
		{
			StringBuilder bld = new StringBuilder();
			bld.Append("(");
			for (int i = 0; i < this.Dimension; i++)
			{
				bld.Append(this[i].ToString());
				bld.Append((i == this.Dimension - 1) ? "" : " ");
			}

			bld.Append(")");
			return bld.ToString();
		}

	};
}
