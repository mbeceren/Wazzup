using System;
using System.Collections.Generic;
using Wazzup.Identity.Models;

namespace Wazzup.Identity
{
	public interface  IUserPool
	{
		void AddUser(User user);
		void RemoveUser(Guid uid);
		User GetUser(Guid uid);
		IEnumerable<User> GetAllUsers();
	}
}
