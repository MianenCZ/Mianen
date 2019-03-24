using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics.LinearAlgebra;

namespace Mianen.Matematics.Geometry2D
{
	public class Polynom<T>
	{
		public Vector<T> DefVector { get; set; }
		public int Degree => this.DefVector.Dimension - 1;

		public T this[int Index]
		{
			get => DefVector[Index];
		}

		public Polynom(Vector<T> DefVector)
		{
			if(DefVector == null)
				throw new ArgumentNullException();

			bool first = true;

			T Zero = (dynamic)DefVector[0] - (dynamic)DefVector[0];

			for (int i = DefVector.Dimension - 1; i >= 0; i--)
			{
				if (first && (dynamic)DefVector[i] != Zero)
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

		public T Eval(T x)
		{
			T arg = (dynamic) this.DefVector[this.Degree - 1] / this.DefVector[this.Degree - 1];
			T Sum = (dynamic)x - x;
			for (int i = 0; i < this.DefVector.Dimension; i++)
			{
				Sum += (dynamic)DefVector[i] * arg;
				arg *= (dynamic)x;
			}

			return Sum;
		}

		public static Polynom<T> Aproximate(Point2D<T>[] Points, int TargetDegree)
		{
			Matrix<T> A = new Matrix<T>(Points.Length, TargetDegree + 1);
			Vector<T> Vector = new Vector<T>(Points.Length);

			T zero = (dynamic)Points[0].X - Points[0].X;
			T one = default(T);
			bool found = false;

			for (int i = 0; i < Points.Length; i++)
			{
				if (Points[i].X != (dynamic) zero)
				{
					one = (dynamic)Points[i].X / Points[i].X;
					found = true;
					break;
				}
			}

			if (!found)
				throw new ArgumentException("Object can not be aproximate to Circle2D");
			for (int i = 0; i < Points.Length; i++)
			{
				T val = one;
				for (int j = TargetDegree; j >= 0; j--)
				{
					A[i, j] = val;
					val *= (dynamic) Points[i].X;
				}

				Vector[i] = Points[i].Y;
			}

			Vector<T> Res = LinerarMath<T>.LeastSquers(A, Vector);

			return new Polynom<T>(Res);
		}
	}
}
