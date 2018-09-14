<?php

///////////// main ///////////////

$cmd = null;
$show_dirs = false;

get_params($argv);
$cleaned_dirs = get_path_dirs();
$result = search_in_dirs($cleaned_dirs, $cmd, $show_dirs);
if ($result != null)
    echo "found at $result\n";
else
    echo "command $cmd not found.\n";
exit (($result != null) ? 0 : 1);

////////////////////////

/* search for $cmd in $dirs, optionally show directories searched */
function search_in_dirs($dirs, $cmd, $show_dirs)
{
    foreach($dirs as $dir){
        if ($show_dirs)
            echo "$dir\n";

        if (check_exact($dir, $cmd))
            return $dir;
    }

    return null;
}

/* look for an exact filename match on the files in $dir */
function check_exact($dir, $cmd)
{
    $result = false;
    if ($handle = @opendir($dir)) {
        while (false !== ($entry = readdir($handle))) {
            if ($entry == $cmd){
                $result = true;
                break;
            }
        }
        closedir($handle);
    }else {
        echo "unable to open $dir\n";
    }

    return $result;
}

/* get an array of directory paths from the path environment variable string */
function get_path_dirs ()
{
    $path_string = getenv("PATH");
    $path_dirs = explode(';', $path_string);
    $cleaned_dirs = array_filter( $path_dirs, function ($dir){
        return ($dir != null && $dir != "");
    });

    return $cleaned_dirs;
}

/* learn 'em */
function usage() {
    echo "usage: php which.php [-p] <command>\n";
    exit(1);
}

/* parse command line arguments */
function get_params($argv){
    global $cmd;
    global $show_dirs;

    if (sizeof($argv) < 2)
        usage();

    for ($i = 1; $i < sizeof($argv); $i++){
        $arg = $argv[$i];
        if ($arg == '-p')
            $show_dirs = true;
        else if ($cmd == null)
            $cmd = $arg;
        else{
            echo "invalid argument: $arg\n";
            usage();
        }
    }

    if ($cmd == null)
        usage();
}


?>