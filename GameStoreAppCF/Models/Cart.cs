using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStoreAppCF.Models
{
    public class Cart
    {
        Dictionary<Game, int> games { get; set; } = new Dictionary<Game, int>();

        public void Add(Game game)
        {
            if (games.ContainsKey(game))
                games[game]++;
            else
                games[game] = 1;
        }
    }


}