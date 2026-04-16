namespace NotasMax.Models
{
    public class ConteudoSimulado
    {
        public Guid TurmaDisciplina_Id {  get; set; }
        public TurmaDisciplina TurmaDisciplina { get; set; } = new TurmaDisciplina();
        public int QuantidadeQuestoes { get; set; } = 0;
        public double Peso { get; set; } = 1.0;
        public List<ResultadoSimulado> Resultados { get; set; } = new List<ResultadoSimulado>();
    }
}
