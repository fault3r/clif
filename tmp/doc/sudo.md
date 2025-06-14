# sudo 

**superuser do** > execute a command as another user.
---

` sudo [OPTION] [COMMAND] `
---

| **OPTION** | description |
|:---:|:---:|
| -u, --user [USER] | run the command as the specified user |
| -b, --background | run the command in the background |
| -i, --login | login to root access |

#### use ` sudo su - ` to switch to the superuser.

## Examples
` sudo apt update `

` sudo --user root ls `

` sudo -i `