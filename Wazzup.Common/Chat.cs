using System.Collections.Generic;

namespace Wazzup.Common
{
	public class Chat : JsonSerializable
	{
		public IList<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
	}
}
