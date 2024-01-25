# SyncData - Database Synchronization
The project aims to showcase integration with two relational (MySQL) databases within a single application.
- Access the live application [here](https://aspnetprojmvc20240125005454.azurewebsites.net/).

## Summary

- [Description](#description)
- [Features](#features)
- [Getting Started](#getting-started)
  - [How to Run Locally](#how-to-run-locally)
     - [Technologies](#technologies)
     - [Running](#running)
- [Deploy](#deploy)
   - [Deployment Instructions](#deployment-instructions)

## Description
The Project Name is a web application developed using ASP.NET Core with Razor Pages, providing a user-friendly interface to interact with a MySQL database.
This application serves as a platform to manage customer data stored in the MySQL database and includes synchronization capabilities with another MySQL database.
**Components:**
- **MySqlController:** A controller responsible for interacting with the MySQL database. It includes methods to retrieve existing customer data, insert and delete customer records.
- **MySqlController2:** A controller responsible for interacting with another MySQL database and synchronize the data.
- **DatabaseController:** A Controller responsible for making the connection between the two databases.
- **CustomersModel:** This model class is responsible for mapping the customer entity to the database, facilitating seamless interactions between the application's logic and the underlying data storage.
- **FrontEnd:** The FrontEnd component is a dynamic ASP.NET Core MVC application designed to provide users with an intuitive interface for seamless interaction with customer data.

## Features
- **MySQL Database Management:** View, add, update, and synchronize customer data stored in a MySQL database.
- **Secondary MySQL Database:** Another MySQL database is integrated to enable synchronization functionalities. 
This database, hosted independently, stores a copy of customer data for redundancy and backup purposes.

## Getting Started
### How the Page Works
1. Add a customer by filling the "Add Customer" form.
2. The new customer will appear with Status = 1 in the database.
3. Sync the databases by clicking the "Sync Databases" button. This will insert the new customers into the second database and change the status to 2.

### How to Run Locally
**Technologies**
- [dotnet](https://dotnet.microsoft.com/download) .NET Core SDK
- [mysql](https://dev.mysql.com/downloads/installer/) MySQL Server

### Running
- Clone the repository: https://github.com/espedrozo/AspNetProjMVC.git
- Run "AspNetProjMVC" project on Visual Studio


## Deploy
The project is currently deployed using Azure services. You can access the live application [here](https://aspnetprojmvc20240125005454.azurewebsites.net/).

**Deployment Instructions**

If you want to deploy a similar project, follow these steps:

1. Azure Deployment:
- Set up an Azure account and create a new web app or deploy the project to an existing Azure web app.
- Configure the connection strings in the Azure portal to connect the app to the primary MySQL database.

2. MySQL Database Setup:
- Set up your primary MySQL database, ensuring it is accessible to the deployed Azure web app.
- Configure the application's connection string in the Azure portal or the app settings to connect to this database.

3. Secondary MySQL Database:
- Set up a secondary MySQL database independently (if applicable).
- Configure the application to connect to this secondary database for synchronization purposes.
