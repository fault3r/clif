# Operators

operators and special characters.
---

| **ARITHMETIC-OPERATOR** | description |
|:---:|:---:|
| + | addition
| - | subtraction
| * | multiplication
| / | division
| % | modulus

| **STRING-COMPARISON** | description |
|:---:|:---:|
| = | equal to |
| != | not equal to |
| > | greater than |
| < | less than |

| **NUMERIC-COMPARISON** | description |
|:---:|:---:|
| -eq | equal to |
| -ne | not equal to |
| -gt | greater than |
| -ge | greater than or equal to |
| -lt | less than |
| -le | less than or equal to |

| **LOGICAL-OPERATOR** | description |
|:---:|:---:|
| && | logical and |
| \|\| | logical or |
| ! | logical not |

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
| -z | check if a string is zero |
| -n | check if a string is non-zero |
| -O | check if a file is owned by current user |
| -G | check if a file is owned by current group |

| **SPECIAL-CHARACTER** | description |
|:---:|:---:|
| #! | it began with the forging of the great shebang💍 <br> ` #!/bin/bash ` |
| # |  indicate the start of a comment <br> ` # linus ` |
| && | execute the second command only if the first succeeds <br> ` mkdir dir && cd dir ` |
| \|\| | execute the second command only if the first fails <br> ` cd dir \|\| echo "not exist" ` |
| ; |  run multiple commands sequentially <br> ` echo "first"; echo "second" ` |
| () | group commands in a subshell <br> ` (cd /tmp && ls) ` |
| {} | group commands in the current shell <br> ` { echo "first"; echo "another"; } ` |
| & | run command in the background <br> ` oneko & ` <br> ` jobs ` to list background jobs <br> ` fg [JOB] ` to bring a background job to the foreground <br> ` bg [JOB] ` to resume a suspended job in the background |
| > | operator redirects the output of a command to a file <br> ` echo "hello" > output.txt ` |
| >> | operator appends the output of a command to a file <br> ` echo "world" >> output.txt ` |
| < | redirect input from a file <br> ` sort < input.txt ` |
| << | allow to provide input directly in shell <br> ` cat << EOF this is a here-document. EOF ` |
| <<< | allow to provide a string as input <br> ` grep "test" <<< "this is a test string." ` |
| \| |  pipe output to another command <br> ` ls \| lolcat ` |
| \\ | prevent the special meaning of a character <br> ` echo "This is a backslash: \\" ` |
| " or ' | allow variable expansion <br> ` echo "double-quoted string: $HOME" ` |
| $? | exit status of the last command <br> ` 0 ` indicates success |
| $! | process id of the last background command |
| $$ | process id of the current shell |
| 0> | standard input (stdin) |
| 1> | standard output (stdout) <br> ` ls -la 1> output.txt ` |
| 2> | standard error (stderr) <br> ` ls /notexist/ 2>> append-error.txt ` |
| 2>&1 or &> | standard output & error <br>` mkdir -v test/ > out-err.txt 2>&1 ` <br> ` mkdir -v test/ &> out-err.txt ` |
| 2> /dev/null | Black-Hole, do nothing on stderr <br> ` mkdir test/ 2> /dev/null ` | 