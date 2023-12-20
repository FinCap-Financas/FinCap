using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCap.Domain.Entities
{
    public abstract class Entidade
    {
        public Guid Uid { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool Deletado { get; set; }
    }
}
