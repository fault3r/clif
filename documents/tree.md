# tree

display the directory structure of the current directory.
---

` tree [OPTION]... [PATH]... `
---

| **OPTION** | description |
|:---:|:---:|
| -d | list directories only |
| -a |  all files, including hidden files |
| -L [LEVEL] | limit the depth of the tree displayed |
| -f |  print the full path prefix for each file |
| -h |  print the size in a more human readable way |

## Examples:
` tree -hf ./ `

` tree -d -L 2 / `
