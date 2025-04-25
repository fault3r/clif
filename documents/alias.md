# alias

create shortcuts for longer commands or to customize command behavior.
---

` alias [OPTION] [NAME='VALUE'] `
---

| **OPTION** | description |
|:---:|:---:|
|  | list of all currently defined aliases |
| -p | print all defined aliases in a reusable format |

### ` unalias [NAME] ` to remove an alias.

### by default aliases are only available in the current shell session. <br> to make them Permanent, configure aliases in ` ~/.bash_aliases ` or directly in ` ~/.bashrc `.

## Examples:
` alias -p `

` alias cls='clear' `
