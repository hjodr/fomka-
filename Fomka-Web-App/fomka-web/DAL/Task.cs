namespace fomka_web.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Task")]
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }

        public int PLId { get; set; }

        public int StandardId { get; set; }

        public int DifficultyLevelId { get; set; }

        [StringLength(1000)]
        public string Title { get; set; }

        public virtual DifficultyLevel DifficultyLevel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mark> Marks { get; set; }

        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }

        public virtual Standard Standard { get; set; }
    }
}
