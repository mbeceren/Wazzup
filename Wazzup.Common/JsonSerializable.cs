using Newtonsoft.Json;

namespace Wazzup.Common
{
	public abstract class JsonSerializable
	{
		protected virtual StringEscapeHandling StringEscapeHandling { get; } = StringEscapeHandling.EscapeHtml;
		protected virtual DateFormatHandling DateFormatHandling { get; } = DateFormatHandling.MicrosoftDateFormat;
		protected virtual System.Globalization.CultureInfo CultureInfo { get; } = new System.Globalization.CultureInfo("tr-TR");
		public string SerializeToJson()
		{
			return JsonConvert.SerializeObject(this, new JsonSerializerSettings
			{
				StringEscapeHandling = StringEscapeHandling,
				DateFormatHandling = DateFormatHandling,
				Culture = CultureInfo
			});
		}
	}
}
