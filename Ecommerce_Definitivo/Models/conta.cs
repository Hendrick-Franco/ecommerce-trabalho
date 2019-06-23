using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    [Table("conta")]
    public class conta
    {
        [Key]
        public int contaId { get; set;}
        public string email { get; set;}
        public string senha { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public byte[] Imagem { get; set; }        
        public List<endereco> enderecos = new List<endereco>();        
    }
}