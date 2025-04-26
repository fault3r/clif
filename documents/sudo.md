# sudo 

**superuser do** > execute a command as another user!
---

` sudo [OPTION] [COMMAND] `
---

| **OPTION** | description |
|:---:|:---:|
| -u, --user [USER] | run the command as the specified user |
| -b, --background | run the command in the background |
| -i, --login | login to root access |

### ` sudo su - ` switch the current user to the superuser

## Examples:
` sudo apt update `

` sudo --user root ls `

` sudo -i `