using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Geometry2D
{
	public struct Point2D<T>
	{
		public T X { get; private set; }
		public T Y { get; private set; }
		
		public Point2D(T X, T Y)
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
		public static implicit operator Point2D<T>(T[] Array)
		{
			if(Array == null)
				throw new ArgumentNullException();
			if(Array.Length != 2)
				throw new ArgumentException();
			return new Point2D<T>(Array[0], Array[1]);
		}
	}
}
