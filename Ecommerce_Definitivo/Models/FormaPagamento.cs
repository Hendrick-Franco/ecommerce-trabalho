using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    [Table("formapagamento")]
    public abstract class FormaPagamento : IFormaP
    {
        [Key]
        public abstract int pagamentoId { get; set; }
        public abstract double valor { get; set; }
    }
}