namespace FC360.EditorApi.Models
{
	using System.Collections.Generic;

	public record GameSpriteDto(
		int[] Data,
		Dictionary<string, LinkDto> Links
	);
}
