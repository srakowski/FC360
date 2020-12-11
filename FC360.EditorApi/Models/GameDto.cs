namespace FC360.EditorApi.Models
{
	using System.Collections.Generic;

	public record GameDto(
		string Name,
		Dictionary<string, LinkDto> Links
		);
}
