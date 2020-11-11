using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllChemist.GUI.Controllers;
using Newtonsoft.Json;

namespace AllChemist.Cells
{
    /// <summary>
    /// Holds all CellTypes. First element in this container wrapper is a default CellType.
    /// </summary>
    public class CellTypeBank
    {
        /// <summary>
        /// All available CellTypes in this CellTypeBank.
        /// </summary>
        public Dictionary<int, CellType> CellTypes { get; private set; }

        public CellTypeBank()
        {
            CellTypes = new Dictionary<int, CellType>();
        }
        /// <summary>
        /// Returns a default CellType in this CellTypeBank.
        /// </summary>
        /// <returns>First element from the container</returns>
        public CellType GetDefaultCellType()
        {
            return CellTypes[0];
        }

    }
}
