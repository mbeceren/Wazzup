using System;

namespace Wazzup.Common
{
	public class Color
	{
		private readonly int _r;
		private readonly int _g;
		private readonly int _b;
		public Color(int r, int g, int b)
		{
			_r = r;
			_g = g;
			_b = b;
		}
		public int R => _r;
		public int G => _g;
		public int B => _b;

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (obj is not Color)
				return false;

			var c = (Color)obj;
			if (c == null)
				return false;

			return c.R == R && c.G == G && c.B == B; 
		}

		public override int GetHashCode()
		{
			return Tuple.Create(_r, _g, _b).GetHashCode();
		}
	}
}
