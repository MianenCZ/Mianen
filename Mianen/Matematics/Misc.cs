using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics
{
	public class NotAllowedOperationException : Exception
	{
		public NotAllowedOperationException() : base() { }
		public NotAllowedOperationException(string Mes) : base(Mes) { }
		public NotAllowedOperationException(string Mes, Exception ex) : base(Mes, ex) { }
		public NotAllowedOperationException(Exception ex) : base("", ex) { }
	}
}
