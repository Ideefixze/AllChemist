﻿using AllChemist.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.GUI.Controllers
{
    public abstract class GUIContextController : IController
    {
        public GUIContextController(MainWindow context)
        {

        }

        public abstract void SetUpModel(World world);

        public virtual void CleanUpModel(World world)
        {
        }
    }
}
