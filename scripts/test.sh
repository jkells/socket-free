#!/bin/sh
cat ~/.ssh/id_rsa | md5sum
#ssh -o StrictHostKeyChecking=no git@github.com
