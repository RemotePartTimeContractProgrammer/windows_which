var fs = require('fs')

let syspath_folders = process.env.path.split(';').filter(dir => dir != "")

for (let i = 0; i < syspath_folders.length; i++) {
    if (fs.existsSync(syspath_folders[i] + "\\" +  cmd)) {
        console.log('Found in: ' + syspath_folders[i])
        process.exit();
    }
}

console.log('command not found')

