<?php
$cmd = $argv[1];
$path_dirs = explode(';', getenv("PATH"));
$cleaned_dirs = array_filter( $path_dirs, function ($dir){return ($dir != null && $dir != "");});
foreach($cleaned_dirs as $dir){
    if (file_exists("$dir\\$cmd")){
        echo "$cmd found at $dir\\$cmd";
        exit;
    }
}
echo "$cmd not found";
?>