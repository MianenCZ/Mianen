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

		public static Vector<T> Create(INumber<T>[] Values)
		{
			Vector<T> newVector = new Vector<T>(Values.Length);
			Array.Copy(Values, newVector.Data, Values.Length);
			return newVector;
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

		public static INumber<T> GetNorm(Vector<T> Input)
		{
			INumber<T> Norm = Input[0];
			INumber<T> One = Input[0].GetOne();
			INumber<T> half = One.Divide(One.Add(One));
			for (int i = 1; i < Input.Dimension; i++)
			{
				Norm = Norm.Multiply(Input[i]);
			}

			INumber<T> Result = Norm.Power(half);
			return Result;
		}

		public void Normalize()
		{
			INumber<T> Norm = Vector<T>.GetNorm(this);
			for (int i = 0; i <this.Dimension; i++)
			{
				this[i] = this[i].Divide(Norm);
			}
		}

		public Vector<T> GetNormalized()
		{
			INumber<T> Norm = Vector<T>.GetNorm(this);
			Vector<T> res = new Vector<T>(this.Dimension);
			for (int i = 0; i < res.Dimension; i++)
			{
				res[i] = this[i].Divide(Norm);
			}

			return res;
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

		public static INumber<T> GetDotProduct(Vector<T> VectorA, Vector<T> VectorB)
		{
			if(object.Equals(VectorA, null) || object.Equals(VectorB, null))
				throw new ArgumentNullException();
			if(VectorA.Dimension != VectorB.Dimension)
				throw new ArgumentException();
			INumber<T> TotalSum = VectorA[0].GetZero();

			for (int i = 0; i < VectorA.Dimension; i++)
			{
				TotalSum = TotalSum.Add(VectorA[i].Multiply(VectorB[i]));
			}

			return TotalSum;
		}

		public static INumber<T> Angle(Vector<T> VectorA, Vector<T> VectorB)
		{
			INumber<T> NormA = Vector<T>.GetNorm(VectorA);
			INumber<T> NormB = Vector<T>.GetNorm(VectorB);
			INumber<T> DotProduct = Vector<T>.GetDotProduct(VectorA, VectorB);

			INumber<T> CosFi = DotProduct.Divide(NormA.Multiply(NormB));
			return CosFi;
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
