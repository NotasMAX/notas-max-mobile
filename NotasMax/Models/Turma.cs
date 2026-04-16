namespace NotasMax.Models
{
    public class Turma
    {
       public Guid Id { get; set; }
       public int Serie { get; set; }
       public int Ano { get; set; }
       public List<Usuario> Alunos {  get; set; } = new List<Usuario>();
    
    }
}
