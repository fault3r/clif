# kill

send signal to process based on PID, typically to terminate it.
---

` kill [OPTION] [PID] `
---

| **OPTION** | description |
|:---:|:---:|
|  | gracefully terminate a process (SIGTERM #15) |
| -[SIGNAL-NUMBER] | specify a signal number to send |
| -s [SIGNAL] | specify a signal name to send |
| -l | list all signal names |

| **Common Signals** | description |
|:---:|:---:|
| SIGTERM #15 | gracefully terminate a process (default signal) |
| SIGKILL #9 | forcefully terminate a process (cannot be caught or ignored) |
| SIGINT #2 | interrupt a process (like Ctrl+C) |
| SIGSTOP #19 | pauses a process |
| SIGCONT #18 | Resumes a process that has been paused by SIGSTOP |

## Examples:
` kill -s SIGTERM 4321 `

` kill -9 1234 `

` kill -l `