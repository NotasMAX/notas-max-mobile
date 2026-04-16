namespace NotasMax.Models
{
    public class Simulado
    {
        public Guid Id { get; set; }
        public int Numero {  get; set; }
        public enum Tipo { objetivo, dissertativo}
        public int bimestre { get; set; }
        public DateTime DataRealidacao { get; set; }
        public Guid Turma_Id { get; set; }
        public Turma Turma { get; set; } = new Turma();
        public List<ConteudoSimulado> Conteudos { get; set; } = new List<ConteudoSimulado>();
    }
}
