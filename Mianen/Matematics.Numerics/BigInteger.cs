using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics
{
	public class BigInteger
	{
		byte[] Values;
		bool signum;
		public int Signum { get { return (signum) ? 1 : -1; }
			private set { ; }
		}

		public BigInteger()
		{
			this.Values = new byte[1];
			Values[0] = 0;
		}
		
		public BigInteger(long Value)
		{
			List<byte> vals = new List<byte>();
			while (Value != 0)
			{
				vals.Add((byte)(Value % 10));
				Value /= 10;
			}
			this.Values = vals.ToArray();
		}



		public override string ToString()
		{
			StringBuilder bld = new StringBuilder();
			for (int i = this.Values.Length - 1; i >= 0; i++)
			{
				bld.Append(this.Values[i].ToString());
			}
			return bld.ToString();
		}

		public static BigInteger GetDefault()
		{
			return new BigInteger();
		}

		public static explicit operator BigInteger(long Value)
		{
			return new BigInteger(Value);
		}

		public static bool TryParse(string Input)
		{
			return true;
		}

	}


}

