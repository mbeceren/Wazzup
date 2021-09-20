using System;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace Wazzup.Messaging
{
	public interface ISocketPool
	{
		void AddSocket(Guid uid, WebSocket socket);
		void RemoveSocket(Guid uid);
		IEnumerable<WebSocket> GetOpenSockets();
	}
}
