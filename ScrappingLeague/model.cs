using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScrappingLeague
{
    class ChampionsRoot
    {
        public Dictionary<string, ChampionsInfo> Data;
        public string V;
    }
    class ChampionsInfo
    {
        public List<string> Tags;
        public ChampionImage Image;
        public string Name;
    }
    public struct ChampionImage
    {
        public string Full;
    }
}
