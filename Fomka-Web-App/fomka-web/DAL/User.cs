namespace fomka_web.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string FirstName { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string LastName { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Surname { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public int Type { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Email { get; set; }

        public int Group { get; set; }

        [Required]
        [StringLength(250)]
        public string Login { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        public virtual Group Group1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mark> Marks { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
