# PowerBI Localization

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
    
    https://github.com/gopinathp1978ms/PowerBI-Localization/blob/GenerateResx.cs
    
    Verify .resx files [Important don’t modify the columns Name & Comment because the name column holds schema object name, and the comments holds conventional long resource name delimited by “#” to load the translated string back to power bi reports. This convention we followed to keep the single resx file to hold multiple dashboards resource strings.] 
    
    ![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/ResxFormat.PNG)
    
# Step 5 Generate Localization .resx files.
 
    
