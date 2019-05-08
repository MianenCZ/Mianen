using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics.INumber_ExplicitDefinition
{
	struct NumberDecimal : INumber<decimal>
	{
		public decimal Value { get; set; }

		public NumberDecimal(decimal Value)
		{
			this.Value = Value;
		}

		public INumber<decimal> Add(INumber<decimal> Number) => new NumberDecimal(this.Value + Number.Value);

		public int CompareTo(INumber<decimal> Number) => this.Value.CompareTo(Number.Value);

		public INumber<decimal> Divide(INumber<decimal> Number) => new NumberDecimal(this.Value / Number.Value);

		public bool Equals(INumber<decimal> Number) => this.Value.Equals(Number.Value);

		public bool IsEqual(INumber<decimal> Number) => this.Value == Number.Value;

		public bool IsGreaterOrEqualThan(INumber<decimal> Number) => this.Value >= Number.Value;

		public bool IsGreaterThan(INumber<decimal> Number) => this.Value > Number.Value;

		public bool IsLowerOrEqualThan(INumber<decimal> Number) => this.Value <= Number.Value;

		public bool IsLowerThan(INumber<decimal> Number) => this.Value < Number.Value;

		public bool IsNotEqual(INumber<decimal> Number) => this.Value != Number.Value;

		public INumber<decimal> Multiply(INumber<decimal> Number) => new NumberDecimal(this.Value * Number.Value);

		public INumber<decimal> Subtract(INumber<decimal> Number) => new NumberDecimal(this.Value * Number.Value);

		public string ToString(IFormatProvider IformatProvider) => this.Value.ToString(IformatProvider);

		public string ToString(string Format) => this.Value.ToString(Format);

		public string ToString(string Format, IFormatProvider IformatProvider) => this.Value.ToString(Format, IformatProvider);
	}
}
