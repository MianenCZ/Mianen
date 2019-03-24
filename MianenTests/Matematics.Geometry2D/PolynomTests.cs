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
		public void PolynomAprox()
		{
			Point2D<double>[] Points = new Point2D<double>[5];
			Points[0] = new Point2D<double>(0,1);
			Points[1] = new Point2D<double>(1,3);
			Points[2] = new Point2D<double>(-1,1);
			Points[3] = new Point2D<double>(2,7);
			Points[4] = new Point2D<double>(-2,3);

			Polynom<double> p = Polynom<double>.Aproximate(Points, 2);

		}
	}
}