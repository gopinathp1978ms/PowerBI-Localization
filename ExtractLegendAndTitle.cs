using System.Collections;
using System.ComponentModel.Design;
using System.Resources;

var CategoryName = "PowerAutomate";
var ReportName = "DailyUsage.Pbix";
var Translated_filefolder = @"C:\..\PbixLocalization\Resources\";
var resxTitleandLegendFilename = Translated_filefolder + "TitleandLegend.resx";
// Reading existing keys to do update
Hashtable resourceEntries = new Hashtable();

foreach(var modelLanguage in Model.Cultures) {
    var resxFilename = Translated_filefolder + modelLanguage.Name + ".resx";
    
    using (ResXResourceReader reader = new ResXResourceReader(resxFilename) { UseResXDataNodes = true })
    {
        foreach (DictionaryEntry resourceEntry in reader)
        {
            if (resourceEntry.Key.ToString().Contains("Title") || resourceEntry.Key.ToString().Contains("Legend")){
                    ResXDataNode node = resourceEntry.Value as ResXDataNode;
                    var comment = node.Comment;
                    var commentArray = comment.Split('#');
                    if (commentArray[0]== CategoryName && commentArray[1]== ReportName) {
                        var translatedText = node.GetValue((ITypeResolutionService)null);
                        var hashKey = modelLanguage.Name + "#" + resourceEntry.Key.ToString();
                        // construct the node
                        ResXDataNode nodeTitle = new ResXDataNode(hashKey, translatedText) { Comment = modelLanguage.Name };
                        resourceEntries.Add(hashKey, nodeTitle);
                    }
                }
        }
    }
}
// filling up the resource files from hastable
using (ResXResourceWriter resourceWriter = new ResXResourceWriter(resxTitleandLegendFilename))
{
    foreach (DictionaryEntry resourceEntry in resourceEntries)
    {
         resourceWriter.AddResource((ResXDataNode)resourceEntry.Value);
    }
    resourceWriter.Generate();
}
"Title and Legends are extracted to the file name TitleandLegend.resx".Output();