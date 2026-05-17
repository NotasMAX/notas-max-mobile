namespace NotasMax.Helpers
{
    public static class TextoHelper
    {

        public static string? ReduzirNome(string? texto, int tamanhoMaximo = 10)
        {

            if (string.IsNullOrEmpty(texto))
                return string.Empty;

            if (texto.Length <= tamanhoMaximo)
                return texto;

            string[] textoFragmentado = texto.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (textoFragmentado.Length < 2)
                return texto;

            int quantidadePartes = textoFragmentado.Length;

            string primeiraParte = textoFragmentado[0];
            char inicialUltimaParte = textoFragmentado[quantidadePartes -1 ][0];
            string resultado = $"{primeiraParte} {inicialUltimaParte}.";

            return resultado;
        }

        public static string ReduzirTexto(string? texto, int tamanhoMaximo)
        {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;
            if (texto.Length <= tamanhoMaximo)
                return texto;
            return texto.Substring(0, tamanhoMaximo) + "...";
        }

        public static string PegarIniciais(string? texto)
        {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;

            string[] textoFragmentado = texto.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int tamanhoTextoFragmentado = textoFragmentado.Length;

            char primeiraLetra = textoFragmentado[0][0];
            char ultimaLetra = textoFragmentado[tamanhoTextoFragmentado - 1][0];
            string resultado = $"{primeiraLetra}{ultimaLetra}";

            return resultado;

        }
    }
}
