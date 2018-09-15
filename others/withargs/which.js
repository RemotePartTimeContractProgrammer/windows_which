var fs = require('fs')


class which {
    log(msg) { console.log(msg) }

    usage(msg) {
        if (msg)
            this.log(msg)
        this.log("Usage: " + __filename + " path/to/directory")
        process.exit(-1)
    }

    check_exact(dir, cmd) {
        try {
            let files = fs.readdirSync(dir)
            return (files.filter(i => i == cmd).length == 1)
        } catch (ex) {
            this.log('error: ' + ex)
        }

        return false
    }

    Resolve() {

        if (process.argv.length <= 2)
            this.usage("")

        let show_paths = false
        let cmd = ""

        for (let i = 2; i < process.argv.length; i++) {
            if (process.argv[i] == '-p')
                show_paths = true
            else {
                if (cmd != "")
                    this.usage("command set twice")
                cmd = process.argv[i]
            }
        }

        let syspath = process.env.path
        let syspath_folders = syspath.split(';').filter(dir => dir != "")
        let found = false

        for (let i = 0; i < syspath_folders.length; i++) {
            let dir = syspath_folders[i]

            if (show_paths)
                this.log(dir)

            if (this.check_exact(dir, cmd)) {
                this.log('Found in: ' + dir)
                found = true
                break
            }
        }

        if (!found)
            this.log('command not found')
    }

}


new which().Resolve()

