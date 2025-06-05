# du

**Disk Usage** > analyze and report on disk usage within directories and files.
---

` du [OPTION]... [FILE] `
---

| **OPTION** | description |
|:---:|:---:|
| -a, --all | display disk usage for all files, not just directories (by default) |
| -h, --human-readable | print sizes in human readable format |
| -s, --summarize | display only a total disk usage 
| -d, --max-depth [NUMBER] | limit the depth of directory traversal to `NUMBER` levels |

## Examples
` du -sh `

` du -ha -d 2 ~/ `