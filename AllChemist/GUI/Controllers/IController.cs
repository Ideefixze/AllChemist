﻿using AllChemist.Model;

namespace AllChemist.GUI.Controllers
{
    /// <summary>
    /// Controller interface. Can be set up for some model to apply changes to.
    /// </summary>
    interface IController
    {
        void SetUpModel(World world);
    }
}
