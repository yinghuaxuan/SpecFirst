Decision table is one of the slim tables defined in FitNesse, which "Supplies the inputs and outputs for decisions. This is similar to the Fit Column Fixture". More details can be found on FitNesse website http://fitnesse.org/FitNesse.UserGuide.WritingAcceptanceTests.SliM.DecisionTable.

When deciding whether a table is decision table, SpecFirst uses the following rules:
- has at least three rows
- the first row must have a single column
- the first and second rows can be either in headers section (thead) or in body section (tbody)
- the first row is not a comment row, e.g. does not just contain the word "comment" (case insensitive)

| Is table a decision table                                                                       ||||
| #Comment                 | Decision Table           | Is Valid? | Validation Error?               |
| ------------------------ | ------------------------ | --------- | ------------------------------- |
| Table with only 1 name   | \<table\>                | false     | Decision table must                |\
| row                      | \<tbody\>                |           | have at least three rows           |\
|                          | \<tr\>                   |           |                                    |\
|                          | \<td colspan="3"\>       |           |                                    |\
|                          | Decision Table Validator |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \</tbody\>               |           |                                    |\
|                          | \</table\>               |           |                                 |   
| Table with               | \<table\>                | false     | Decision table must                |\
| 1 name and               | \<tbody\>                |           | have at least three rows           |\
| 1 header                 | \<tr\>                   |           |                                    |\
| row                      | \<td colspan="3"\>       |           |                                    |\
|                          | Decision Table Name      |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \<tr\>                   |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 1  |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 2  |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 3  |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \</tbody\>               |           |                                    |\
|                          | \</table\>               |           |                                 |   
| Table name               | \<table\>                | false     | The first row of the decision      |\
| row has                  | \<tbody\>                |           | table must have a single column    |\
| more than                | \<tr\>                   |           |                                    |\
| 1 column                 | \<td\>                   |           |                                    |\
|                          | Decision Table Name      |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \<tr\>                   |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 1  |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 2  |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 3? |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \<tr\>                   |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 1    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 2    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 3    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \</tbody\>               |           |                                    |\
|                          | \</table\>               |           |                                 |   
| Table marked             | \<table\>                | false     | The first row is a comment row      |\
| as comment (             | \<tbody\>                |           |     |\
| the word comment         | \<tr\>                   |           |                                    |\
| can be in any            | \<td\>                   |           |                                    |\
| case)                    | Comment      |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \<tr\>                   |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Name      |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \<tr\>                   |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 1  |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 2  |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 3? |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \<tr\>                   |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 1    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 2    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 3    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \</tbody\>               |           |                                    |\
|                          | \</table\>               |           |                                 |   
| Table with               | \<table\>                | true      | ""                                 |\
| 1 name and               | \<tbody\>                |           |                                    |\
| 1 header                 | \<tr\>                   |           |                                    |\
| and 1 data               | \<td colspan="3"\>       |           |                                    |\
| row                      | Decision Table Name      |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \<tr\>                   |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 1  |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 2  |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Header 3? |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \<tr\>                   |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 1    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 2    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 3    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \</tbody\>               |           |                                    |\
|                          | \</table\>               |           |                                 |   
| Table with               | \<table\>                | true      | ""                                 |\
| 1 name and               | \<thead\>                |           |                                    |\
| 1 header row             | \<tr\>                   |           |                                    |\
| as headerand             | \<th colspan="3"\>       |           |                                    |\
| 1 datarow                | Decision Table Name      |           |                                    |\
|                          | \</th\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \<tr\>                   |           |                                    |\
|                          | \<th\>                   |           |                                    |\
|                          | Decision Table Header 1  |           |                                    |\
|                          | \</th\>                  |           |                                    |\
|                          | \<th\>                   |           |                                    |\
|                          | Decision Table Header 2  |           |                                    |\
|                          | \</th\>                  |           |                                    |\
|                          | \<th\>                   |           |                                    |\
|                          | Decision Table Header 3? |           |                                    |\
|                          | \</th\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \</thead\>               |           |                                    |\
|                          | \<tbody\>                |           |                                    |\
|                          | \<tr\>                   |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 1    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 2    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \<td\>                   |           |                                    |\
|                          | Decision Table Data 3    |           |                                    |\
|                          | \</td\>                  |           |                                    |\
|                          | \</tr\>                  |           |                                    |\
|                          | \</tbody\>               |           |                                    |\
|                          | \</table\>               |           |                                 |   

