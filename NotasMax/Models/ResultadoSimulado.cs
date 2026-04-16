namespace NotasMax.Models
{
    public class ResultadoSimulado
    {
        public Guid Aluno_Id { get; set;  }
        public Usuario Aluno { get; set; } = new Usuario();
        public decimal Nota {  get; set; }
        public int Acertos { get; set; } 
        public enum NotificacaoEnviada { enviada=1, pendente=0}
    }
}
