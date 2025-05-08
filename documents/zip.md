# zip

create, extract and manipulate zip archive files.
---

` zip [OPTION] [OUTPUT] [FILE]... `
---

| **OPTION** | description |
|:---:|:---:|
| -u | only changed or new files |
| -r | archive directories recursively |
| -d | delete a file or folder from the contents of a zipfile <br> use `[DIRECTORY]/\*` to delete a whole directory and all content |
| -e | encrypt the zip file with a password |
| -z | add zipfile comment |
| -v | verbose operation |
| -T | test the integrity of the zipfile |

#### `unzip -l [ZIPFILE]` ***list the contents of the zipfile without extracting***
#### `unzip -o [ZIPFILE]` ***overwrite existing files without prompting***
#### `unzip [ZIPFILE] -d [EXTRACT/PATH]` ***specify the target directory for extraction***

## Examples
` zip -r archive.zip file1.txt file2.txt dir3/ `

` zip -d archive.zip dir/\* `

` zip -rezv archive.zip dir/ file.cs `

` unzip -o archive.zip -d extdir/ `
