using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.Geometry3D
{
	public struct Point3D<T>
	{
		public INumber<T> X { get; private set; }
		public INumber<T> Y { get; private set; }
		public INumber<T> Z { get; private set; }

		public Point3D(INumber<T> X, INumber<T> Y, INumber<T> Z)
		{
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <exception cref="ArgumentNullException">Input array is null</exception>
		/// <exception cref="ArgumentException">Input array is not lenght 2</exception>
		/// <param name="Array"></param>
		public static implicit operator Point3D<T>(INumber<T>[] Array)
		{
			if(Array == null)
				throw new ArgumentNullException();
			if(Array.Length != 3)
				throw new ArgumentException();
			return new Point3D<T>(Array[0], Array[1], Array[2]);
		}

		public static INumber<T> GetDistance(Point3D<T> A, Point3D<T> B)
		{
			//TODO: Math
			INumber<T> X = (A.X).Subtract(B.X);
			INumber<T> Xsq = X.Multiply(X);
			INumber<T> Y = (A.Y).Subtract(B.Y);
			INumber<T> Ysq = Y.Multiply(Y);
			INumber<T> Z = (A.Z).Subtract(B.Z);
			INumber<T> Zsq = Z.Multiply(Z);
			INumber<T> one = A.X.GetOne();
			INumber<T> half = one.Divide(one.Add(one));
			return (Xsq.Add(Ysq).Add(Zsq)).Power(half);
		}
	}
}
