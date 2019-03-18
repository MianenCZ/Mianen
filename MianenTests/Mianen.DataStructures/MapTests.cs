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
	public class MapTests
	{
		[TestMethod()]
		public void MapTest()
		{
			Map<string, string> A = new Map<string, string>();

		}

		[TestMethod()]
		public void AddTest1()
		{
			Map<int, string> A = new Map<int, string>();
			Assert.IsTrue(A.Add("1", 1));
		}

		[TestMethod()]
		public void AddTest2()
		{
			Map<int, string> A = new Map<int, string>();
			Assert.IsTrue(A.Add(1, "1"));
		}

		[TestMethod()]
		public void AddTest3()
		{
			Map<int, string> A = new Map<int, string>();
			Assert.IsTrue(A.Add(1, "1"));
			Assert.IsFalse(A.Add(1, "1"));
			Assert.IsFalse(A.Add("1", 1));
		}

		[TestMethod()]
		public void ContainsItemTest()
		{
			Map<int, string> A = new Map<int, string>();
			Assert.IsTrue(A.Add(1, "1"));
			Assert.IsTrue(A.ContainsItem(1));
			Assert.IsTrue(A.ContainsItem("1"));
			Assert.IsFalse(A.ContainsItem(2));
			Assert.IsFalse(A.ContainsItem("2"));
		}

		[TestMethod]
		public void CountTest()
		{
			Map<int, string> A = new Map<int, string>();
			A.Add(1, "");
			Assert.AreEqual(1, A.Count);
		}

		[TestMethod]
		public void CountTest2()
		{
			Map<int, string> A = new Map<int, string>();
			for (int i = 0; i < 100; i++)
			{
				A.Add(i, i.ToString());
			}
			Assert.AreEqual(100, A.Count);
		}
	}
}