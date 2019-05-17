using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.Geometry2D
{
	public struct Point2D<T> where T : INumber
	{
		public INumber<T> X { get; private set; }
		public INumber<T> Y { get; private set; }
		
		public Point2D(INumber<T> X, INumber<T> Y)
		{
			this.X = X;
			this.Y = Y;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <exception cref="ArgumentNullException">Input array is null</exception>
		/// <exception cref="ArgumentException">Input array is not lenght 2</exception>
		/// <param name="Array"></param>
		public static implicit operator Point2D<T>(INumber<T>[] Array)
		{
			if(Array == null)
				throw new ArgumentNullException();
			if(Array.Length != 2)
				throw new ArgumentException();
			return new Point2D<T>(Array[0], Array[1]);
		}

		public static INumber<T> GetDistance(Point2D<T> A, Point2D<T> B)
		{
			try
			{
				//TODO: Math
				return (INumber<T>)(dynamic)(Math.Pow( ((A.X - (dynamic)B.X)*(A.X - (dynamic)B.X) + (A.Y - (dynamic)B.Y) * (A.Y - (dynamic)B.Y)), 0.5d));
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
			{
				throw new NotAllowedOperationException($"Operation != is not define for {typeof(T)} class", ex);
			}

		}
	}
}
