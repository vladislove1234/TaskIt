# TaskIt
Welcome to the main repository of _TaskIt_.

## About
Тут має бути багато тексту 

## How to run
### _Installation_
For back-end:                 
Install [ASP.NET](https://dotnet.microsoft.com/download/dotnet/3.1) 
and [MSSql Server 2019](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
For front-end:                  
Install [Node.js and npm](https://nodejs.org/en/).
### _Start_
If you start app at first time, you need run these commands to setup db:
```
cd server
dotnet restore
dotnet ef database update
```
To run an application, you need to open terminal in project folder and run these two commands:
```sh
cd client
npm run app
```
After these commands:
- Back-End server will be started at port 5001;
- All Front-End dependencies will be installed;
- Application will be built;
- Front-End will be started at port 8080.

Also, you will need to open http://localhost:8080/ in your browser.

