using System.ComponentModel.DataAnnotations;

namespace MagicVilla.Model.Dto
{
	public class VillaDto
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(25)]
		public string Name { get; set; }
		public int Sqrft { get; set; }
		public int Occupency { get; set; }
	}
}
