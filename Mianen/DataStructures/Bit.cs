using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.DataStructures
{
	public struct Bit
	{
		public byte Value;

		public static implicit operator Bit(int val)
		{
			if (!(val == 0 || val == 1))
				throw new ArgumentException();
			return new Bit() { Value = (byte)val };
		}

		public static implicit operator Bit(byte val)
		{
			if (!(val == 0 || val == 1))
				throw new ArgumentException();
			return new Bit() { Value = val };
		}

		public static implicit operator int(Bit Value)
		{
			return Value.Value;
		}

		public static implicit operator byte(Bit Value)
		{
			return Value.Value;
		}

		public static byte ToByte(Bit[] Values, Endianity endian)
		{
			byte res = 0;
			for (int i = 0; i < 8; i++)
			{
				res += (byte)(Values[i] << ((endian == Endianity.LittleEndian) ? i : 7 - i));
			}

			return res;
		}


	}
}
