﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics.INumber_ExplicitDefinition
{
	struct NumberInt : INumber<int>
	{
		public int Value { get; set; }

		public NumberInt(int Value)
		{
			this.Value = Value;
		}

		public INumber<int> Add(INumber<int> Number) => new NumberInt(this.Value + Number.Value);

		public int CompareTo(INumber<int> Number) => this.Value.CompareTo(Number.Value);

		public INumber<int> Divide(INumber<int> Number) => new NumberInt(this.Value / Number.Value);

		public bool Equals(INumber<int> Number) => this.Value.Equals(Number.Value);

		public bool IsEqual(INumber<int> Number) => this.Value == Number.Value;

		public bool IsGreaterOrEqualThan(INumber<int> Number) => this.Value >= Number.Value;

		public bool IsGreaterThan(INumber<int> Number) => this.Value > Number.Value;

		public bool IsLowerOrEqualThan(INumber<int> Number) => this.Value <= Number.Value;

		public bool IsLowerThan(INumber<int> Number) => this.Value < Number.Value;

		public bool IsNotEqual(INumber<int> Number) => this.Value != Number.Value;

		public INumber<int> Multiply(INumber<int> Number) => new NumberInt(this.Value * Number.Value);

		public INumber<int> Subtract(INumber<int> Number) => new NumberInt(this.Value * Number.Value);

		public string ToString(IFormatProvider IformatProvider) => this.Value.ToString(IformatProvider);

		public string ToString(string Format) => this.Value.ToString(Format);

		public string ToString(string Format, IFormatProvider IformatProvider) =>
			this.Value.ToString(Format, IformatProvider);
	}
}