# mv

**Move** > move or rename files and directories.
---

` mv [OPTION]... [SOURCE] [DESTINATION] `
---

| **OPTION** | description |
|:---:|:---:|
| -v, --verbose | explain what is being done |
| -i, --interactive | prompt before overwrite |
| -f, --force | do not prompt before overwriting |
| -n, --no-clobber | do not overwrite an existing file |
| -u, --update | control which existing files are updated for move |

#### ` mv ` command moves directories automatically without needing a recursive option.

## Examples
` mv -v test/ /home/ `

` mv -viu *.txt bckDir/ `