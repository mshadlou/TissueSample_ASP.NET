
# TissueSample_ASP.NET

> You must have Visual Studio and install dependencies by .NET CLI or from NuGet Package Manager.

This repo belongs to "One problem, two solutions" and here I present solution using ASP.NET Stack.
Another repo is ![MEAN-Stack example](https://github.com/mshadlou/TissueSample_MEAN).

## Goals
Imagine that you are going to have a BioBank that get the Collections and Samples associated with each Collection. Data should be saved in DB and user should be able to alter, add, or delete tham.
Having said, I'm going address following goals by this repo.: <br> 
1. Display a list of collections on the home page (including their title and associated disease).
2. Drill into a collection’s record to view the details of their currently associated samples.
3. Add a new sample to an existing collection.
4. Create a new collection.
5. Follow OOP best practices by enabling maintainability, testability, usability, and extensibility.
6. Deploy in the cloud

This demo has been developed by Microsoft ASP.NET Stack based on Blazor WebAssembly App; it means that we have three parts in our project:
* Client – It contains the client side/front-end code and the pages.
* Server – It contains the server side/back-end codes like database connection, controllers, and web API.
* Shared – It contains the shared code that can be accessed by both client and server.


### To run the application

1. Install Microsoft Visual Studio.
2. Clone this repository.
3. Open `TissueSample2.sln` by visual studio and press `Ctrl+F5`
4. Open `https://localhost:44310/` to view the frontend.


## High-level design

### Database, Models, and Entity Framework
In this project, we have two tables and they have been created in Azure SQL DB by following command.

``` SQL
CREATE TABLE [collections] (
    [c_id]         INT            IDENTITY (1, 1) NOT NULL,
    [disease_term] NVARCHAR (500) NOT NULL,
    [title]        NVARCHAR (500) NOT NULL,
    [date]         DATE           NOT NULL,
    CONSTRAINT [PK_collections] PRIMARY KEY CLUSTERED ([c_id] ASC)
);

CREATE TABLE [samples] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [c_id]        INT            NOT NULL,
    [donor_count] INT            NOT NULL,
    [mat_type]    NVARCHAR (500) NOT NULL,
    [date]        DATE           NOT NULL,
    CONSTRAINT [PK_samples] PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([c_id]) REFERENCES [collections] ([c_id])
);
```

Next step is to get the connection string and set it up into the `Server/appsettings.json` <br>

Owing to the tables, we create two model classes (`Schema`) for Collections and Samples in Shared folder. They will contain the model properties and annotated data. You can change the schema for individual tables.
To carry out the data validation, I'm using a mix of Data Annotation and Fluent API. Former is shown in both models while later is explained in `Server/Models/DatabaseContext.cs` where I explain the Entity Mapping to multiple tables using Entity Framework.
Configuration of the EF is applied by `Microsoft.EntityFrameworkCore.ModelBuilder` where the `DbContext` class has a method called `OnModelCreating` taking an instance of `ModelBuilder` as a parameter.<br> 
Put it simple, DatabaseContext.cs will help our code to add data access layer to the application.<br>

### Server's Services
* First service in server is carried out by CRUD operations on the database. On this matter, two Services have been defined: `CollectionManager.cs` and `SampleManager.cs`. These services implements two interfaces as `ICollection` and `ISample`.
* Second service in server is provided by Web API defined by `CollectionController.cs` and `SampleController.cs`. 
* Third service is on adding razor pages. This is mapped by `builder.Services.AddRazorPages()` in `Program.cs` 

### Client
I have implemented several Razor components including Dashboard, Modal, Sample Form and Collection Form. Dashboard covers the tables and data-binding. It injects the `IDashboardManager` which is implemented by `DashboardManager` class. 
Using dependency injection of IModal, ICollectionManager and ISampleManager, DashboardManager presents a class of controlling all compoents of Razor view Comps.<br>

By passing an instance of `IDashboardManager` to other view components such as Modal, Sample Form and Collection Form, we are able to implement better maintainability, and extensibility.



## Cloud
### Azure Web App
Azure PaaS enables developers to build, deliver, monitor, scale and run applications entirely in the cloud.
I have enabled the integration between Azure Deops Repos and Azure Paas. Check the demo in following link.
<a href="https://tissuesample.azurewebsites.net/"> Live Demo</a>


[![License: GPL v2](https://img.shields.io/badge/License-GPL_v2-blue.svg)](https://www.gnu.org/licenses/old-licenses/gpl-2.0.en.html)
