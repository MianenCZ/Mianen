using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.LinearAlgebra
{
	public static class Vector
	{
		public static Vector<T> Create<T>(INumber<T>[] Values)
		{
			Vector<T> newVector = new Vector<T>(Values.Length);
			Array.Copy(Values, newVector.Data, Values.Length);
			return newVector;
		}
		public static INumber<T> GetNorm<T>(Vector<T> Input)
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

		public static Vector<T> GetReverse<T>(Vector<T> Input)
		{
			Vector<T> newVector = new Vector<T>(Input.Dimension);
			for (int i = 0; i < newVector.Dimension; i++)
			{
				newVector[i] = Input[Input.Dimension - i - 1];
			}

			return newVector;
		}

		public static INumber<T> GetDotProduct<T>(Vector<T> VectorA, Vector<T> VectorB)
		{
			if (object.Equals(VectorA, null) || object.Equals(VectorB, null))
				throw new ArgumentNullException();
			if (VectorA.Dimension != VectorB.Dimension)
				throw new ArgumentException();
			INumber<T> TotalSum = VectorA[0].GetZero();

			for (int i = 0; i < VectorA.Dimension; i++)
			{
				TotalSum = TotalSum.Add(VectorA[i].Multiply(VectorB[i]));
			}

			return TotalSum;
		}

		public static INumber<T> Angle<T>(Vector<T> VectorA, Vector<T> VectorB)
		{
			INumber<T> NormA = Vector.GetNorm(VectorA);
			INumber<T> NormB = Vector.GetNorm(VectorB);
			INumber<T> DotProduct = Vector.GetDotProduct(VectorA, VectorB);

			INumber<T> CosFi = DotProduct.Divide(NormA.Multiply(NormB));
			return CosFi;
		}



	}
}
