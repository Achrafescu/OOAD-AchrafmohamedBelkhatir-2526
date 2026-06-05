using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleStaticEnumOefenblad.Exercises.Classes
{
    internal class TekstAnalyse
    {
        private static string[] verbodenWoorden = new string[] { "delete", "drop", "truncate" };
        private static string[] verbodenKarakters = new string[] { "!", "@", "#", "$", "%" };

        static public int AantalWoorden(string tekst)
        {
            if (string.IsNullOrEmpty(tekst)) return 0;
            return tekst.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        }

        static public bool BevatVerbodenWoord(string tekst)
        {
            if (string.IsNullOrEmpty(tekst)) return false;
            foreach (string woord in verbodenWoorden)
            {
                if (tekst.Contains(woord, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        static public bool BevatVerbodenKarakter(string tekst)
        {
            if (string.IsNullOrEmpty(tekst)) return false;
            foreach (string karakter in verbodenKarakters)
            {
                if (tekst.Contains(karakter))
                {
                    return true;
                }
            }
            return false;
        }

        static public bool IsGeschiktVoorTitel(string tekst)
        {
            if (string.IsNullOrEmpty(tekst)) return false;
            if (tekst.Length < 5 || tekst.Length > 30) return false;
            if (BevatVerbodenWoord(tekst) || BevatVerbodenKarakter(tekst)) return false;
            return true;
        }
    }
}
