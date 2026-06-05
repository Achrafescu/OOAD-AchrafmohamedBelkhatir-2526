using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleOverervingOefenblad.Exercises.Classes.CatalogusItem
{
    internal abstract class CatalogusItem
    {
        public string Titel { get; set; } = string.Empty;
        public string InventarisNummer { get; set; } = string.Empty;
    }
}
