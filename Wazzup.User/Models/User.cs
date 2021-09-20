using System;
using Wazzup.Common;

namespace Wazzup.Identity.Models
{
	public class User
	{
		private Color _color = null;
		private readonly string _nickname;
		private readonly Guid _id = Guid.NewGuid();

		public User(string nickname)
		{
			_nickname = nickname;
		}

		public Guid Id => _id;
		public string Nickname => _nickname;
		public Color Color => _color;
		public void SetColor(Color color)
		{
			_color = color;
		}
	}
}
