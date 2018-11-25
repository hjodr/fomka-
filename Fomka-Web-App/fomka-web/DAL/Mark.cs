namespace fomka_web.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mark")]
    public partial class Mark
    {
        public int Id { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        public int TaskId { get; set; }

        public int UserId { get; set; }

        public double Value { get; set; }

        public virtual Task Task { get; set; }

        public virtual User User { get; set; }
    }
}
