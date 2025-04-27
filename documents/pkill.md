# pkill

**Process Kill** > send signals to processes based on a PATTERN, typically to terminate them.
---

` pkill [OPTION]... [PATTERN] `
---

| **OPTION** | description |
|:---:|:---:|
|  | gracefully terminate processes (SIGTERM #15) |
| -[SIGNAL-NUMBER] | specify a signal number to send |
| --signal [SIGNAL] | specify a signal name or number to send |
| -e, --echo | display what is killed |
| -n, --newest | select most recently started |
| -o, --oldest | select least recently started |
| -f, --full | match against the full command line <br> use full process name to match |

## Examples:
` pkill firefox `

` pkill -9 "thunderbird" `

` pkill -e --signal 15 firefox `


