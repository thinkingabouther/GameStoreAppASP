namespace GameStoreAppCF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Game")]
    public partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            Review = new HashSet<Review>();
            Order = new HashSet<Order>();
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int Difficulty { get; set; }

        public int Min_Duration { get; set; }

        public int Max_Duration { get; set; }

        public int Min_Players { get; set; }

        public int Max_Players { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public int? Author_ID { get; set; }

        public int? Type_ID { get; set; }

        public int? Genre_ID { get; set; }

        public int? Manufacturer_ID { get; set; }

        public virtual Author Author { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual Type Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Review { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
