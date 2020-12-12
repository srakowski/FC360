namespace FC360.EditorApi.Models
{
	using System.Collections.Generic;

	public record AllGameSpritesDto(
		int[][] Data,
		Dictionary<string, LinkDto> Links
	);
}
