var md = require("./node_modules/markdown-it")()
			.use(require("./node_modules/markdown-it-multimd-table"));

try {
    result = md.render(markdownTable);
} catch (err) {
    result = err.toString();
}