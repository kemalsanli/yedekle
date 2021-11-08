[Turkish](https://github.com/kemalsanli/yedekle/blob/main/README.md) | English

 [![Download](https://img.shields.io/github/downloads/kemalsanli/yedekle/total?)](https://github.com/kemalsanli/yedekle/releases)
[![GitHub last commit](https://img.shields.io/github/last-commit/kemalsanli/yedekle?)](https://github.com/kemalsanli/yedekle/commits/main)
[![GitHub](https://img.shields.io/github/license/kemalsanli/yedekle?)](https://github.com/kemalsanli/yedekle/blob/main/LICENSE)
 
 # yedekle

A Windows application that can help you make zero-configuration backups when Git is not used.
[Download.](https://github.com/kemalsanli/yedekle/releases/download/Windows/Yedekle.exe)

## Information

"yedekle" means backup in Turkish, and this app does backup with zero-configuration. Just drag it to your desired folder and start to use it. 

If you wish to send new PR please do not break the *ZERO-CONFIGURATION* principle.

I am not actively developing this project.

## Starting

### Requirements

* A Windows PC.

### Installation

* Since the app is portable not require any specific installation. Just put the application in desired folder.

## Help

Still not came across specific issue, 
```
...but if something goes wrong, just restart.
```

## UI and Usage

For general use drag the application to the desired folder and run.

![UI 1](https://github.com/kemalsanli/yedekle/blob/main/images/yedekle.png?raw=true)

When the "Yedekle" button is pressed, it creates a new folder in the current location named Yedekler in the folder and copies the contents of the current folder with the timestamp and revision number. 

![Folder](https://github.com/kemalsanli/yedekle/blob/main/images/folder.png?raw=true)

The "R" key stands for "Rollback", it overwrites the folder contents with changes from the last revision.

The "C" key stands for "Clear", it deletes all contents in the current directory except the "Yedekler" folder.


## Version History

* 0.4
    * Released.

## Contribute
Feel free to contribute ^^

## License
[MIT](https://github.com/kemalsanli/yedekle/blob/main/LICENSE)
