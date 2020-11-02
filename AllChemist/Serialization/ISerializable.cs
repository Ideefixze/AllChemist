using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.GUI.Controllers
{
    /*
     * Interface for all objects that will be serialized and deserialized (e.g.: CellTypeBanks)
     */
    interface ISerializable<T>
    {
        public string Serialize();
        public T Deserialize(string json);
    }
}
