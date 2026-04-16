namespace NotasMax.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome {get; set;}
        public string Email { get; set;}
        public string TelefoneContato { get; set; }
        public string Senha { get; set;}
        public enum Tipo_Usuara
            { 
                aluno = 0, 
                professor,
                administrador
            }
        public string TelefoneResponsavel { get; set; }
        public string NomeResponsavel { get; set; }


        public string ResetToken { get; set; }
        public DateTime ResetTokenExpiry { get; set; }
    }
}
