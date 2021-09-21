using Wazzup.Common;

namespace Wazzup.Messaging.Models
{
	public abstract class Message : JsonSerializable
	{
		public string Text { get; set; }
	}
}
