#!/bin/bash

echo "Don't enter a passphrase!"

ssh-keygen -f travis_rsa
i=1; 

while read line 
do
  printf "TRAVIS_SSH_KEY_%03d=" $i 
  echo $line
  i=$[i+1]  
done < travis_rsa | travis encrypt -s 
