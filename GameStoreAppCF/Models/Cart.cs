using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStoreAppCF.Models
{
    [Serializable]
    public class Cart
    {
        public Dictionary<string, int> Games { get; set; } = new Dictionary<string, int>();

        public void Add(string game)
        {
            if (Games.ContainsKey(game))
                Games[game]++;
            else
                Games[game] = 1;
        }

        public void Delete(string game)
        {
            Games.Remove(game);
        }
    }


}