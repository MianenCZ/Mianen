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
	public class JsonReaderTests
	{
		[TestMethod()]
		public void JsonReaderConstructTest01()
		{
			string File2 = "{\"head\" : {\"plus_x\" : [ -1, 0, 0 ],\"plus_z\" : [ 0, -1, 0 ],\"position\" : [ 0, -0.0078, -0.0148 ]}}";
			string Res2 = "\"head\" : {\"plus_x\" : [ -1, 0, 0 ],\"plus_z\" : [ 0, -1, 0 ],\"position\" : [ 0, -0.0078, -0.0148 ]}";
			JsonReader rd2 = new JsonReader(File2);
			Assert.AreEqual(Res2, rd2.ToString());

		}
		[TestMethod()]
		public void JsonReaderTest1()
		{
			string File = "{\"Path\":\"LongPath\"}";
			JsonReader rd = new JsonReader(File);
			Assert.AreEqual("LongPath", rd.GetData("Path"));

		}

		[TestMethod()]
		public void JsonReaderTest2()
		{
			string File2 = "{\"head\" : {\"plus_x\" : [ -1, 0, 0 ],\"plus_z\" : [ 0, -1, 0 ],\"position\" : [ 0, -0.0078, -0.0148 ]}}";
			JsonReader rd = new JsonReader(File2);
			Assert.AreEqual("LongPath", rd.GetData("head"), rd.GetData("head"));
		}


		[TestMethod()]
		public void GetTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetSubDictionaryTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void GetFromJsonTest()
		{
			Assert.Fail();
		}
	}
}