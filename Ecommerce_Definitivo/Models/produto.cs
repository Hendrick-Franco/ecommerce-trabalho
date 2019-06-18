using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    [Table("produto")]
    public class produto
    {
        [Key]
        public int produtoId { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }
        public string descricao { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        public byte[] Imagem { get; set; }
        public bool vitrine { get; set; }
    }
}