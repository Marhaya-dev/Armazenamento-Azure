using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoA3.Models
{
    public class Arquivo
    {
        public int Id { get; set; }

        [Required]
        public string File { get; set; }

        public DateTime? Data { get; set; }
    }
}
