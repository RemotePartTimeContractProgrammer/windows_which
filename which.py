import os
import sys

dirs  = (os.environ['PATH']).split(';')
clean_dirs = filter(lambda dir: dir != '', dirs)
for dir in clean_dirs :
    expected_path = dir + "\\" + sys.argv[1]  
    if os.path.isfile(expected_path) :
        print 'found at ' + expected_path
        exit()

print 'command not found'
