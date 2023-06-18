# RichsSnackRack
This Project is a .Net sandbox for testing new technologies, design principles, paradigms, packages etc. asl well as documenting any challenges along the way

# Challenges
## Adding Docker Support For MySql

The ability to add Docker support for any .net core project to start with is pretty straight for to start because both Visual Studio and Studio for Mac have convient add Docker Support buttons which will default add docker.

However what is a struggle is any time you need to adjust the Yaml file for docker compose. I'm not awful at using VS For Mac for that so I gave it a shot. I based a lot of wha I need from here [Adding MySQL to ASP.NET Core App With Docker Compose](https://code-maze.com/mysql-aspnetcore-docker-compose/), here [How to Create a MySql Instance with Docker Compose](https://medium.com/@chrischuck35/how-to-create-a-mysql-instance-with-docker-compose-1598f3cc1bee)


My first attempt at building the compose from VS for Mac came out like this.
![Docker Compose First Try](Images/dbenvironmentsmisplaced.png)

I took a guess that I formatted things in correctly so I opened VS Code which can have a number of extensions installed to correctly format Yaml and help me find the problem because format is most likely the problem with Yaml.
![Open in VS Code](Images/TryingVsCodeInstead.png)

After checking the articles I realized that since the environment variables are arrays I just had them misplaced and they needed to moved over. After that however I ran into a different problem with volumes.

![Forgot Volumes](Images/ForgotTheVolumes.png)

Ok after adding the Volumes here is what the final Docker file looked in both VS Code and VS For Mac

![VS For ](Images/FinalLookInVsForMac.png)



So after all these changes I tried another Docker compose again now this time I got this error but after letting everything stopped I realized the web app spun up before the docker container was fully ready

![Web App No Db](Images/WebAppWasActiveBeforeDb.png)

And after all of that 
![Sucess!](Images/Success.png)