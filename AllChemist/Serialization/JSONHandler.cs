using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AllChemist.Serialization
{
    public static class JSONHandler
    {
        public static T Load<T>(string json)
        {
            var trace = new MemoryTraceWriter();
            
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
            {
                TraceWriter = trace,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented

            });

        }

        public static string Save<T>(T target)
        {
            return JsonConvert.SerializeObject(target, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented

            });
        }
    }
}
/*
 * {2020-11-06T15:51:04.645 Info Deserializing AllChemist.Cells.Ruleset.Ruleset using creator with parameters: author, Name. Path 'AuthorName', line 2, position 15.
2020-11-06T15:51:04.659 Info Started deserializing AllChemist.Cells.CellTypeBank. Path 'CellTypeBank.CellTypes', line 5, position 16.
2020-11-06T15:51:04.664 Info Started deserializing System.Collections.Generic.Dictionary`2[System.Int32,AllChemist.Cells.CellType]. Path 'CellTypeBank.CellTypes.0', line 6, position 10.
2020-11-06T15:51:04.664 Info Started deserializing AllChemist.Cells.CellType. Path 'CellTypeBank.CellTypes.0.Id', line 7, position 13.
2020-11-06T15:51:04.672 Info Started deserializing AllChemist.Cells.CellBehaviour. Path 'CellTypeBank.CellTypes.0.<CellBehaviour>k__BackingField.Rules', line 11, position 18.
2020-11-06T15:51:04.674 Info Started deserializing System.Collections.Generic.List`1[AllChemist.Cells.Rules.IRule]. Path 'CellTypeBank.CellTypes.0.<CellBehaviour>k__BackingField.Rules', line 11, position 20.
2020-11-06T15:51:04.677 Verbose Resolved type 'AllChemist.Cells.Rules.NeighbourChangeTo, AllChemist' to AllChemist.Cells.Rules.NeighbourChangeTo. Path 'CellTypeBank.CellTypes.0.<CellBehaviour>k__BackingField.Rules[0].$type', line 13, position 77.
2020-11-06T15:51:04.677 Info Deserializing AllChemist.Cells.Rules.NeighbourChangeTo using creator with parameters: id, list, to. Path 'CellTypeBank.CellTypes.0.<CellBehaviour>k__BackingField.Rules[0].neighbourId', line 14, position 28.
2020-11-06T15:51:04.677 Info Started deserializing System.Collections.Generic.List`1[System.Int32]. Path 'CellTypeBank.CellTypes.0.<CellBehaviour>k__BackingField.Rules[0].neighbourCounts', line 15, position 34.
2020-11-06T15:51:04.678 Info Finished deserializing System.Collections.Generic.List`1[System.Int32]. Path 'CellTypeBank.CellTypes.0.<CellBehaviour>k__BackingField.Rules[0].neighbourCounts', line 17, position 15.}
*/