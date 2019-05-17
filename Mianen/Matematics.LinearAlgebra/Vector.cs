using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.LinearAlgebra
{
	public class Vector<T>
	{
		public int Dimension { get; private set; }

		private INumber<T>[] Data;

		public INumber<T> this[int i, bool IndexFromZero = true]
		{
			get => Data[(IndexFromZero) ? i : i - 1];
			set => Data[(IndexFromZero) ? i : i - 1] = value;
		}

		public Vector(int Dimension)
		{
			this.Dimension = Dimension;
			this.Data = new INumber<T>[Dimension];
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
				INumber<T> Sum = A[i, 0].Multiply(B[0]);

				for (int k = 1; k < B.Dimension; k++)
				{
					Sum = Sum.Add(A[i, k].Multiply(B[k]));
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
				newVector[i] = A[i].Add(B[i]);
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

		public static INumber<T> GetNorm(Vector<T> Input)
		{
			INumber<T> Norm = Input[0];
			for (int i = 1; i < Input.Dimension; i++)
			{
				Norm = Norm.Multiply(Input[i]);
			}

			return Norm;
		}

		public static Vector<T> GetReverse(Vector<T> Input)
		{
			Vector<T> newVector = new Vector<T>(Input.Dimension);
			for (int i = 0; i < newVector.Dimension; i++)
			{
				newVector[i] = Input[Input.Dimension - i - 1];
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
