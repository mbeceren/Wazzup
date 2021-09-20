using System.Threading.Tasks;

namespace Wazzup.Messaging
{
	public interface ISocketPoolManager
	{
		Task Publish(byte[] data);
	}
}
