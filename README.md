# Parcel Pirate
_snatches messages from all over the high seas!_

## What is it?
- a bare minimum polling service

## Features
- it will poll **slack**
 - future: email, lync, and other data sources (like web scrape chat)
- designed to be run _slow_ loop or cron job
- minimal dependencies
- minimal setup
- persisted connections to 3rd party services avoided

## notes
- [godep](https://github.com/tools/godep)
 - ```go get github.com/tools/godep```
 - todo: figure this out later. using isolated VMs for development anyways
- ```go test ./src/github.com/wambosa/*```