using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    [Table("ItemVenda")]
    public class ItemCarrinho
    {
        [Key]
        public int itemVendaId { get; set; }
        public produto produto;
        public int quantidade { get; set; }

        public int vendaId { get; set; }
        public virtual venda venda { get; set; }
        public ItemCarrinho(produto produto, int quantidade)
        {
            this.produto = produto;
            this.quantidade = quantidade;
        }
    }
}