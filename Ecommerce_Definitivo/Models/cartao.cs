using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    public class cartao : FormaPagamento
    {
        [Key]
        public string numero { get; set; }
        public string datevalidade { get; set; }
        public string codigoseguranca { get; set; }
        public conta conta;

    }
}