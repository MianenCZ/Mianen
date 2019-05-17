using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics.LinearAlgebra;
using Newtonsoft.Json.Serialization;
using Mianen.Matematics;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.Geometry2D
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T">NumberDef system to define precision</typeparam>
	public struct Circle2D<T>
	{
		public Point2D<T> Center { get; set; }
		public INumber<T> Radius { get; set; }

		public Circle2D(Point2D<T> Center, INumber<T> Radius)
		{
			this.Center = Center;
			this.Radius = Radius;
		}

		public override string ToString()
		{
			return $"Circle2D([{Center.X};{Center.Y}], r = {Radius})";
		}

		///*
		public static Circle2D<T> Aproximate(Point2D<T>[] Points)
		{
			Matrix<T> A = new Matrix<T>(Points.Length, 3);
			Vector<T> Vector = new Vector<T>(Points.Length);

			INumber<T> zero = Points[0].X.GetZero();
			INumber<T> one = Points[0].X.GetOne();

			for (int i = 0; i < Points.Length; i++)
			{
				A[i, 0] = Points[i].X.Add(Points[i].X);
				A[i, 1] = Points[i].Y.Add(Points[i].Y);
				A[i, 2] = one;
				INumber<T> Xsq = Points[i].X.Multiply(Points[i].X);
				INumber<T> Ysq = Points[i].Y.Multiply(Points[i].Y);
				Vector[i] = Xsq.Add(Ysq);
			}


			Vector<T> Res = LinerarMath<T>.LeastSquers(A, Vector);
			INumber<T> half = zero.Divide(zero.Add(zero));
			INumber<T> Rsq = Res[2].Add(Res[1].Multiply(Res[1])).Add(Res[0].Multiply(Res[0]));
			Circle2D<T> res = new Circle2D<T>
			{
				Radius = Rsq.Power(half),
				Center = new Point2D<T>(Res[0], Res[1]),
			};

			return res;
		}

		//*/
	}
}
