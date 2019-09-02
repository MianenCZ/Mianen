using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.LinearAlgebra
{
	public class Vector<T>
	{
		public int Dimension { get; private set; }

		internal INumber<T>[] Data;

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

		public static Vector<T> operator *(INumber<T> Alpha, Vector<T> Input)
		{
			Vector<T> res = new Vector<T>(Input.Dimension);
			for (int i = 0; i < res.Dimension; i++)
			{
				res[i] = Input[i].Multiply(Alpha);
			}

			return res;
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

		public void Normalize()
		{
			INumber<T> Norm = Vector.GetNorm(this);
			for (int i = 0; i <this.Dimension; i++)
			{
				this[i] = this[i].Divide(Norm);
			}
		}
		public Vector<T> GetNormalized()
		{
			INumber<T> Norm = Vector.GetNorm(this);
			Vector<T> res = new Vector<T>(this.Dimension);
			for (int i = 0; i < res.Dimension; i++)
			{
				res[i] = this[i].Divide(Norm);
			}

			return res;
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
