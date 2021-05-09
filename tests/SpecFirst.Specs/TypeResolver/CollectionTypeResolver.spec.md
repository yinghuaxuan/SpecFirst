The CollectionTypeResolver is to infer the type for a collection of data.  
It will return the most compatible type for the collection.

| Infer type from collection text                                                                                                              ||||
| #Comment                        | Collection                          | Collection Type? | Parsed Collection?                                   |
| ------------------------------- | ----------------------------------- | ---------------- | ---------------------------------------------------- |
| integer only                    | "[1, 2, 3, 4]"                      | integer          | "new int[] {1, 2, 3, 4}"                             |
| decimal only                    | "[3M, 12.5M, 0.0M]"                 | decimal          | new decimal[] {3M, 12.5M, 0.0M}                      |
| double only                     | "[12.5, 3D, 5d, 0.0D]"              | double           | new double[] {12.5D, 3D, 5D, 0.0D}                    |
| string only                     | "[input, "output", comment, "-12"]" | string           | "new string[] {"input", "output", "comment", "-12"}" |
| string only                     | "[input, "output, comment, -12"]"   | string           | new string[] {"input", "\"output", "comment", "-12\""}  |
| integer and decimal             | "[1, 2, 1M, 2m]"                    | decimal          | new decimal[] {1, 2, 1M, 2M}                         |
| integer and double              | "[1, 2, 1D, 2d]"                    | double           | new double[] {1, 2, 1D, 2D}                          |
| decimal and double              | "[1D, 1d, 1M, 1m]"                  | object           | new object[] {1D, 1D, 1M, 1M}                        |
| number and string               | "[1, 1D, 1d, 1M, 1m, "1"]"          | object           | new object[] {1, 1D, 1D, 1M, 1M, "1"}                |
| missing ending bracket          | "[1, 1D, 1d, 1M, 1m, "1""           | string           | "[1, 1D, 1d, 1M, 1m, \"1\""                |
| extra starting bracket          | "[[1, 1D, 1d, 1M, 1m, "1"]"         | string           | "[[1, 1D, 1d, 1M, 1m, "1"]"                          |
| extra ending bracket            | "[1, 1D, 1d, 1M, 1m, "1"]]"         | string           | "[1, 1D, 1d, 1M, 1m, "1"]]"                          |
| empty comma                     | "[1,,]"                             | string           | "[1,,]"                                              |


