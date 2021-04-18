var md = require("./node_modules/markdown-it")()
    .use(require("./node_modules/markdown-it-multimd-table"),{
        multiline: true,
        rowspan: true,
        headerless: true,
    });

try {
    result = md.render(markdownTable);
} catch (err) {
    result = err.toString();
}