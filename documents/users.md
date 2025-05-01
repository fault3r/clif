# users

display the usernames of users currently logged into the system.
---

` users `
---

### ` cat /etc/passwd ` display all users.

### ` id [USERNAME] ` display user and groups information for the specified user.

---

### ***Add User*** ` adduser [USERNAME] ` <br>
#### creates a new user and prompts you to set a password and other details.

---

### ***Delete User*** ` deluser [USERNAME] ` <br>
#### remove a user account.
#### use ` deluser --remove-home [USERNAME] ` to delete home directory of user.

## Examples:
` sudo adduser t-user `

` sudo id t-user `
