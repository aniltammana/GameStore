using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.DB;
using Microsoft.AspNetCore.Mvc;
using GameStore.Models;
using Microsoft.AspNetCore.Http;

namespace GameStore.Controllers
{
	public class LoginController : Controller
	{
		private readonly GameContext _context;

		public LoginController(GameContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			string browseSessionId = Request.Cookies["CartSessionId"];
			if (browseSessionId == null)
			{
				string sessionId = StartCartSession();
				ViewData["CartSessionId"] = sessionId;
				Session session = new Session()
				{
					Id = sessionId
				};
				_context.Add(session);
				_context.SaveChanges();
			}
			else
			{
				Session session = new Session()
				{
					Id = Request.Cookies["CartSessionId"],
					Username = null
				};
				Session session_check = _context.Sessions
					.Where(x => x.Id == browseSessionId).FirstOrDefault();
				if (session_check == null)
				{
					_context.Add(session);
					_context.SaveChanges();
				}

			}

			return View();
		}

		[HttpPost]
		public ActionResult Authorize(GameStore.Models.User user)
		{
			
			using (var db = new GameContext())
			{
				var userDetails = db.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
				//checks for matching user and password and returns user
				if (userDetails == null)
				{
					user.LoginErrorMessage = "Wrong username or password";
					//returns to /Login/Index for the user to login again
					return View("Index", user);
				}
				else
				{
					HttpContext.Session.SetString("username", user.Username);
					//tie username with CartSessionId
					string userSession = Request.Cookies["CartSessionId"];
					Session session = _context.Sessions.Find(userSession);
					session.Username = user.Username;
					_context.Update(session);
					_context.SaveChanges();

					//proceeds to shop for games in /Home/Index <- pls change this to the shop View
					return RedirectToAction("Index", "Games");
				}
			}

		}

		public string StartCartSession()
		{
			string sessionId = System.Guid.NewGuid().ToString();
			CookieOptions options = new CookieOptions();
			Response.Cookies.Append("CartSessionId", sessionId, options);

			return sessionId;
		}

		public ActionResult LogOut()
		{
			//clear the session cookie and direct to login page at /Login/Index
			HttpContext.Session.Clear();
			EndSession();
			return RedirectToAction("Index", "Login");
		}

		public IActionResult EndSession() 
		{
			return RedirectToAction("Index"); 
		}

	}
}