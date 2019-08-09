using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics
{
	public class NLong : INumber<long>
	{
		public long Value { get; set; }

		public NLong(long Value)
		{
			this.Value = Value;
		}

		public INumber<long> Add(INumber<long> Number) => new NLong(this.Value + Number.Value);

		public int CompareTo(INumber<long> Number) => this.Value.CompareTo(Number.Value);

		public INumber<long> Divide(INumber<long> Number) => new NLong(this.Value / Number.Value);

		public bool Equals(INumber<long> Number) => this.Value == Number.Value;

		public bool IsEqual(INumber<long> Number) => this.Value == Number.Value;

		public bool IsGreaterOrEqualThan(INumber<long> Number) => this.Value >= Number.Value;

		public bool IsGreaterThan(INumber<long> Number) => this.Value > Number.Value;

		public bool IsLowerOrEqualThan(INumber<long> Number) => this.Value <= Number.Value;

		public bool IsLowerThan(INumber<long> Number) => this.Value < Number.Value;

		public bool IsNotEqual(INumber<long> Number) => this.Value != Number.Value;

		public INumber<long> Multiply(INumber<long> Number) => new NLong(this.Value * Number.Value);

		public INumber<long> Subtract(INumber<long> Number) => new NLong(this.Value * Number.Value);

		public override string ToString() => this.Value.ToString();

		public string ToString(IFormatProvider IformatProvider) => this.Value.ToString(IformatProvider);

		public string ToString(string Format) => this.Value.ToString(Format);

		public string ToString(string Format, IFormatProvider IformatProvider) => this.Value.ToString(Format, IformatProvider);

		public INumber<long> GetZero() => new NLong(0);

		public INumber<long> GetOne() => new NLong(1);

		public INumber<long> Negative() => new NLong(-Value);

		public INumber<long> Power(INumber<long> Exponent)
		{
			long val = (long)Math.Pow((double)Value, (double)Exponent.Value);
			return new NLong(val);
		}

		public static implicit operator NLong(long Value)
		{
			return new NLong(Value);
		}
	}
}
