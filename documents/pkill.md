# pkill

**Process Kill** > send signals to processes based on a pattern, typically to terminate them.
---

` pkill [OPTION]... [PATTERN] `
---

| **OPTION** | description |
|:---:|:---:|
|  | gracefully terminate processes (SIGTERM #15) |
| -[SIGNAL-NUMBER] | specify a signal number to send |
| --signal [SIGNAL] | specify a signal name or number to send |
| -u, --euid [USERNAME] | processes owned by the user |
| -e, --echo | display what is killed |
| -n, --newest | select most recently started |
| -o, --oldest | select least recently started |

### ` pkill -u [USERNAME] ` to **Logout** the current user account.

## Examples:
` pkill firefox `

` pkill -9 "thunderbird" `

` pkill -e --signal 15 firefox `

` pkill -u t-user thunderbird `


