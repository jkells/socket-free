#!/bin/bash

curl -T ./src/bin/Debug/SocketFree.exe -u $DEPLOY_CRED ftp://vps2.kared.net
