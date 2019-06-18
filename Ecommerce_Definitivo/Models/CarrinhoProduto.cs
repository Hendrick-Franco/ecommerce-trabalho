using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    [Table("carrinhoproduto")]
    public class CarrinhoProduto
    {
        [Key]
        public int carrinhoId { get; set; }
        public conta Produto { get; set; }
        public int quantidade { get; set; }
        public virtual conta Conta { get; set; }
    }
}