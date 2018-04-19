using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script;
using Newtonsoft.Json;


namespace puertos
{
    class gameResponse
    {
        public game game { get; set; }
        public hero hero { get; set; }
        public string token { get; set; }
        public string viewUrl { get; set; }
        public string playUrl { get; set; }

    }
    class game
    {
        public string idGame { get; set; }
        public int turns { get; set; }
        public int maxTurns { get; set; }
        public List<hero> heroes { get; set; }
        public board board { get; set; }
        public bool finished { get; set; }
    }
    //////////////////7/ dentro de la clase game ///////////////////////////////////
    
 
    class board
 {
     public int size { get; set; }
     public string tiles { get; set; }

 }
    //////////////////////////////7// fin de clase game //////////////////////
    class hero
    {
        public int id { get; set; }
        public string name { get; set; }
        public string userId { get; set; }
        public int elo { get; set; }
        public pos pos { get; set; }
        public int life { get; set; }
        public int gold { get; set; }
        public int mineCount { get; set; }
        public spawnPos spawnPos { get; set; }
        public bool crash { get; set; }
    }


    ////////////// dentro de hero ///////////////
    class pos
    {
        public int x { get; set; }
        public int y { get; set; }
    }
    class spawnPos
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    ////////////// dentro de hero ///////////////


}
