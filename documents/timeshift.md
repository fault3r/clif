# timeshift

Timeshift is a system restore utility for create and manage snapshots of system.
---

` timeshift [COMMAND] `
---

| **COMMAND** | description |
|:---:|:---:|
| | open timeshift in GUI mode |
| --list | list all available snapshots |
| --create | create a new snapshot |
| --restore | restore to a snapshot |
| --delete | delete a snapshot |

### ` sudo apt install timeshift ` to install timeshift.

### `--snapshot [NAME]` use this flag to restore or delete a specific snapshot.

## Examples
` sudo timeshift --create `

` sudo timeshift --restore --snapshot "2025-09-25_04-26-26" `

` sudo timeshift --delete `