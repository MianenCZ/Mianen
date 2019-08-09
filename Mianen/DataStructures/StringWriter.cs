using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting;

namespace Mianen.DataStructures
{
	class StringWriter : TextWriter
	{
		public StringWriter() : base()
		{

		}

		public override Encoding Encoding => throw new NotImplementedException();

		public override IFormatProvider FormatProvider => base.FormatProvider;

		public override string NewLine { get => base.NewLine; set => base.NewLine = value; }

		public override void Close()
		{
			base.Close();
		}

		public override ObjRef CreateObjRef(Type requestedType)
		{
			return base.CreateObjRef(requestedType);
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override void Flush()
		{
			base.Flush();
		}

		public override Task FlushAsync()
		{
			return base.FlushAsync();
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override object InitializeLifetimeService()
		{
			return base.InitializeLifetimeService();
		}

		public override string ToString()
		{
			return base.ToString();
		}

		public override void Write(char value)
		{
			base.Write(value);
		}

		public override void Write(char[] buffer)
		{
			base.Write(buffer);
		}

		public override void Write(char[] buffer, int index, int count)
		{
			base.Write(buffer, index, count);
		}

		public override void Write(bool value)
		{
			base.Write(value);
		}

		public override void Write(int value)
		{
			base.Write(value);
		}

		public override void Write(uint value)
		{
			base.Write(value);
		}

		public override void Write(long value)
		{
			base.Write(value);
		}

		public override void Write(ulong value)
		{
			base.Write(value);
		}

		public override void Write(float value)
		{
			base.Write(value);
		}

		public override void Write(double value)
		{
			base.Write(value);
		}

		public override void Write(decimal value)
		{
			base.Write(value);
		}

		public override void Write(string value)
		{
			base.Write(value);
		}

		public override void Write(object value)
		{
			base.Write(value);
		}

		public override void Write(string format, object arg0)
		{
			base.Write(format, arg0);
		}

		public override void Write(string format, object arg0, object arg1)
		{
			base.Write(format, arg0, arg1);
		}

		public override void Write(string format, object arg0, object arg1, object arg2)
		{
			base.Write(format, arg0, arg1, arg2);
		}

		public override void Write(string format, params object[] arg)
		{
			base.Write(format, arg);
		}

		public override Task WriteAsync(char value)
		{
			return base.WriteAsync(value);
		}

		public override Task WriteAsync(string value)
		{
			return base.WriteAsync(value);
		}

		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			return base.WriteAsync(buffer, index, count);
		}

		public override void WriteLine()
		{
			base.WriteLine();
		}

		public override void WriteLine(char value)
		{
			base.WriteLine(value);
		}

		public override void WriteLine(char[] buffer)
		{
			base.WriteLine(buffer);
		}

		public override void WriteLine(char[] buffer, int index, int count)
		{
			base.WriteLine(buffer, index, count);
		}

		public override void WriteLine(bool value)
		{
			base.WriteLine(value);
		}

		public override void WriteLine(int value)
		{
			base.WriteLine(value);
		}

		public override void WriteLine(uint value)
		{
			base.WriteLine(value);
		}

		public override void WriteLine(long value)
		{
			base.WriteLine(value);
		}

		public override void WriteLine(ulong value)
		{
			base.WriteLine(value);
		}

		public override void WriteLine(float value)
		{
			base.WriteLine(value);
		}

		public override void WriteLine(double value)
		{
			base.WriteLine(value);
		}

		public override void WriteLine(decimal value)
		{
			base.WriteLine(value);
		}

		public override void WriteLine(string value)
		{
			base.WriteLine(value);
		}

		public override void WriteLine(object value)
		{
			base.WriteLine(value);
		}

		public override void WriteLine(string format, object arg0)
		{
			base.WriteLine(format, arg0);
		}

		public override void WriteLine(string format, object arg0, object arg1)
		{
			base.WriteLine(format, arg0, arg1);
		}

		public override void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			base.WriteLine(format, arg0, arg1, arg2);
		}

		public override void WriteLine(string format, params object[] arg)
		{
			base.WriteLine(format, arg);
		}

		public override Task WriteLineAsync(char value)
		{
			return base.WriteLineAsync(value);
		}

		public override Task WriteLineAsync(string value)
		{
			return base.WriteLineAsync(value);
		}

		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			return base.WriteLineAsync(buffer, index, count);
		}

		public override Task WriteLineAsync()
		{
			return base.WriteLineAsync();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
