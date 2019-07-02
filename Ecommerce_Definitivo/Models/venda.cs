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

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime dataVenda { get; set; }
        public decimal vlrTotal { get; set; }
        public IFormaP formapagamento { get; set; }

        public int ContaId { get; set; }
        public virtual conta Conta { get; set; }
    }
}