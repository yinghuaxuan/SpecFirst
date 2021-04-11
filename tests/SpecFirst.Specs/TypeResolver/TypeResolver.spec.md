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

| Infer type from number text                                                                                       ||||
| #Comment                        | Number                          | Actual Type? | Parsed Number?                    |
| ------------------------------- | ------------------------------- | ------------ | --------------------------------- |
| integer                         | 0                               | integer      | "0"                               |
| integer                         | 12                              | integer      | "12"                              |
| positive integer                | +12                             | integer      | "12"                              |
| negative integer                | -12                             | integer      | "-12"                             |
| integer with exponent notation  | -103E+06                        | integer      | "-103E+06"                        |
| integer, max value              | 2147483647                      | integer      | "2147483647"                      |
| integer, min value              | -2147483648                     | integer      | "-2147483648"                     |
| integer, max value + 1          | 2147483648                      | double       | "2147483648"                      |
| integer, min value - 1          | -2147483649                     | double       | "-2147483649"                     |
| decimal notation                | 12.5M                           | decimal      | "12.5M"                           |
| decimal notation                | -12.5M                          | decimal      | "-12.5M"                          |
| decimal notation                | 12.5m                           | decimal      | "12.5m"                           |
| decimal notation                | -12.5m                          | decimal      | "-12.5m"                          |
| decimal, max value + 1          | 79228162514264337593543950336   | double       | "79228162514264337593543950336D"  |
| decimal, min value - 1          | -79228162514264337593543950336  | double       | "-79228162514264337593543950336D" |
| decimal notation, max value + 1 | 79228162514264337593543950336M  | string       | "79228162514264337593543950336M"  |
| decimal notation, min value - 1 | -79228162514264337593543950336M | string       | "-79228162514264337593543950336M" |
| double                          | 0.0                             | double       | "0.0"                             |
| double                          | 12.5                            | double       | "12.5"                            |
| positive double                 | -12.5                           | double       | "-12.5"                           |
| negative double                 | -12.5                           | double       | "-12.5"                           |
| double notation                 | 12.5D                           | double       | "12.5D"                           |
| double notation                 | -12.5D                          | double       | "-12.5D"                          |
| double notation                 | 12.5d                           | double       | "12.5D"                           |
| double notation                 | -12.5d                          | double       | "-12.5D"                          |
| decimal with exponent notation  | -2.09550901805872E-05M          | decimal      | "-2.09550901805872E-05M"          |
| decimal with exponent notation  | -2.09550901805872E+05M          | decimal      | "-2.09550901805872E+05M"          |
| double with exponent notation   | -2.09550901805872E-05D          | double       | "-2.09550901805872E-05D"          |
| double with exponent notation   | -2.09550901805872E+05D          | double       | "-2.09550901805872E+05D"          |
| double, beyond max value        | 2.7976931348623157E+308         | string       | "2.7976931348623157E+308"         |
| double, below min value         | -2.7976931348623157E+308        | string       | "-2.7976931348623157E+308"        |
| leading spaces not considered   | 12                              | integer      | "12"                              |
| trailing spaces not considered  | 12                              | integer      | "12"                              |
| not valid number                | -2.7976931348623157E            | string       | "-2.7976931348623157E"            |
| not valid number                | -2.                             | string       | "-2."                             |

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
