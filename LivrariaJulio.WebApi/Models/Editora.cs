using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LivrariaJulio.WebApi.Models
{
    [Table("Editora")]
    public class Editora
    {
        [Key]
        public int EditoraID { get; set; }

        [Required]
        public string Nome { get; set; }
    }
}