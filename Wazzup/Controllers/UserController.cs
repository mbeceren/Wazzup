using Microsoft.AspNetCore.Mvc;
using Wazzup.Identity;
using Wazzup.Identity.Exceptions;
using Wazzup.Identity.Models;
using Wazzup.Web.Models;
using Wazzup.Web.Utils;

namespace Wazzup.Web.Controllers
{
	[Route("user")]
	public class UserController : Controller
	{
		private readonly IUserPool _userPool;
		public UserController(IUserPool userPool)
		{
			_userPool = userPool;
		}

		[Route("add")]
		[HttpPost]
		public IActionResult CreateUser([FromBody]AddUserRequest request)
		{
			try
			{
				var user = new User(StringHelper.StripHTML(request.Nickname));
				_userPool.AddUser(user);
				return Json(user.Id);
			}
			catch (UserNickNameAlreadyExistException)
			{
				return BadRequest();
			}
		}
	}
}
