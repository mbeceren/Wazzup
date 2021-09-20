using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Wazzup.Common;
using Wazzup.Identity.Exceptions;
using Wazzup.Identity.Models;

namespace Wazzup.Identity
{
	public class UserPool : IUserPool
	{
		private readonly IList<User> _users = new List<User>();
		public void AddUser(User user)
		{
			if (_users.Any(a => a.Id == user.Id))
				return;

			var cultureInfo = new CultureInfo("tr-TR");
			if (_users.Any(a => a.Nickname.Trim().ToLower(cultureInfo).Equals(user.Nickname.Trim().ToLower(cultureInfo))))
				throw new UserNickNameAlreadyExistException();

			user.SetColor(GetUniqueColor());

			_users.Add(user);
		}

		public User GetUser(Guid uid)
		{
			return _users.FirstOrDefault(f => f.Id.Equals(uid));
		}

		public void RemoveUser(Guid uid)
		{
			var userToRemove = GetUser(uid);
			if (userToRemove != null)
				_users.Remove(userToRemove);
		}

		private Color GenerateColor()
		{
			Random rnd = new Random();
			return new Color(rnd.Next(255), rnd.Next(255), rnd.Next(255));
		}

		private Color GetUniqueColor()
		{
			var color = GenerateColor();
			if (_users.Any(a => a.Color.Equals(color)))
				color = GetUniqueColor();

			return color;
		}
	}
}
