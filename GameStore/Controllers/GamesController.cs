using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.DB;
using GameStore.Models;
using Microsoft.AspNetCore.Http;

namespace GameStore.Controllers
{
	public class GamesController : Controller
	{
		private readonly GameContext _context;

		public GamesController(GameContext context)
		{
			_context = context;
		}

		// GET: Games
		public async Task<IActionResult> Index(string SearchString)
		{
			if (Request.Cookies["CartSessionId"] == null)
			{
				string sessionId = StartCartSession();
				Session session = new Session()
				{
					Id = sessionId
				};
				_context.Add(session);
				_context.SaveChanges();
			}

			var games = from m in _context.Games
				select m;
			if (!string.IsNullOrEmpty(SearchString))
			{
				games = games.Where(s => s.Title.Contains(SearchString)|| s.GenreId.Contains(SearchString));
			}


			return View(await games.ToListAsync());
		}

		public string StartCartSession()
		{
			string sessionId = System.Guid.NewGuid().ToString();
			CookieOptions options = new CookieOptions();
			Response.Cookies.Append("CartSessionId", sessionId, options);

			return sessionId;
		}


		public List<Game> GetGamesByGenre(string GenreId)
		{
				List<Game> games = _context.Games.Where(x => x.GenreId == GenreId).ToList();
				return games;	
		}

		public IActionResult Details(string id)
		{
			var game = _context.Games.Find(id);

			return View(game);
		}
	}
}
