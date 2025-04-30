# tail

display the end of files.
---

` tail [OPTION] [FILE]... `
---

| **OPTION** | description |
|:---:|:---:|
| -n , --lines [NUMBER] | print the last NUMBER lines instead of the last 10 |
| -c , --bytes [NUMBER] | print the last NUMBER bytes |
| -f, --follow | follow the file in real-time mode |

## Examples:
` tail --lines 5 file.txt `

` tail -f file.txt `
