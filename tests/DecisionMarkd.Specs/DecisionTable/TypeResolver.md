Each decision table will contain multiple rows & columns of data.  
Each row of the decison table will become a test case for the 
testing method and each column will become an input variable into 
the method.  
In order to generate the test case data in the correct format (for
example, datetime will be converted to something like
new DateTime(day, month, year) when generating the test data), we
need to figure out the proper data type for each column.  
The TypeResolver is designed for this task. The TypeResolver will 
take in a value in string format and a hint type and produce the 
final type. A hint type is the type resolved from previous values
in the same column.


+-------------------------------+------------------------+---------------+----------------+
|Infer type from integer value 
+-------------------------------+------------------------+---------------+----------------+
|#Comment                       | Value In String        |Hint Type      |Actual Type?
+-------------------------------+------------------------+---------------+----------------+
|integer, no hint type          |12                      |null           |int
+-------------------------------+------------------------+---------------+----------------+
|integer, same hint type        |12                      |int            |int
+-------------------------------+------------------------+---------------+----------------+
|integer, nullable hint type    |12                      |int?           |int?
+-------------------------------+------------------------+---------------+----------------+
|integer, compatable hint type  |12                      |decimal        |decimal
+-------------------------------+------------------------+---------------+----------------+
|integer, compatable hint type  |12                      |decimal?       |decimal?
+-------------------------------+------------------------+---------------+----------------+
|integer, incompatable hint type|12                      |string         |string
+-------------------------------+------------------------+---------------+----------------+
|integer, incompatable hint type|12                      |datetime       |string
+-------------------------------+------------------------+---------------+----------------+
|integer, incompatable hint type|12                      |bool           |string


+-------------------------------+------------------------+---------------+----------------+
|Infer type from decimal value 
+-------------------------------+------------------------+---------------+----------------+
|#Comment                       | Value In String        |Hint Type      |Actual Type?
+-------------------------------+------------------------+---------------+----------------+
|decimal, no hint type          |12.5                    |null           |decimal
+-------------------------------+------------------------+---------------+----------------+
|decimal, same hint type        |12.5                    |decimal        |decimal
+-------------------------------+------------------------+---------------+----------------+
|decimal, nullable hint type    |12.5                    |decimal?       |decimal?
+-------------------------------+------------------------+---------------+----------------+
|decimal, incompatable hint type|12.5                    |int            |decimal
+-------------------------------+------------------------+---------------+----------------+
|decimal, incompatable hint type|12.5                    |int?           |decimal?
+-------------------------------+------------------------+---------------+----------------+
|decimal, incompatable hint type|12.5                    |string         |string
+-------------------------------+------------------------+---------------+----------------+
|decimal, incompatable hint type|12.5                    |datetime       |string
+-------------------------------+------------------------+---------------+----------------+
|decimal, incompatable hint type|12.5                    |bool           |string


+-------------------------------+------------------------+---------------+----------------+
|Infer type from bool value 
+-------------------------------+------------------------+---------------+----------------+
|#Comment                       | Value In String        |Hint Type      |Actual Type?
+-------------------------------+------------------------+---------------+----------------+
|bool, no hint type             |true                    |null           |bool
+-------------------------------+------------------------+---------------+----------------+
|bool, no hint type             |false                   |null           |bool 
+-------------------------------+------------------------+---------------+----------------+
|bool, same hint type           |true                    |bool           |bool
+-------------------------------+------------------------+---------------+----------------+
|bool, same hint type           |false                   |bool           |bool
+-------------------------------+------------------------+---------------+----------------+
|bool, nullable hint type       |true                    |bool?          |bool?
+-------------------------------+------------------------+---------------+----------------+
|bool, nullable hint type       |false                   |bool?          |bool?
+-------------------------------+------------------------+---------------+----------------+
|bool, case insensitive         |True                    |bool           |bool
+-------------------------------+------------------------+---------------+----------------+
|bool, case insensitive         |False                   |bool           |bool
+-------------------------------+------------------------+---------------+----------------+
|bool, case insensitive         |TRUE                    |bool           |bool
+-------------------------------+------------------------+---------------+----------------+
|bool, case insensitive         |FALSE                   |bool           |bool
+-------------------------------+------------------------+---------------+----------------+
|integer, incompatable hint type|true                    |int            |string
+-------------------------------+------------------------+---------------+----------------+
|integer, incompatable hint type|true                    |int?           |string
+-------------------------------+------------------------+---------------+----------------+
|integer, incompatable hint type|true                    |string         |string
+-------------------------------+------------------------+---------------+----------------+
|integer, incompatable hint type|true                    |datetime       |string
+-------------------------------+------------------------+---------------+----------------+
|integer, incompatable hint type|true                    |decimal        |string

+--------------------------------+-------------------------+---------------+----------------+
|Infer type from datetime value 
+--------------------------------+-------------------------+---------------+----------------+
|#Comment                        | Value In String         |Hint Type      |Actual Type?
+--------------------------------+-------------------------+---------------+----------------+
|datetime, no hint type          |2012-12-25 23:59:59      |null           |datetime
+--------------------------------+-------------------------+---------------+----------------+
|datetime, same hint type        |2012-12-25 23:59:59      |datetime       |datetime
+--------------------------------+-------------------------+---------------+----------------+
|datetime, nullable hint type    |2012-12-25 23:59:59      |datetime?      |datetime?
+--------------------------------+-------------------------+---------------+----------------+
|datetime, incompatable hint type|2012-12-25 23:59:59      |int            |string
+--------------------------------+-------------------------+---------------+----------------+
|datetime, incompatable hint type|2012-12-25 23:59:59      |string         |string
+--------------------------------+-------------------------+---------------+----------------+
|datetime, incompatable hint type|2012-12-25 23:59:59      |decimal        |string
+--------------------------------+-------------------------+---------------+----------------+
|datetime, incompatable hint type|2012-12-25 23:59:59      |bool           |string
+--------------------------------+-------------------------+---------------+----------------+
|datetime, not supported format  |25/12/2012 23:59:59      |datetime       |string
+--------------------------------+-------------------------+---------------+----------------+
|datetime, not supported format  |2012-1-1 23:59:59        |datetime       |string
+--------------------------------+-------------------------+---------------+----------------+
|datetime, not supported format  |25 December 2012 23:59:59|datetime       |string
+--------------------------------+-------------------------+---------------+----------------+
|datetime, out of range (min)    |0000-12-31 00:00:00      |datetime       |string
+--------------------------------+-------------------------+---------------+----------------+
|datetime, out of range (max)    |10000-01-01 00:00:01     |datetime       |string