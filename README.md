# WebCampGit

A sample project used to mimic slowness in ASP.NET scenarios. This is used to test use-case and helpfulness of various diagnostic tools associated with ASP.NET for WebApps hosted in Azure App Service

[![Build Status](https://dev.azure.com/puneetgdevops/FrontDoorTest/_apis/build/status/puneetg1983.WebCampGit?branchName=master)](https://dev.azure.com/puneetgdevops/FrontDoorTest/_build/latest?definitionId=3&branchName=master)

# Contributing to this sample project

You can Fork this repo and then generate a standard GIT pull request by following the instructions here - https://help.github.com/articles/creating-a-pull-request-from-a-fork/. Before submitting a pull request make sure that your code works and that you have tested it on an Azure Web App

# Stress Testing this App

There are many tools that exist on the net but my favorite is still TinyGet which is part of IIS 6.0 Resource Kit tools which you can download from https://www.microsoft.com/en-in/download/details.aspx?id=17275


# A sample Stress Test File

Copy this content and move this to a batch file. Replace ```<yourwebApp>``` with the name of your Web App and feel free to add more URLs to this list

```
start cmd /k "c:\Program Files (x86)\IIS Resources\TinyGet\tinyget.exe" -srv:<yourwebApp>.azurewebsites.net -uri:/ -t -threads:5
start cmd /k "c:\Program Files (x86)\IIS Resources\TinyGet\tinyget.exe" -srv:<yourwebApp>.azurewebsites.net -uri:/SlowSql.aspx -t -threads:10
start cmd /k "c:\Program Files (x86)\IIS Resources\TinyGet\tinyget.exe" -srv:<yourwebApp>.azurewebsites.net -uri:/SlowPage.aspx -t -threads:5
start cmd /k "c:\Program Files (x86)\IIS Resources\TinyGet\tinyget.exe" -srv:<yourwebApp>.azurewebsites.net -uri:/YSOD.aspx -t -threads:1
start cmd /k "c:\Program Files (x86)\IIS Resources\TinyGet\tinyget.exe" -srv:<yourwebApp>.azurewebsites.net -uri:/TracePage.aspx -t -threads:2
start cmd /k "c:\Program Files (x86)\IIS Resources\TinyGet\tinyget.exe" -srv:<yourwebApp>.azurewebsites.net -uri:/ -t -threads:5
start cmd /k "c:\Program Files (x86)\IIS Resources\TinyGet\tinyget.exe" -srv:<yourwebApp>.azurewebsites.net -uri:/SlowSql.aspx -t -threads:10
start cmd /k "c:\Program Files (x86)\IIS Resources\TinyGet\tinyget.exe" -srv:<yourwebApp>.azurewebsites.net -uri:/SlowPage.aspx -t -threads:5
start cmd /k "c:\Program Files (x86)\IIS Resources\TinyGet\tinyget.exe" -srv:<yourwebApp>.azurewebsites.net -uri:/YSOD.aspx -t -threads:1
start cmd /k "c:\Program Files (x86)\IIS Resources\TinyGet\tinyget.exe" -srv:<yourwebApp>.azurewebsites.net -uri:/TracePage.aspx -t -threads:2

```

## Deployment Link:
## Deployment Link:
<a href="https://deploy.azure.com?repository=https://github.com/puneet-gupta/WebCampGit" target="_blank">
    <img src="https://azurecomcdn.azureedge.net/mediahandler/acomblog/media/Default/blog/deploybutton.png"/>
</a> 
