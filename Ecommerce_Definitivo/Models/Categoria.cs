using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        public int CategoriaId { get; set; }
        [Display(Name = "Nome da Categoria")]
        public String CategoriaNome { get; set; }

        public List<produto> Produtos { get; set; }
    }
}