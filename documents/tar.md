# tar

**Tape Archive** > create, extract and manipulate tar archive files.
---

` tar [OPTION]... [FILE]... `
---

| **OPTION** | description |
|:---:|:---:|
| -f [FILENAME] | specify the archive filename <br> |
| -c | create a new archive |
| -x | extract files from an archive |
| -t | list the contents of an archive |
| -z | compress or decompress using gzip(.tar.gz or .tgz) |
| -j | compress or decompress using bzip2(.tar.bz2) |
| -C [DIRECTORY] | change to directory before performing operations |
| -v | verbosely list files processed |

### all tar options are typically used with ` -f ` option.

## Examples:
` tar -vcf my-archive.tar test/ test.txt test.cs `

` tar -vxf my-archive.tar `

` tar -zxf my-gzip.tgz -C /home/extract `

` tar -vtf arch.tar `
