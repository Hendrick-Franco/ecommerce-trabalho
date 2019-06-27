using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    [Table("venda")]
    public class venda
    {
        [Key]
        public int vendaId { get; set; }

        public DateTime dataVenda { get; set; }
        public decimal vlrTotal { get; set; }
        public IFormaP formapagamento { get; set; }

        public conta Conta;
    }
}