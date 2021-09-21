using System;

namespace Wazzup.Messaging.Models
{
	public class OutgoingMessage : Message
	{
		private readonly DateTime _date = DateTime.Now;
		public Sender Sender { get; set; }
		public string Time => _date.ToLongTimeString();
	}
}
