using System.Collections;
using System.ComponentModel.Design;
using System.IO;
using System.Resources;

var CategoryName = "PowerAppsAdmin";
var ReportName ="DailyUsage.Pbix";
var Translated_filefolder = @"C:\gopip\pp\Admin-Analytics\1023\PowerPlatformAdminAnalytics\Dashboards\PbixLocalization\Resources\";

List<string> langs = new List<string>();
var langFiles = Directory.GetFiles(Translated_filefolder);
foreach (var langFile in langFiles)
{
    langs.Add(langFile.Replace(Translated_filefolder,""));
}

foreach (var modelLanguage in langs)
{
    var Translated_file = Translated_filefolder + modelLanguage;
    var language = modelLanguage.Replace(".resx","");
    ResXResourceReader rr = new ResXResourceReader(Translated_file);
    rr.UseResXDataNodes = true;
    IDictionaryEnumerator dict = rr.GetEnumerator();
    while (dict.MoveNext())
    {
         ResXDataNode node = (ResXDataNode)dict.Value;
         var objectName = node.Name;
         var comment = node.Comment;
         var value1 = node.GetValue((ITypeResolutionService)null);
         var commentArray = comment.Split('#');
         if (commentArray[0]== CategoryName && commentArray[1]== ReportName) {
             if (commentArray[2]=="Table") {
             Model.Tables[commentArray[3]].Columns[objectName].TranslatedNames[language] = value1.ToString();
             }
            if (commentArray[2]=="Measure") {
            Model.Tables[commentArray[3]].Measures[objectName].TranslatedNames[language] = value1.ToString();
             }
         }
    }
}
"All Language files are imported"