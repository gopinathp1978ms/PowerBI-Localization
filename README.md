# Power BI dashboards Localization is now simple as like Localizing Web Resource Files. !!!

We use external tool called "Tabular editor" [Tabular Editor](https://tabulareditor.com/) to localize Power BI Chart titles, legends,  chart series, etc.  


# Steps

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Flow.PNG)

# Step 1 Add Power BI Data Table.


    <div style="display: inline-block;">
<code class="language-c">Create a Power BI Empty table “LocalizationTable” to hold measures for chart titles and legends, because some objects (titles & legends) translations should be implemented using DAX formula.</code>
    </div>

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/LocalizationTable.PNG)

# Step 2 Add Languages.
Using "Import Translations" option we could add bulk langauges.

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/AddLanguages.PNG)

<div style="-webkit-column-count: 2; -moz-column-count: 2; column-count: 2; -webkit-column-rule: 1px dotted #e0e0e0; -moz-column-rule: 1px dotted #e0e0e0; column-rule: 1px dotted #e0e0e0;">
    <div style="display: inline-block;">
<code class="language-c">{
  "cultures": [
    {
      "name": "en-US"
    },
    {
      "name": "ar-SA"
    },
    {
      "name": "bg-BG"
    }
        ]
        }
        </code>
    </div>
</div>

# Step 3 Key-in en-US seeds.
Key in "English phrases" for every power bi table & measures as like below. 

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/en-USSeed.PNG)

# Step 4 Generate Localization .resx files.
    Leverage attached below scripts to generate the .resx files after modifying the report name and resx folder name as appropriate.  
    We use "Folder Name" and "Report Name" parameters to distinguish the localization keys in the same file.
    
 [GenerateResx.cs](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/56287ea36df16bab1c0275dc5787e06914e76e27/GenerateResx.cs)
    
    
    Verify .resx files [Important don’t modify the columns Name & Comment because the name column holds schema object name, and the 
    comments holds conventional long resource name delimited by “#” to load the translated string back to power bi reports. 
    This convention we followed to keep the single resx file to hold multiple dashboards resource strings.] 
    

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/ResxFormat.PNG)
    

# Step 5 Send Localization .resx files.
 
    Generated .resx files can be kept in Azure devops for versioning & coordination with external localization teams. 

# Step 6 Import "translated resx" files.
 
    Generated .resx files can be kept in Azure devops for versioning & coordination with external localization teams.
    Consider changing the highlighted variables “Reportname, foldername” before running the script through advance scripting.
    
 [Importtranslations.cs](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/56287ea36df16bab1c0275dc5787e06914e76e27/Importtranslations.cs)
 
    Verify imported translations
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Imported.PNG)

# Step 7 Applying translations to PBI Objects.

## Simple tiles
    Use measure/ table property as indicated
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Tile.PNG)

## Grid Headers 
    Use table property/field as it is, dont format after dropping the property or fields.     
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Grid.PNG)
    
## Chart Legends (Chart Series)
   Use table property/field as it is, dont format after dropping the property or fields.     

## Chart Titles.
    Expression-based title for all type of charts. Expression formula works against data , so we should push all localized data 
    into the power bi table. We could use the same “LocalizationTable” table created for the localization purpose.
    
## Step - A Extract Title Keys to Power BI Localization Data Table
    Attached script will help you to extract the conventional titles and legends into an another resource file “globaltitleandlegend.resx”, 
    so that it could be easily copiable from VS editor to Power BI table editor by using universal  copy/paste shortcuts.
    
[ExtractLegendAndTitle.cs](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/f7142054be4b6cd18ce90cc7b31cf7215e2fd5c7/ExtractLegendAndTitle.cs)

## Step - B DAX expressions.  

TitleMakerTrendChart = VAR UserPreferedLanguage =
USERCULTURE()   
RETURN
CALCULATE (
SELECTEDVALUE ( LocalizationTable[TranslatedText] ),
FILTER ( LocalizationTable, LocalizationTable[LangId] = UserPreferedLanguage ),
LocalizationTable[ObjectName] = UserPreferedLanguage & "#TitleMakerTrendChart"
)

## Step - C Chart title configuration.
![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Title.PNG)

# Verify on PBIE
Publish the PBIX to a Premium PBI workspace (not the Power BI Premium sku). 
Verification can be 2 ways as below:
1.	By changing the browser language.
2.	By using the embedded URL, by adding “&language=languageCode&formatlocale= languageCode” at the end. 
languageCode is the language we want to display, say en-US, ja-JP, de-DE etc. 
ex: https://app.powerbi.com/reportEmbed?reportId=e5f7e55d-0934-4ad8-8eb1-be40693ed5bf&autoAuth=true&ctid=b3b8586a-63b1-46d8-a96d-831495d8a757&config=eyJjbHVzdGVyVXJsIWx5c2lzLndpbmRvd3MubmV0LyJ9&filter=LocalizationLanguageLocale%2FLanguageLocale%20eq%20%27en-US%27&language=en-US&formatlocale=en-US


![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Embed.PNG) 


# Power BI dashboards Localization is now simple as like Localizing Web Resource Files. !!!
