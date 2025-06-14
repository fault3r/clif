# lshw

**List Hardware** > list hardware configuration information.
---

` lshw [FORMAT] [OPTION]... `
---

| **FORMAT** | description |
|:---:|:---:|
| | list all hardware configuration |
| -businfo | list bus information |
| -short |  list a brief summary |
| -html | generate an html report |
| -json | generate a json report |

| **OPTION** | description |
|:---:|:---:|
| -class [processor\|memory\|display\|storage\|disk\|network\|bus\|multimedia\|input\|bridge] | filter the output based on specific hardware |

## Examples
` sudo lshw -short `

` sudo lshw -html -class disk > hw-info.html `
