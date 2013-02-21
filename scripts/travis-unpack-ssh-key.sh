#!/bin/bash

echo "Unpacking TRAVIS_SSH_KEY into ~/.ssh/id_rsa"
env | grep TRAVIS_SSH_KEY | sort | cut -d= -f2 > ~/.ssh/id_rsa
chmod 700 ~/.ssh/id_rsa
