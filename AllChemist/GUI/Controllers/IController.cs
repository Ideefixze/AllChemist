using AllChemist.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.GUI.Controllers
{
    interface IController
    {
        void SetUpModel(World world);
    }
}
