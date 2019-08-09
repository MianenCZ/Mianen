using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics
{
	public class NInt : INumber<int>
	{
		public int Value { get; set; }

		public NInt(int Value)
		{
			this.Value = Value;
		}

		public INumber<int> Add(INumber<int> Number) => new NInt(this.Value + Number.Value);

		public int CompareTo(INumber<int> Number) => this.Value.CompareTo(Number.Value);

		public INumber<int> Divide(INumber<int> Number) => new NInt(this.Value / Number.Value);

		public bool Equals(INumber<int> Number) => this.Value == Number.Value;

		public bool IsEqual(INumber<int> Number) => this.Value == Number.Value;

		public bool IsGreaterOrEqualThan(INumber<int> Number) => this.Value >= Number.Value;

		public bool IsGreaterThan(INumber<int> Number) => this.Value > Number.Value;

		public bool IsLowerOrEqualThan(INumber<int> Number) => this.Value <= Number.Value;

		public bool IsLowerThan(INumber<int> Number) => this.Value < Number.Value;

		public bool IsNotEqual(INumber<int> Number) => this.Value != Number.Value;

		public INumber<int> Multiply(INumber<int> Number) => new NInt(this.Value * Number.Value);

		public INumber<int> Subtract(INumber<int> Number) => new NInt(this.Value * Number.Value);

		public override string ToString() => this.Value.ToString();

		public string ToString(IFormatProvider IformatProvider) => this.Value.ToString(IformatProvider);

		public string ToString(string Format) => this.Value.ToString(Format);

		public string ToString(string Format, IFormatProvider IformatProvider) =>
			this.Value.ToString(Format, IformatProvider);

		public INumber<int> GetZero() => new NInt(0);

		public INumber<int> GetOne() => new NInt(1);

		public INumber<int> Negative() => new NInt(-Value);

		public INumber<int> Power(INumber<int> Exponent)
		{
			int val = (int)Math.Pow((double)Value, (double)Exponent.Value);
			return new NInt(val);
		}

		public static implicit operator NInt(int Value)
		{
			return new NInt(Value);
		}
	}
}
