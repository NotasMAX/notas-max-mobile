namespace NotasMax.Helpers
{
    public static class TextoHelper
    {

        public static string? ReduzirTexto(string texto)
        {

            if (texto == null)
                return "";

            if (texto.Length <= 10)
                return texto;

            string[] textoFragmentado = texto.Split(' ');

            string primeiraParte = textoFragmentado[0];
            char inicialSegundaParte = textoFragmentado[1][0];
            string resultado = $"{primeiraParte} {inicialSegundaParte}.";

            return resultado;
        }
    }
}
