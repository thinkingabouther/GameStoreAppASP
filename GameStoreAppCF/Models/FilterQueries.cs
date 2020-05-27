using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStoreAppCF.Models
{
    public class FilterQueries
    {
        public static int GetMinDuration()
        {
            using (var db = new GameStoreDB())
            {
                return (from game in db.Game select game.Min_Duration).Min();
            }
        }

        public static int GetMaxDuration()
        {
            using (var db = new GameStoreDB())
            {
                return (from game in db.Game select game.Max_Duration).Max();
            }
        }

        public static int GetMinPlayers()
        {
            using (var db = new GameStoreDB())
            {
                return (from game in db.Game select game.Min_Players).Min();
            }
        }

        public static int GetMaxPlayers()
        {
            using (var db = new GameStoreDB())
            {
                return (from game in db.Game select game.Max_Players).Max();
            }
        }

        public static double GetMinPrice()
        {
            using (var db = new GameStoreDB())
            {
                return (from game in db.Game select game.Price).Min();
            }
        }

        public static double GetMaxPrice()
        {
            using (var db = new GameStoreDB())
            {
                return (from game in db.Game select game.Price).Max();
            }
        }

        public static List<Game> GetGamesParamsWithFilter(string startsWith,
                                                                double minPrice, double maxPrice,
                                                                int minDuration, int maxDuration,
                                                                int minDifficulty, int maxDifficulty,
                                                                int minPlayers, int maxPlayers,
                                                                string genre, string type)
        {
            using (var db = new GameStoreDB())
            {
                var selectedGames = from game in db.Game
                                    where
                                    maxPrice >= game.Price &&
                                    minPrice <= game.Price &&
                                    minDuration <= game.Min_Duration &&
                                    maxDuration >= game.Max_Duration &&
                                    minDifficulty <= game.Difficulty &&
                                    maxDifficulty >= game.Difficulty &&
                                    minPlayers <= game.Min_Players &&
                                    maxPlayers >= game.Max_Players
                                    select game;
                if (genre != null)
                {
                    selectedGames = from game in selectedGames
                                    where game.Genre.Name == genre
                                    select game;
                }
                if (type != null)
                {
                    selectedGames = from game in selectedGames
                                    where game.Type.Name == type
                                    select game;
                }
                if (startsWith != string.Empty)
                {
                    selectedGames = from game in selectedGames
                                    where game.Name.StartsWith(startsWith)
                                    select game;
                }
                return selectedGames.ToList<Game>();
            }
        }
    }
}