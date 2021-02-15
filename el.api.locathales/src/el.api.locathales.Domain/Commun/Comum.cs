using System;
using System.Collections.Generic;
using System.Text;

namespace el.api.locathales.Domain.Commun
{
    public static class Comum
    {
        public static bool ValidateEnumValue(int valorEnum, Type tipoEnum)
        {
            return Enum.IsDefined(tipoEnum, valorEnum);
        }
    }
}
