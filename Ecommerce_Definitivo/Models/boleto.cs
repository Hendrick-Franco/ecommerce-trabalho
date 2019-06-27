using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    public class boleto : FormaPagamento
    {
        [Key]
        public int boletoId { get; set; }
        public string codigo { get; set; }
    }
}