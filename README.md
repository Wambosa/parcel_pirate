# Parcel Pirate
_snatches messages from all over the high seas!_

## What is it?
- a bare minimum polling service

## Features
- it will poll **slack**
 - future: email, lync, and other data sources (like web scrape chat)
- designed to be run slow loop or cron job
- minimal dependencies and minimal setup
- persisted connections to 3rd party services avoided

## notes
- [golang install](https://golang.org/doc/install)
 - ```wget https://storage.googleapis.com/golang/go1.7.3.linux-amd64.tar.gz```
 - ```tar -C /usr/local -xzf go1.7.3.linux-amd64.tar.gz```
 - ```export PATH=$PATH:/usr/local/go/bin```
- [godep install](https://github.com/tools/godep)
 - ```go get github.com/tools/godep```
 - figure this out later. using isolated VMs for development anyways