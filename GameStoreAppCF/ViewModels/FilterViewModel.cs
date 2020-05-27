using GameStoreAppCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreAppCF.ViewModels
{
    public class FilterViewModel
    {
        public string StartsWith { get; set; }

        public int MaxDifficulty { get; set; }

        public int MinDifficulty { get; set; }

        public int MinDuration { get; set; }
        
        public int MaxDuration { get; set; }

        public double MinPrice { get; set; }

        public double MaxPrice { get; set; }

        public double MinPlayers { get; set; }

        public double MaxPlayers { get; set; }

        public Genre ChosenGenre { get; set; }

        public SelectList Genres { get; set; }

        public SelectList Types { get; set; }

        public Models.Type ChosenType { get; set; }

    }
}