# tail

display the end of files.
---

` tail [OPTION] [FILE]... `
---

| **OPTION** | description |
|:---:|:---:|
| -n , --lines [NUMBER] | print the last NUM lines instead of the last 10 |
| -c , --bytes [NUMBER] | print the last NUM bytes |
| -f, --follow | display appended data in real-time mode |

## Examples:
` tail --lines 5 file.txt `

` tail -f file.txt `
