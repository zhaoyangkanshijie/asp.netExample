namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ryougichan.r_user")]
    public partial class r_user
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [StringLength(100)]
        public string phone { get; set; }

        [Required]
        [StringLength(64)]
        public string password { get; set; }

        [Required]
        [StringLength(100)]
        public string nickname { get; set; }

        public int gender { get; set; }

        public DateTime? birthday { get; set; }

        [StringLength(100)]
        public string region { get; set; }

        [StringLength(150)]
        public string description { get; set; }

        public DateTime regTime { get; set; }

        public DateTime latestLoginTime { get; set; }

        [Required]
        [StringLength(100)]
        public string latestLoginIP { get; set; }

        public int status { get; set; }
    }
}
