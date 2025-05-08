# df

**Disk Free** > report the amount of disk space used and available on file systems.
---

` df [OPTION]... `
---

| **OPTION** | description |
|:---:|:---:|
| -a, --all | include pseudo, duplicate and inaccessible file systems |
| -h, --human-readable | print sizes in human readable format |
| -t, --type [FILESYSTEM] | display file systems of a specific type |
| -x, --exclude-type [FILESYSTEM] | exclude file systems of a specific type |

## Examples
` df -h `

` df -ha --type tmpfs `
