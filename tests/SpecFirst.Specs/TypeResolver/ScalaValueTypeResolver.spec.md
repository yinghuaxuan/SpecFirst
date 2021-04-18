The TypeResolver is to infer the type of the data from 
its text representation.  

The following data types are supported:
- number (integer, double, decimal)
- datetime
- boolean
- string (if text is not any of the types above)

### Resolve type for numbers
- If the text is a number and it does not contain '.', 
we interpret it as integer. 
    - If it is beyond the interger size, we 
try double. 
    - If it is beyond double, we interpret it as string. 
- If the text contain '.' in it, 
we interpret it as double. 
    - If it is beyond the double size, we 
interpret it as string. 
- If it has D or d suffix, we always interpret it as double
- If it has M or m suffix, we always interpret it as decimal

| Infer type from number text                                                                                         ||||
| #Comment                        | Text Value                        | Actual Type? | Serialized Value?                 |
| ------------------------------- | --------------------------------- | ------------ | --------------------------------- |
| integer                         | "0"                               | integer      | "0"                               |
| integer                         | "12"                              | integer      | "12"                              |
| positive integer                | "+12"                             | integer      | "12"                              |
| negative integer                | "-12"                             | integer      | "-12"                             |
| integer with exponent notation  | "-103E+06"                        | integer      | "-103E+06"                        |
| integer, max value              | "2147483647"                      | integer      | "2147483647"                      |
| integer, min value              | "-2147483648"                     | integer      | "-2147483648"                     |
| integer, max value + 1          | "2147483648"                      | double       | "2147483648D"                     |
| integer, min value - 1          | "-2147483649"                     | double       | "-2147483649D"                    |
| decimal notation                | "12.5M"                           | decimal      | "12.5M"                           |
| decimal notation                | "-12.5M"                          | decimal      | "-12.5M"                          |
| decimal notation                | "12.5m"                           | decimal      | "12.5M"                           |
| decimal notation                | "-12.5m"                          | decimal      | "-12.5M"                          |
| decimal, max value + 1          | "79228162514264337593543950336"   | double       | "79228162514264337593543950336D"  |
| decimal, min value - 1          | "-79228162514264337593543950336"  | double       | "-79228162514264337593543950336D" |
| decimal notation, max value + 1 | "79228162514264337593543950336M"  | string       | "79228162514264337593543950336M"  |
| decimal notation, min value - 1 | "-79228162514264337593543950336M" | string       | "-79228162514264337593543950336M" |
| double                          | "0.0"                             | double       | "0.0D"                            |
| double                          | "12.5"                            | double       | "12.5D"                           |
| positive double                 | "-12.5"                           | double       | "-12.5D"                          |
| negative double                 | "-12.5"                           | double       | "-12.5D"                          |
| double notation                 | "12.5D"                           | double       | "12.5D"                           |
| double notation                 | "-12.5D"                          | double       | "-12.5D"                          |
| double notation                 | "12.5d"                           | double       | "12.5D"                           |
| double notation                 | "-12.5d"                          | double       | "-12.5D"                          |
| decimal with exponent notation  | "-2.09550901805872E-05M"          | decimal      | "-2.09550901805872E-05M"          |
| decimal with exponent notation  | "-2.09550901805872E+05M"          | decimal      | "-2.09550901805872E+05M"          |
| double with exponent notation   | "-2.09550901805872E-05D"          | double       | "-2.09550901805872E-05D"          |
| double with exponent notation   | "-2.09550901805872E+05D"          | double       | "-2.09550901805872E+05D"          |
| double, beyond max value        | "2.7976931348623157E+308"         | string       | "2.7976931348623157E+308"         |
| double, below min value         | "-2.7976931348623157E+308"        | string       | "-2.7976931348623157E+308"        |
| leading spaces not considered   | "   12"                           | integer      | "12"                              |
| trailing spaces not considered  | "12   "                           | integer      | "12"                              |
| not valid number                | "-2.7976931348623157E"            | string       | "-2.7976931348623157E"            |
| not valid number                | "-2."                             | string       | "-2."                             |

### Resolve type from boolean text
Convert text to boolean and ignore cases.

| Infer type from boolean text                                              ||||
| #Comment                     | Text Value | Actual Type? | Serialized Value? |
| ---------------------------- | ---------- | ------------ | ----------------- |
| bool, camel case             | "True"     | bool         | "true"            |
| bool, lower case             | "true"     | bool         | "true"            |
| bool, upper case             | "TRUE"     | bool         | "true"            |
| bool, mixed case             | "TrUe"     | bool         | "true"            |
| bool, camel case             | "False"    | bool         | "false"           |
| bool, lower case             | "false"    | bool         | "false"           |
| bool, upper case             | "FALSE"    | bool         | "false"           |
| bool, mixed case             | "FalSe"    | bool         | "false"           |
| not valid boolean text       | "Truee"    | string       | "Truee"           |
| not valid boolean text       | "FalSee"   | string       | "FalSee"          |

### Resolve type from datetime text
For datetime, we only accept yyyy-MM-dd HH:mm:ss as a valid datetime format.
For all other formats, we interpret them as string.

| Infer type from datetime text                                                                                           ||||
| #Comment                        | Text Value                  | Actual Type? | Serialized Value?                           |
| ------------------------------- | --------------------------- | ------------ | ------------------------------------------- |
| datetime in yyyy-MM-dd HH:mm:ss | "2012-12-25 23:59:59"       | datetime     | "new DateTime(2012, 12, 25, 23, 59, 59, 0)" |
| datetime, not supported format  | "25/12/2012 23:59:59"       | string       | "25/12/2012 23:59:59"                       |
| datetime, not supported format  | "2012-1-1 23:59:59"         | string       | "2012-1-1 23:59:59"                         |
| datetime, not supported format  | "25 December 2012 23:59:59" | string       | "25 December 2012 23:59:59"                 |
| datetime, out of range (min)    | "0000-12-31 00:00:00"       | string       | "0000-12-31 00:00:00"                       |
| datetime, out of range (max)    | "10000-01-01 00:00:01"      | string       | "10000-01-01 00:00:01"                      |

### Resolve type from string text
For any text other than number, boolean and datetime, we will interpret them as string.  
We allow certain special characters and escape characters in the text.

| Infer type from string text                                                                                       ||||
| #Comment                                            | Text Value             | Actual Type? | Serialized Value?      |
| --------------------------------------------------- | ---------------------- | ------------ | ---------------------- |
| string without quote                                | this is a string       | string       | "this is a string"     |
| fully quoted string is same as string without quote | "this is a string"     | string       | "this is a string"     |
| escape fully quoted string to keep the quotes       | "\"this is a string\"" | string       | "\"this is a string\"" |
| half quoted string will keep the quotes             | "this is a string      | string       | "this is a string      |
| half quoted string will keep the quotes             | this is a string"      | string       | this is a string"      |
| string with quotes in it                            | this is a "string"     | string       | "this is a \"string\"" |
| string with nested quotes in it                     | "this is a "string""   | string       | "this is a \"string\"" |
| string with one quote in it                         | this is a "string      | string       | "this is a \"string"   |
| string with a backslash in it                       | this is a \\string     | string       | "this is a \\string"   |
| string with escape characters                       | this is a \"string     | string       | "this is a \"string"   |
| quoted string with escape characters                | "this is a \"string"   | string       | "this is a \"string"   |
| string with great than and less than character      | "this is a \<string\>"   | string       | "this is a \<string\>"   |
| white spaces will be kept                           | "  "                   | string       | "  "                   |



