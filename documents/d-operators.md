# Operators

operators and special characters.
---


| **NUMERIC-COMPARISON** | description |
|:---:|:---:|
| -eq | equal to |
| -ne | not equal to |
| -gt | greater than |
| -ge | greater than or equal to |
| -lt | less than |
| -le | less than or equal to |

| **OPERATOR** | description |
|:---:|:---:|
| && | logical AND <br> run a second command only if the first command succeeds <br> ` mkdir dir && cd dir ` |
| \|\| | logical OR <br> run a second command only if the first command fails <br> ` cd dir \|\| echo "not exist" ` |
| ; | command separator <br> run multiple commands sequentially, whether the previous command succeeded or failed <br> ` echo "first"; echo "second" ` |
| () | subshell <br> allow to group commands and run them in a separate environment <br> ` (cd /tmp && ls) ` |




| & | background execution <br> run command in the background <br> ` oneko & ` |
| > | redirect <br> operator redirects the output of a command to a file <br> ` echo "hello" > output.txt ` |
| >> | append <br> operator appends the output of a command to a file <br> ` echo "world" >> output.txt ` |
| \ | line continuation <br> continue a command onto the next line <br> ` echo "hell"\ "o" ` |





