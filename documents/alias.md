# alias

create shortcuts for longer commands or to customize command behavior.
---

` alias [OPTION] [NAME='VALUE'] `
---

| **OPTION** | description |
|:---:|:---:|
| -p | print all defined aliases in a reusable format |

### ` alias ` to display list of all defined aliases.

### ` unalias [NAME] ` to remove an alias.

### by default aliases are only available in the current shell session. <br> to make them Permanent, configure aliases in ` ~/.bash_aliases ` or directly in ` ~/.bashrc `.

## Examples
` alias -p `

` alias cls='clear' `

` unalias cls `
