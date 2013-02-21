#!/bin/bash

echo hello world >> test
git commit -a -m "Commit from travis"
git push origin master 
