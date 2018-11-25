namespace fomka_web.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Error")]
    public partial class Error
    {
        public int Id { get; set; }

        [Required]
        [StringLength(5000)]
        public string Description { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public int Type { get; set; }

        public DateTime Date { get; set; }
    }
}
