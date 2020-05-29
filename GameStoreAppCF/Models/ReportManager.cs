using GameStoreAppCF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStoreAppCF.Models
{
    public class ReportManager
    {
        public static List<GamesToReportViewModel> PrepareGamesToReport(GameStoreDB db, DateTime dateFrom, DateTime dateTo)
        {
            var gamesToReport = new List<GamesToReportViewModel>();
            var games = new List<Game>();
            var actualDateTo = dateTo.AddDays(1);
            var orders = (from order in db.Order
                         where order.Date >= dateFrom &&
                         order.Date <= actualDateTo
                         select order).ToList();
            foreach (var order in orders)
            {
                foreach (var game in order.Game)
                {
                    games.Add(game);
                }
            }
            var groupedGames = from game in games group games by game.Name;
            foreach (var game in groupedGames)
            {
                gamesToReport.Add(new GamesToReportViewModel()
                {
                    Name = game.Key,
                    Quantity = game.Count(),
                    Price = game.First().First().Price,
                    Total = game.First().First().Price * game.Count()
                });
            }
            return gamesToReport;
            
        }
    }
}