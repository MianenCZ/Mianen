﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics
{
	public class NFloat : INumber<float>
	{
		public float Value { get; set; }

		public NFloat(float Value)
		{
			this.Value = Value;
		}

		public INumber<float> Add(INumber<float> Number) => new NFloat(this.Value + Number.Value);

		public int CompareTo(INumber<float> Number) => this.Value.CompareTo(Number.Value);

		public INumber<float> Divide(INumber<float> Number) => new NFloat(this.Value / Number.Value);

		public bool Equals(INumber<float> Number) => this.Value == Number.Value;

		public bool IsEqual(INumber<float> Number) => this.Value == Number.Value;

		public bool IsGreaterOrEqualThan(INumber<float> Number) => this.Value >= Number.Value;

		public bool IsGreaterThan(INumber<float> Number) => this.Value > Number.Value;

		public bool IsLowerOrEqualThan(INumber<float> Number) => this.Value <= Number.Value;

		public bool IsLowerThan(INumber<float> Number) => this.Value < Number.Value;

		public bool IsNotEqual(INumber<float> Number) => this.Value != Number.Value;

		public INumber<float> Multiply(INumber<float> Number) => new NFloat(this.Value * Number.Value);

		public INumber<float> Subtract(INumber<float> Number) => new NFloat(this.Value * Number.Value);

		public override string ToString() => this.Value.ToString();

		public string ToString(IFormatProvider IformatProvider) => this.Value.ToString(IformatProvider);

		public string ToString(string Format) => this.Value.ToString(Format);

		public string ToString(string Format, IFormatProvider IformatProvider) => this.Value.ToString(Format, IformatProvider);

		public INumber<float> GetZero() => new NFloat(0);

		public INumber<float> GetOne() => new NFloat(0);

		public INumber<float> Negative() => new NFloat(-Value);

		public INumber<float> Power(INumber<float> Exponent)
		{
			float val = (float)Math.Pow((double)Value, (double)Exponent.Value);
			return new NFloat(val);
		}

		public static implicit operator NFloat(float Value)
		{
			return new NFloat(Value);
		}
	}
}