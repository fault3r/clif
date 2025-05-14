# Operators

operators and special characters.
---

| **FILE-TEST** | description |
|:---:|:---:|
| -e | checks if a file exists |
| -d | check if a file is a directory |
| -f | check if a file is a regular file |
| -L | check if a file is a symbolic link |
| -s | check if a file is not empty |
| -r | check if a file is readable |
| -w | check if a file is writable |
| -x | check if a file is executable |
| -O | check if a file is owned by the effective user id |
| -G | check if a file is owned by the effective group id |
| -z | check if the lenght of a string is zero |
| -n | check if the lenght of a string is non-zero |

| **NUMERIC-COMPARISON** | description |
|:---:|:---:|
| -eq | equal to |
| -ne | not equal to |
| -gt | greater than |
| -ge | greater than or equal to |
| -lt | less than |
| -le | less than or equal to |

| **STRING-COMPARISON** | description |
|:---:|:---:|
| = | equal to |
| != | not equal to |
| > | greater than |
| < | less than |

| **ARITHMETIC-COMPARISON** | description |
|:---:|:---:|
| + | addition
| - | subtraction
| * | multiplication
| / | division
| % | modulus

| **LOGICAL** | description |
|:---:|:---:|
| && | logical and |
| \|\| | logical or |
| ! | logical not |

| **SPECIAL-CHARACTER** | description |
|:---:|:---:|
| && | run a second command only if the first command succeeds <br> ` mkdir dir && cd dir ` |
| \|\| | run a second command only if the first command fails <br> ` cd dir \|\| echo "not exist" ` |
| ; |  run multiple commands sequentially, whether the previous command succeeded or failed <br> ` echo "first"; echo "second" ` |
| () | allow to group commands and run them in a separate environment <br> ` (cd /tmp && ls) ` |
| & | run command in the background <br> ` oneko & ` |
| > | operator redirects the output of a command to a file <br> ` echo "hello" > output.txt ` |
| >> | operator appends the output of a command to a file <br> ` echo "world" >> output.txt ` |




