using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.DataStructures
{

	public class ArrayAmortizer<T>
	{
		public int Length { get => Data.Length * ChunkSize; }
		public int ChunkSize { get; private set; }
		public int ChunkCount { get => Data.Length; }
		public bool IsReadOnly { get => false; }

		protected T[][] Data;

		public T this[int index]
		{
			get => this.Data[index / ChunkSize][index % ChunkSize];
			set => this.Data[index / ChunkSize][index % ChunkSize] = value;
		}


		public ArrayAmortizer()
		{
			this.ChunkSize = 64;
			this.Data = new T[2][];
			this.Data[0] = new T[ChunkSize];
			this.Data[1] = new T[ChunkSize];
		}

		private ArrayAmortizer(int ChunkCount, int ChunkSize)
		{
			this.ChunkSize = ChunkSize;
			this.Data = new T[ChunkCount][];
			for (int i = 0; i < ChunkCount; i++)
			{
				this.Data[i] = new T[ChunkSize];
			}
		}


		public void Expand(out int Delta)
		{
			Delta = (ChunkCount / 2) * ChunkSize;
			T[][] newData = new T[ChunkCount * 2][];

			for (int i = 0; i < ChunkCount / 2; i++)
			{
				newData[i] = new T[ChunkSize];
			}
			for (int i = 0; i < ChunkCount; i++)
			{
				newData[i + ChunkCount / 2] = this.Data[i];
			}
			for (int i = ChunkCount + ChunkCount / 2; i < newData.Length; i++)
			{
				newData[i] = new T[ChunkSize];
			}
			this.Data = newData;
		}

		public bool Contains(T item)
		{
			for (int i = 0; i < ChunkCount; i++)
			{
				if (this.Data[i].Contains(item))
					return true;
			}
			return false;
		}

		public static ArrayAmortizer<T> GetCopy(ArrayAmortizer<T> Sourse, int Index, int Length)
		{
			ArrayAmortizer<T> newMemo = new ArrayAmortizer<T>(Sourse.ChunkCount, Sourse.ChunkSize);

			for (int i = 0; i < Length; i++)
			{
				newMemo[Index + i] = Sourse[Index + i];
			}
			return newMemo;
		}
	}
}
