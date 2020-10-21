using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace AllChemist
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

        public string SaveToJson()
        {
            return JsonConvert.SerializeObject(CellTypes, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

        public bool LoadFromJson(string json)
        {
            CellTypes = (Dictionary<int, CellType>)JsonConvert.DeserializeObject(json,typeof(Dictionary<int,CellType>), new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            
            return CellTypes != null;

        }

        public override bool Equals(object obj)
        {

            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                CellTypeBank ctb = (CellTypeBank)obj;
                return SaveToJson() == ctb.SaveToJson();
            }
        }

    }
}
