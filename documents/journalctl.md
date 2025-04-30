# journalctl

**Journal Control** > view logs from the system and services in a structured format.
---

` journalctl [OPTION]... `
---

| **OPTION** | description |
|:---:|:---:|
| | display all system logs |
| -b, --boot -[NUMBER] | display current boot logs <br> or logs from previous `NUMBER` boots |
| -f, --follow | follow the journal |
| -S, --since ["yyyy-mm-dd [hh:mm:ss]"] | logs from the beginning of SINCE up to the present time |
| -U, --until ["yyyy-mm-dd [hh:mm:ss]"] | logs up to UNTIL |
| -u, --unit [UNIT] | logs for a specific unit |
| -p, --priority emerg\|alert\|crit\|err\|warning\|notice\|info\|debug | filter logs by priority level |


## Examples:
` journalctl -b -f `

` journalctl --boot -3 `

` journalctl --since "2025-04-30 18:00:00" `

` journalctl -S "2025-04-25" -U "2025-04-30" `