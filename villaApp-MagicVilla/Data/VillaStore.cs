using villaApp_MagicVilla.Models.Dto;

namespace villaApp_MagicVilla.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> VillaList = new List<VillaDto>()
        {
           new VillaDto { Id = 1, Name ="Kelowna"},
           new VillaDto { Id = 2, Name ="Kamploops"}
        };
           
     }
}
