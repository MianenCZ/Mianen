using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mianen.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.DataStructures.Tests
{
	[TestClass()]
	public class BitDefinerTests
	{
		[TestMethod()]
		public void BitDefinerConstruct()
		{
			BitDefiner def = new BitDefiner(32);
			Assert.AreEqual(4, def.ArrayLenght);
			Assert.AreEqual(4, def.ArrayLenght);
			for (int i = 0; i < def.Length; i++)
			{
				Assert.AreEqual(new Bit() { Value = 0 }, def[i]);
			}
		}

		[TestMethod()]
		public void ToStringTest01()
		{
			BitDefiner def = new BitDefiner(32);
			for (int i = 0; i < def.Length; i += 2)
			{
				def[i] = 1;
			}

			Assert.AreEqual("01010101 01010101 01010101 01010101", def.ToString());
		}

		[TestMethod()]
		public void ToStringTest02()
		{
			BitDefiner def = new BitDefiner(31);
			for (int i = 0; i < def.Length; i += 2)
			{
				def[i] = 1;
			}

			Assert.AreEqual("1010101 01010101 01010101 01010101", def.ToString());
		}

		[TestMethod()]
		public void ToStringTestLittle01()
		{
			BitDefiner def = new BitDefiner(32);
			for (int i = 0; i < def.Length; i += 2)
			{
				def[i] = 1;
			}

			Assert.AreEqual("10101010 10101010 10101010 10101010", def.ToString(Endianity.LittleEndian));
		}

		[TestMethod()]
		public void ToStringTestLittle02()
		{
			BitDefiner def = new BitDefiner(31);
			for (int i = 0; i < def.Length; i += 2)
			{
				def[i] = 1;
			}

			Assert.AreEqual("10101010 10101010 10101010 1010101", def.ToString(Endianity.LittleEndian));
		}

		[TestMethod()]
		public void ToStringTestBig01()
		{
			BitDefiner def = new BitDefiner(32);
			for (int i = 0; i < def.Length; i += 2)
			{
				def[i] = 1;
			}

			Assert.AreEqual("01010101 01010101 01010101 01010101", def.ToString(Endianity.BigEndian));
		}

		[TestMethod()]
		public void ToStringTestBig02()
		{
			BitDefiner def = new BitDefiner(31);
			for (int i = 0; i < def.Length; i += 2)
			{
				def[i] = 1;
			}

			Assert.AreEqual("1010101 01010101 01010101 01010101", def.ToString(Endianity.BigEndian));
		}

		/// <summary>
		/// Zero -> Zero
		/// </summary>
		[TestMethod()]
		public void TwosComplementTest01()
		{
			BitDefiner def = new BitDefiner(4);
			BitDefiner twos = BitDefiner.TwosComplement(def);
			Assert.IsTrue(DefineSame(def, twos));
		}

		[TestMethod()]
		public void TwosComplementTest02()
		{
			BitDefiner def = new BitDefiner(8);
			def[1] = 1;
			BitDefiner twos = BitDefiner.TwosComplement(def);
			BitDefiner res = new BitDefiner(8);
			for (int i = 0; i < 8; i++)
			{
				if (i != 0)
					res[i] = 1;
			}

			Assert.IsTrue(DefineSame(res, twos));
		}

		[TestMethod()]
		public void TwosComplementReverseTest01()
		{
			BitDefiner def = new BitDefiner(32);
			for (int i = 0; i < def.Length; i += 2)
			{
				def[i] = 1;
			}
			BitDefiner tmp = BitDefiner.TwosComplement(def);
			BitDefiner tmp2 = BitDefiner.RevertTwosComplement(tmp);
			Assert.IsTrue(DefineSame(def, tmp2));
		}

		[TestMethod()]
		public void Negation01()
		{
			BitDefiner def = new BitDefiner(32);
			BitDefiner neg = ~def;
			for (int i = 0; i < def.Length; i++)
			{
				Assert.AreEqual(new Bit() { Value = 0 }, def[i]);
				Assert.AreEqual(new Bit() { Value = 1 }, neg[i]);
			}
		}

		/// <summary>
		/// 0+0 = 0;
		/// </summary>
		[TestMethod()]
		public void AddTest00()
		{
			BitDefiner nul = new BitDefiner(32);
			BitDefiner res = nul + nul;
			Assert.IsTrue(DefineSame(res, nul));
		}

		/// <summary>
		/// Carry
		/// </summary>
		[TestMethod()]
		public void AddTest01()
		{
			BitDefiner nul = new BitDefiner(32);
			BitDefiner def = new BitDefiner(32);
			for (int i = 0; i < def.Length; i += 2)
			{
				def[i] = 1;
			}

			BitDefiner a = def + nul;
			for (int i = 0; i < def.Length; i += 2)
			{
				Assert.IsTrue(DefineSame(a, def));
			}
		}

		/// <summary>
		/// No carry
		/// </summary>
		[TestMethod()]
		public void AddTest02()
		{
			BitDefiner def = new BitDefiner(4);
			def[1] = 1;
			def[3] = 1;
			BitDefiner res = new BitDefiner(4);
			res[2] = 1;

			BitDefiner a = def + def;
			for (int i = 0; i < def.Length; i += 2)
			{
				Assert.IsTrue(DefineSame(res, a));
			}
		}

		/// <summary>
		/// FullCarry
		/// </summary>
		[TestMethod()]
		public void AddTest03()
		{
			BitDefiner def = new BitDefiner(32);
			BitDefiner neg = ~def;
			BitDefiner one = new BitDefiner(32);
			one[0] = 1;
			BitDefiner res = neg + one;
			Assert.IsTrue(DefineSame(def, res));

		}

		[TestMethod()]
		public void SubtractTest00()
		{
			BitDefiner nul = new BitDefiner(32);
			BitDefiner res = nul - nul;
			Assert.IsTrue(DefineSame(res, nul));
		}

		[TestMethod()]
		public void GetExpandedTest01()
		{
			BitDefiner nul = new BitDefiner(32);
			BitDefiner res = new BitDefiner(64);
			Assert.IsTrue(DefineSame(res, BitDefiner.GetExpandedCopy(nul, 64)));
		}

		[TestMethod()]
		public void GetExpandedTest02()
		{
			BitDefiner nul = new BitDefiner(4);
			nul.Data[0] = 0xf;
			BitDefiner res = new BitDefiner(8);
			res.Data[0] = 0xff;
			Assert.IsTrue(DefineSame(res, BitDefiner.GetExpandedCopy(nul, 8)));
		}

		[TestMethod()]
		public void GetExpandedTest03()
		{
			BitDefiner nul = new BitDefiner(4);
			nul.Data[0] = 0xf;
			BitDefiner res = new BitDefiner(9);
			res.Data[0] = 0xff;
			res.Data[1] = 0x1;
			Assert.IsTrue(DefineSame(res, BitDefiner.GetExpandedCopy(nul, 9)));
		}

		[TestMethod()]
		public void GetBytes01()
		{
			byte[] a = { 1, 2, 3, 4 };

			BitDefiner bt = new BitDefiner(a, Endianity.LittleEndian);

			byte[] b = BitDefiner.GetByteArray(bt);

			for (int i = 0; i < a.Length; i++)
			{
				Assert.IsTrue(a[i] == b[i]);
			}


		}

		[TestMethod()]
		public void GetBytes02()
		{
			byte[] a = { 4, 3, 2, 1 };
			byte[] a_ = { 1, 2, 3, 4 };
			BitDefiner bt = new BitDefiner(a, Endianity.BigEndian);

			byte[] b = BitDefiner.GetByteArray(bt);

			for (int i = 0; i < a.Length; i++)
			{
				Assert.IsTrue(a_[i] == b[i]);
			}


		}


		public static bool DefineSame(BitDefiner A, BitDefiner B)
		{
			if (A == null && B == null)
				return true;
			if ((A == null && B != null) || (A != null && B == null))
				return false;

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
}
