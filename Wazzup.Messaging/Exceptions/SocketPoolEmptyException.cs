using System;

namespace Wazzup.Messaging.Exceptions
{
	public class SocketPoolEmptyException : SocketException
	{
		public override string Message => "There is no socket in socket pool.";
		public override int StatusCode => SocketExceptionStatusCodes.PoolEmpty; 
	}
}
