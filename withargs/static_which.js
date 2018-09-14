var fs = require('fs')

class static_which {
    static log(msg) { console.log(msg) }

    static usage(msg) {
        if (msg)
            static_which.log(msg)
        static_which.log("Usage: node " + __filename + " <command>")
        process.exit(-1)
    }

    static check_exact(dir, cmd) {
        try {
            let files = fs.readdirSync(dir)
            return (files.filter(i => i == cmd).length == 1)
        } catch (ex) {
            static_which.log('error: ' + ex)
        }

        return false
    }

    static Resolve() {
        if (process.argv.length <= 2)
            static_which.usage("")

        let show_paths = false
        let cmd = ""

        for (let i = 2; i < process.argv.length; i++) {
            if (process.argv[i] == '-p')
                show_paths = true
            else {
                if (cmd != "")
                    static_which.usage("command set twice")
                cmd = process.argv[i]
            }
        }

        let syspath_folders = process.env.path.split(';').filter(dir => dir != "")
        let found = null;

        for (let i = 0; i < syspath_folders.length; i++) {
            let dir = syspath_folders[i]

            if (show_paths)
                static_which.log(dir)

            if (static_which.check_exact(dir, cmd)) {
                found = dir
                break
            }
        }

        static_which.log(
            (found)
            ? "found in " + found
            : "command not found");
    }

}


static_which.Resolve()


