using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AllChemist.GUI.Controllers
{
    class CellTypeBankJSONDataLoadController : JSONDataLoadController<CellTypeBank>
    {
        public CellTypeBankJSONDataLoadController(string defaultFilename) : base("rulesets", defaultFilename)
        {

            fileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        }

        public override CellTypeBank GetData()
        {
            string fileContent = "";
            Stream fileStream = fileDialog.OpenFile();
            Console.WriteLine("Opening file: " + fileDialog.FileName);

            using (StreamReader reader = new StreamReader(fileStream))
            {
                fileContent = reader.ReadToEnd();
            }

            fileStream.Close();
            CellTypeBank ctb = new CellTypeBank();
            ctb.LoadFromJson(fileContent);
            return ctb;
        }
    }
}
