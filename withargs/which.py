import fnmatch
import os
import sys

def usage() :
    print 'which -p <command>'
    exit()


def check_exact(dir, file2find) :
    if show_dirs :
        print 'checking in ' + dir
    try:
        for filename in os.listdir(dir):
            if filename == file2find:
                return True
        return False
    except Exception as e:
        print e
        return False


def find_in_dirs(dirs, file2find) :
    for dir in dirs :
        if dir == '':
            continue
        result = check_exact(dir, file2find)
        if result == True :
            print 'found in ' + dir + '\\' + file2find
            return True
    print file2find + ' not found.'
    return False



dirs  = (os.environ['PATH']).split(';')
show_dirs = False
file2find = ''

if len(sys.argv) == 3:
    if sys.argv[1] == '-p':
        show_dirs = True
        file2find = sys.argv[2]
    else :
        usage()
else :
    file2find = sys.argv[1]
    

find_in_dirs(dirs, file2find)


