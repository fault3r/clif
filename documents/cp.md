# cp

**Copy** > copy files and directories.
---

` cp [OPTION]... [SOURCE] [DESTINATION] `
---

| **OPTION** | description |
|:---:|:---:|
| -r, --recursive | copy directories recursively |
| -v, --verbose | explain what is being done |
| -i, --interactive | prompt before overwrite |
| -f, --force | do not prompt before overwriting or fail |
| -n, --no-clobber | do not overwrite an existing file or fail |
| -u, --update | control which existing files are updated for copy | 

## Examples
` cp -rv test/ /home/ `

` cp -iv --update *.* bckDir/ `
