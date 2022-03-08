using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XamrinFirstApp.Models
{
    public class Country
    {
        public Country()
        {
            this.CreationDateTime = DateTime.Now;
        }

        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(4)]
        public string ShortName { get; set; }

        public DateTime CreationDateTime { get; }
    }
}
