using System.Collections;
using System.ComponentModel.Design;
using System.Resources;

var ReportName = "MakerActivity.Pbix";
var resxTitleandLegendFilename = @"C:\gopip\pp\Admin-Analytics\HelperTool\files\PbiLocalizationResources\Resources\globaltitleandlegend.resx";
// Reading existing keys to do update
Hashtable resourceEntries = new Hashtable();

foreach(var modelLanguage in Model.Cultures) {
    var resxFilename = @"C:\gopip\pp\Admin-Analytics\HelperTool\files\PbiLocalizationResources\Resources\" + modelLanguage.Name + ".resx";
    
    using (ResXResourceReader reader = new ResXResourceReader(resxFilename) { UseResXDataNodes = true })
    {
        foreach (DictionaryEntry resourceEntry in reader)
        {
            if (resourceEntry.Key.ToString().Contains("Title") || resourceEntry.Key.ToString().Contains("Legend")){
                    ResXDataNode node = resourceEntry.Value as ResXDataNode;
                    var translatedText = node.GetValue((ITypeResolutionService)null);
                    var hashKey = modelLanguage.Name + "#" + resourceEntry.Key.ToString();
                    // construct the node
                    ResXDataNode nodeTitle = new ResXDataNode(hashKey, translatedText) { Comment = modelLanguage.Name };
                    resourceEntries.Add(hashKey, nodeTitle);
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
