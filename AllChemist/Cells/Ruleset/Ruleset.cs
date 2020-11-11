using AllChemist.GUI.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist.Cells.Ruleset
{
    /// <summary>
    /// Simple ruleset class. Contains CellTypeBank and meta data.
    /// </summary>
    public class Ruleset
    {
        public string AuthorName;
        public string Name;
        public CellTypeBank CellTypeBank;

        public Ruleset(string author, string name)
        {
            AuthorName = author;
            Name = name;
            CellTypeBank = new CellTypeBank();
        }

    }
}
