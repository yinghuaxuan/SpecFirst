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

| Infer type from number text                                                    |||
| #Comment                        | Number                          | Actual Type? |
| ------------------------------- | ------------------------------- | ------------ |
| integer                         | 0                               | integer          |
| integer                         | 12                              | integer          |
| positive integer                | +12                             | integer          |
| negative integer                | -12                             | integer          |
| integer with exponent notation  | -103E+06                         | integer          |
| integer, max value              | 2147483647                      | integer          |
| integer, min value              | -2147483648                     | integer          |
| integer, max value + 1          | 2147483648                      | double       |
| integer, min value - 1          | -2147483649                     | double       |
| decimal notation                | 12.5M                           | decimal      |
| decimal notation                | -12.5M                          | decimal      |
| decimal notation                | 12.5m                           | decimal      |
| decimal notation                | -12.5m                          | decimal      |
| decimal, max value + 1          | 79228162514264337593543950336   | double       |
| decimal, min value - 1          | -79228162514264337593543950336  | double       |
| decimal notation, max value + 1 | 79228162514264337593543950336M  | string       |
| decimal notation, min value - 1 | -79228162514264337593543950336M | string       |
| double                          | 0.0                             | double       |
| double                          | 12.5                            | double       |
| positive double                 | -12.5                           | double       |
| negative double                 | -12.5                           | double       |
| double notation                 | 12.5D                           | double       |
| double notation                 | -12.5D                          | double       |
| double notation                 | 12.5d                           | double       |
| double notation                 | -12.5d                          | double       |
| decimal with exponent notation  | -2.09550901805872E-05M          | decimal      |
| decimal with exponent notation  | -2.09550901805872E+05M          | decimal      |
| double with exponent notation   | -2.09550901805872E-05D          | double       |
| double with exponent notation   | -2.09550901805872E+05D          | double       |
| double, beyond max value        | 2.7976931348623157E+308         | string       |
| double, below min value         | -2.7976931348623157E+308        | string       |
| leading spaces not considered   |     12                          | integer      |
| trailing spaces not considered  | 12                              | integer      |
| not valid number                | -2.7976931348623157E            | string       |
| not valid number                | -2.                             | string       |

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
