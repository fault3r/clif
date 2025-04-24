# usermod

**User Mode** > modify user accounts, change the username, home directory, shell, and group memberships.
---

` usermod [OPTION] [USERNAME] `
---

| **OPTION** | description |
|:---:|:---:|
| -d, --home [DIRECTORY] | change the user's home directory |
| -d [DIRECTORY] -m, --move-home  | move the contents of the old home directory |
| -l, --login [USERNAME] | change the username |
| -aG [GROUPNAME] | append user to a group |

## Examples:
` sudo usermod -d /home/user-home -m t-user `
