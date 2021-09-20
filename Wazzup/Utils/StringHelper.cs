using System.Text.RegularExpressions;

namespace Wazzup.Web.Utils
{
	public static class StringHelper
	{
		public static string StripHTML(string input)
		{
			return Regex.Replace(input, "<.*?>", string.Empty);
		}
	}
}
