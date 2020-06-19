Serialize the string data into the test data. When a column contains mixed type of data, we 
will categorize the column data type as string. However, when serializing the data into their 
string format, we need to format the data based on their actual type. For example, if a string 
column contains a datetime, we will serialize the datetime data into "yyyy-MM-dd HH:mm:ss" format.

+-------------------------------+-----------------+-----------------+----------------+
|Serialize string data 
+-------------------------------+-----------------+-----------------+----------------+
|#Comment                       | Value           |Actual Data Type |Serialized in Test Data?
+-------------------------------+-----------------+-----------------+----------------+
|integer                        |12               |int              |12
+-------------------------------+-----------------+-----------------+----------------+
|Decimal                        |12.5             |decimal          |12.5
+-------------------------------+-----------------+-----------------+----------------+
|Boolean                        |true             |bool             |true
+-------------------------------+-----------------+-----------------+----------------+
|Boolean                        |false            |bool             |false
+-------------------------------+-----------------+-----------------+----------------+
|Datetime                       |2012-12-25       |Datetime         |2012-12-25 00:00:00
+-------------------------------+-----------------+-----------------+----------------+
|null                           |null             |int?             |null
+-------------------------------+-----------------+-----------------+----------------+
|null                           |null             |decimal?         |null
+-------------------------------+-----------------+-----------------+----------------+
|null                           |null             |bool?            |null
+-------------------------------+-----------------+-----------------+----------------+
|null                           |null             |DateTime?        |null

