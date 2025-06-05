# mv

**Move** > move or rename files and directories.
---

` mv [OPTION]... [SOURCE] [DESTINATION] `
---

| **OPTION** | description |
|:---:|:---:|
| -u, --update | control which existing files are updated for move |
| -n, --no-clobber | do not overwrite an existing file |
| -f, --force | do not prompt before overwriting |
| -i, --interactive | prompt before overwrite |
| -v, --verbose | explain what is being done |

#### ` mv ` command moves directories automatically without needing a recursive option.

## Examples
` mv -v test/ /home/ `

` mv -viu *.txt bckDir/ `