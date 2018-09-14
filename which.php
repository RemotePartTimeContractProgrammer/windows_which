<?php

$cmd = null;
$show_dirs = false;

get_params($argv);

$path_string = getenv("PATH");
$path_dirs = explode(';', $path_string);
$cleaned_dirs = array_filter( $path_dirs, function ($dir){
    return ($dir != null && $dir != "");
});
$result = null;

foreach($cleaned_dirs as $dir){
    if ($show_dirs)
        echo "$dir\n";

    try{
        if ($handle = @opendir($dir)) {
            while (false !== ($entry = readdir($handle))) {
                if ($entry == $cmd){
                    $result = $dir;
                    break;
                }
            }
            closedir($handle);
        }else {
            echo "unable to open $dir\n";
        }
    }catch(Exception $e){
        echo "Error opening $dir";
    }

    if ($result != null)
        break;
}


if ($result != null)
    echo "found at $result\n";
else
    echo "command $cmd not found.\n";


exit (($result != null) ? 0 : 1);

//////////////////////

function usage() {
    echo "usage: php which.php [-p] <command>\n";
    exit(1);
}

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