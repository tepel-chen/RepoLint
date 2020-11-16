# RepoLint
A program to lint (find problems) files submitted to the [KtaneContent](https://github.com/Timwi/KtaneContent) repo.

## Usage
```
RepoLint <path>
```
Depending on what `path` points to, it'll be treated differently and use different rules.
### Directory
 * A directory will be linted like it's a copy of the KtaneContent repo.
 * Excluded rules: `W3CValidator`.

### Archives
 * An archive will be extracted and linted.
 * Excluded rules: None.

### Other files
 * Just that file will be linted.
 * Excluded rules: `ExpectedFiles`.

## Rules
### ConsecutiveBRs
Consecutive `<br>` tags should not be used. Instead use `<p>` tags for sections of text or add some `margin:` to space an element out.
### ConsecutiveEmptyLines
Consecutive empty lines leads to inconsistent spacing between sections. Use one empty line.
### DOCTYPE
HTML files without `<!DOCTYPE html>` can enable [quirks mode](https://developer.mozilla.org/en-US/docs/Web/HTML/Quirks_Mode_and_Standards_Mode). Add it to the top of the file.
### ExpectedFiles
Every module should have a Component SVG and JSON file. Make sure both are included and named the same as the module name.
### FontFamily
Any font family used should be included by the CSS. Add a `@font-face` declaration for any new fonts used.
### GraphicsFolder
Graphics must be in the appropriate folder named for the module to keep graphics separated. Put the manual graphics into a folder with the same name as the module name.
### HeadTag
To keep the format of manuals consistent, the tags in the head are kept consistent. The message should give the exact HTML that you can copy into the manual.
### HtmlHeadBody
The html, head and body tags should be included and on separate lines to keep the format of manuals consistent. Either add the missing tag or make sure each tag is on it's own line.
### InvalidJSON
JSON files must use a valid syntax. Fix the syntax error explained in the message.
### MinifySVG
To keep the size of component SVGs down, they should be minified. Minify them with [SVGOMG](https://jakearchibald.github.io/svgomg/).
### NoManualContent
The `<div id="ManualContent">` wrapper element is a legacy from the original manuals. Remove it from the manual.
### NoTabs
To keep things consistent spaces are used instead of tabs. Convert the tabs to spaces.
### NoTextSVG
`<text>` elements can be displayed inconsistently depending on the fonts that the user has. They should be converted to `<path>` elements to make sure they're consistent.
### ParentFolder
.html, .json files should be in HTML and JSON folders respectively. .svg files can be anywhere under the img folder. Move each file is in it's correct folder.
### RuleSeed
If you include the `ruleseed.js` script or the setRules and setDefaultRules functions, you need to include the other if the manual supports ruleseed. Either add the one that's missing or remove it if the manual doesn't support ruleseed.
### TemplateManual
Template Manual files are only for demonstration purposes only. Remove them since they're not being used.

As for the `Template Manual.html` file itself, it should either be renamed to the name of the module or it should be removed if it's unused.
### TwoIndentJSON
To keep indentation in JSON files consistent, all JSON files should use 2 space indentation. Fix the indentation of the JSON file to be using 2 spaces.
### W3CValidator
To make sure HTML is valid and well-written, it is run through the W3C Validator. Fix any problems with the HTML that's found.