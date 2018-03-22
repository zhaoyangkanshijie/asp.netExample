namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ryougichan.r_admin")]
    public partial class r_admin
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Required]
        [StringLength(64)]
        public string password { get; set; }

        [Required]
        [StringLength(100)]
        public string nickname { get; set; }

        public DateTime latestLoginTime { get; set; }

        [Required]
        [StringLength(100)]
        public string latestLoginIP { get; set; }

        [Required]
        [StringLength(100)]
        public string authority { get; set; }

        public int status { get; set; }
    }
}
