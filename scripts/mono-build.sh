#!/bin/sh -x
#export EnableNuGetPackageRestore=true
cd ../src
xbuild $@ 
