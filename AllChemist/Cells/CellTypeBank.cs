using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AllChemist.GUI.Controllers;
using Newtonsoft.Json;

namespace AllChemist.Cells
{
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

        public CellType GetDefaultCellType()
        {
            return CellTypes[0];
        }

        /*
        public string Serialize()
        {
            return JsonConvert.SerializeObject(CellTypes, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            });
        }

        public CellTypeBank Deserialize(string json)
        {
            CellTypes = (Dictionary<int, CellType>)JsonConvert.DeserializeObject(json,typeof(Dictionary<int,CellType>), new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
                
            });
            Console.WriteLine("Loaded Cell Types:");
            foreach(var v in CellTypes)
            {
                Console.WriteLine(v.Value);
            }
            return this;
        }*/

        /*
        public override bool Equals(object obj)
        {

            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                CellTypeBank ctb = (CellTypeBank)obj;
                return Serialize() == ctb.Serialize();
            }
        }
        */

    }
}
