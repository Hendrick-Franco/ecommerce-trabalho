using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace Ecommerce_Definitivo.Models
{
    public class Context : DbContext

    {

        public Context() : base("Baseecommerce")
        {
        }

        public DbSet<conta> Conta { get; set; }
        public DbSet<produto> produto { get; set; }
        public DbSet<endereco> endereco { get; set; }
        public DbSet<venda> venda { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ItemCarrinho> ItemVenda { get; set; }



        public DbSet<FormaPagamento> formpagamento { get; set; }

        //public System.Data.Entity.DbSet<Ecommerce_Definitivo.Models.cartao> cartao { get; set; }

        //public System.Data.Entity.DbSet<Ecommerce_Definitivo.Models.boleto> boleto { get; set; }

    }
}
