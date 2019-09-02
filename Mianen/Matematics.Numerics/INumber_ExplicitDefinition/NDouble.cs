using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics
{
	public class NDouble : INumber<double>
	{
		public double Value { get; set; }

		public NDouble(double Value)
		{
			this.Value = Value;
		}

		public INumber<double> GetCopy() => new NDouble(this.Value);

		public INumber<double> Add(INumber<double> Number) => new NDouble(this.Value + Number.Value);

		public int CompareTo(INumber<double> Number) => this.Value.CompareTo(Number.Value);

		public INumber<double> Divide(INumber<double> Number) => new NDouble(this.Value / Number.Value);

		public bool Equals(INumber<double> Number) => this.Value == Number.Value;

		public bool IsEqual(INumber<double> Number) => this.Value == Number.Value;

		public bool IsGreaterOrEqualThan(INumber<double> Number) => this.Value >= Number.Value;

		public bool IsGreaterThan(INumber<double> Number) => this.Value > Number.Value;

		public bool IsLowerOrEqualThan(INumber<double> Number) => this.Value <= Number.Value;

		public bool IsLowerThan(INumber<double> Number) => this.Value < Number.Value;

		public bool IsNotEqual(INumber<double> Number) => this.Value != Number.Value;

		public INumber<double> Multiply(INumber<double> Number) => new NDouble(this.Value * Number.Value);

		public INumber<double> Subtract(INumber<double> Number) => new NDouble(this.Value * Number.Value);

		public override string ToString() => this.Value.ToString();

		public string ToString(IFormatProvider IformatProvider) => this.Value.ToString(IformatProvider);

		public string ToString(string Format) => this.Value.ToString(Format);

		public string ToString(string Format, IFormatProvider IformatProvider) => this.Value.ToString(Format, IformatProvider);

		public INumber<double> GetZero() => new NDouble(0);

		public INumber<double> GetOne() => new NDouble(0);

		public INumber<double> Negative() => new NDouble(-Value);

		public INumber<double> Power(INumber<double> Exponent)
		{
			double val = Math.Pow((double)Value, (double)Exponent.Value);
			return new NDouble(val);
		}

		public static implicit operator NDouble(double Value)
		{
			return new NDouble(Value);
		}

	}
}
