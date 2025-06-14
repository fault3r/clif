
# nano

create and edit text files directly from the terminal.
---

` nano [OPTION]... [FILE] `
---

| **OPTION** | description |
|:---:|:---:|
| -v, --view | view mode (read-only) |
| -Y, --syntax [LANGUAGE] | syntax definition to use for coloring |
| -B, --backup | create a backup of orginal file, ~ appended to backup filename |
| -m, --mouse | enable the use of the mouse |
| -l, --linenumbers | show line numbers in front of the text |

## Examples
` nano -lv examp.txt `

` sudo nano --syntax sh -lm .bashrc.sh `

` nano -B examp.txt `