# PowerBI Localization

We use external tool called "Tabular editor" [Tabular Editor](https://tabulareditor.com/) to localize Power BI Chart titles, legends,  chart series, etc.  


# Steps

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Flow.PNG)

# Step 1 Add Power BI Data Table.

<div style="-webkit-column-count: 2; -moz-column-count: 2; column-count: 2; -webkit-column-rule: 1px dotted #e0e0e0; -moz-column-rule: 1px dotted #e0e0e0; column-rule: 1px dotted #e0e0e0;">
    <div style="display: inline-block;">
<code class="language-c">Create a Power BI Empty table “LocalizationTable” to hold measures for chart titles and legends, because some objects (titles & legends) translations should be implemented using DAX formula.</code>
    </div>
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
