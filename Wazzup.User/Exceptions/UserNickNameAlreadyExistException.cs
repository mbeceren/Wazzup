using System;

namespace Wazzup.Identity.Exceptions
{
	public class UserNickNameAlreadyExistException : Exception
	{
		public override string Message => "Girilen nickname zaten mevcut. Lütfen başka bir nickname deneyin.";
	}
}
