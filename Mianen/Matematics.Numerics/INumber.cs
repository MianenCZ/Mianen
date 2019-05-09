using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics
{
	public interface INumber
	{
		INumber Add(INumber Number);
		INumber Subtract(INumber Number);
		INumber Multiply(INumber Number);
		INumber Divide(INumber Number);
		bool IsGreaterThan(INumber Number);
		bool IsGreaterOrEqualThan(INumber Number);
		bool IsLowerThan(INumber Number);
		bool IsLowerOrEqualThan(INumber Number);
		bool IsEqual(INumber Number);
		bool IsNotEqual(INumber Number);

		INumber GetOne();
		INumber GetZero();



		string ToString(IFormatProvider IformatProvider);
		string ToString(string Format);
		string ToString(string Format, IFormatProvider IformatProvider);

	}

	public interface INumber<T>
	{
		T Value { get; set; }
	}
}
