namespace NotasMax.Models
{
    public class TurmaDisciplina
    {
      public Guid Turma_Id { get; set; }
      public Turma Turma { get; set; } = new Turma();
      public Guid Professor_Id { get; set; }
      public Usuario Usuario { get; set; } = new Usuario();
      public Guid Materia_Id { get; set; }
      public Materia Materia { get; set; } = new Materia();
    }
}
