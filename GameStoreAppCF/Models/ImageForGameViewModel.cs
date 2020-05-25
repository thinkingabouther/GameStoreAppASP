using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameStoreAppCF.Models
{
    public class ImageForGameViewModel
    {
        [Key]
        public int Key { get; set; }

        public int GameID { get; set; }

        [NotMapped]
        [Display(Name = "Добавить изображение")]
        public HttpPostedFileBase Image { get; set; }
    }
}