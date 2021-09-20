using Newtonsoft.Json;
using System;

namespace Wazzup.Messaging.Models
{
	public abstract class Message
	{
		public string Text { get; set; }
		public string SerializeToJson()
		{
			return JsonConvert.SerializeObject(this, new JsonSerializerSettings
			{
				StringEscapeHandling = StringEscapeHandling.EscapeHtml,
				DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
				Culture = new System.Globalization.CultureInfo("tr-TR")
			});
		}
	}
}
