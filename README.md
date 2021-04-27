# Power BI Localization is now as simple as Localizing Web Resource Files. !!!

We use an external tool called  [Tabular Editor](https://tabulareditor.com/) to localize Power BI Chart titles, legends, chart series, etc.  

After installing you should see the Tabular Editor tab in the External Tools menu.

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/TabularEditorView.png)

# Steps Overview

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Flow.PNG)

# Step 1 Add Power BI Data Table

<div style="display: inline-block;">
<code class="language-c">Create a Power BI Empty table “LocalizationTable” to hold measures for chart titles and legends, because translations for some objects (titles & legends) should be implemented using DAX formula.</code>
</div>

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/LocalizationTable.PNG)

To create a new empty table click on the Create Table option in the Home menu of your Power BI Desktop. In the Create Table dialog box type in the name of your table in the Name field, for example "LocalizationTable", and then click on the Load button.

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/CreateTable.png)

# Step 2 Add Languages
You can add languages individually via the New Translation option in Tabular Editor.  Right click on the Translations model and select the New Translation drop down option.  

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/AddLang.PNG)

Select your desired culture in the Select Culture dialog and click OK.

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/SelectCulture.png)

## Bulk language Import 
To add many languages at one time use a file like [languages.json](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/151806907d072fa69868c6e88a0182fbbafd5406/languages.json) via the Import Translations... option from the Tabular Editor Tools menu.

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/AddLanguages.PNG)

From the Import Translations dialog box click on the Import button to add the languages from the .json file.

# Step 3 Key-in en-US seeds
Using the Tabular Editor Key in "English phrases" for every Power BI table value and measures as shown below. 

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/en-USSeed.PNG)

From the table select the column you want to translate and in the properties window scroll down to the Translated Names property.  Type in the name of the column in the "en-US" row.

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/KeyInColumn.png)

To localize Titles and Legends you will need to add measures to hold those values.  In your LocalizationTable right click to bring up the drop down menu.  Select Create New and then select Measure.

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/CreateNewMeasure.png)

In the table replace the text "New Measure" with the text of the title or legend you want to localize.  Be sure to append the measure name with "Title" or "Legend" as appropriate.  We use this convention to extract the measures for localization.

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/NewMeasure.png)

Then key-in the value of the title or legend just like you did for the table column values.  Note:  Be sure to leave off "Title" or "Legend" from the actual string.

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/KeyInMeasure.png)

# Step 4 Generate Localization .resx files
Use the GenerateResx.cs script (attached below) to generate .resx files for localization.  Before running the script you will need to modify the CatagoryName and ReportName variables.  Some teams use the CategoryName to help categorize the strings.  You can create a new category name if needed, for example "UIString".  ReportName is the name of your PowerBI report.

The script uses the the CatagoryName and ReportName variables to distinguish the localization keys (IDs) in the same file.

Finally, be sure to update the path in the resxFilename variable to match the desired folder location on your machine.
    
 ## script
 [GenerateResx.cs](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/GenerateResx.cs)
    
After you've modified the script, copy it, and paste it in the Advanced Scripting tab in Tabular Editor.  Click on the Run Script button (the green arrow) to run the script.

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/RunScript.png)

Verify the generated .resx files.  Look in the folder you specified in the generate script (the resxFilename variable).  Important Note:  Don’t modify the columns Name & Comment (<Val><![CDATA[???]]></Val> and <Cmt Name="Dev">???</Cmt>) because the Name column holds schema object name, and the Comment column holds the conventional long resource name delimited by “#” to load the translated strings back into Power BI reports.  We used this convention to allow us to hold strings from multiple dashboards in a single .resx resource file.
    
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/ResxFormat.PNG)

# Step 5 Send Localization .resx files

Follow your normal localization process to localize the generated .resx files.  You can keep these .resx files in Azsure DevOps for versioning and coordination with external localization teams. 

# Step 6 Import "translated resx" files
 
When you receive the localized .resx files consider keeping them in Azure devops for versioning and coordination with your external localization teams. 

Reminder:  Before running the import script be sure to verify the values of the CategoryName, ReportName, and Translated_FilesFolder variables in your script are updated correctly.
 
  ## script
 [Importtranslations.cs](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Importtranslations.cs)
 
 After you've modified the script, copy it, and paste it in the Advanced Scripting tab in Tabular Editor.  Click on the Run Script button (the green arrow) to run the script.

Verify imported translations

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Imported.PNG)

# Step 7 Applying translations to PBI Objects

## Simple tiles
Use measure/table property as shown below.
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Tile.PNG)

## Grid Headers 
Use table property/field as it is, don't format after dropping the property or fields.     
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Grid.PNG)
    
## Chart Legends (Chart Series)
Use table property/field as it is, don't format after dropping the property or fields.     

## Chart Titles
We use Expression-based titles for all types of charts.  The expression formula works against data, so we push all title keys into the Power BI table. Use the same “LocalizationTable” table you created earlier to store the values.
    
Follow steps "A"-"E" below to extract title keys and paste them to the Power BI data table. 
    
## Step - A Extract Title Keys to Power BI Localization Data Table
The attached script below will help you to extract the conventional titles and legends into an another resource file “titleandlegend.resx”, so that you can easily copy it from the Visual Studio editor to your Power BI table editor by using universal copy/paste shortcuts.

 ## script
[ExtractLegendAndTitle.cs](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/ExtractLegendAndTitle.cs)

## Step - B Open extracted Resx file "titleandlegend.resx" in visualstudio to copy the table
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/5e5319df1e18bebab1632fcdff6b5e3028978daa/ExtractedTitle.png)

## Step - C Copy the Clipboard data to Power BI Table "LocalizationTable"
From the Home menu in Power BI Desktop select the Transform data tab.
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/TransformData.png)

Select the LocalizationTable and click on the gear icon in the Source step under APPLIED STEPS.
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/PastingLegendsTitles.png)

In the Create Table dialog box select the table and paste in your copied data from Visual Studio.  Click on the OK button.
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/PastingLegendsTitles2.png)

Right click on your column headers to rename them, ObjectName, TranslatedText, and LangId.
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/PastingLegendsTitles3.png)

Make sure your table column names are as highhighted below so that Step-D DAX expression works without many changes.
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/92d762995486bff9d40e0ca314f5f4690efd8b7a/CopyTable.PNG)


## Step - D Ensure DAX expressions for all title measures  
For each measure in your table copy the DAX expression below,

#TitleMeasureName = VAR UserPreferedLanguage =
USERCULTURE()   
RETURN
CALCULATE (
SELECTEDVALUE ( LocalizationTable[TranslatedText] ),
FILTER ( LocalizationTable, LocalizationTable[LangId] = UserPreferedLanguage ),
LocalizationTable[ObjectName] = UserPreferedLanguage & "#TitleMeasureName"
)

and paste it in the Expression Editor tab window in the Tabular Editor.  Update the #TitleMeasureName to match the value of your measure.

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/ExpressionEditor.png)

Repeat this process for each translatable measure in your table.

## Step - E Chart title configuration.
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Title.PNG)

# Verify on Power BI Embedded Solution.
Publish the PBIX to a Premium PBI workspace (not the Power BI Premium sku). Use either method below to verify the localization of your report:
1.	By changing the browser language.
2.	By using the embedded URL, by adding “&language=languageCode&formatlocale= languageCode” at the end. 
languageCode is the language we want to display, for example en-US, ja-JP, de-DE etc. 
ex: https://app.powerbi.com/reportEmbed?reportId=e5f7e55d-0934-4ad8-8eb1-be40693ed5bf&autoAuth=true&ctid=b3b8586a-63b1-46d8-a96d-831495d8a757&config=eyJjbHVzdGVyVXJsIWx5c2lzLndpbmRvd3MubmV0LyJ9&filter=LocalizationLanguageLocale%2FLanguageLocale%20eq%20%27en-US%27&language=en-US&formatlocale=en-US


![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Embed.PNG) 


# Localization result.
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Result.png) 


# Power BI dashboards Localization is now as simple as Localizing Web Resource Files. !!!
