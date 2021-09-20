using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using Wazzup.Messaging.Exceptions;

namespace Wazzup.Messaging
{
	public class SocketPool : ISocketPool
	{
		private readonly Dictionary<Guid, WebSocket> _sockets = new Dictionary<Guid, WebSocket>();
		private readonly int _maxSocketCount;
		public SocketPool(int maxSocketCount)
		{
			_maxSocketCount = maxSocketCount;
		}
		public void AddSocket(Guid uid, WebSocket socket)
		{
			if (_sockets.Count >= _maxSocketCount)
				throw new SocketPoolSizeExceededException();

			if (!_sockets.ContainsKey(uid))
				_sockets.Add(uid, socket);
		}

		public void RemoveSocket(Guid uid)
		{
			if (_sockets.ContainsKey(uid))
				_sockets.Remove(uid);
		}

		public IEnumerable<WebSocket> GetOpenSockets()
		{
			foreach(var s in _sockets.Values)
			{
				if (s.State == WebSocketState.Open)
					yield return s;
			}
		}
	}
}
