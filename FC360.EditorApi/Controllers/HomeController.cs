namespace FC360.EditorApi.Controllers
{
	using FC360.Core;
	using Microsoft.AspNetCore.Mvc;

	[Route("/")]
	public class HomeController : ControllerBase
	{
		[HttpGet]
		public string Index([FromServices] FantasyConsole fc)
		{
			return fc.Mem.ActiveGame;
		}
	}
}
