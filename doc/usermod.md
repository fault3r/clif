# usermod

**User Mode** > modify user accounts, change the username, home directory, shell and group memberships.
---

` usermod [OPTION] [USERNAME] `
---

| **OPTION** | description |
|:---:|:---:|
| -d, --home [PATH] | change the user's home directory |
| -m, --move-home | move the contents of the old home directory <br> only valid in combination with the `-d` option |
| -l, --login [NEW-USERNAME] | change the username |
| -aG [GROUPNAME] | append user to a group |

#### cannot modify username of account while it is in use.

## Examples
` sudo usermod -d /home/user-new-home -m test-user `

` sudo usermod -aG testgroup test-user `

` sudo usermod --login new-username any-user `
