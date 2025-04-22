# apt

**Advanced Package Tool** > a package management system used by Debian-based Linux distributions.
---

` apt [COMMAND] [OPTION] `
---

| **COMMAND** | description |
|:---:|:---:|
| update | update the local package index with the latest information |
| upgrade | upgrade installed packages to their latest versions |
| list | display a list of system packages |
| show [PACKAGE] | show information of a package |
| install [PACKAGE] | install a package |
| install ./[PACKAGE.deb] | install a deb package |
| remove [PACKAGE] | remove a package |
| search '[PACKAGE]' | search a package |

| **OPTION** | description |
|:---:|:---:|
| -y, --yes | prompt the confirms |

## Examples:
` sudo apt update -y `

` sudo apt update -y && sudo apt upgrade -y `

` sudo apt install neofetch `
