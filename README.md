# Vehicle Performance Software Skills Challenge - Rest API

This .NET 8 api is responsible for parsing JSON files, storing the parsed data in a database and then exposing the data through a RESTful API. It allows clients to query, filter, and aggregate data.

## Development Environment

### Dependencies

This project is built using .NET 8 and relies on the following key dependencies:

- **Entity Framework Core** - For ORM support and managing database migrations.
- **SQLite** - Chosen as the database for development due to its simplicity and ease of setup.
- **Swashbuckle (Swagger)** - For API documentation and testing purposes.

## What Has Been Built

The Vehicle Performance Software Skills Challenge REST API is designed to parse JSON files related to vehicle performance data, store this data in a SQLite database, and expose the data through a RESTful API. This setup allows for querying, filtering, and aggregating data efficiently.

### Key Features

- **JSON Parsing**: The API includes functionality to parse JSON files containing vehicle performance data, such as circuits, driver standings, drivers, lap times, and races.
- **Data Storage**: Parsed data is stored in a SQLite database, providing a lightweight yet powerful way to manage data for development purposes.
- **RESTful API**: The API exposes endpoints for accessing and manipulating the stored data, allowing clients to perform operations like querying for specific data, filtering results, and aggregating data points.
- **Automatic Database Management**: On startup, the application automatically creates the database (if it doesn't exist) and applies any pending migrations, ensuring the database schema is always up to date.
- **Data Seeding**: For local debugging and testing, the API supports seeding the database with initial data from provided JSON files, simulating a real-world scenario and making it easier to start working with the API immediately.

### Development Environment Setup

To set up your development environment for working on this project, you will need:

1. **.NET 8 SDK**: Ensure you have the .NET 8 SDK installed on your machine.
2. **SQLite**: The project uses SQLite as the development database. No separate installation is required as SQLite is embedded within the application through Entity Framework Core.
3. **Visual Studio Code or another IDE**: While you can use any IDE or text editor, Visual Studio Code is recommended for its excellent C# and .NET support.

### Database

The database should be automatically created and migrations applied by the `startup.cs` file.

#### Seeding

To seed the necessary data for local debugging, you need to create a `dataset` folder in the root of your project directory. Inside the `dataset` folder, you should include the following JSON files: `circuits.json`, `driver_standings.json`, `drivers.json`, `lap_times.json`, and `races.json`. These files should contain the data provided in the challenge description. Once you have created the `dataset` folder and populated it with the required JSON files, you can proceed with local debugging and making a `GET` request to the endpoint.

### Running the Project Locally

1. **Clone the repository** to your local machine.
2. **Navigate to the project directory** where the `VehiclePerformanceSoftware.sln` file is located.
3. **Restore dependencies**: Run `dotnet restore` to restore all the necessary .NET dependencies.
4. **Start the application**: Run `dotnet run` within the API project directory. This will start the API and automatically apply any pending database migrations.
5. **Seed the database** (if necessary) by placing the required JSON files in the `dataset` folder as described in the "Seeding" section above.

### Accessing the API Documentation

Once the application is running, you can access the Swagger UI to test the API endpoints and view the API documentation by navigating to `http://localhost:<port>/` in your web browser.

## Conclusion

This REST API provides a robust solution for managing and exposing vehicle performance data, leveraging the power of .NET 8 and SQLite for development. Its design focuses on ease of use, scalability, and flexibility, making it an ideal starting point for developers looking to work with vehicle performance data.
