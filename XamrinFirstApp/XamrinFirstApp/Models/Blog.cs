using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SQLitePCL;

namespace XamrinFirstApp.Models
{
    public class Blog
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Url { get; set; }

        //public List<Post> Posts { get; set; } = new List<Post>();
    }
}
