namespace FC360.EditorApi.Controllers
{
	using FC360.Core;
	using FC360.EditorApi.Models;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Net.Http;

	[Route("api/fc360")]
	[ApiController]
	public class FC360Controller : ControllerBase
	{
		[HttpGet]
		public ActionResult<AppDto> GetApp([FromServices] FantasyConsole fc)
		{
			var links = new Dictionary<string, LinkDto>
			{
				{ "self", GetRootLink() },
			};

			if (GameController.IsGetGameAvailable(fc))
			{
				links.Add("game", GameController.GetGameLink());
			}

			return Ok(new AppDto(
				links
				));
		}

		public static LinkDto GetRootLink()
		{
			return new LinkDto(HttpMethod.Get.Method, "/api/fc360");
		}
	}
}
