using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mianen.Matematics.Geometry2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics.LinearAlgebra;

namespace Mianen.Matematics.Geometry2D.Tests
{
	[TestClass()]
	public class PolynomTests
	{
		[TestMethod()]
		public void PolynomTest()
		{
			Polynom<double> P = new Polynom<double>(Vector<double>.Create(new double[] { 1, 1, 1 }));
			Assert.AreEqual(2, P.Degree);
		}

		[TestMethod()]
		public void IndexTest()
		{
			Polynom<double> P = new Polynom<double>(Vector<double>.Create(new double[] { 1, 2, 3 }));
			Assert.AreEqual(2, P.Degree);
			Assert.AreEqual(1, P[0]);
			Assert.AreEqual(2, P[1]);
			Assert.AreEqual(3, P[2]);
		}

		[TestMethod()]
		public void EvalTest()
		{
			Polynom<double> P = new Polynom<double>(Vector<double>.Create(new double[] { 1, 1, 1 }));
			Assert.AreEqual(2, P.Degree);
			Assert.AreEqual(1,P.Eval(0));
			Assert.AreEqual(3,P.Eval(1));
			Assert.AreEqual(1,P.Eval(-1));
			Assert.AreEqual(7,P.Eval(2));
			Assert.AreEqual(13,P.Eval(3));
		}

		[TestMethod()]
		public void PolynomAprox01()
		{
			Point2D<double>[] Points = new Point2D<double>[5];
			Points[0] = new Point2D<double>(0,1);
			Points[1] = new Point2D<double>(1,3);
			Points[2] = new Point2D<double>(-1,1);
			Points[3] = new Point2D<double>(2,7);
			Points[4] = new Point2D<double>(-2,3);

			Polynom<double> p = Polynom<double>.Aproximate(Points, 2);

		}

		[TestMethod()]
		public void PolynomAprox02()
		{
			Point2D<double>[] Points = new Point2D<double>[38];
			Points[0] = new Point2D<double>(160, 60);
			Points[1] = new Point2D<double>(168, 60 );
			Points[2] = new Point2D<double>(168, 58 );
			Points[3] = new Point2D<double>(169, 96 );
			Points[4] = new Point2D<double>(170, 70 );
			Points[5] = new Point2D<double>(170, 62 );
			Points[6] = new Point2D<double>(172, 61 );
			Points[7] = new Point2D<double>(172, 79 );
			Points[8] = new Point2D<double>(175, 72 );
			Points[9] = new Point2D<double>(177, 62 );
			Points[10] = new Point2D<double>(180, 86 );
			Points[11] = new Point2D<double>(180, 60 );
			Points[12] = new Point2D<double>(180, 75 );
			Points[13] = new Point2D<double>(180, 70 );
			Points[14] = new Point2D<double>(181, 76 );
			Points[15] = new Point2D<double>(183, 65 );
			Points[16] = new Point2D<double>(183, 77 );
			Points[17] = new Point2D<double>(183, 87 );
			Points[18] = new Point2D<double>(183, 76 );
			Points[19] = new Point2D<double>(184, 65 );
			Points[20] = new Point2D<double>(185, 90 );
			Points[21] = new Point2D<double>(185, 65 );
			Points[22] = new Point2D<double>(185, 80 );
			Points[23] = new Point2D<double>(185, 72 );
			Points[24] = new Point2D<double>(185, 85 );
			Points[25] = new Point2D<double>(189, 85 );
			Points[26] = new Point2D<double>(190, 75 );
			Points[27] = new Point2D<double>(190, 70 );
			Points[28] = new Point2D<double>(191, 70 );
			Points[29] = new Point2D<double>(193, 71 );
			Points[30] = new Point2D<double>(194, 87 );
			Points[31] = new Point2D<double>(195, 95 );
			Points[32] = new Point2D<double>(178, 73 );
			Points[33] = new Point2D<double>(187, 85 );
			Points[34] = new Point2D<double>(195, 88 );
			Points[35] = new Point2D<double>(167, 63 );
			Points[36] = new Point2D<double>(195, 79 );
			Points[37] = new Point2D<double>(185, 110);
			Polynom<double> p = Polynom<double>.Aproximate(Points, 2);
			Console.WriteLine(p.DefVector);
			Assert.IsTrue(true, p.DefVector.ToString());
		}
	}
}