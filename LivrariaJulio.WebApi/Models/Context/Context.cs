using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LivrariaJulio.WebApi.Models.Context
{
    public class Context : DbContext
    {
        public Context() : base("MySql")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Autor> Autores { get; set; }

        public DbSet<Editora> Editoras { get; set; }

        public DbSet<Livro> Livros { get; set; }
    }
}