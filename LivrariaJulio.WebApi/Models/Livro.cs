using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LivrariaJulio.WebApi.Models
{
    [Table("Livro")]
    public class Livro
    {
        [Key]
        public int LivroID { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        public int AutorID { get; set; }

        [ForeignKey("AutorID")]
        public virtual Autor Autor { get; set; }

        public int EditoraID { get; set; }

        [ForeignKey("EditoraID")]
        public virtual Editora Editora { get; set; }
    }
}