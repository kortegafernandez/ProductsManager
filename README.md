# ProductsManager

## Architecture

The **ProductsManager** project follows the Clean Architecture principles, which promotes a separation of concerns and maintainability. The project is structured into the following components:

- **ProductsManager.Domain**: Contains the domain entities, business logic, and core functionalities of the application.

- **ProductsManager.Infrastructure.Persistence**: Handles data access and storage. It includes database context, migrations, and repository implementations.

- **ProductsManager.Infrastructure.Shared**: Contains shared utilities and components used across different layers of the application.

- **ProductsManager.Tests**: Contains unit tests for various components of the application.

- **ProductsManager.API**: Implements the API layer responsible for handling incoming HTTP requests, invoking the application services, and returning appropriate responses.

- **ProductsManager.Application**: Contains application bussines logic, acting as the bridge between the API layer and the domain layer.

## Patterns

The project utilizes several design patterns to enhance its structure and maintainability:

- **CQRS (Command Query Responsibility Segregation)**: Separates read and write operations.

- **Mediator**: Implements the mediator pattern to centralize communication between components, promoting loose coupling.

- **Repository**: Implements the repository pattern for data access, providing a clean and consistent interface for accessing and managing domain entities.

## Libraries

- AutoMapper: Objects mapping
- Fluent Validation: Commands and queries validations
- MediatR: Mediator implementation
- Moq, XUnit: Unit test implementation
- NLog: Performance logging to track response time
- In-Memory Cache: lightweight cache implementation
- mockAPI: external discount API implementation


## Steps to Run the Site

Follow these steps to clone and run the **ProductsManager** project:

1. **Clone the Project**: Use the following command to clone the repository to your local machine:

   ```bash
   git clone https://github.com/kortegafernandez/ProductsManager.git
2. **Navigate to Project Directory**: Change into the project directory:

   ```bash
   cd ProductsManager

3. **Run the Application**: Execute the application using the appropriate commands based on your development environment (e.g., Visual Studio, dotnet CLI, etc.):

   ```bash
   dotnet run


