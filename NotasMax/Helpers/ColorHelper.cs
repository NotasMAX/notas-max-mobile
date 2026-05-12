using System;
using System.Collections.Generic;
using System.Text;

namespace NotasMax.Helpers
{
    public static class ColorHelper
    {
        public static Color DefinirCor(double valor)
        {
            if (valor < 5)
            {
                return Color.FromArgb("#EF4343");
            }
            else if (valor >= 5 && valor < 7)
            {
                return Color.FromArgb("#FFBB0F");
            }
            return Color.FromArgb("#2EB867");
        }
    }
}
