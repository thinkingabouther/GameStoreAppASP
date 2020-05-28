using GameStoreAppCF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStoreAppCF.ViewModels
{
    public class GamesToReportViewModel
    {
        [Display(Name="Название игры")]
        public string Name { get; set; }
        [Display(Name = "Количество проданных игр")]
        public int Quantity { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }
        [Display(Name = "Общая выручка")]
        public double Total { get; set; }
    }
}