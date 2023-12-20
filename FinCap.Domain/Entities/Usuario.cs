namespace FinCap.Domain.Entities
{
    public class Usuario : Entidade
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string FotoPerfil { get; set; }

        #region Relacionamentos
        public List<Conta> Contas { get; set; }
        public List<Categoria> Categorias { get; set; }
        #endregion
    }
}
