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
        public string Author_Name { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Review_Content { get; set; }

        public int? Game_ID { get; set; }

        public virtual Game Game { get; set; }
    }
}
