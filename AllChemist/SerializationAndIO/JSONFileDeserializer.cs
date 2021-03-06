﻿using AllChemist.Serialization;
using Microsoft.Win32;
using System;
using System.IO;

namespace AllChemist.SerializationAndIO
{
    /// <summary>
    /// Deserializes file content to the new object of type T.
    /// </summary>
    /// <typeparam name="T">Type of deserialized object</typeparam>
    public class JSONFileDeserializer<T> : IJSONDeserializer<T>
    {
        public OpenFileDialog fileDialog;

        public JSONFileDeserializer(string folderName, string defaultFileName)
        {
            fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + folderName;
            fileDialog.FileName = fileDialog.InitialDirectory + "\\" + defaultFileName;
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
