# Getting Start

creating project

```bash
dotnet new webapi -o [name]
```

# Runing Project

## with cmd version

```bash
dotnet run
```

## with docker

```bash
docker build -t vichakuy .
docker run -it --rm -p [any port]:80 --name aspnetcore_sample vichakuy
```

# Folder Structure

1. Controllers 

keep the Controllers files

2. Properties

keep configuration of statring server

3. Program.cs

When we start server, this file will be executed

4. Startup.cs

invoke after Program.cs, keeping packages configurations and setting serivec, env..

5. [projectname].csproj

keep configuration of this project

# Package

```bash
dotnet add package BCrypt.Net-Next
```