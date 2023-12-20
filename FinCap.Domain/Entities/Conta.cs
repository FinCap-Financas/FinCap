using FinCap.Domain.Enums;

namespace FinCap.Domain.Entities
{
    public class Conta : Entidade
    {
        public string Descricao { get; set; }
        public Tipo Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public Guid UidUsuario { get; set; }

        #region Relacionamentos
        public Usuario Usuario { get; set; }
        public List<Transacao> Transacoes { get; set; }
        #endregion
    }
}
