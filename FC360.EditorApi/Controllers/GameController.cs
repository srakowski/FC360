namespace FC360.EditorApi.Controllers
{
	using FC360.Core;
	using FC360.EditorApi.Models;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Net.Http;

	[Route("api/fc360/game")]
	[ApiController]
	public class GameController : ControllerBase
	{
		[HttpGet]
		public ActionResult<GameDto> GetGame([FromServices] FantasyConsole fc)
		{
			if (!IsGetGameAvailable(fc))
			{
				return NotFound();
			}

			var gameName = fc.Mem.ActiveGameName;

			return Ok(new GameDto(
				gameName,
				new Dictionary<string, LinkDto>
				{
					{ "parent", FC360Controller.GetRootLink() },
					{ "self", GetGameLink() },
					{ "game.code", GetGameCodeLink() },
				}));
		}

		[HttpGet("code")]
		public ActionResult<GameCodeDto> GetGameCode([FromServices] FantasyConsole fc)
		{
			if (!IsGetGameAvailable(fc))
			{
				return NotFound();
			}

			return Ok(new GameCodeDto(
				fc.Mem.CodeBuffer,
				new Dictionary<string, LinkDto>
				{
					{ "parent", GetGameLink() },
					{ "self", GetGameCodeLink() },
				}));
		}

		[HttpPut("code")]
		public ActionResult PutGameCode(
			[FromServices] FantasyConsole fc,
			GameCodeDto gameCodeDto)
		{
			if (!IsGetGameAvailable(fc))
			{
				return NotFound();
			}

			fc.Mem.CodeBuffer = gameCodeDto.Data;
			fc.Sys.Game.Save();

			return Redirect(GetGameCodeLink().Href);
		}

		public static bool IsGetGameAvailable(FantasyConsole fc)
		{
			var gameName = fc.Mem.ActiveGameName;
			return !string.IsNullOrEmpty(gameName);
		}

		public static LinkDto GetGameLink() =>
			new LinkDto(HttpMethod.Get.Method, "/api/fc360/game");

		public static LinkDto GetGameCodeLink() =>
			new LinkDto(HttpMethod.Get.Method, "/api/fc360/game/code");

		public static LinkDto PutGameCodeLink() =>
			new LinkDto(HttpMethod.Put.Method, "/api/fc360/game/code");
	}
}
