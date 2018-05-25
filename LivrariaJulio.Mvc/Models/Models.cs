using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LivrariaJulio.Mvc.Models
{
    public class Autor
    {
        [Key]
        public int AutorID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }
    }

    public class Editora
    {
        [Key]
        public int EditoraID { get; set; }

        [Required]
        public string Nome { get; set; }
    }

    public class Livro
    {
        [Key]
        public int LivroID { get; set; }

        [Required]
        public string Titulo { get; set; }

        public int AutorID { get; set; }

        public virtual Autor Autor { get; set; }

        public int EditoraID { get; set; }

        public virtual Editora Editora { get; set; }
    }
}