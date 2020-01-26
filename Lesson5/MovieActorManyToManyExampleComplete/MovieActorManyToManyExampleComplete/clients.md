* To start the implementation from scratch you can download und unpack ExamEmptyTemplateMVCReact.zip from the course repository.

* This application is implemented using sqlite. You can change it to a database of your choice in the startup.cs file, Do not forget to add the missing dependecies in the csproj files. In addition you need to create the migrations-script. Check lesson 1,2 and 3 for more information. 

* To setup the application run the following commands in the console inside the main folder of this application:
    - dotnet restore  #It might give you a warning, but that should not be a problem 
    - npm install    #It will download all the dependencies, but it might give you some warnings

* To run the application run the following commands in the console inside the main folder of this application(note: check the location of package.json and csproj files and execute the commands accordingly) 
    - .node_modules/.bin/webpack -w #You could use also this command instead: "npm run fe" which will run the same command
    - dotnet run

To see the application type localhost:5000 in a browser of your choice!

Good luck!