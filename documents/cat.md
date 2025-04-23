# cat

**Concatenate** > display the contents of files.
---

` cat [OPTION]... [FILE]... `
---

| **OPTION** | description |
|:---:|:---:|
| > FILE | create a new file by redirecting the output |
| >> FILE | append the contents of a file to another file |
| -n, --number | number all output lines |
| -E, --show-ends | display $ at end of each line |
| -T, --show-tabs | display TAB characters as ^I |

### the ` head ` and ` tail ` commands to view the beginning and end of files.

## Examples:
` cat file1.txt file2.txt `

` cat file1.txt file2.txt > cat.txt `

` cat file1.txt >> update.txt `

` head file.txt `