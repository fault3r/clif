# systemctl

**System Control** > query or send control commands to the system manager.
---

` systemctl [COMMAND] `
---

| **COMMAND** | description |
|:---:|:---:|
| list-units ["EXPRESSION] | display the currently active units <br> pass `--all` to see loaded but inactive units |
| list-unit-files [EXPRESSION] | display all unit files available on the system |
| --type=service\|socket\|target\|mount\|automount\|timer\|path\|slice\|scope | filter the output based on the type of units <br> only valid in combination with the `list-units` or `list-unit-files` |
| status [UNIT] | display status of units |
| show [UNIT] | display properties of units |
| start [UNIT] | start(activate) units |
| stop [UNIT] | stop(deactivate) units |
| restart [UNIT] | start or restart units |
| reload [UNIT] | reload units |
| enable [UNIT] | enable a unit to start at boot |
| disable [UNIT] | disable a unit from starting at boot |
| daemon-reload | reloads the systemd manager configuration |
| suspend | suspend the system |
| reboot | reboot the system |
| poweroff | shutdown the system |

## Examples:
` systemctl list-units --all --type=service "systemd*" `

` systemctl status "systemd*" `

` systemctl reboot `