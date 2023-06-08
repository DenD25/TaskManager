# TaskManager

###### Project Status: In Development

The project consists of an API for project management. 

After creating an account, users can create their projects, create tasks for these projects, and add or remove other users from the projects. Additionally, users can assign specific users to each task for execution. Owners have the ability to change the status of tasks (to do, in progress, done). 

Users can also set or change their profile pictures (previous photo deleting). Cloudinary is used for storing photos.


## Technologies

The project uses the following technologies:

- .NET Core
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- AutoMapper
- CloudinaryDotNet
- ASP.NET Core Identity
- Swagger

## Project Structure

- `ApplicationCore` : Contains the application logic and interfaces.
- `Infrastructure` : Contains data logic, models and DTOs.
- `TaskManagerAPI` : Contains the Web API controllers.

## Planned Features

- More roles in projects for managment ( at the moment only `owner` can make changes );
- Chat between members of project;
- Front-End part of project;
- Adding third-party APIs to extend the project;
- Email confirmation;
