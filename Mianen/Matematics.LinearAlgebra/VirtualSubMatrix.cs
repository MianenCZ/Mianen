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
	public class VirtualSubMatrix<T> : Matrix<T>
	{
		//Jus junk to fuck with compiler
		private VirtualSubMatrix(int RowCount, int ColumnCount) : base (0,0)
		{

		}
		public int FirstRow { get; internal set; }
		public int FirstColumn { get; internal set; }

		Matrix<T> MotherMatrix;

		public new INumber<T> this[int i, int j, bool IndexFromZero = true]
		{
			get
			{
				if (IndexFromZero)
				{
					if (i < 0 || j < 0 || i >= RowCount || j >= ColumnCount)
						throw new IndexOutOfRangeException();
					return MotherMatrix[FirstRow - i, FirstColumn - j];
				}
				else
				{
					if (i < 1 || j < 1 || i > RowCount || j > ColumnCount)
						throw new IndexOutOfRangeException();
					return MotherMatrix[FirstRow - 1 - i, FirstColumn - 1 - j];
				}
			}
			//set => Data[(IndexFromZero) ? i : i - 1, (IndexFromZero) ? j : j - 1] = value;
		}

		public VirtualSubMatrix(Matrix<T> MotherMatrix, int FirstRow, int FirstColumn, int RowCount, int ColumnCount) : base(1,1)
		{
			this.RowCount = RowCount;
			this.ColumnCount = ColumnCount;
			this.FirstColumn = FirstColumn;
			this.FirstRow = FirstRow;
			this.Data = null;
			this.MotherMatrix = MotherMatrix;
		}

		public override string ToString()
		{
			int[] MaxLenghts = new int[this.ColumnCount];

			for (int i = 0; i < this.RowCount; i++)
			{
				for (int j = 0; j < this.ColumnCount; j++)
				{
					if (this[i, j].ToString().Length > MaxLenghts[j])
						MaxLenghts[j] = this[i, j].ToString().Length;
				}
			}

			StringBuilder bld = new StringBuilder();

			for (int i = 0; i < this.RowCount; i++)
			{
				for (int j = 0; j < this.ColumnCount; j++)
				{
					bld.Append("   ");

					for (int q = 0; q < MaxLenghts[j] - this[i, j].ToString().Length; q++)
						bld.Append(" ");
					bld.Append(this[i, j]);
				}
				bld.AppendLine();
			}

			return bld.ToString();
		}


	}
}
