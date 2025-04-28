# systemctl

**System Control** > query or send control commands to the system manager.
---

` systemctl [COMMAND] `
---

| **COMMAND** | description |
|:---:|:---:|
| list-units [PATTERN] | display the currently active units <br> pass `--all` to see loaded but inactive units |
| list-unit-files [PATTERN] | display all unit files available on the system |
| `--type=` option used to filter the output based on the type of units | \|service, socket, target, mount, automount, timer, path, slice, scope\| |
| --------------------------------------------- | --------------------------------------------- |
| status [PATTERN] | display status of units |
| show [PATTERN] | display properties of units |
| start [UNIT] | start(activate) units |
| stop [UNIT] | stop(deactivate) units |
| restart [UNIT] | start or restart units |
| reload [UNIT] | reload units |
| enable [UNIT] | enable a unit to start at boot |
| disable [UNIT] | disable a unit from starting at boot |
| --------------------------------------------- | --------------------------------------------- |
| daemon-reload | reloads the systemd manager configuration |
| --------------------------------------------- | --------------------------------------------- |
| suspend | suspend the system |
| reboot | reboot the system |
| poweroff | shutdown the system |

## Examples:
` systemctl list-units --all --type=service "systemd*" `

` systemctl status "systemd*" `

` systemctl reboot `