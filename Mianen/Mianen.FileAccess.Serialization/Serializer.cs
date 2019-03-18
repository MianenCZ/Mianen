using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Converters;

namespace Mianen.FileAccess.Serialization
{
	public static class Serializer
	{
		public static bool SerializeJsonObject<T>(T serializableObject, string fileName)
		{
			try
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Converters.Add(new JavaScriptDateTimeConverter());
				serializer.NullValueHandling = NullValueHandling.Ignore;

				using (StreamWriter sw = new StreamWriter(fileName))
				using (JsonWriter writer = new JsonTextWriter(sw))
				{
					serializer.Serialize(writer, serializableObject);
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool DeserializeJsonObject<T>(string fileName, out T loadedobject)
		{
			try
			{
				FileStream fs = new FileStream(fileName, FileMode.Open);
				StreamReader rd = new StreamReader(fs);

				loadedobject = JsonConvert.DeserializeObject<T>(rd.ReadToEnd());
				return true;
			}
			catch
			{
				loadedobject = default(T);
				return false;
			}
		}

		/// <summary>
		/// Serializes an object into Xml file
		/// </summary>
		/// <typeparam name="T">Serializable type of object</typeparam>
		/// <param name="serializableObject">object to serialize</param>
		/// <param name="fileName">Path to save</param>
		/// <returns>Flag of succes</returns>
		public static bool SerializeXmlObject<T>(T serializableObject, string fileName)
		{
			if (serializableObject == null) { return false ; }

			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
				using (MemoryStream stream = new MemoryStream())
				{
					serializer.Serialize(stream, serializableObject);
					stream.Position = 0;
					xmlDocument.Load(stream);
					xmlDocument.Save(fileName);
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Deserializes an xml file into an object
		/// </summary>
		/// <typeparam name="T">type of object</typeparam>
		/// <param name="fileName">Path to load</param>
		/// <returns>Flag of succes</returns>
		public static bool DeSerializeXmlObject<T>(string fileName, out T loadedobject)
		{
			loadedobject = default(T);
			if (string.IsNullOrEmpty(fileName)) { return false; }

			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(fileName);
				string xmlString = xmlDocument.OuterXml;

				using (StringReader read = new StringReader(xmlString))
				{
					Type outType = typeof(T);

					XmlSerializer serializer = new XmlSerializer(outType);
					using (XmlReader reader = new XmlTextReader(read))
					{
						loadedobject = (T)serializer.Deserialize(reader);
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Serializes an object into binary file
		/// </summary>
		/// <typeparam name="T">Serializable type of object</typeparam>
		/// <param name="serializableObject">object to serialize</param>
		/// <param name="fileName">Path to save</param>
		/// <returns>Flag of succes</returns>
		public static bool SerializeBinaryObject<T>(T serializableObject, string fileName)
		{
			try
			{
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new FileStream(fileName, FileMode.Create, System.IO.FileAccess.Write, FileShare.None);
				formatter.Serialize(stream, serializableObject);
				stream.Close();
				stream.Dispose();
				return true;
			} catch
			{
				return false;
			}
		}

		/// <summary>
		/// Deserializes an binary file into an object
		/// </summary>
		/// <typeparam name="T">type of object</typeparam>
		/// <param name="fileName">Path to load</param>
		/// <returns>Flag of succes</returns>
		public static bool DeSerializeBinaryObject<T>(string fileName, out T loadedObject)
		{
			loadedObject = default(T);
			try
			{
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new FileStream(fileName, FileMode.Open, System.IO.FileAccess.Read, FileShare.Read);
				T obj = (T)formatter.Deserialize(stream);
				stream.Close();
				loadedObject = obj;
				return true;
			}
			catch
			{ return false; }
		}
	}
}
