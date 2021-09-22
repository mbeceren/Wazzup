using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wazzup.Common;
using Wazzup.Identity;
using Wazzup.Identity.Models;
using Wazzup.Messaging;
using Wazzup.Messaging.Exceptions;
using Wazzup.Messaging.Models;
using Wazzup.Web.Utils;

namespace Wazzup.Web.Controllers
{
	[Route("message")]
	public class MessageController : Controller
	{
		private readonly ISocketPoolManager _socketPoolManager;
		private readonly ISocketPool _socketPool;
		private readonly IUserPool _userPool;
		public MessageController(ISocketPool socketPool, ISocketPoolManager socketPoolManager, IUserPool userPool)
		{
			_socketPoolManager = socketPoolManager;
			_socketPool = socketPool;
			_userPool = userPool;
		}
		public async Task Connect([FromQuery]Guid uid)
		{
			var user = _userPool.GetUser(uid);
			using (WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync())
			{
				try
				{
					_socketPool.AddSocket(user.Id, webSocket);
					await PublishChatList();
					await HandleSocket(user, webSocket);
				}
				catch(SocketException ex)
				{
					HttpContext.Response.StatusCode = ex.StatusCode;
					_userPool.RemoveUser(uid);
					await PublishChatList();
				}
			}
		}

		private async Task HandleSocket(User user, WebSocket webSocket)
		{
			var buffer = new byte[1024 * 4];
			WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			while (!result.CloseStatus.HasValue)
			{
				if (result.Count == 0)
					continue;

				var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
				var outgoingMessage = new OutgoingMessage()
				{
					Sender = new Sender
					{
						Color = user.Color,
						Nickname = user.Nickname
					},
					Text = StringHelper.StripHTML(receivedMessage)
				};

				var outgoingData = Encoding.UTF8.GetBytes(outgoingMessage.SerializeToJson());
				await _socketPoolManager.Publish(outgoingData);

				buffer = new byte[1024 * 4];
				result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			}
			await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
			_socketPool.RemoveSocket(user.Id);
			_userPool.RemoveUser(user.Id);
			await PublishChatList();
		}

		private async Task PublishChatList()
		{
			var chat = new Chat();
			foreach(var user in _userPool.GetAllUsers())
			{
				var chatUser = new ChatUser()
				{
					Color = user.Color,
					NickName = user.Nickname
				};
				chat.ChatUsers.Add(chatUser);
			}

			var chatData = Encoding.UTF8.GetBytes(chat.SerializeToJson());
			await _socketPoolManager.Publish(chatData);
		}
	}
}

