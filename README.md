EndPoint is an API developed with the ASP.NET Web API framework using .NET 8. The objective of this project is to allow users to manage their daily tasks, offering functionalities such as creating, editing, deleting and listing tasks. Additionally, you can filter tasks by status, due date and priority.

**Features**
1) Create, edit and delete tasks
2) List tasks
3) Filter by status, due date and priority
4) JWT authentication

**The Tasks entity has the following fields:**
**Id**: int (Unique identifier)
**Title**: string (Task title)
**Description**: string (Detailed description of the task)
**Due Date**: DateTime (Deadline for completion)
**Completed**: bool (Indicates whether the task has been completed)
**Priority**: string (Task priority, e.g.** High, Medium, Low**)

**Technologies Used**
Language: C#
Framework: ASP.NET Web API
Database: MySQL
ORM: Entity Framework Core
Tests: XUnit
Documentation: Swagger
Design Pattern: Repository
Authentication: JWT
Rate Limiter: Control of requests per user


**How to Run the Project**

1) Open the project in Visual Studio.

2) Configure the connection string in the appsettings.json file with your database data:

"ConnectionStrings": {
  "Default": "Server=;Port=;User ID=;Password=;Database="
}

3) Run the update-database command in the Package Manager Console to create the tables in the database.

4) Start the project.

5) Visit Swagger at https://localhost:7001/swagger/index.html to test the API.

**Authenticate to use the task endpoints:**

Create an account using the /api/users/register endpoint.

Log in with the /api/users/login endpoint to obtain the authentication token.

In Swagger or your API testing tool, click "Authorize".

Enter the token in the format:

Bearer your_token_generated

After authorizing, you will be able to use the Tasks endpoints.
