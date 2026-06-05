using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleKlassenOefenblad.Exercises.Classes
{
    internal class Werknemer
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        private decimal _salaris;
        public decimal Salaris 
        {
            get { return _salaris; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Salaris kan niet negatief zijn");
                }
                 _salaris = value;
            } 
        }

        private DateOnly _inDienstSinds;
        public DateOnly InDienstSinds
        { 
            get {return _inDienstSinds ;} 
            set
            { 
                if (value > DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new ArgumentException("Datum indiensttreding kan niet in de toekomst liggen");
                }
                _inDienstSinds = value; 
            } 
        }

        public int Ancienniteit
        {
            get
            {
                DateOnly vandaag = DateOnly.FromDateTime(DateTime.Now);
                int jaren = vandaag.Year - InDienstSinds.Year;
                if (vandaag < InDienstSinds.AddYears(jaren))
                {
                    jaren--;
                }
                return jaren;
            }
        }

        public int Seniority
        {
            get
            {
                if (Ancienniteit < 2)
                {
                    return 0; // junior
                }
                else if (Ancienniteit < 5)
                {
                    return 1; // medior
                }
                else
                {
                    return 2; // senior
                }
            }
        }

        public decimal GeefOpslag(decimal opslagPercentage)
        {

            /*switch (Seniority)
            {
                case 0: opslagPercentage = 0.05; break; // junior
                case 1: opslagPercentage = 0.07; break; // medior
                case 2: opslagPercentage = 0.10; break; // senior
                default: opslagPercentage = 0; break;
            }*/
            decimal opslagBedrag = Salaris * (opslagPercentage / 100);

            return Salaris += opslagBedrag;
        }

    }
}
