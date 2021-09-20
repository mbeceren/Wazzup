using System;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Wazzup.Messaging.Exceptions;

namespace Wazzup.Messaging
{
	public class SocketPoolManager : ISocketPoolManager
	{
		public readonly ISocketPool _pool;
		public SocketPoolManager(ISocketPool pool)
		{
			_pool = pool;
		}

		public Task Publish(byte[] data)
		{
			var sockets = _pool.GetOpenSockets();
			if (!sockets.Any())
				return Task.FromException(new SocketPoolEmptyException());

			foreach(var socket in sockets)
			{
				socket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, CancellationToken.None);
			}

			return Task.CompletedTask;
		}
	}
}
