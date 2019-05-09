using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics
{
	struct NumberLong : INumber<long>, INumber
	{
		public long Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public INumber Add(INumber Number)
		{
			throw new NotImplementedException();
		}

		public INumber Divide(INumber Number)
		{
			throw new NotImplementedException();
		}

		public INumber GetOne()
		{
			throw new NotImplementedException();
		}

		public INumber GetZero()
		{
			throw new NotImplementedException();
		}

		public bool IsEqual(INumber Number)
		{
			throw new NotImplementedException();
		}

		public bool IsGreaterOrEqualThan(INumber Number)
		{
			throw new NotImplementedException();
		}

		public bool IsGreaterThan(INumber Number)
		{
			throw new NotImplementedException();
		}

		public bool IsLowerOrEqualThan(INumber Number)
		{
			throw new NotImplementedException();
		}

		public bool IsLowerThan(INumber Number)
		{
			throw new NotImplementedException();
		}

		public bool IsNotEqual(INumber Number)
		{
			throw new NotImplementedException();
		}

		public INumber Multiply(INumber Number)
		{
			throw new NotImplementedException();
		}

		public INumber Subtract(INumber Number)
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
