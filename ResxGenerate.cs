using System.Collections;
using System.ComponentModel.Design;
using System.Resources;

var CategoryName = "TenantLevelAppAdmin";
var ReportName = "MakerActivity.Pbix";
//en-US hashtable to copy all keys to regional resource files
Hashtable resourceEntries_US = new Hashtable();

foreach(var modelLanguage in Model.Cultures) {
    var resxFilename = @"C:\gopip\pp\Admin-Analytics\1023\PowerPlatformAdminAnalytics\Dashboards\PbixLocalization\Resources\" + modelLanguage.Name + ".resx";
    
    // Reading existing keys to do update
    Hashtable resourceEntries = new Hashtable();
    using (ResXResourceReader reader = new ResXResourceReader(resxFilename) { UseResXDataNodes = true })
    {
        try
        {
        foreach (DictionaryEntry resourceEntry in reader)
        {
                ResXDataNode node = resourceEntry.Value as ResXDataNode;
                var comment = node.Comment;
                var commentArray = comment.Split('#');
                var hashkey = commentArray[0] + "#" + commentArray[1] + "#" + commentArray[2] + "#" + commentArray[3] + "#" + resourceEntry.Key.ToString();
                resourceEntries.Add(hashkey, node);
        }
        } catch(Exception ex ){}
    }

    // Columns =============================================================
    foreach(var Column in Model.AllColumns) {
         var enUsCulture=Model.Cultures.Where(s=>s.Name==modelLanguage.Name);
         var translatedText = string.Empty;
         // hastable key exist  check 
         var matchKey= CategoryName + "#" + ReportName + "#Table#" + Column.Table.Name + "#";
         var comment = matchKey;
         foreach(var Language in enUsCulture) {
             translatedText = Column.TranslatedNames[Language.Name];
             comment = comment + Column.TranslatedDescriptions[Language.Name];
         }
         // construct the node
         ResXDataNode node = new ResXDataNode(Column.Name, translatedText) { Comment = comment };
         // hastable key exist  check 
         matchKey= matchKey + Column.Name;
         if (resourceEntries.ContainsKey(matchKey))
         {
             resourceEntries[matchKey]= node;
         } 
         else if (translatedText!= string.Empty) {
             resourceEntries.Add(matchKey, node);
         }
    }

    // Measures =============================================================
    foreach(var Measure in Model.AllMeasures) {
         var enUsCulture=Model.Cultures.Where(s=>s.Name==modelLanguage.Name);
         var translatedText = string.Empty;
         var matchKey= CategoryName + "#" + ReportName + "#Measure#" + Measure.Table.Name + "#";
         var comment = matchKey;
         foreach(var Language in enUsCulture) {
                translatedText = Measure.TranslatedNames[Language.Name];
                comment = comment + Measure.TranslatedDescriptions[Language.Name];
         }
         // construct the node
         ResXDataNode node = new ResXDataNode(Measure.Name, translatedText) { Comment = comment };
         // hastable key exist  check 
         matchKey= matchKey + Measure.Name;
         if (resourceEntries.ContainsKey(matchKey))
         {
             resourceEntries[matchKey]= node;
         } 
         else if (translatedText!= string.Empty) {
             resourceEntries.Add(matchKey, node);
         }
    }
     
    // Clone en-US keys for further regional language processing
        if (modelLanguage.Name=="en-US") {
            using (ResXResourceWriter resourceWriter = new ResXResourceWriter(resxFilename)) {
                var items = resourceEntries.Cast<DictionaryEntry>().OrderBy(entry => entry.Key).ToList();
                
                foreach (DictionaryEntry resourceEntry in items) {
                    // filling up the resource files from hastable
                    resourceWriter.AddResource((ResXDataNode)resourceEntry.Value);
                    
                    //Clone to en-US hashtable
                    var currentNode = (ResXDataNode)resourceEntry.Value;
                    var value1 = currentNode.GetValue((ITypeResolutionService)null);
                    ResXDataNode copyNode = new ResXDataNode(currentNode.Name, value1.ToString()) { Comment = currentNode.Comment };
                    resourceEntries_US.Add(resourceEntry.Key,copyNode);
                }
             resourceWriter.Generate();   
            }
        }
        else {
                // filling up the resource files from hastable
                using (ResXResourceWriter resourceWriter = new ResXResourceWriter(resxFilename))
                {
                    var items = resourceEntries_US.Cast<DictionaryEntry>().OrderBy(entry => entry.Key).ToList();
                    foreach (DictionaryEntry resourceEntry in items)
                    {
                        if (resourceEntries.ContainsKey(resourceEntry.Key.ToString()))
                         {
                             resourceWriter.AddResource((ResXDataNode)resourceEntries[resourceEntry.Key.ToString()]);
                         } 
                         else {
                             var node = (ResXDataNode)resourceEntry.Value;
                             ResXDataNode copyNode = new ResXDataNode(node.Name, "") { Comment = node.Comment };
                             resourceWriter.AddResource(copyNode);
                         }
                    }
                    resourceWriter.Generate();
                }
        }
}
