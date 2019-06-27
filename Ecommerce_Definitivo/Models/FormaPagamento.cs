using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    [Table("formapagamento")]
    public class FormaPagamento : IFormaP
    {
        [Key]
        public int pagamentoId { get; set; }
        public double valor { get; set; }
    }
}