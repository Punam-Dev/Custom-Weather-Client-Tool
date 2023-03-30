# Introduction

Create a Command Line tool in Dotnet ( preferably in dotnet core ) which will take user
input as City and gives the weather related information e.g. Temperature , Windspeed
etc.
The command Line Tool must call the following end point to fetch the related information
https://api.open-meteo.com/v1/forecast?latitude=18.9667&longitude=72.8333&current_weather=true

# Pre-requisite

1. Visual Studio 2019 or higher
2. NET Core SDK 3.1.426, can be downloaded from https://download.visualstudio.microsoft.com/download/pr/b70ad520-0e60-43f5-aee2-d3965094a40d/667c122b3736dcbfa1beff08092dbfc3/dotnet-sdk-3.1.426-win-x64.exe.

# Build & Run Application

1. Open solution file (/src/WeatherClient.sln) using Visual Studio 2019 or higher.
2. To build application click on Build Solution under Build tab.
3. To run the application in debug mode click on Start Debugging under Debug tab. 
4. To run the application without debug mode click on Start Without Debugging under Debug tab.

# How to use?

1. Open local json city dump file(\src\WeatherClient.ConsoleApp\in.json) and copy any city name.
2. Run the application by following Build & Run Application step.
3. It will ask you to enter city name. Paste the city name.

