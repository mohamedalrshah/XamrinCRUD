using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XamrinFirstApp.Models
{
    public class Country
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(4)]
        public string ShortName { get; set; }

        public DateTime CreationDateTime { get; set; }

        public List<City> Cities { get; set; }
    }
}
