# PowerBI Localization

We use external tool called "Tabular editor" [Tabular Editor](https://tabulareditor.com/) to localize Power BI Chart titles, legends,  chart series, etc.  


# Steps

![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Flow.PNG)

# Step 1 Add Power BI Data Table.

<div style="-webkit-column-count: 2; -moz-column-count: 2; column-count: 2; -webkit-column-rule: 1px dotted #e0e0e0; -moz-column-rule: 1px dotted #e0e0e0; column-rule: 1px dotted #e0e0e0;">
    <div style="display: inline-block;">
<code class="language-c">Create a Power BI Empty table “LocalizationTable” to hold measures for chart titles and legends.  Chart Titles and Legends (Property/Field localization is sufficient for Chart series legends, only data driven legends) requires explicit measure for localization purpose.   All these title & legend measure name should contain “Title” or “Legend”, later we are leveraging this convention to extract only these measures and load into the power bi table.
</code>
    </div>
    <div style="display: inline-block;">
       
       ![Alt text](https://github.com/gopinathp1978ms/PowerBI-Localization/blob/main/Flow.PNG)
    </div>
</div>


