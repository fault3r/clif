# pgrep

**Process grep** > search for processes based on a pattern. specifically designed to find and return the process IDs.
---

` pgrep [OPTION]... [EXPRESSION] `
---

| **OPTION** | description |
|:---:|:---:|
| | list the process PIDs |
| -l, --list-name | list the process names with PIDs |
| -a, --list-full | list the processes with full command line |
| -u, --euid [USERNAME] | processes owned by the user |
| -c, --count | count the number of matching processes |
| -n, --newest | select most recently started |
| -o, --oldest | select least recently started |

## Examples:
` pgrep code `

` pgrep -l systemd `

` pgrep -n `
