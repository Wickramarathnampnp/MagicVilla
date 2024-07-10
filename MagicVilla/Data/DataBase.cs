using MagicVilla.Model.Dto;

namespace MagicVilla.Data
{
	public static class DataBase
	{
		public static List<VillaDto> VillaList =new List<VillaDto> { 
			new VillaDto{Id =1,Name="Beach view",Sqrft = 300,Occupency =3},
			new VillaDto{Id =2,Name="Pool view",Sqrft = 400,Occupency =5}
		};
	}
}
