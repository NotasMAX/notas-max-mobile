namespace NotasMax.Helpers
{
    public static class TextoHelper
    {

        public static string? ReduzirTexto(string texto, int tamanho =10)
        {

            if (texto == null)
                return "";

            if (texto.Length <= tamanho)
                return texto;

            string[] textoFragmentado = texto.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (textoFragmentado.Length < 2)
                return texto; 

            string primeiraParte = textoFragmentado[0];
            char inicialSegundaParte = textoFragmentado[1][0];
            string resultado = $"{primeiraParte} {inicialSegundaParte}.";

            return resultado;
        }
    }
}
