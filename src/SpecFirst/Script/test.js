var md = require('markdown-it')()
			.use(require('markdown-it-multimd-table'));

const exampleTable =
"|             |          Grouping           || \n" +
"First Header  | Second Header | Third Header | \n" +
" ------------ | :-----------: | -----------: | \n" +
"Content       |          *Long Cell*        || \n" +
"Content       |   **Cell**    |         Cell | \n" +
"                                               \n" +
"New section   |     More      |         Data | \n" +
"And more      | With an escaped '\\|'       || \n" +
"[Prototype table]                              \n";

console.log(md.render(exampleTable));