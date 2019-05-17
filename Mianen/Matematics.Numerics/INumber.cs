using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics
{
	public interface INumber<T> : IComparable<INumber<T>>, IEquatable<INumber<T>>
	{
		T Value { get; set; }
		INumber<T> Add(INumber<T> Number);
		INumber<T> Subtract(INumber<T> Number);
		INumber<T> Multiply(INumber<T> Number);
		INumber<T> Divide(INumber<T> Number);
		INumber<T> Negative();
		INumber<T> Power(INumber<T> Exponent);
		bool IsGreaterThan(INumber<T> Number);
		bool IsGreaterOrEqualThan(INumber<T> Number);
		bool IsLowerThan(INumber<T> Number);
		bool IsLowerOrEqualThan(INumber<T> Number);
		bool IsEqual(INumber<T> Number);
		bool IsNotEqual(INumber<T> Number);

		INumber<T> GetZero();
		INumber<T> GetOne();	

		string ToString(IFormatProvider IformatProvider);
		string ToString(string Format);
		string ToString(string Format, IFormatProvider IformatProvider);

	}
}
