#!/bin/sh
cat ~/.ssh/id_rsa | head -n2
cat ~/.ssh/id_rsa | tail -n2
#ssh -o StrictHostKeyChecking=no git@github.com
