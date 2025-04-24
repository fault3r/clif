# chown

**Change Owner** > change the owner and group of files.
---

` chown [OPTION]... [OWNER][:GROUP] [FILE]... `
---

| **OPTION** | description |
|:---:|:---:|
| -R, --recursive | change files and directories recursively |
| -v, --verbose | verbose mode |

## Examples:
` chown faUser file.txt `

` chown -vR faUser:faGroup faDir/ `

` chown :faGroup file.txt `