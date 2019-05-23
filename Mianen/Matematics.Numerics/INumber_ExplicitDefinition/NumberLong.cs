using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics
{
	public class NumberLong : INumber<long>
	{
		public long Value { get; set; }

		public NumberLong(long Value)
		{
			this.Value = Value;
		}

		public INumber<long> Add(INumber<long> Number) => new NumberLong(this.Value + Number.Value);

		public int CompareTo(INumber<long> Number) => this.Value.CompareTo(Number.Value);

		public INumber<long> Divide(INumber<long> Number) => new NumberLong(this.Value / Number.Value);

		public bool Equals(INumber<long> Number) => this.Value.Equals(Number.Value);

		public bool IsEqual(INumber<long> Number) => this.Value == Number.Value;

		public bool IsGreaterOrEqualThan(INumber<long> Number) => this.Value >= Number.Value;

		public bool IsGreaterThan(INumber<long> Number) => this.Value > Number.Value;

		public bool IsLowerOrEqualThan(INumber<long> Number) => this.Value <= Number.Value;

		public bool IsLowerThan(INumber<long> Number) => this.Value < Number.Value;

		public bool IsNotEqual(INumber<long> Number) => this.Value != Number.Value;

		public INumber<long> Multiply(INumber<long> Number) => new NumberLong(this.Value * Number.Value);

		public INumber<long> Subtract(INumber<long> Number) => new NumberLong(this.Value * Number.Value);

		public string ToString(IFormatProvider IformatProvider) => this.Value.ToString(IformatProvider);

		public string ToString(string Format) => this.Value.ToString(Format);

		public string ToString(string Format, IFormatProvider IformatProvider) => this.Value.ToString(Format, IformatProvider);

		public INumber<long> GetZero() => new NumberLong(0);

		public INumber<long> GetOne() => new NumberLong(1);

		public INumber<long> Negative() => new NumberLong(-Value);

		public INumber<long> Power(INumber<long> Exponent)
		{
			long val = (long)Math.Pow((double)Value, (double)Exponent.Value);
			return new NumberLong(val);
		}

		public static implicit operator NumberLong(long Value)
		{
			return new NumberLong(Value);
		}
	}
}
