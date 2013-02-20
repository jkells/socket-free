#!/bin/bash

curl -Q '-SITE CHMOD 644 SocketFree.exe' -T ./src/bin/Debug/SocketFree.exe -u $DEPLOY_CRED ftp://vps2.kared.net
