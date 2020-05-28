using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStoreAppCF.ViewModels
{
    public class GamesToCartViewModel
    {
        [Display(Name = "Игры")]
        public string Name { get; set; }
        [Display(Name = "В корзине")]
        public int Quantity { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }
        [Display(Name = "Итог")]
        public double Total { get; set; }
    }
}