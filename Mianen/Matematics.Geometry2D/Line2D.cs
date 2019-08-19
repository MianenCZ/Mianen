using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics.LinearAlgebra;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.Geometry2D
{
	class Line2D<T>
	{
		public Vector<T> ParametricVector { get; set; }
		public Point2D<T> Point { get; set; }

		public Line2D(Vector<T> ParametricVector, Point2D<T> Point)
		{
			if (object.Equals(ParametricVector, null) || object.Equals(Point, null))
				throw new ArgumentNullException();
			if (ParametricVector.Dimension != 2)
				throw new ArgumentException();
			this.ParametricVector = ParametricVector;
			this.Point = Point;
		}

		/// <summary>
		/// Equals to definition y= Px + Q
		/// </summary>
		/// <param name="P"></param>
		/// <param name="Q"></param>
		public Line2D(INumber<T> P, INumber<T> Q)
		{
			Vector<T> A = Vector<T>.Create(new INumber<T>[2]{ P.GetZero(), Q });
			Vector<T> B = Vector<T>.Create(new INumber<T>[2]{ P.GetOne(), P.Add(Q) });
			Vector<T> Par = A - B;
			this.ParametricVector = (A - B).GetNormalized();
			this.Point = new Point2D<T>(P.GetZero(), Q);
		}


		public static INumber<T> Angle(Line2D<T> LineA, Line2D<T> LineB)
		{
			INumber<T> CosFi = Vector<T>.Angle(LineA.ParametricVector, LineB.ParametricVector);
			return CosFi;
		}


		public static Line2D<T> Aproximate(Point2D<T>[] Points)
		{
			Matrix<T> A = new Matrix<T>(Points.Length, 2);
			Vector<T> Vector = new Vector<T>(Points.Length);
			INumber<T> zero = Points[0].X.GetZero();
			INumber<T> one = Points[0].X.GetOne();

			for (int i = 0; i < Points.Length; i++)
			{
				A[i, 0] = Points[i].X.Add(Points[i].X);
				Vector[i] = Points[i].Y;
			}
			Vector<T> Res = LinearMath.LeastSquers(A, Vector);
			return new Line2D<T>(Res[0], Res[1]);
		}





	}
}
