using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mianen.Matematics.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics.Tests
{
	[TestClass()]
	public class SNumberTest
	{
		[TestMethod()]
		public void RunTest()
		{
			SNumber s1 = new SNumber("ahoj");
			SNumber s2 = new SNumber("ahoj");
			var res = s1.Add(s2);
			Console.WriteLine(res.Value.ToString());
		}


		public class SNumber : INumber<string>
		{
			public string Value { get; set; }

			public SNumber(string Value)
			{
				this.Value = Value;
			}
			public INumber<string> GetCopy() => new SNumber(this.Value);

			public INumber<string> Add(INumber<string> Number)
			{
				if (this.Value.Length != Number.Value.Length)
					throw new ArgumentException();
				StringBuilder bld = new StringBuilder();

				for (int i = 0; i < this.Value.Length; i++)
				{
					bld.Append((char)((this.Value[i] + Number.Value[i]) % 128));
				}
				return new SNumber(bld.ToString());
			}

			public int CompareTo(INumber<string> other)
			{
				throw new NotImplementedException();
			}

			public INumber<string> Divide(INumber<string> Number)
			{
				throw new NotImplementedException();
			}

			public bool Equals(INumber<string> other)
			{
				throw new NotImplementedException();
			}

			public INumber<string> GetOne()
			{
				throw new NotImplementedException();
			}

			public INumber<string> GetZero()
			{
				throw new NotImplementedException();
			}

			public bool IsEqual(INumber<string> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsGreaterOrEqualThan(INumber<string> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsGreaterThan(INumber<string> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsLowerOrEqualThan(INumber<string> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsLowerThan(INumber<string> Number)
			{
				throw new NotImplementedException();
			}

			public bool IsNotEqual(INumber<string> Number)
			{
				throw new NotImplementedException();
			}

			public INumber<string> Multiply(INumber<string> Number)
			{
				throw new NotImplementedException();
			}

			public INumber<string> Negative()
			{
				throw new NotImplementedException();
			}

			public INumber<string> Power(INumber<string> Exponent)
			{
				throw new NotImplementedException();
			}

			public INumber<string> Subtract(INumber<string> Number)
			{
				throw new NotImplementedException();
			}

			public string ToString(IFormatProvider IformatProvider)
			{
				throw new NotImplementedException();
			}

			public string ToString(string Format)
			{
				throw new NotImplementedException();
			}

			public string ToString(string Format, IFormatProvider IformatProvider)
			{
				throw new NotImplementedException();
			}
		}





	}
}
