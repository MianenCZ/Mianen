using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.DataStructures
{
	public class BitDefiner
	{
		public int Length { get; }
		public int ArrayLenght
		{
			get => Data.Length;
		}
		internal byte[] Data;

		public Bit this[int Index]
		{
			get
			{
				if (Index >= Length)
					throw new IndexOutOfRangeException();
				else
				{
					int var = (Data[Index / 8]) & (1 << Index % 8);
					return (var != 0) ? 1 : 0;
				}
			}
			set
			{
				if (Index >= Length)
					throw new IndexOutOfRangeException();
				else
				{
					byte dat = Data[Index / 8];
					if (value == 1)
						dat |= (byte)(1 << (Index % 8));
					else
					{
						dat &= (byte)~(1 << (Index % 8));
					}

					Data[Index / 8] = dat;
				}
			}

		}

		public BitDefiner(int length)
		{
			this.Length = length;
			int arrayLenght = (length / 8) + ((length % 8 != 0) ? 1 : 0);
			this.Data = new byte[arrayLenght];
		}

		public BitDefiner(byte[] Data, Endianity end)
		{
			this.Data = new byte[Data.Length];
			Array.Copy(Data, this.Data, Data.Length);
			this.Length = Data.Length * 8;
			if (end == Endianity.BigEndian)
			{
				Array.Reverse(this.Data);
			}
		}

		public override string ToString()
		{
			StringBuilder bld = new StringBuilder();
			for (int i = this.Length - 1; i >= 0; i--)
			{
				bld.Append((((i + 1) % 8) == 0 && i + 1 != 0 && i + 1 != this.Length) ? " " : "");
				bld.Append((this[i] == 1) ? "1" : "0");
			}

			return bld.ToString();
		}

		public string ToString(Endianity indian)
		{
			if (indian == Endianity.BigEndian)
				return this.ToString();
			StringBuilder bld = new StringBuilder();
			for (int i = 0; i < this.Length; i++)
			{
				bld.Append(((i % 8) == 0 && i != 0) ? " " : "");
				bld.Append((this[i] == 1) ? "1" : "0");
			}

			return bld.ToString();
		}

		public static BitDefiner GetCopy(BitDefiner Sourse)
		{
			if (Sourse == null)
				throw new ArgumentNullException();
			BitDefiner newDef = new BitDefiner(Sourse.Length);
			Array.Copy(Sourse.Data, newDef.Data, Sourse.ArrayLenght);
			return newDef;
		}

		public static BitDefiner GetOverflowCopy(BitDefiner Sourse, int NewLenght, int BeginIndex)
		{
			if (Sourse == null)
				throw new ArgumentNullException();
			if (NewLenght < 1 || BeginIndex < 0 || BeginIndex >= Sourse.Length)
				throw new ArgumentException();
			BitDefiner newBit = new BitDefiner(NewLenght);
			for (int i = 0; i < NewLenght && i + BeginIndex < Sourse.Length; i++)
			{
				newBit[i] = Sourse[i + BeginIndex];
			}

			return newBit;
		}

		public static BitDefiner GetExpandedCopy(BitDefiner Sourse, int NewLenght)
		{
			if (Sourse == null)
				throw new ArgumentNullException();
			if (NewLenght < Sourse.Length)
				throw new ArgumentException();
			BitDefiner newBit = new BitDefiner(NewLenght);
			for (int i = 0; i < Sourse.Length; i++)
			{
				newBit[i] = Sourse[i];
			}
			for (int i = Sourse.Length; i < NewLenght; i++)
			{
				newBit[i] = Sourse[Sourse.Length - 1];
			}

			return newBit;
		}

		public static BitDefiner TwosComplement(BitDefiner Input)
		{
			BitDefiner tmp = ~Input;
			BitDefiner One = new BitDefiner(Input.Length);
			One[0] = 1;
			return tmp + One;
		}

		public static BitDefiner RevertTwosComplement(BitDefiner Input)
		{
			BitDefiner One = new BitDefiner(Input.Length);
			One[0] = 1;
			return ~(Input - One);
		}

		public static byte[] GetByteArray(BitDefiner Input)
		{
			byte[] res = new byte[Input.ArrayLenght];
			Array.Copy(Input.Data, res, res.Length);
			//Array.Reverse(res);
			return res;
		}

		public static BitDefiner operator ~(BitDefiner Input)
		{
			BitDefiner newBit = new BitDefiner(Input.Length);
			Array.Copy(Input.Data, newBit.Data, Input.ArrayLenght);
			for (int i = 0; i < newBit.ArrayLenght; i++)
			{
				newBit.Data[i] ^= 0xff;
			}

			return newBit;
		}

		public static BitDefiner operator ^(BitDefiner A, BitDefiner B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();
			if (A.Length != B.Length)
				throw new ArgumentException();

			BitDefiner newBit = new BitDefiner(A.Length);
			for (int i = 0; i < newBit.Data.Length; i++)
			{
				newBit.Data[i] = (byte)(A.Data[i] ^ B.Data[i]);
			}

			return newBit;
		}

		public static BitDefiner operator |(BitDefiner A, BitDefiner B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();
			if (A.Length != B.Length)
				throw new ArgumentException();

			BitDefiner newBit = new BitDefiner(A.Length);
			for (int i = 0; i < newBit.Data.Length; i++)
			{
				newBit.Data[i] = (byte)(A.Data[i] | B.Data[i]);
			}

			return newBit;
		}

		public static BitDefiner operator &(BitDefiner A, BitDefiner B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();
			if (A.Length != B.Length)
				throw new ArgumentException();

			BitDefiner newBit = new BitDefiner(A.Length);
			for (int i = 0; i < newBit.Data.Length; i++)
			{
				newBit.Data[i] = (byte)(A.Data[i] & B.Data[i]);
			}

			return newBit;
		}

		public static BitDefiner operator <<(BitDefiner A, int b)
		{
			if (A == null)
				throw new ArgumentNullException();
			if (b < 0)
				throw new ArgumentException();

			BitDefiner newBit = new BitDefiner(A.Length);
			for (int i = b; i < A.Length; i++)
			{
				newBit[i] = A[i - b];
			}

			return newBit;
		}

		public static BitDefiner operator >>(BitDefiner A, int b)
		{
			if (A == null)
				throw new ArgumentNullException();
			if (b < 0)
				throw new ArgumentException();

			BitDefiner newBit = new BitDefiner(A.Length);
			for (int i = 0; i < newBit.Length - b; i++)
			{
				newBit[i] = A[i + b];
			}

			return newBit;
		}

		public static BitDefiner operator +(BitDefiner A, BitDefiner B)
		{
			if (A == null || B == null)
				throw new ArgumentNullException();
			if (A.Length != B.Length)
				throw new ArgumentException();

			BitDefiner newBit = new BitDefiner(A.Length);
			byte Carry = 0;
			for (int i = 0; i < A.ArrayLenght; i++)
			{
				int Sum = (A.Data[i] + B.Data[i] + Carry);
				newBit.Data[i] = (byte)(Sum % 0x100);
				if (Sum >= 0x100)
					Carry = 1;
				else
					Carry = 0;
			}


			return newBit;
		}

		public static BitDefiner operator -(BitDefiner A, BitDefiner B)
		{
			BitDefiner b = BitDefiner.TwosComplement(B);
			return A + b;
		}

		public static bool DefinesEqual(BitDefiner A, BitDefiner B)
		{
			if (A.Length != B.Length)
				return false;
			for (int i = 0; i < A.Length; i++)
			{
				if (A[i] != B[i])
					return false;
			}
			return true;
		}
	}

	public enum Endianity
	{
		LittleEndian,
		BigEndian
	}
}
