using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using AllChemist.Serialization;

namespace AllChemist.Serialization
{
    public class JSONFileDeserializer<T> : IJSONDeserializer<T>
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
            T toBeDeserialized = JSONHandler.Load<T>(fileContent);
            return toBeDeserialized;
        }

        public virtual void ShowOpenFileDialog() { fileDialog.ShowDialog(); }
    }
}
