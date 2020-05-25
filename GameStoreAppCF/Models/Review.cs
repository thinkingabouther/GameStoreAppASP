namespace GameStoreAppCF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Имя автора")]
        public string Author_Name { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата отзыва")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Содержание отзыва")]
        public string Review_Content { get; set; }

        public int? Game_ID { get; set; }

        public virtual Game Game { get; set; }
    }
}
