var fs = require('fs')

function log(msg) { console.log(msg) }

function usage(msg) {
    if (msg)
        log(msg)
    log("Usage: " + __filename + " path/to/directory")
    process.exit(-1)
}

function check_exact(dir, cmd) {
    try {
        let items = fs.readdirSync(dir)
        return (items.filter(i => i == cmd).length == 1)
    } catch (ex) {
        log('error: ' + ex)
    }

    return false
}

let show_paths = false
let cmd = "";
let syspath = process.env.path
let syspath_folders = syspath.split(';').filter(dir => dir != "")
let found = false

if (process.argv.length <= 2)
    usage("")

for (let i = 2; i < process.argv.length; i++) {
    if (process.argv[i] == '-p')
        show_paths = true
    else {
        if (cmd != "")
            usage("command set twice")
        cmd = process.argv[i]
    }
}

for (let i = 0; i < syspath_folders.length; i++) {
    let dir = syspath_folders[i]

    if (show_paths)
        log(dir)

    if (check_exact(dir, cmd)) {
        log('Found in: ' + dir)
        found = true
        break
    }
}

if (!found)
    log('command not found')
