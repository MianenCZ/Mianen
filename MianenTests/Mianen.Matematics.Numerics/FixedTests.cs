using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Mianen.Matematics.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.DataStructures;

namespace Mianen.Matematics.Numerics.Test
{
	[TestClass()]
	public class FixedTests
	{
		[TestMethod()]
		public void FixedPointConstruct01()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>();
			Assert.AreEqual(24, Fixed<Q24_8>.FullLength);
			Assert.AreEqual(8, Fixed<Q24_8>.DecimalLength);
			Assert.AreEqual(32, Fixed<Q24_8>.NumberLength);
		}

		[TestMethod()]
		public void FixedPointConstruct02()
		{
			Fixed<Q8_24> a = new Fixed<Q8_24>();
			Assert.AreEqual(8, Fixed<Q8_24>.FullLength);
			Assert.AreEqual(24, Fixed<Q8_24>.DecimalLength);
			Assert.AreEqual(32, Fixed<Q8_24>.NumberLength);
		}

		[TestMethod()]
		public void FixedPointConstruct03()
		{
			Fixed<Q16_16> a = new Fixed<Q16_16>();
			Assert.AreEqual(16, Fixed<Q16_16>.FullLength);
			Assert.AreEqual(16, Fixed<Q16_16>.DecimalLength);
			Assert.AreEqual(32, Fixed<Q16_16>.NumberLength);
		}

		[TestMethod()]
		public void FixedPointConstructInt01()
		{
			Fixed<Q8_24> a = new Fixed<Q8_24>(42);
			Assert.AreEqual(8, Fixed<Q8_24>.FullLength);
			Assert.AreEqual(24, Fixed<Q8_24>.DecimalLength);
			Assert.AreEqual(32, Fixed<Q8_24>.NumberLength);
			BitDefiner q = new BitDefiner(new byte[] { 0, 0, 0, 42 }, Endianity.LittleEndian);
			Assert.IsTrue(ByteArrayComp(a.Data, q));
		}

		[TestMethod()]
		public void FixedPointConstructInt02()
		{
			Fixed<Q16_16> a = new Fixed<Q16_16>(298);
			Assert.AreEqual(16, Fixed<Q16_16>.FullLength);
			Assert.AreEqual(16, Fixed<Q16_16>.DecimalLength);
			Assert.AreEqual(32, Fixed<Q16_16>.NumberLength);
			BitDefiner q = new BitDefiner(new byte[] { 0, 0, 42, 1 }, Endianity.LittleEndian);
			Assert.IsTrue(ByteArrayComp(a.Data, q));
		}

		[TestMethod()]
		public void FixedPointConstructIntOverflow01()
		{
			Fixed<Q8_24> a = new Fixed<Q8_24>(298);
			Assert.AreEqual(8, Fixed<Q8_24>.FullLength);
			Assert.AreEqual(24, Fixed<Q8_24>.DecimalLength);
			Assert.AreEqual(32, Fixed<Q8_24>.NumberLength);
			BitDefiner q = new BitDefiner(new byte[] { 0, 0, 0, 42 }, Endianity.LittleEndian);
			Assert.IsTrue(ByteArrayComp(a.Data, q));
		}

		[TestMethod()]
		public void FixedPointConstructInt03()
		{
			Fixed<Q8_24> a = new Fixed<Q8_24>(-4);
			Assert.AreEqual(8, Fixed<Q8_24>.FullLength);
			Assert.AreEqual(24, Fixed<Q8_24>.DecimalLength);
			Assert.AreEqual(32, Fixed<Q8_24>.NumberLength);
			BitDefiner q = new BitDefiner(new byte[] { 0, 0, 0, 252 }, Endianity.LittleEndian);

			Assert.IsTrue(ByteArrayComp(a.Data, q));
		}

		[TestMethod()]
		public void FixedPointConstructDouble01()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(0);
			Assert.AreEqual(24, Fixed<Q24_8>.FullLength);
			Assert.AreEqual(8, Fixed<Q24_8>.DecimalLength);
			Assert.AreEqual(32, Fixed<Q24_8>.NumberLength);
			a.Data[7] = 1;
			BitDefiner q = new BitDefiner(new byte[] { 128, 0, 0, 0 }, Endianity.LittleEndian);

			Assert.IsTrue(ByteArrayComp(a.Data, q));
			Assert.AreEqual(0.5, a.ToDouble());
		}

		[TestMethod()]
		public void FixedPointConstructCrash()
		{
			Assert.ThrowsException<ArgumentException>(() => { new Fixed<Q1_1>(); });
		}

		[TestMethod()]
		public void FixedPointConstuctImplicit00()
		{
			Fixed<Q16_16> Val = 0;
			Assert.AreEqual(0, Val.ToDouble());
		}

		[TestMethod()]
		public void FixedPointConstuctImplicit01()
		{
			Fixed<Q16_16> Val = 3;
			Assert.AreEqual(3, Val.ToDouble());
		}

		[TestMethod()]
		public void FixedPointConstuctImplicit02()
		{
			Fixed<Q16_16> Val = -3;
			Assert.AreEqual(-3, Val.ToDouble());
		}

		[TestMethod()]
		public void FixedPointConstuctImplicit03()
		{
			Fixed<Q8_24> Val = 256;
			Assert.AreEqual(0, Val.ToDouble());
		}

		[TestMethod()]
		public void Task_Q24_8()
		{
			var f1 = new Fixed<Q24_8>(3);
			Assert.AreEqual(3, f1.ToDouble());

			var f2 = new Fixed<Q24_8>(2);
			Assert.AreEqual(2, f2.ToDouble());
			var f3 = f1.Add(f2);
			Assert.AreEqual(5, f3.ToDouble());

			f3 = f1.Multiply(f2);
			Assert.AreEqual(6, f3.ToDouble());

			f1 = new Fixed<Q24_8>(19);
			Assert.AreEqual(19, f1.ToDouble());
			f2 = new Fixed<Q24_8>(13);
			Assert.AreEqual(13, f2.ToDouble());
			f3 = f1.Multiply(f2);
			Assert.AreEqual(247, f3.ToDouble());

			f1 = new Fixed<Q24_8>(3);
			f2 = new Fixed<Q24_8>(2);
			f3 = f1.Divide(f2);
			Assert.AreEqual(1.5, f3.ToDouble());

			f1 = new Fixed<Q24_8>(248);
			Assert.AreEqual(248, f1.ToDouble());
			f2 = new Fixed<Q24_8>(10);
			Assert.AreEqual(10, f2.ToDouble());
			f3 = f1.Divide(f2);
			Assert.AreEqual(24.796875, f3.ToDouble());

			f1 = new Fixed<Q24_8>(625);
			Assert.AreEqual(625, f1.ToDouble());
			f2 = new Fixed<Q24_8>(1000);
			Assert.AreEqual(1000, f2.ToDouble());
			f3 = f1.Divide(f2);
			Assert.AreEqual(0.625, f3.ToDouble());
		}

		[TestMethod()]
		public void Task_Q16_16()
		{
			var f1 = new Fixed<Q16_16>(3);
			Assert.AreEqual("3", f1.ToString());

			var f2 = new Fixed<Q16_16>(2);
			Assert.AreEqual("2", f2.ToString());
			var f3 = f1.Add(f2);
			Assert.AreEqual("5", f3.ToString());

			f3 = f1.Multiply(f2);
			Assert.AreEqual("6", f3.ToString());

			f1 = new Fixed<Q16_16>(19);
			Assert.AreEqual("19", f1.ToString());
			f2 = new Fixed<Q16_16>(13);
			Assert.AreEqual("13", f2.ToString());
			f3 = f1.Multiply(f2);
			Assert.AreEqual("247", f3.ToString());

			f1 = new Fixed<Q16_16>(248);
			Assert.AreEqual("248", f1.ToString());
			f2 = new Fixed<Q16_16>(10);
			Assert.AreEqual("10", f2.ToString());
			f3 = f1.Divide(f2);
			Assert.AreEqual("24.7999877929688", f3.ToString());
			//24.7999877929688
			//24.7999877929688
			f1 = new Fixed<Q16_16>(625);
			Assert.AreEqual("625", f1.ToString());
			f2 = new Fixed<Q16_16>(1000);
			Assert.AreEqual("1000", f2.ToString());
			f3 = f1.Divide(f2);
			Assert.AreEqual("0.625", f3.ToString());

		}

		[TestMethod()]
		public void Task_Q8_24()
		{
			var f1 = new Fixed<Q8_24>(3);
			Assert.AreEqual("3", f1.ToString());
			var f2 = new Fixed<Q8_24>(2);
			Assert.AreEqual("2", f2.ToString());
			var f3 = f1.Add(f2);
			Assert.AreEqual("5", f3.ToString());

			f3 = f1.Multiply(f2);
			Assert.AreEqual("6", f3.ToString());

			f1 = new Fixed<Q8_24>(19);
			Assert.AreEqual("19", f1.ToString());
			f2 = new Fixed<Q8_24>(13);
			Assert.AreEqual("13", f2.ToString());
			f3 = f1.Multiply(f2);
			Assert.AreEqual("-9", f3.ToString());

			f1 = new Fixed<Q8_24>(248);
			Assert.AreEqual("-8", f1.ToString());
			f2 = new Fixed<Q8_24>(10);
			Assert.AreEqual("10", f2.ToString());
			f3 = f1.Divide(f2);
			Assert.AreEqual("-0.799999952316284", f3.ToString());

			f1 = new Fixed<Q8_24>(625);
			Assert.AreEqual("113", f1.ToString());
			f2 = new Fixed<Q8_24>(1000);
			Assert.AreEqual("-24", f2.ToString());
			f3 = f1.Divide(f2);
			Assert.AreEqual("-4.70833331346512", f3.ToString());

		}



		[TestMethod()]
		public void AddTest01()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(4);
			Fixed<Q24_8> b = new Fixed<Q24_8>(4);
			Fixed<Q24_8> res = a.Add(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(8);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void AddTest02()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(-4);
			Fixed<Q24_8> b = new Fixed<Q24_8>(4);
			Fixed<Q24_8> res = a.Add(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(0);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void AddTest03()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(-4);
			Fixed<Q24_8> b = new Fixed<Q24_8>(-4);
			Fixed<Q24_8> res = a.Add(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(-8);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void AddTest04()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(-100);
			Fixed<Q24_8> b = new Fixed<Q24_8>(200);
			Fixed<Q24_8> res = a.Add(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(100);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void AddOperatorTest01()
		{
			Fixed<Q24_8> a = 4;
			Fixed<Q24_8> b = 4;
			Fixed<Q24_8> res = a + b;
			Fixed<Q24_8> c = 8;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
			//*/
		}

		[TestMethod()]
		public void AddOperatorTest02()
		{
			Fixed<Q24_8> a = -4;
			Fixed<Q24_8> b = 4;
			Fixed<Q24_8> res = a + b;
			Fixed<Q24_8> c = 0;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void AddOperatorTest03()
		{
			Fixed<Q24_8> a = -4;
			Fixed<Q24_8> b = -4;
			Fixed<Q24_8> res = a + b;
			Fixed<Q24_8> c = -8;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void AddOperatorTest04()
		{
			Fixed<Q24_8> a = -100;
			Fixed<Q24_8> b = 200;
			Fixed<Q24_8> res = a + b;
			Fixed<Q24_8> c = 100;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void AddOperatorTest05()
		{
			Fixed<Q24_8> a = -100;
			Fixed<Q24_8> res = a + 200;
			Fixed<Q24_8> c = 100;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void AddOperatorTest06()
		{
			Fixed<Q24_8> a = -100;
			Fixed<Q24_8> res = 200 + a;
			Fixed<Q24_8> c = 100;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractTest00()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(0);
			Fixed<Q24_8> b = new Fixed<Q24_8>(0);
			Fixed<Q24_8> res = a.Subtract(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(0);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractTest01()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(4);
			Fixed<Q24_8> b = new Fixed<Q24_8>(4);
			Fixed<Q24_8> res = a.Subtract(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(0);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractTest02()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(-4);
			Fixed<Q24_8> b = new Fixed<Q24_8>(-4);
			Fixed<Q24_8> res = a.Subtract(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(0);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractTest03()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(5);
			Fixed<Q24_8> b = new Fixed<Q24_8>(4);
			Fixed<Q24_8> res = a.Subtract(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(1);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractTest04()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(-4);
			Fixed<Q24_8> b = new Fixed<Q24_8>(0);
			Fixed<Q24_8> res = a.Subtract(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(-4);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractOperatorTest00()
		{
			Fixed<Q24_8> a = 0;
			Fixed<Q24_8> b = 0;
			Fixed<Q24_8> res = a - b;
			Fixed<Q24_8> c = 0;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractOperatorTest01()
		{
			Fixed<Q24_8> a = 4;
			Fixed<Q24_8> b = 4;
			Fixed<Q24_8> res = a - b;
			Fixed<Q24_8> c = 0;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractOperatorTest02()
		{
			Fixed<Q24_8> a = -4;
			Fixed<Q24_8> b = -4;
			Fixed<Q24_8> res = a - b;
			Fixed<Q24_8> c = 0;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractOperatorTest03()
		{
			Fixed<Q24_8> a = 5;
			Fixed<Q24_8> b = 4;
			Fixed<Q24_8> res = a - b;
			Fixed<Q24_8> c = 1;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractOperatorTest04()
		{
			Fixed<Q24_8> a = -4;
			Fixed<Q24_8> b = 0;
			Fixed<Q24_8> res = a - b;
			Fixed<Q24_8> c = -4;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractOperatorTest05()
		{
			Fixed<Q24_8> a = -4;
			Fixed<Q24_8> res = a - 15;
			Fixed<Q24_8> c = -19;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void SubtractOperatorTest06()
		{
			Fixed<Q24_8> a = -4;
			Fixed<Q24_8> res = 10 - a;
			Fixed<Q24_8> c = 14;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void DivideTest01()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(0);
			Fixed<Q24_8> b = new Fixed<Q24_8>(0);
			Assert.ThrowsException<DivideByZeroException>(() =>
			{
				a.Divide(b);
			});
		}

		[TestMethod()]
		public void DivideTest02()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(21);
			Fixed<Q24_8> b = new Fixed<Q24_8>(3);
			Fixed<Q24_8> c = new Fixed<Q24_8>(7);
			Fixed<Q24_8> d = a.Divide(b);

			Assert.IsTrue(ByteArrayComp(d.Data, c.Data));
		}

		[TestMethod()]
		public void DivideOperatorTest01()
		{
			Fixed<Q24_8> a = 0;
			Fixed<Q24_8> b = 0;
			Assert.ThrowsException<DivideByZeroException>(() =>
			{
				var c = a / b;
			});
		}

		[TestMethod()]
		public void DivideOperatorTest02()
		{
			Fixed<Q24_8> a = 21;
			Fixed<Q24_8> b = 3;
			Fixed<Q24_8> c = 7;
			Fixed<Q24_8> d = a / b;

			Assert.IsTrue(ByteArrayComp(d.Data, c.Data));
		}

		[TestMethod()]
		public void DivideOperatorTest03()
		{
			Fixed<Q24_8> a = 10;
			Fixed<Q24_8> b = 20;
			Fixed<Q24_8> c = a / b;
			Assert.AreEqual(0.5d, c.ToDouble());
		}

		[TestMethod()]
		public void DivideOperatorTest04()
		{
			Fixed<Q24_8> a = 10;
			Fixed<Q24_8> b = -20;
			Fixed<Q24_8> c = a / b;
			Assert.AreEqual(-0.5d, c.ToDouble());
		}

		[TestMethod()]
		public void DivideOperatorTest05()
		{
			Fixed<Q24_8> a = 10;
			Fixed<Q24_8> c = a / 20;
			Assert.AreEqual(0.5d, c.ToDouble());
		}

		[TestMethod()]
		public void DivideOperatorTest06()
		{
			Fixed<Q24_8> a = 8;
			Fixed<Q24_8> c = 1 / a;
			Assert.AreEqual(0.125d, c.ToDouble());
		}

		[TestMethod()]
		public void MytliplyTest00()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(0);
			Fixed<Q24_8> b = new Fixed<Q24_8>(0);
			Fixed<Q24_8> res = a.Multiply(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(0);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void MytliplyTest01()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(4);
			Fixed<Q24_8> b = new Fixed<Q24_8>(4);
			Fixed<Q24_8> res = a.Multiply(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(16);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void MytliplyTest02()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(128);
			Fixed<Q24_8> b = new Fixed<Q24_8>(1068);
			Fixed<Q24_8> res = a.Multiply(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(136704);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void MytliplyTest03()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(-1);
			Fixed<Q24_8> b = new Fixed<Q24_8>(1);
			Fixed<Q24_8> res = a.Multiply(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(-1);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void MytliplyTest04()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(-100);
			Fixed<Q24_8> b = new Fixed<Q24_8>(-100);
			Fixed<Q24_8> res = a.Multiply(b);
			Fixed<Q24_8> c = new Fixed<Q24_8>(10000);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void MytliplyTest05()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(0);
			Fixed<Q24_8> b = new Fixed<Q24_8>(0);
			a.Data[7] = 1; // a = 0,5
			b.Data[7] = 1; // n = 0,5
			Assert.AreEqual(0.5, a.ToDouble());
			Assert.AreEqual(0.5, b.ToDouble());
			Fixed<Q24_8> res = a.Multiply(b);
			Assert.AreEqual(0.25, res.ToDouble());
			BitDefiner c = new BitDefiner(new byte[] { 64, 0, 0, 0 }, Endianity.LittleEndian);
			Assert.IsTrue(ByteArrayComp(res.Data, c));
		}

		public void MytliplyOperatorTest00()
		{
			Fixed<Q24_8> a = 0;
			Fixed<Q24_8> b = 0;
			Fixed<Q24_8> res = a * b;
			Fixed<Q24_8> c = 0;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void MytliplyOperatorTest01()
		{
			Fixed<Q24_8> a = 4;
			Fixed<Q24_8> b = 4;
			Fixed<Q24_8> res = a * b;
			Fixed<Q24_8> c = 16;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void MytliplyOperatorTest02()
		{
			Fixed<Q24_8> a = 128;
			Fixed<Q24_8> b = 1068;
			Fixed<Q24_8> res = a * b;
			Fixed<Q24_8> c = new Fixed<Q24_8>(136704);
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void MytliplyOperatorTest03()
		{
			Fixed<Q24_8> a = -1;
			Fixed<Q24_8> b = 1;
			Fixed<Q24_8> res = a * b;
			Fixed<Q24_8> c = -1;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void MytliplyOperatorTest04()
		{
			Fixed<Q24_8> a = -100;
			Fixed<Q24_8> b = -100;
			Fixed<Q24_8> res = a * b;
			Fixed<Q24_8> c = 10000;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void MytliplyOperatorTest05()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(0);
			Fixed<Q24_8> b = new Fixed<Q24_8>(0);
			a.Data[7] = 1; // a = 0,5
			b.Data[7] = 1; // n = 0,5
			Assert.AreEqual(0.5, a.ToDouble());
			Assert.AreEqual(0.5, b.ToDouble());
			Fixed<Q24_8> res = a * b;
			Assert.AreEqual(0.25, res.ToDouble());
			BitDefiner c = new BitDefiner(new byte[] { 64, 0, 0, 0 }, Endianity.LittleEndian);
			Assert.IsTrue(ByteArrayComp(res.Data, c));
		}

		[TestMethod()]
		public void MytliplyOperatorTest06()
		{
			Fixed<Q24_8> a = -100;
			Fixed<Q24_8> res = a * -100;
			Fixed<Q24_8> c = 10000;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void MytliplyOperatorTest07()
		{
			Fixed<Q24_8> a = -100;
			Fixed<Q24_8> res = -100 * a;
			Fixed<Q24_8> c = 10000;
			Assert.IsTrue(ByteArrayComp(res.Data, c.Data));
		}

		[TestMethod()]
		public void ToDouble01()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(42);
			Assert.AreEqual((double)42, a.ToDouble());
		}

		[TestMethod()]
		public void ToDouble02()
		{
			Fixed<Q24_8> a = new Fixed<Q24_8>(-42);
			Assert.AreEqual((double)-42, a.ToDouble());
		}

		[TestMethod()]
		public void ExplicitReType01()
		{
			Fixed<Q8_24> a = 60;
			Fixed<Q8_24> c = -15;

			Fixed<Q24_8> a_ = (Fixed<Q24_8>)((c / a) + a);
			Assert.AreEqual(59.75d, a_.ToDouble());
		}

		[TestMethod()]
		public void ExplicitReType02()
		{
			Fixed<Q24_8> a = 60;
			Fixed<Q24_8> c = -15;

			Fixed<Q8_24> a_ = (Fixed<Q8_24>)((c / a) + a);
			Assert.AreEqual(59.75d, a_.ToDouble());
		}

		[TestMethod()]
		public void ExplicitReType03()
		{
			Fixed<Q16_16> a = 256;
			Fixed<Q8_24> c = (Fixed<Q8_24>)a;
			Assert.AreNotEqual(a.ToDouble(), c.ToDouble());
			Assert.AreEqual(0d, c.ToDouble());
		}
		
		[TestMethod()]
		public void ExplicitReType04()
		{
			BitDefiner b = new BitDefiner(32);
			b[0] = 1;
			Fixed<Q8_24> a = new Fixed<Q8_24>(b);

			Fixed<Q24_8> c = (Fixed<Q24_8>)a;
			Assert.AreNotEqual(a.ToDouble(), c.ToDouble());
			Assert.AreEqual(0d, c.ToDouble());
		}

		public class Q1_1 : FixedPointDefiner
		{
			public override int FullPart => 1;

			public override int DecimalPart => 1;
		}

		public class Q15 : FixedPointDefiner
		{
			public override int FullPart => 0;

			public override int DecimalPart => 15;
		}

		public bool ByteArrayComp(BitDefiner a, BitDefiner b)
		{
			if (a.Length != b.Length)
				return false;
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
					return false;
			}

			return true;
		}
	}
}
