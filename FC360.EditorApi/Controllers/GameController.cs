namespace FC360.EditorApi.Controllers
{
	using FC360.Core;
	using FC360.EditorApi.Models;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Linq;
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
					{ "game.sprites", GetAllGameSpritesLink() },
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

		[HttpGet("sprites")]
		public ActionResult<AllGameSpritesDto> GetAllGameSprites([FromServices] FantasyConsole fc)
		{
			if (!IsGetGameAvailable(fc))
			{
				return NotFound();
			}

			return Ok(new AllGameSpritesDto(
				fc.Mem.SpriteBuffer.Sprites.Select(s => s.Select(b => (int)b).ToArray()).ToArray(),
				new Dictionary<string, LinkDto>
				{
					{ "parent", GetGameLink() },
					{ "self", GetAllGameSpritesLink() },
					{ "sprite[i]", GetGameSpriteLink(":i") },
				}));
		}

		[HttpGet("sprites/{i}")]
		public ActionResult<GameSpriteDto> GetGameSprite(int i, [FromServices] FantasyConsole fc)
		{
			if (!IsGetGameAvailable(fc))
			{
				return NotFound();
			}

			return Ok(new GameSpriteDto(
				fc.Mem.SpriteBuffer[(byte)i].Select(b => (int)b).ToArray(),
				new Dictionary<string, LinkDto>
				{
					{ "parent", GetAllGameSpritesLink() },
					{ "self", GetGameSpriteLink(i.ToString()) }
				}));
		}

		[HttpPut("sprites/{i}")]
		public ActionResult<GameSpriteDto> PutGameSprite(int i, [FromBody] GameSpriteDto dto, [FromServices] FantasyConsole fc)
		{
			if (!IsGetGameAvailable(fc))
			{
				return NotFound();
			}

			fc.Mem.SpriteBuffer[(byte)i] = new Sprite(dto.Data.Select(i => (byte)i).ToArray());
			fc.Sys.Game.Save();

			return Redirect(GetGameSpriteLink(i.ToString()).Href);
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

		public static LinkDto GetAllGameSpritesLink() =>
			new LinkDto(HttpMethod.Get.Method, "/api/fc360/game/sprites");

		public static LinkDto GetGameSpriteLink(string i) =>
			new LinkDto(HttpMethod.Get.Method, $"/api/fc360/game/sprites/{i}");
	}
}
