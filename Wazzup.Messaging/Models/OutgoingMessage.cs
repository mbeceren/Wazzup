using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wazzup.Messaging.Models
{
	public class OutgoingMessage : Message
	{
		public Sender Sender { get; set; }
		public DateTime Date { get; set; }
	}
}
