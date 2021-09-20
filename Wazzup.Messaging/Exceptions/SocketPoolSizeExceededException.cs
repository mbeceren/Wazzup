using System;

namespace Wazzup.Messaging.Exceptions
{
	public class SocketPoolSizeExceededException : SocketException
	{
		public override string Message => "Pool size exceeded.";
		public override int StatusCode => SocketExceptionStatusCodes.PoolSizeExceed;
	}
}
