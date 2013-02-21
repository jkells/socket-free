#!/bin/bash

ssh-keygen -f travis_rsa
i=1; 

while read -r line 
do
  strip_equals=$(echo "$line" | tr '=' '!')
  printf 'TRAVIS_SSH_KEY_%03d="%s"\n' $i "$strip_equals" 
  i=$[i+1]  
done < travis_rsa | travis encrypt -s |awk '{print "  - secure: " $0 }'

