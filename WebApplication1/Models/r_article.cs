namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ryougichan.r_article")]
    public partial class r_article
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Required]
        [StringLength(120)]
        public string title { get; set; }

        [StringLength(120)]
        public string subTitle { get; set; }

        [Required]
        [StringLength(100)]
        public string author { get; set; }

        [Required]
        [StringLength(100)]
        public string editor { get; set; }

        [StringLength(150)]
        public string authorDesc { get; set; }

        [StringLength(100)]
        public string license { get; set; }

        public DateTime createTime { get; set; }

        public DateTime updateTime { get; set; }

        [Column("abstract")]
        [StringLength(2000)]
        public string _abstract { get; set; }

        [StringLength(200)]
        public string keywords { get; set; }

        [Required]
        [StringLength(16777215)]
        public string content { get; set; }

        [StringLength(16777215)]
        public string reference { get; set; }

        [StringLength(150)]
        public string thumb { get; set; }
    }
}
