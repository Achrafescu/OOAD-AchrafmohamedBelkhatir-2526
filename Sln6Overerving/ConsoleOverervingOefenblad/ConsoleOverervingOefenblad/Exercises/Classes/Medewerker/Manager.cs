using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleOverervingOefenblad.Exercises.Classes.Medewerker
{
    internal class Manager :Medewerker
    {
        public int TeamGrootte { get; }
        public Manager(string naam, string afdeling, int teamGrootte) : base(naam, afdeling)
        {
            TeamGrootte = teamGrootte;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, team: {TeamGrootte} personen";
        }
    }
}
