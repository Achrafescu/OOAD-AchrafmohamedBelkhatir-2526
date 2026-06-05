using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleOverervingOefenblad.Exercises.Classes.ValidatieRegel
{
    internal class MagNietBevattenRegel : ValidatieRegel
    {
        public List<string> VerbodenWaarde { get; private set; }

        public MagNietBevattenRegel(List<string> verbodenWaarde)
        {
            VerbodenWaarde = verbodenWaarde;
        }

        public override bool IsGeldig(string waarde)
        {
            foreach (string verboden in VerbodenWaarde)
            {
                if (waarde.Contains(verboden, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }


        public override string FoutBoodschap => $"Waarde mag geen van de volgende woorden bevatten: '{String.Join(", ", VerbodenWaarde)}'.";    
    }
}
