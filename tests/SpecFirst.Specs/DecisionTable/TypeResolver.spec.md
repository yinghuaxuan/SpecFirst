The TypeResolver has two functions:
* Figure out the actual type of each individual data  
In order to generate the test case data in the correct format (for
example, datetime will be converted to something like
new DateTime(day, month, year) when generating the test data), we
need to figure out the actual type of the data. 
* Figure out the actual type of the column   
The type of the column will be used to generate signatures for
the test method parameters.

## Resolve type for scala values

### Resolve type for numbers
If the text is a number and it does not contain '.' or 'E'/'e' in it, 
we interpret it as integer. If it is beyond the interger size, we will 
try decimal. If it is beyond decimal, we will interpret it as string.


| Infer type from integer text                            |||
| #Comment                     | Text Value  | Actual Type? |
| ---------------------------- | ----------- | ------------ |
| integer                      | 0           | int          |
| integer                      | 12          | int          |
| negative integer             | -12         | int          |
| integer, max value           | 2147483647  | int          |
| integer, min value           | -2147483648 | int          |
| integer, max value + 1       | 2147483648  | decimal      |
| integer, min value - 1       | -2147483649 | decimal      |


| Infer type from decimal text                                                  |||
| #Comment                        | Text Value                     | Actual Type? |
| ------------------------------- | ------------------------------ | ------------ |
| decimal                         | 0.0                            | decimal      |
| decimal                         | 12.5                           | decimal      |
| negative decimal                | -12.5                          | decimal      |
| decimal, contain an exponent    | 2.09550901805872E-05           | decimal      |
| decimal, contain an exponent    | 2.09550901805872e-05           | decimal      |
| decimal, contain a leading sign | -2.09550901805872e-05          | decimal      |
| decimal, max value              | 79228162514264337593543950335  | decimal      |
| decimal, min value              | -79228162514264337593543950335 | decimal      |
| decimal, max value + 1          | 79228162514264337593543950336  | string       |
| decimal, min value - 1          | -79228162514264337593543950336 | string       |

### Resolve type from boolean text
Convert text to boolean and ignore cases.

| Infer type from boolean text                           |||
| #Comment                     | Text Value | Actual Type? |
| ---------------------------- | ---------- | ------------ |
| bool, camel case             | True       | bool         |
| bool, lower case             | true       | bool         |
| bool, upper case             | TRUE       | bool         |
| bool, mixed case             | TrUe       | bool         |
| bool, camel case             | False      | bool         |
| bool, lower case             | false      | bool         |
| bool, upper case             | FALSE      | bool         |
| bool, mixed case             | FalSe      | bool         |
| not valid boolean text       | Truee      | string       |
| not valid boolean text       | FalSee     | string       |

### Resolve type from datetime text
For datetime, we only accept yyyy-MM-dd HH:mm:ss as a valid datetime format.
For all other formats, we interpret them as string.

| Infer type from datetime text                                            |||
| #Comment                        | Text Value                | Actual Type? |
| ------------------------------- | ------------------------- | ------------ |
| datetime in yyyy-MM-dd HH:mm:ss | 2012-12-25 23:59:59       | datetime     |
| datetime, not supported format  | 25/12/2012 23:59:59       | string       |
| datetime, not supported format  | 2012-1-1 23:59:59         | string       |
| datetime, not supported format  | 25 December 2012 23:59:59 | string       |
| datetime, out of range (min)    | 0000-12-31 00:00:00       | string       |
| datetime, out of range (max)    | 10000-01-01 00:00:01      | string       |

### Resolve type for the column
The column type will be used as the type of parameters in the generated test methods.

| Infer column type from integer values ||||
| ------------------------------------- | ---------- | --------- | ------------ |
| #Comment                              | Text Value | Hint Type | Actual Type? |
| all integers                          | 12         | null      | int          |
| integers with null values             | 12         | int       | int          |
| integer, nullable hint type           | 12         | int?      | int?         |
| integer, compatible hint type         | 12         | decimal   | decimal      |
| integer, compatible hint type         | 12         | decimal?  | decimal?     |
| integer, incompatible hint type       | 12         | string    | string       |
| integer, incompatible hint type       | 12         | datetime  | string       |
| integer, incompatible hint type       | 12         | bool      | string       |

|Infer column type from decimal values 
|--
|#Comment                       |Text Value              |Hint Type      |Actual Type?
|decimal, no hint type          |12.5                    |null           |decimal
|decimal, same hint type        |12.5                    |decimal        |decimal
|decimal, nullable hint type    |12.5                    |decimal?       |decimal?
|decimal, incompatible hint type|12.5                    |int            |decimal
|decimal, incompatible hint type|12.5                    |int?           |decimal?
|decimal, incompatible hint type|12.5                    |string         |string
|decimal, incompatible hint type|12.5                    |datetime       |string
|decimal, incompatible hint type|12.5                    |bool           |string


|Infer column type from bool value 
|--
|#Comment                       |Text Value              |Hint Type      |Actual Type?
|bool, no hint type             |true                    |null           |bool
|bool, no hint type             |false                   |null           |bool 
|bool, same hint type           |true                    |bool           |bool
|bool, same hint type           |false                   |bool           |bool
|bool, nullable hint type       |true                    |bool?          |bool?
|bool, nullable hint type       |false                   |bool?          |bool?
|bool, case insensitive         |True                    |bool           |bool
|bool, case insensitive         |False                   |bool           |bool
|bool, case insensitive         |TRUE                    |bool           |bool
|bool, case insensitive         |FALSE                   |bool           |bool
|integer, incompatible hint type|true                    |int            |string
|integer, incompatible hint type|true                    |int?           |string
|integer, incompatible hint type|true                    |string         |string
|integer, incompatible hint type|true                    |datetime       |string
|integer, incompatible hint type|true                    |decimal        |string


|Infer column type from datetime value 
|--
|#Comment                        |Text Value               |Hint Type      |Actual Type?
|datetime, no hint type          |2012-12-25 23:59:59      |null           |datetime
|datetime, same hint type        |2012-12-25 23:59:59      |datetime       |datetime
|datetime, nullable hint type    |2012-12-25 23:59:59      |datetime?      |datetime?
|datetime, incompatible hint type|2012-12-25 23:59:59      |int            |string
|datetime, incompatible hint type|2012-12-25 23:59:59      |string         |string
|datetime, incompatible hint type|2012-12-25 23:59:59      |decimal        |string
|datetime, incompatible hint type|2012-12-25 23:59:59      |bool           |string
|datetime, not supported format  |25/12/2012 23:59:59      |datetime       |string
|datetime, not supported format  |2012-1-1 23:59:59        |datetime       |string
|datetime, not supported format  |25 December 2012 23:59:59|datetime       |string
|datetime, out of range (min)    |0000-12-31 00:00:00      |datetime       |string
|datetime, out of range (max)    |10000-01-01 00:00:01     |datetime       |string