using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mianen.Matematics.Numerics
{
	public abstract class FixedPointDefiner
	{
		public abstract int FullPart { get; }
		public abstract int DecimalPart { get; }

		public FixedPointDefiner()
		{

		}

	}

	public class Q24_8 : FixedPointDefiner
	{
		public override int FullPart
		{
			get => 24;
		}

		public override int DecimalPart
		{
			get => 8;
		}
	}

	public class Q16_16 : FixedPointDefiner
	{
		public override int FullPart
		{
			get => 16;
		}

		public override int DecimalPart
		{
			get => 16;
		}
	}

	public class Q8_24 : FixedPointDefiner
	{
		public override int FullPart
		{
			get => 8;
		}

		public override int DecimalPart
		{
			get => 24;
		}
	}
}
