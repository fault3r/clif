# ln

**Link** > create links for files and directories.
---

` ln [OPTION]... [SOURCE] [LINK] `
---

| **OPTION** | description |
|:---:|:---:|
| |  create hard link |
| -s, --symbolic | create symbolic link |
| -f, --force | remove existing destination file |
| -i, --interactive | prompt whether to remove destination |
| -v, --verbose | explain what is being done |

#### hard link not allowed for directory.

## Examples
` ln source.txt link.txt `

` ln -vfs SourceDir/ Dir-Link `