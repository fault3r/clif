# wget

**Web Get** > downloading files from the web.
---

` wget [OPTION]... [URL] `
---

| **OPTION** | description |
|:---:|:---:|
|  | download a single file |
| -O [FILENAME] | download a file with a different name |
| -c,  --continue | resume an interrupted download |
| -b,  --background | download in the background |
| -i [INPUT.txt] | download files from a local or external list <br> each URL on a new line |

## Examples:
` wget http://example.com/file.zip `

` wget -O my-file.zip http://example.com/file.zip `

` wget -i urls.txt `
