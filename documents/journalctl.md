# journalctl

**Journal Control** > view logs from the system and services in a structured format.
---

` journalctl [OPTION]... `
---

| **OPTION** | description |
|:---:|:---:|
| | display all journals |
| --system | display system journal |
| --user | display the current user journal |
| -b, --boot -[NUMBER] | display current boot journal <br> or journal from previous `NUMBER` boots |
| --list-boots | display list all boot IDs |
| -n, --lines | limit the output to a specific number of lines |
| -f, --follow | follow the journal |
| -S, --since [DATE [TIME]] | logs from the beginning of `SINCE` up to the present time |
| -U, --until [DATE [TIME]] | logs up to `UNTIL` |
| -u, --unit [UNIT] | logs for a specific unit |
| -p, --priority emerg\|alert\|crit\|err\|warning\|notice\|info\|debug | filter logs by priority level |

## Examples:
` journalctl -b -f `

` journalctl --boot -2 `

` journalctl --since "2025-04-30 18:00:00" `

` journalctl -S "2025-04-25" -U "2025-04-30" `

` journalctl --system -u docker -p info -n 10 `