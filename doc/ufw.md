# ufw

**Uncomplicated Firewall** > managing a netfilter firewall on Linux systems.
---

` ufw [COMMAND] `
---

| **COMMAND** | description |
|:---:|:---:|
| status | status of firewall |
| status verbose | show verbose firewall status |
| enable | enables the firewall |
| disable | disables the firewall |
| reload | reload firewall |
| reset | reset to default settings |
| allow [SERVICE\|PORT] | allow a specific service or port number |
| delete allow [SERVICE\|PORT] | delete an allow rule |
| deny [SERVICE\|PORT] | deny a specific service or port number |
| delete deny [SERVICE\|PORT] | delete a deny rule |

## Examples
` sudo ufw status `

` sudo ufw allow samba `

` sudo ufw deny 2600 `

` sudo ufw delete allow samba `