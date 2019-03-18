using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Mianen.DataStructures
{
	public class JsonReader
	{
		JObject JRoot;

		public string this[string Index]
		{
			get => GetData(Index);
		}

		public JsonReader(string jsonText)
		{
			JRoot = JObject.Parse(jsonText);
		}

		public JsonReader(TextReader rd)
		{
			JRoot = JObject.Parse(rd.ReadToEnd());
		}
		
		public override string ToString()
		{
			return JRoot.ToString();
		}


		public string GetData(string Key)
		{
			try
			{
				string[] keys = Key.Split(new char[] { '/' });
				JObject tmp = JRoot;
				for (int i = 0; i < keys.Length - 1; i++)
				{
					tmp = GetSub(tmp, keys[i]);
				}
				tmp.TryGetValue(keys[keys.Length - 1], out JToken outp);
				return outp.ToString();
			}
			catch (Exception ex)
			{
				throw new NotInJson("Path is not define in Json file", ex);
			}
		}

		private JObject GetSub(JObject var, string Key)
		{
			JToken tk;
			var.TryGetValue(Key, out tk);
			return JObject.Parse(tk.ToString());
		}

		public class NotInJson : Exception
		{
			public NotInJson() : base() { }
			public NotInJson(string Message) : base(Message) { }
			public NotInJson(string Message, Exception innerexceptions) : base(Message, innerexceptions) { }
		}
	}
}
