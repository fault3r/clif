# passwd

**Password** > change or remove user account password.
---

` passwd [OPTION] [USERNAME] `
---

| **OPTION** | description |
|:---:|:---:|
| -d, --delete | delete the password of user account |

#### do not pass `[USERNAME]` to change the password of the current user account.

## Examples
` passwd `

` sudo passwd t-user `

` sudo passwd -d t-user `
