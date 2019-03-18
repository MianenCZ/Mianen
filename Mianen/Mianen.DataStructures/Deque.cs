using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mianen.DataStructures
{
	public class Deque<T> : IList<T>
	{

		private DequeMemo<T> Data;
		private int LeftMemo = 16;
		private int RightMemo = 16;
		public Direction Dir { get; private set; }
		public int Capacity { get => LeftMemo + RightMemo; }

		public T First { get => this[0]; }
		public T Last { get => this.Data[Data.Right - 1]; }

		public Deque()
		{
			this.Data = new DequeMemo<T>();
			this.Data.Left = LeftMemo;
			this.Data.Right = RightMemo;
			this.Dir = Direction.Standard;
		}

		public Deque(int capacity)
		{
			this.Data = new DequeMemo<T>();
			this.LeftMemo = capacity / 2;
			this.RightMemo = capacity - LeftMemo;
			this.Data.Left = LeftMemo;
			this.Data.Right = RightMemo;
			this.Dir = Direction.Standard;
		}

		//Done
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
					throw new ArgumentOutOfRangeException();

				if (this.Dir == Direction.Standard)
					return this.Data[Data.Left + index];
				else
					return this.Data[Data.Right - index - 1];
			}
			set
			{
				if (index < 0 || index >= this.Count)
					throw new ArgumentOutOfRangeException();

				if (this.Dir == Direction.Standard)
					this.Data[Data.Left + index] = value;
				else
					this.Data[Data.Right - index - 1] = value;
			}
		}

		//done
		public int Count => Data.Right - Data.Left;

		//Done
		public bool IsReadOnly => Data.IsReadOnly;

		//Done
		public void Add(T item)
		{
			if (this.Dir == Direction.Standard)
				AddRight(item);
			else
				AddLeft(item);
		}

		//Done
		public void AddBegin(T item)
		{
			if (this.Dir == Direction.Standard)
				AddLeft(item);
			else
				AddRight(item);
		}

		//Done
		public bool Remove(T item)
		{
			if (this.Count == 0)
				return false;

			if (item == null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == null)
					{
						this.RemoveAt(i);
						return true;
					}
				}
				return false;


			}

			if (item.Equals(this[this.Count - 1]))
			{
				if (this.Dir == Direction.Standard)
					this.RemoveRight();
				else
					this.RemoveLeft();

				return true;
			}

			for (int i = 0; i < this.Count; i++)
			{
				if (item.Equals(this[i]))
				{
					this.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		//Done
		private void AddLeft(T item)
		{
			this.Data.Left -= 1;
			this.Data[Data.Left] = item;
			ExpandLeft();


		}

		//Done
		private void AddRight(T item)
		{
			this.Data[Data.Right] = item;
			this.Data.Right += 1;


			ExpandRight();
		}

		private void ExpandLeft()
		{
			if (Data.Left == 0)
				Data.Expand();
		}

		private void ExpandRight()
		{
			if (Data.Right == this.Data.Length)
				Data.Expand();
		}

		//Done
		private void RemoveLeft()
		{
			if (this.Count <= 0)
				throw new ArgumentOutOfRangeException();
			this.Data[this.Data.Left] = default(T);
			Data.Left++;
			Press();
		}

		//Done
		private void RemoveRight()
		{
			if (this.Count <= 0)
				throw new ArgumentOutOfRangeException();
			this.Data[this.Data.Right - 1] = default(T);
			Data.Right--;
			Press();
		}

		//Done
		public void Clear()
		{
			for (int i = this.Data.Left; i < this.Data.Right; i++)
			{
				this.Data[i] = default(T);
			}
			this.Data.Left = LeftMemo;
			this.Data.Right = LeftMemo;
			Press();
		}

		//Done
		public bool Contains(T item)
		{
			return Data.Contains(item);
		}

		//Done
		public void CopyTo(T[] array, int arrayIndex)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[arrayIndex + i] = this[i];
			}
		}

		//Done
		public IEnumerator<T> GetEnumerator()
		{
			return new DequeEnumerator<T>(this);
		}

		//Check return value if item is not in Deque
		public int IndexOf(T item)
		{
			if (item == null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == null)
					{
						return i;
					}
				}
				return -1;
			}


			for (int i = 0; i < this.Count; i++)
			{
				if (item.Equals(this[i]))
				{
					return i;
				}
			}
			return -1;
		}

		public void Insert(int index, T item)
		{
			if (index < 0 || index > this.Count)
				throw new ArgumentOutOfRangeException();
			if (index == 0)
			{
				if (this.Dir == Direction.Standard)
					AddLeft(item);
				else
					AddRight(item);
				return;
			}

			if (index == this.Count)
			{
				if (this.Dir == Direction.Standard)
					AddRight(item);
				else
					AddLeft(item);
				return;
			}


			if (Dir == Direction.Standard)
			{
				//int ToAdd = (this.Dir == Direction.Standard) ? Data.Left + index : Data.Right - index;

				int ToAdd = Data.Left + index;

				for (int i = this.Data.Right; i > ToAdd; i--)
				{
					this.Data[i] = this.Data[i - 1];
				}
				this.Data[ToAdd] = item;
				Data.Right += 1;
				ExpandRight();
			}
			else
			{

				int ToAdd = Data.Right - index;
				for (int i = Data.Left; i < ToAdd; i++)
				{
					this.Data[i - 1] = this.Data[i];
				}
				this.Data[ToAdd - 1] = item;
				Data.Left--;
				ExpandLeft();
			}
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
				throw new ArgumentOutOfRangeException();

			if (index == 0)
			{
				if (this.Dir == Direction.Standard)
					RemoveLeft();
				else
					RemoveRight();
				return;
			}

			if (index == this.Count - 1)
			{
				if (this.Dir == Direction.Standard)
					RemoveRight();
				else
					RemoveLeft();
				return;
			}


			if (this.Dir == Direction.Standard)
			{
				int ToRemove = Data.Left + index;

				for (int i = ToRemove + 1; i < this.Data.Right; i++)
				{
					this.Data[i - 1] = this.Data[i];
				}
				this.Data[Data.Right - 1] = default(T);
				Data.Right -= 1;
			}
			else
			{
				int ToRemove = Data.Right - index - 1;

				for (int i = ToRemove - 1; i >= Data.Left; i--)
				{
					this.Data[i + 1] = this.Data[i];
				}
				this.Data[Data.Left] = default(T);
				Data.Left++;
			}

			//int ToRemove = (this.Dir == Direction.Standard) ? Data.Left + index : Data.Right - index - 1;


			Press();
		}

		//Done
		IEnumerator IEnumerable.GetEnumerator()
		{
			for (int i = 0; i < this.Count; i++)
			{
				yield return this[i];
			}
		}

		//Done
		public void Reverse()
		{
			if (this.Dir == Direction.Standard)
				this.Dir = Direction.Reverse;
			else
				this.Dir = Direction.Standard;
		}

		public void Press()
		{
			/*
			if (this.Count < this.Capacity / 4)
			{
				//NeedToPressArray
				int newLeftMemo = 16;
				int newRightMemo = this.Count + 10;

				T[] newData = new T[newLeftMemo + newRightMemo];

				for(int i = 0; i < this.Count; i++)
				{
					newData[i + newLeftMemo] = this.Data[i + this.Data.Left];
				}
				int ActCount = this.Count;
				this.Data.Left = newLeftMemo;
				this.Data.Right = Data.Left + ActCount;
				this.LeftMemo = newLeftMemo;
				this.RightMemo = newRightMemo;
				this.Data = newData;
				newData = null;
			}
			//*/

		}

		public static Deque<T> GetCopy(Deque<T> old)
		{
			Deque<T> newDeque = new Deque<T>();
			newDeque.Data = old.Data;
			newDeque.Data.Left = old.Data.Left;
			newDeque.Data.Right = old.Data.Right;
			return newDeque;
		}

		public class DequeMemo<T> : ArrayAmortizer<T>
		{
			public int Left { get; set; }
			public int Right { get; set; }

			public void Expand()
			{
				this.Left = Left + (ChunkCount / 2) * ChunkSize;
				this.Right = Right + (ChunkCount / 2) * ChunkSize;
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
		}

	}

	//Done
	public enum Direction
	{
		Standard,
		Reverse
	}

	//Done
	public class DequeEnumerator<T> : IEnumerator<T>
	{
		Deque<T> Input;
		int index;
		public DequeEnumerator(Deque<T> Input)
		{
			this.Input = Input;
			this.index = -1;
		}

		public T Current => Input[index];

		object IEnumerator.Current => throw new NotImplementedException();

		public void Dispose()
		{
			this.Input = null;
		}

		public bool MoveNext()
		{
			index++;
			return index < Input.Count;
		}

		public void Reset()
		{
			index = 0;
		}
	}

	//Done
	public static class DequeTest
	{
		public static IList<T> GetReverseView<T>(Deque<T> d)
		{
			if (d == null)
				throw new ArgumentNullException();
			var q = Deque<T>.GetCopy(d);
			q.Reverse();
			return q;
		}
	}
}

