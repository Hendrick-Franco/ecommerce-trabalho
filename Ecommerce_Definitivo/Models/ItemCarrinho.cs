using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    public class ItemCarrinho
    {
        public produto produto;
        public int quantidade { get; set; }

        public ItemCarrinho(produto produto, int quantidade)
        {
            this.produto = produto;
            this.quantidade = quantidade;
        }
    }
}