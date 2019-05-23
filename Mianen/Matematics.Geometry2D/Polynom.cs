using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics.LinearAlgebra;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.Geometry2D
{
	public class Polynom<T>
	{
		public Vector<T> DefVector { get; set; }
		public int Degree => this.DefVector.Dimension - 1;

		public INumber<T> this[int Index]
		{
			get => DefVector[Index];
		}

		public Polynom(Vector<T> DefVector)
		{
			if(DefVector == null)
				throw new ArgumentNullException();

			bool first = true;

			INumber<T> zero = DefVector[0].GetZero();

			for (int i = DefVector.Dimension - 1; i >= 0; i--)
			{
				if (first && DefVector[i].IsNotEqual(zero))
				{
					first = false;
					this.DefVector = new Vector<T>(i + 1);
					break;
				}
			}

			if (this.DefVector == null)
				throw new ArgumentException();

			for (int i = 0; i < this.DefVector.Dimension; i++)
			{
				this.DefVector[i] = DefVector[i];
			}
		}

		public INumber<T> Eval(INumber<T> x)
		{
			INumber<T> arg = x.GetOne();
			INumber<T> Sum = x.GetZero();
			for (int i = 0; i < this.DefVector.Dimension; i++)
			{
				Sum = Sum.Add(DefVector[i].Multiply(arg));
				arg = arg.Multiply(x);
			}
			return Sum;
		}

		public static Polynom<T> Aproximate(Point2D<T>[] Points, int TargetDegree)
		{
			Matrix<T> A = new Matrix<T>(Points.Length, TargetDegree + 1);
			Vector<T> Vector = new Vector<T>(Points.Length);

			INumber<T> zero = Points[0].X.GetZero();
			INumber<T> one = Points[0].X.GetOne();

			for (int i = 0; i < Points.Length; i++)
			{
				INumber<T> val = one;
				for (int j = TargetDegree; j >= 0; j--)
				{
					A[i, j] = val;
					val = val.Multiply(Points[i].X);
				}
				Vector[i] = Points[i].Y;
			}

			Vector<T> Res = LinearMath<T>.LeastSquers(A, Vector);

			return new Polynom<T>(Vector<T>.GetReverse(Res));
		}
	}
}
