﻿+-----------+-------------------------------------------------------------------+----------+----------------+
|Decision Table Validator 
+-----------+-------------------------------------------------------------------+----------+----------------+
|#Comment   | Decision Table                                                    |Is Valid? |Validation Error
+-----------+-------------------------------------------------------------------+----------+----------------+
|Table with |\<table\>                                                          |false     |Decision table must
|only 1 name| \<col style="width:72.34%"\>                                      |          |have at least three rows
|row        | \<col style="width:10.64%"\>                                      |          |
|           | \<col style="width:17.02%"\>                                      |          |
|           | \<tbody\>                                                         |          |
|           |  \<tr\>                                                           |          | 
|           |   \<td colspan="3"\>                                              |          |
|           |     Decision Table Validator                                      |          |
|           |   \</td\>                                                         |          |
|           |  \</tr\>                                                          |          |
|           | \</tbody\>                                                        |          |
|           |\</table\>                                                         |          | 
+-----------+-------------------------------------------------------------------+----------+----------------+
|Table with |\<table\>                                                          |false     |Decision table must
|1 name and | \<col style="width:72.34%"\>                                      |          |have at least three rows
|1 header   | \<col style="width:10.64%"\>                                      |          |
|row        | \<col style="width:17.02%"\>                                      |          |
|           | \<tbody\>                                                         |          |
|           |  \<tr\>                                                           |          | 
|           |   \<td colspan="3"\>                                              |          |
|           |     Decision Table Validator                                      |          |
|           |   \</td\>                                                         |          |
|           |  \</tr\>                                                          |          |
|           | \<tr\>                                                            |          |
|           |  \<td\>                                                           |          |
|           |  \<td\>                                                           |          |
|           |   Decision Table                                                  |          |
|           |  \</td\>                                                          |          |
|           |  \<td\>                                                           |          |
|           |   Is Valid?                                                       |          |
|           |  \</td\>                                                          |          |
|           |  \<td\>                                                           |          |
|           |   Validation Error                                                |          |
|           |  \</td\>                                                          |          |
|           |  \</tr\>                                                          |          |
|           | \</tbody\>                                                        |          |
|           |\</table\>                                                         |          | 
+-----------+-------------------------------------------------------------------+----------+----------------+
|Table with |\<table\>                                                          |true      |
|1 name and | \<col style="width:72.34%"\>                                      |          |
|1 header   | \<col style="width:10.64%"\>                                      |          |
|and 1 data | \<col style="width:17.02%"\>                                      |          |
|row        | \<tbody\>                                                         |          |
|           |  \<tr\>                                                           |          | 
|           |   \<td colspan="3"\>                                              |          |
|           |     Decision Table Validator                                      |          |
|           |   \</td\>                                                         |          |
|           |  \</tr\>                                                          |          |
|           | \<tr\>                                                            |          |
|           |  \<td\>                                                           |          |
|           |  \<td\>                                                           |          |
|           |   Decision Table                                                  |          |
|           |  \</td\>                                                          |          |
|           |  \<td\>                                                           |          |
|           |   Is Valid?                                                       |          |
|           |  \</td\>                                                          |          |
|           |  \<td\>                                                           |          |
|           |   Validation Error                                                |          |
|           |  \</td\>                                                          |          |
|           |  \</tr\>                                                          |          |
|           | \<tr\>                                                            |          |
|           |  \<td\>                                                           |          |
|           |  \<td\>                                                           |          |
|           |   Decision Table                                                  |          |
|           |  \</td\>                                                          |          |
|           |  \<td\>                                                           |          |
|           |   Is Valid?                                                       |          |
|           |  \</td\>                                                          |          |
|           |  \<td\>                                                           |          |
|           |   Validation Error                                                |          |
|           |  \</td\>                                                          |          |
|           |  \</tr\>                                                          |          |
|           | \</tbody\>                                                        |          |
|           |\</table\>                                                         |          | 
+-----------+-------------------------------------------------------------------+----------+----------------+
