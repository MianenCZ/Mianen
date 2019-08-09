using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics
{
	public static class INumberExtensions
	{
		public static INumber<T> Sum<T>(this IEnumerable<INumber<T>> Values)
		{
			IEnumerator<INumber<T>> ValueEnum = Values.GetEnumerator();
			ValueEnum.MoveNext();
			INumber<T> sum = ValueEnum.Current.GetZero();

			foreach (INumber<T> num in Values)
			{
				sum = sum.Add(num);
			}

			return sum;
		}
	}
}
