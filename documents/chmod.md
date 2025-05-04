# chmod

**Change Mode** > managing permissions of files.
---

` chmod [OPTION]... [[OWNER]-|=|+[PERMISSION]] [FILE]... `
---

| **OPTION** | description |
|:---:|:---:|
| -R, --recursive | change files and directories recursively |
| -v, --verbose | verbose mode |

| **OWNER** | description |
|:---:|:---:|
| u | user |
| g | group |
| o | others |
| a | all |

| **PERMISSION** | description |
|:---:|:---:|
|  | no permissions |
| x | execute |
| w | write |
| r | read |

### ***Numeric mode syntax*** ` chmod [OPTION]... [UGO-NUMBER] [FILE]... ` <br>
#### each digit represents the permissions for ` user `, ` group ` and ` others `.

| **[UGO-NUMBER]** | description |
|:---:|:---:|
| 0 | no permissions |
| 1 | execute |
| 2 | write |
| 4 | read |

## Examples:
` chmod ug+wx file.txt `

` chmod -vR 750 ./ `

` chmod o= file.txt `