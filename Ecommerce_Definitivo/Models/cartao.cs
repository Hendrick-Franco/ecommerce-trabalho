using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    public class cartao:FormaPagamento
    {
       
        public string numero { get; set; }
        public override double valor { get; set; }
        public override int pagamentoId { get; set; }
    }
}