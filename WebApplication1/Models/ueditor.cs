namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ryougichan.ueditor")]
    public class ueditor
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Required]
        public string content { get; set; }
    }
}