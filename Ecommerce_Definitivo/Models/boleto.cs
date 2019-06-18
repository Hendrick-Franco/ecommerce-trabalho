using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    public class boleto : FormaPagamento
    {
        public override double valor { get; set; }
        public string codigo { get; set; }
        public override int pagamentoId { get; set; }
    }
}