using FinCap.Domain.Enums;

namespace FinCap.Domain.Entities
{
    public class Transacao : Entidade
    {
        public string Descricao {  get; set; }
        public decimal Valor {  get; set; }
        public Modo Modo { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public Guid UidConta { get; set; }
        public Guid UidCategoria { get; set; }

        #region Relacionamentos
        public Conta Conta { get; set; }
        public Categoria Categoria { get; set; }
        #endregion
    }
}
