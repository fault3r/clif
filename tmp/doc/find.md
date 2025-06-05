# find

search for files and directories within a specified directory hierarchy.
---

` find [PATH] [OPTION]... [EXPRESSION] `
---

| **OPTION** | description |
|:---:|:---:|
| -name | case-sensitive files search by name |
| -iname | case-insensitive files search by name |
| -type f\|d | specify the type of file <br> (`f` for files, `d` for directories) |
| -size +\|-[SIZE]M\|k | search for files of a specific size <br> (`M` for MB, `k` for KB) |
| -mtime +\|-[DAY] | search for files modified within a certain number of days |
| -delete | delete the found files |
| -exec [COMMAND] {} \\;\|+ | execute a command on the found files <br> (`{}` found files, `\;` singleton mode, `+` statement mode) |

## Examples
` find ./ -type f -iname search `

` find ./ -size +1M `

` find /home -name "*.txt" -exec cat {} + `

` find ~/ -mtime 7 `