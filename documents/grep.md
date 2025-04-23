# grep

**Global Regular Expression Print** > search for specific patterns within files or input streams.
---

` grep [OPTION]... "PATTERN" [FILE]... `
---

| **OPTION** | description |
|:---:|:---:|
| -i, --ignore-case | ignore case distinctions in patterns and data |
| -v, --invert-match | select non-matching lines |
| -r, --recursive | recursively search through directories |
| -l, --files-with-matches | print only names of files |
| -n, --line-number | print line number with output lines |
| -c, --count | print only a count of selected lines per file |
| -w, --word-regexp | match only whole words |
| -o, --only-matching | show only nonempty parts of lines that match |

### use ` grep ` in combination with other commands using pipes ` | `

## Examples:
` grep -ric "a" ./ `

` grep "search" file.txt `

` cat file.txt | grep "search" -i `