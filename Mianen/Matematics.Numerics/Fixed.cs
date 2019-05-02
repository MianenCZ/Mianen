using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.DataStructures;


namespace Mianen.Matematics.Numerics
{
	public class Fixed<T> : IComparable<T>, IEquatable<T>, IConvertible where T : FixedPointDefiner, new()
	{
		public static int DecimalLength;
		public static int FullLength;
		public static int NumberLength;

		internal BitDefiner Data;

		static Fixed()
		{
			T Q = new T();
			DecimalLength = Q.DecimalPart;
			FullLength = Q.FullPart;
			NumberLength = DecimalLength + FullLength;
		}

		public Fixed()
		{
			if (NumberLength % 8 != 0)
				throw new ArgumentException();
			this.Data = new BitDefiner(NumberLength);
		}

		public Fixed(int Value)
		{
			T Q = new T();
			if (NumberLength % 8 != 0)
				throw new ArgumentException();
			this.Data = new BitDefiner(NumberLength);
			byte[] Val = BitConverter.GetBytes(Math.Abs(Value));
			BitDefiner dat = new BitDefiner(Val, Endianity.LittleEndian);
			dat = dat << DecimalLength;
			if (Value < 0)
				dat = BitDefiner.TwosComplement(dat);
			this.Data = dat;
		}

		internal Fixed(BitDefiner Data)
		{
			this.Data = Data;
		}

		/// <summary>
		/// Add Fixed number to SourseValue
		/// </summary>
		/// <typeparam name="Q">Type of second Argument</typeparam>
		/// <param name="Value">Sourse Value</param>
		/// <exception cref="ArgumentException">If presision of types T and Q are not equal</exception>
		/// <exception cref="ArgumentNullException">Value is null</exception>
		/// <returns>New Fixed Value in type of Sourse Fixed instance</returns>
		public Fixed<T> Add<Q>(Fixed<Q> Value) where Q : FixedPointDefiner, new()
		{
			if (object.Equals(Value, null))
				throw new ArgumentNullException();
			T t = new T();
			Q q = new Q();
			if (t.FullPart != q.FullPart || t.DecimalPart != q.DecimalPart)
				throw new ArgumentException();

			Fixed<T> ret = new Fixed<T>();

			ret.Data = this.Data + Value.Data;

			return ret;

			//return new Fixed<T>(BitConverter.GetBytes(a+b));
		}

		/// <summary>
		/// Subtract Fixed number to SourseValue
		/// </summary>
		/// <typeparam name="Q">Type of second Argument</typeparam>
		/// <param name="Value">Sourse Value</param>
		/// <exception cref="ArgumentException">If presision of types T and Q are not equal</exception>
		/// <exception cref="ArgumentNullException">Value is null</exception>
		/// <returns>New Fixed Value in type of Sourse Fixed instance</returns>
		public Fixed<T> Subtract<Q>(Fixed<Q> Value) where Q : FixedPointDefiner, new()
		{
			if (object.Equals(Value, null))
				throw new ArgumentNullException();
			T t = new T();
			Q q = new Q();
			if (t.FullPart != q.FullPart || t.DecimalPart != q.DecimalPart)
				throw new ArgumentException();

			Fixed<T> ret = new Fixed<T>();
			ret.Data = this.Data - Value.Data;
			return ret;
		}

		/// <summary>
		/// Multiply Fixed number by SourseValue
		/// </summary>
		/// <typeparam name="Q">Type of second Argument</typeparam>
		/// <param name="Value">Sourse Value</param>
		/// <exception cref="ArgumentException">If presision of types T and Q are not equal</exception>
		/// <exception cref="ArgumentNullException">Value is null</exception>
		/// <returns>New Fixed Value in type of Sourse Fixed instance</returns>
		public Fixed<T> Multiply<Q>(Fixed<Q> Value) where Q : FixedPointDefiner, new()
		{
			if (object.Equals(Value, null))
				throw new ArgumentNullException();
			if (Fixed<T>.FullLength != Fixed<Q>.FullLength || Fixed<T>.DecimalLength != Fixed<Q>.DecimalLength)
				throw new ArgumentException();

			BitDefiner a = BitDefiner.GetExpandedCopy(this.Data, this.Data.Length + Value.Data.Length);
			BitDefiner b = BitDefiner.GetCopy(Value.Data);
			BitDefiner res = new BitDefiner(this.Data.Length + Value.Data.Length);

			for (int i = 0; i < b.Length; i++)
			{
				if (b[i] == 1)
				{
					res += (a << i);
				}
			}

			BitDefiner Result = BitDefiner.GetOverflowCopy(res, Fixed<Q>.NumberLength, Fixed<Q>.DecimalLength);
			Fixed<T> ResFix = new Fixed<T>(Result);

			return ResFix;
		}

		/// <summary>
		///Divide Fixed number by SourseValue
		/// </summary>
		/// <typeparam name="Q">Type of second Argument</typeparam>
		/// <param name="Value">Sourse Value</param>
		/// <exception cref="ArgumentException">If presision of types T and Q are not equal</exception>
		/// <exception cref="ArgumentNullException">Value is null</exception>
		/// <exception cref="DivideByZeroException">Value reprezents zero</exception>
		/// <returns>New Fixed Value in type of Sourse Fixed instance</returns>
		public Fixed<T> Divide<Q>(Fixed<Q> Value) where Q : FixedPointDefiner, new()
		{
			if (object.Equals(Value, null))
				throw new ArgumentNullException();
			if (Fixed<T>.FullLength != Fixed<Q>.FullLength || Fixed<T>.DecimalLength != Fixed<Q>.DecimalLength)
				throw new ArgumentException();
			if (Value.Data == new BitDefiner(Value.Data.Length))
				throw new DivideByZeroException();

			//Předopoklad - Podle zadání
			//Není odolné vůči rozšíření zadání na jiný počet celkových bitů

			byte[] arr_a = BitDefiner.GetByteArray(this.Data);
			byte[] arr_b = BitDefiner.GetByteArray(Value.Data);
			Int32 a = BitConverter.ToInt32(arr_a, 0);
			Int32 b = BitConverter.ToInt32(arr_b, 0);

			Int64 val1 = ((Int64)a << DecimalLength);

			Int64 val2 = (Int64)b;
			Int64 val3 = val1 / val2;

			Int32 res = (Int32)(val3);
			Fixed<T> newFix = new Fixed<T>(new BitDefiner(BitConverter.GetBytes(res), Endianity.LittleEndian));

			return newFix;
		}

		public double ToDouble()
		{
			double Val = 0;
			int Signum = 1;

			BitDefiner vals = null;
			if (this.Data[NumberLength - 1] == 1)
			{
				vals = BitDefiner.RevertTwosComplement(this.Data);
				Signum = -1;
			}
			else
			{
				vals = BitDefiner.GetCopy(this.Data);
			}

			int Exponent = -DecimalLength;
			for (int i = 0; i < vals.Length; i++)
			{
				Val += Math.Pow(2, Exponent) * vals[i];
				Exponent++;
			}

			return Val * Signum;
		}

		public static implicit operator Fixed<T>(int Value)
		{
			return new Fixed<T>(Value);
		}

		public static explicit operator Fixed<T>(Fixed<Q16_16> Value)
		{
			return MovePoint(Value);
		}

		public static explicit operator Fixed<T>(Fixed<Q8_24> Value)
		{
			return MovePoint(Value);
		}

		public static explicit operator Fixed<T>(Fixed<Q24_8> Value)
		{
			return MovePoint(Value);
		}

		public static Fixed<T> MovePoint<T_>(Fixed<T_> Value) where T_ : FixedPointDefiner, new()
		{
			Fixed<T> newVal = new Fixed<T>();
			int fullTarg = Math.Min(Fixed<T>.FullLength, Fixed<T_>.FullLength);
			//FullPartCopy
			for (int i = 0; i < fullTarg; i++)
			{
				newVal.Data[Fixed<T>.DecimalLength + i] = Value.Data[Fixed<T_>.DecimalLength + i];
			}
			//Signum distribution
			for (int i = Fixed<T>.DecimalLength + fullTarg; i < Fixed<T>.NumberLength; i++)
			{
				newVal.Data[i] = newVal.Data[Fixed<T>.DecimalLength + fullTarg - 1];
			}
			//Decimal distribution
			int dectarg = Math.Min(Fixed<T>.DecimalLength, Fixed<T_>.DecimalLength);
			for (int i = 0; i < dectarg; i++)
			{
				newVal.Data[Fixed<T>.DecimalLength - i - 1] = Value.Data[Fixed<T_>.DecimalLength - i - 1];
			}


			return newVal;

		}

		public static Fixed<T> operator +(Fixed<T> A, Fixed<T> B)
		{
			return A.Add(B);
		}

		public static Fixed<T> operator -(Fixed<T> A, Fixed<T> B)
		{
			return A.Subtract(B);
		}

		public static Fixed<T> operator *(Fixed<T> A, Fixed<T> B)
		{
			return A.Multiply(B);
		}

		public static Fixed<T> operator /(Fixed<T> A, Fixed<T> B)
		{
			return A.Divide(B);
		}

		public static bool operator ==(Fixed<T> A, Fixed<T> B) => A.ToDouble() == B.ToDouble();

		public static bool operator !=(Fixed<T> A, Fixed<T> B) => A.ToDouble() != B.ToDouble();

		public static bool operator >(Fixed<T> A, Fixed<T> B) => A.ToDouble() > B.ToDouble();

		public static bool operator >=(Fixed<T> A, Fixed<T> B) => A.ToDouble() >= B.ToDouble();

		public static bool operator <(Fixed<T> A, Fixed<T> B) => A.ToDouble() < B.ToDouble();

		public static bool operator <=(Fixed<T> A, Fixed<T> B) => A.ToDouble() <= B.ToDouble();

		public override string ToString()
		{
			return this.ToDouble().ToString();
		}

		public string ToString(IFormatProvider Format)
		{
			return this.ToDouble().ToString(Format);
		}

		public string ToString(string Pattern)
		{
			return this.ToDouble().ToString(Pattern);
		}

		public string ToString(string Pattern, IFormatProvider Format)
		{
			return this.ToDouble().ToString(Pattern, Format);
		}

		public override bool Equals(object obj)
		{
			try
			{
				var o = (obj as Fixed<T>);
				return this.Data.Equals(o.Data);

			}
			catch
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return this.ToDouble().GetHashCode();
		}

		public int CompareTo(T other)
		{
			throw new NotImplementedException();
		}

		public bool Equals(T other)
		{
			throw new NotImplementedException();
		}

		public TypeCode GetTypeCode()
		{
			throw new NotImplementedException();
		}

		public bool ToBoolean(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public char ToChar(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public sbyte ToSByte(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public byte ToByte(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public short ToInt16(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public ushort ToUInt16(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public int ToInt32(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public uint ToUInt32(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public long ToInt64(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public ulong ToUInt64(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public float ToSingle(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public double ToDouble(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public decimal ToDecimal(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public DateTime ToDateTime(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public object ToType(Type conversionType, IFormatProvider provider)
		{
			throw new NotImplementedException();
		}
	}
}
