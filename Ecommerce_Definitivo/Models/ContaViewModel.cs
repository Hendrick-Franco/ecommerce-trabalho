using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    public class ContaViewModel
    {
        public Int32 contaId { get; set; }
        [Required(ErrorMessage = "É obrigatório informar um E-mail!", AllowEmptyStrings = false)]
        public string email { get; set; }
        [Required(ErrorMessage = "É obrigatório informar uma senha!", AllowEmptyStrings = false)]
        public string senha { get; set; }
        [Required(ErrorMessage = "É obrigatório informar um nome!", AllowEmptyStrings = false)]

        public string nome { get; set; }
        [Required(ErrorMessage = "É obrigatório informar um CPF!", AllowEmptyStrings = false)]

       
        public string cpf { get; set; }

        [Required(ErrorMessage = "É obrigatório inserir uma imagem!", AllowEmptyStrings = false)]

        [DataType(DataType.Upload)]
        [Display(Name = "Imagem")]
        public HttpPostedFileBase ImageUpload { get; set; }

        
    }
}