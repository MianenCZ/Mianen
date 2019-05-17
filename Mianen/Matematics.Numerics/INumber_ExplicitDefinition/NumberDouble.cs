﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics.INumber_ExplicitDefinition
{
	struct NumberDouble : INumber<double>
	{
		public double Value { get; set; }

		public NumberDouble(double Value)
		{
			this.Value = Value;
		}

		public INumber<double> Add(INumber<double> Number) => new NumberDouble(this.Value + Number.Value);

		public int CompareTo(INumber<double> Number) => this.Value.CompareTo(Number.Value);

		public INumber<double> Divide(INumber<double> Number) => new NumberDouble(this.Value / Number.Value);

		public bool Equals(INumber<double> Number) => this.Value.Equals(Number.Value);

		public bool IsEqual(INumber<double> Number) => this.Value == Number.Value;

		public bool IsGreaterOrEqualThan(INumber<double> Number) => this.Value >= Number.Value;

		public bool IsGreaterThan(INumber<double> Number) => this.Value > Number.Value;

		public bool IsLowerOrEqualThan(INumber<double> Number) => this.Value <= Number.Value;

		public bool IsLowerThan(INumber<double> Number) => this.Value < Number.Value;

		public bool IsNotEqual(INumber<double> Number) => this.Value != Number.Value;

		public INumber<double> Multiply(INumber<double> Number) => new NumberDouble(this.Value * Number.Value);

		public INumber<double> Subtract(INumber<double> Number) => new NumberDouble(this.Value * Number.Value);

		public string ToString(IFormatProvider IformatProvider) => this.Value.ToString(IformatProvider);

		public string ToString(string Format) => this.Value.ToString(Format);

		public string ToString(string Format, IFormatProvider IformatProvider) => this.Value.ToString(Format, IformatProvider);
	}
}
