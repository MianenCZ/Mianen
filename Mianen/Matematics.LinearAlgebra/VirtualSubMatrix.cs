using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Mianen.Matematics;
using Mianen.Matematics.Numerics;

namespace Mianen.Matematics.LinearAlgebra
{
	internal class VirtualSubMatrix<T> : Matrix<T>
	{
		//Jus junk to fuck with compiler
		private VirtualSubMatrix(int RowCount, int ColumnCount) : base (0,0)
		{

		}

		public new INumber<T> this[int i, int j, bool IndexFromZero = true]
		{
			get
			{


				return null;
			}
			//set => Data[(IndexFromZero) ? i : i - 1, (IndexFromZero) ? j : j - 1] = value;
		}

		public VirtualSubMatrix(Matrix<T> Reference, int FirstRow, int FirstColumn, int RowCount, int ColumnCount) : base(1,1)
		{
			this.Data = null;
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}
