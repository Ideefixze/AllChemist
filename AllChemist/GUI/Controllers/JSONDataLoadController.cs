using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AllChemist.GUI.Controllers
{
    class JSONDataLoadController<T>
    {
        public OpenFileDialog fileDialog;

        public JSONDataLoadController(string folderName, string defaultFileName)
        {
            fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + folderName;
            fileDialog.FileName = fileDialog.InitialDirectory +"\\"+ defaultFileName;
        }

        public virtual T GetData() { return default(T); }

        public virtual void ShowOpenFileDialog() { fileDialog.ShowDialog(); }
    }
}
