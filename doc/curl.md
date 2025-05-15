# curl

**Client for URLs** > transferring data with URLs, supporting various protocols.
---

` curl [OPTION]... [URL] `
---

| **OPTION** | description |
|:---:|:---:|
| -X, --request [GET\|POST] | specify request method |
| -H, --header [HEADER] | pass custom headers to the server |
| -d, --data [DATA] | send data in a POST request |
| -u, --user [USERNAME:PASSWORD] | specify username and password for authentication |
| -O, --remote-name | save file with the same name as the remote file |
| -o, --output [FILENAME] | save output to a file |
| -I, --head | fetch the headers only (HEAD request) |
| -L, --location | follow redirects |
| -v, --verbose | make the operation more talkative |
| -s, --silent | silent mode. don't show progress meter or error messages |

## Examples
` curl -X POST -d "name=hamed&age=33" http://example.com/api `

` curl -X POST -H "Content-Type: application/json" -d '{"name":"hamed", "age":33}' http://example.com/api `

` curl -u username:password http://example.com `

` curl -vO http://example.com/file.txt `

` curl -o output.html http://example.com `