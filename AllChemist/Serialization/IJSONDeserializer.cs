using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.GUI.Controllers
{
    /*
     * Returns some kind of data 
     */
    interface IJSONDeserializer<T>
    {
        public T GetData();
    }
}
