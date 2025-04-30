# touch

create empty files or to update the timestamps of existing files.
---

` touch [OPTION]... [FILE]... `
---

| **OPTION** | description |
|:---:|:---:|
|  | create new file |
| -t | set timestamp, `yyyymmddhhmm` |
| -r [FILE] | use another file's timestamp as reference |
| -c [FILE] | update the timestamp only if the file already exists (no create)|

## Examples:
` touch test1.txt test2.txt `

` touch -r ref.txt new.txt `

` touch -t 199209250426 file.cs `