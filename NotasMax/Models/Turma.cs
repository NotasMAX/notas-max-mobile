using NotasMax.Models.Usuarios;

namespace NotasMax.Models
{
    public class Turma
    {
        public Guid Id { get; set; }
        public int Serie { get; set; }
        public int Ano { get; set; }
        public List<string> Alunos { get; set; } = new List<string>();

        public string SerieComposta
        {
            get
            {
                return (Serie + "º EM");
            }
        }

    }
}
