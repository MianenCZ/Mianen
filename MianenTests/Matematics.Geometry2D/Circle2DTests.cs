using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mianen.Matematics.Geometry2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mianen.Matematics.LinearAlgebra;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.Geometry2D.Tests
{
	[TestClass()]
	public class Circle2DTests
	{
		[TestMethod()]
		public void Circle2D()
		{
			//TODO: Uncomment
			//Circle2D<float> a = new Circle2D<float>(new Point2D<float>(0, 0), 1);
		}

		[TestMethod()]
		public void Circle2DAprox()
		{
			Point2D<double>[] vals = new Point2D<double>[4];
			vals[0] = new Point2D<double>(new NumberDouble(1), new NumberDouble(0));
			vals[1] = new Point2D<double>(new NumberDouble(-1), new NumberDouble(0));
			vals[2] = new Point2D<double>(new NumberDouble(0), new NumberDouble(-1));
			vals[3] = new Point2D<double>(new NumberDouble(0), new NumberDouble(-1));
			//TODO: Uncomment
			/*
			Circle2D<double> res = new Circle2D<double>(new Point2D<double>(0, 0), 1);
			Circle2D<double> apr = Circle2D<double>.Aproximate(vals);
			Assert.AreEqual(res.Radius, apr.Radius);
			Assert.AreEqual(res.Center.X, apr.Center.X);
			Assert.AreEqual(res.Center.Y, apr.Center.Y);
			//*/

		}
	}
}
