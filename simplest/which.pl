$cmd = @ARGV[0];
my @dirs = split /;/, $ENV{'PATH'};
foreach $dir (@dirs){
    $path = $dir . "\\" . $cmd;
    if (-e $path){
        print "found match at $path\n";
        exit;
    }
}

print "command $cmd not found";
