using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LivrariaJulio.WebApi.Models
{
    [Table("Autor")]
    public class Autor
    {
        [Key]
        public int AutorID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
    }
}