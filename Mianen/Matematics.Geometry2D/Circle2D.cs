using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics.LinearAlgebra;
using Newtonsoft.Json.Serialization;
using Mianen.Matematics;

namespace Mianen.Matematics.Geometry2D
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T">NumberDef system to define precision</typeparam>
	public struct Circle2D<T>
	{
		public Point2D<T> Center { get; set; }
		public T Radius { get; set; }

		public Circle2D(Point2D<T> Center, T Radius)
		{
			this.Center = Center;
			this.Radius = Radius;
		}

		public override string ToString()
		{
			return $"Circle2D([{Center.X};{Center.Y}], r = {Radius})";
		}
		/*
		public static Circle2D<T> Aproximate(Point2D<T>[] Points)
		{	
			Matrix<T> A = new Matrix<T>(Points.Length, 3);
			Vector<T> Vector = new Vector<T>(Points.Length);

			INumber<T> one = default(T);
			bool found = false;

			for (int i = 0; i < Points.Length; i++)
			{
				try
				{
					one = (dynamic)Points[i].X / Points[i].X;
					found = true;
					break;
				}
				catch (DivideByZeroException)
				{
					continue;
				}
			}
			if(!found)
				throw new ArgumentException("Object can not be aproximate to Circle2D");

			try
			{
				for (int i = 0; i < Points.Length; i++)
				{
					A[i, 0] = 2 * (dynamic) Points[i].X;
					A[i, 1] = 2 * (dynamic) Points[i].Y;
					A[i, 2] = one;
					Vector[i] = (dynamic) Points[i].X * (dynamic) Points[i].X + (dynamic) Points[i].Y * (dynamic) Points[i].Y;
				}
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
			{
				throw new NotAllowedOperationException($"Operations * or + si not define on {typeof(T)}", ex);
			}

			Vector<T> Res = LinerarMath<T>.LeastSquers(A, Vector);
			Circle2D<T> res = new Circle2D<T>
			{
				Radius = (T)Math.Pow((dynamic)Res[2] + (dynamic)Res[1] * (dynamic)Res[1] + (dynamic)Res[0] * (dynamic)Res[0], 0.5),
				Center = new Point2D<T>(Res[0], Res[1]),
			};

			return res;
		}
		//*/
	}
}
