using System;
using System.ComponentModel.DataAnnotations;

namespace PracticeAPI.Models
{
    public class Item
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public long Size { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateUpload { get; set; }

        public string URL { get; set; }
    }
}
