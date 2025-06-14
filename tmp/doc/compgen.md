# compgen

**Completion Generator** > display possible completions.
---

` compgen [OPTION]... [EXPRESSION] `
---

| **OPTION** | description |
|:---:|:---:|
| -a | list all aliases |
| -c | list all commands available |
| -e | list all environment variables |
| -k | list all keywords |
| -v | list all shell variables |
| -d | list all directories |
| -f | list all files and directories |
| -u | list all users |
| -g | list all groups |

## Examples
` compgen -e `

` compgen -ac dotnet `

` compgen -f Doc `