namespace FinCap.Domain.Entities
{
    public class Categoria : Entidade
    {
        public string Descricao { get; set; }
        public bool Publica { get; set; }
        public Guid UidUsuario { get; set; }

        #region Relacionamentos
        public Usuario Usuario { get; set; }
        public List<Transacao> Transacoes { get; set; }
        #endregion
    }
}
