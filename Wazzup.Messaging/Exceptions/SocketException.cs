using System;

namespace Wazzup.Messaging.Exceptions
{
	public abstract class SocketException : Exception
	{
		public virtual int StatusCode { get; }
	}
}
