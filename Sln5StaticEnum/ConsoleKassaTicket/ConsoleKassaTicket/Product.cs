using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleKassaTicket
{
    internal class Product
    {
        public string Naam { get; set; }
        public decimal Eenheidsprijs { get; set; }
        public string Code { get; set; }


        public Product() { }
        public Product(string naam, decimal eenheidsprijs, string code)
        {
            Naam = naam;
            Eenheidsprijs = eenheidsprijs;

            if (!string.IsNullOrEmpty(code) && code.Length == 6 && (code[0] == 'p' || code[0] == 'P'))
            {
                Code = char.ToUpper(code[0]) + code.Substring(1);
            }
            else
            {
                throw new ArgumentException("Code must be a 6-character string starting with the letter 'p' and not null or empty.");
            }
        }

        public static bool ValideerCode(string code)
        {
            return !string.IsNullOrEmpty(code) && code.Length == 6 && (code[0] == 'p' || code[0] == 'P');
        }

        public override string ToString()
        {
            return $"({Code}) {Naam}: {Eenheidsprijs:C}";
        }


    }
}
