using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace AllChemist.GUI.Controllers
{
    class JSONFileDeserializer<T> : IJSONDeserializer<T> where T : ISerializable<T>, new()
    {
        public OpenFileDialog fileDialog;

        public JSONFileDeserializer(string folderName, string defaultFileName)
        {
            fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + folderName;
            fileDialog.FileName = fileDialog.InitialDirectory +"\\"+ defaultFileName;
            fileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        }

        public virtual T GetData() 
        {
            string fileContent = "";
            Stream fileStream = fileDialog.OpenFile();
            Console.WriteLine("Opening file: " + fileDialog.FileName);

            using (StreamReader reader = new StreamReader(fileStream))
            {
                fileContent = reader.ReadToEnd();
            }

            fileStream.Close();
            T toBeDeserialized = new T();
            toBeDeserialized.Deserialize(fileContent);
            return toBeDeserialized;
        }

        public virtual void ShowOpenFileDialog() { fileDialog.ShowDialog(); }
    }
}
