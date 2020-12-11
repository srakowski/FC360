namespace FC360.EditorApi.Models
{
	using System.Collections.Generic;

	public record GameCodeDto(
		string Data,
		Dictionary<string, LinkDto> Links
	);
}
