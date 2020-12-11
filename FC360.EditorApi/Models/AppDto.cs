namespace FC360.EditorApi.Models
{
	using System.Collections.Generic;

	public record AppDto(
		Dictionary<string, LinkDto> Links
	);
}
