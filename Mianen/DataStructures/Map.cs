using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.DataStructures
{
	public class Map<T1,T2>
	{
		private Dictionary<T1, T2> _forward;
		private Dictionary<T2, T1> _reverse;

		public int Count
		{
			get => _forward.Count;
		}

		public Map()
		{
			_forward = new Dictionary<T1, T2>();
			_reverse = new Dictionary<T2, T1>();
		}

		public T1 this[T2 Key]
		{
			get => _reverse[Key];
		}

		public T2 this[T1 Key]
		{
			get => _forward[Key];
		}

		public bool Add(T1 Key, T2 Value)
		{
			return sAdd(Key, Value);
		}

		public bool Add(T2 Key, T1 Value)
		{
			return sAdd(Value, Key);
		}

		public bool ContainsItem(T1 Item)
		{
			return _forward.ContainsKey(Item);
		}

		public bool ContainsItem(T2 Item)
		{
			return _reverse.ContainsKey(Item);
		}

		public bool GetPair(T1 Input, out T1 a, out T2 b)
		{
			a = default(T1);
			b = default(T2);
			if (!this.ContainsItem(Input))
				return false;
			else
			{
				a = Input;
				b = this[a];
				return true;
			}
		}

		public bool GetPair(T2 Input, out T1 a, out T2 b)
		{
			a = default(T1);
			b = default(T2);
			if (!this.ContainsItem(Input))
				return false;
			else
			{
				a = this[b];
				b = Input;
				return true;
			}
		}

		internal bool sAdd(T1 a, T2 b)
		{
			if (_forward.ContainsKey(a) || _reverse.ContainsKey(b))
				return false;
			_forward[a] = b;
			_reverse[b] = a;
			return true;
		}

	}
}
