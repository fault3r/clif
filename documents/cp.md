# cp

**Copy** > copy files and directories.
---

` cp [OPTION]... [SOURCE] [DESTINATION] `
---

| **OPTION** | description |
|:---:|:---:|
| -r, --recursive | copy directories recursively |
| -u, --update | control which existing files are updated for copy | 
| -n, --no-clobber | do not overwrite an existing file or fail |
| -f, --force | do not prompt before overwriting or fail |
| -i, --interactive | prompt before overwrite |
| -v, --verbose | explain what is being done |

## Examples
` cp -rv test/ /home/ `

` cp -iv --update *.* bckDir/ `
