# CocktailsApp

CocktailsApp is a **Cocktails application** built using **Domain-Driven Design (DDD)** principles with a **React** frontend and **ASP.NET Core** backend. 

This application allows users to create custom cocktails using their available ingredients.

It features stock management and billing to track inventory and expenses efficiently. 

Users are organized into groups, where they can order cocktails available within their group. 

Additionally, users can create their own groups to manage personal cocktails and stock.

## Features
- **Cocktail Creation** – Create cocktails by combining ingredients available in stocks.
- **Stock Management** – Track ingredient availability and manage inventory.
- **Order Cocktails** – Order cocktails within related groups.
- **Pricing Management** – Monitor cost and manage cocktail pricing.
- **Join Group** – Connect with others to share and enjoy cocktails within communities.

---

## Technologies Used

- **Frontend**: React, TypeScript, Axios, CSS
- **Backend**: ASP.NET Core (Web API), Entity Framework Core (for database operations)
- **Database**: PostgresSQL (or any RDBMS supported by EF Core)
- **Authentication**: JWT (JSON Web Tokens)
- **Deployment**: AWS

---

## Project Structure

The project is organized using **Domain-Driven Design (DDD)** principles, which separates the solution into different layers for better maintainability and scalability.

```yaml
📂 CocktailsApp
│── 📂 src
│   │── 📂 Domain               # Core business logic (Entities, Value Objects, Interfaces)
│   │── 📂 Application          # Use cases and DTOs, following CQRS principle
│   │── 📂 Infrastructure       # Persistence (EF Core), External services
│   │── 📂 API                  # ASP.NET Core Web API (Controllers, Middleware)
│── 📂 tests                    # Unit & Integration Tests
│   │── 📂 Domain.Tests         # Domain layer tests
│   │── 📂 Application.Tests    # Application layer tests
│   │── 📂 Infrastructure.Tests # Infrastructure layer tests
│   │── 📂 API.Tests            # API layer tests (Integration)
│── 📂 client                   # React Frontend (TypeScript, Vite or CRA)
│── 📂 .github                  # GitHub Actions (CI/CD)
│── .editorconfig.txt           # Linting and formating configuration
│── LICENSE.txt                 # Project license
│── README.md                   # Project documentation
│── CocktailsApp.sln            # .NET Solution file
```

---

## Setup Instructions

### **1. Backend Setup (ASP.NET Core)**

#### **Prerequisites:**
- Install **.NET 7.0 SDK** or later.
- SQL Server (or any RDBMS supported by EF Core).

#### **Steps to run:**
1. Clone the repository:
```bash
git clone https://github.com/Wizni4/CocktailsApp.git
cd CocktailsApp
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Apply database migrations:
```bash
dotnet ef database update
```

4. Run the backend:
```bash
dotnet run
```

The API should be running at [https://localhost:5001](https://localhost:5001) (or the URL configured in your launchSettings.json).

---

### **2. Frontend Setup (React)**

#### **Prerequisites:**
- Install Node.js (version 16 or later) and npm.

#### **Steps to run:**

1. Navigate to the client folder:
```bash
cd client
```

2. Install dependencies:
```bash
npm install
```

3. Run the frontend development server:
```bash
npm start
```
The frontend should now be running at [https://localhost:3000](https://localhost:3000).

---

### **3. Deployment on AWS**

This project is ready to be deployed to AWS using services such as EC2 for hosting the backend API, RDS for database storage, and S3 for frontend deployment.

Refer to the [deployment/](./deployment/) folder for Terraform configuration files and Docker setup to deploy the application on AWS.

---

## API Documentation

The CocktailsApp API provides the following endpoints:

### Tasks
- In Progress

### Authentication
- In Progress

---

## Testing
The project includes unit and integration tests for each layer of the application.

1. Run Unit Tests (Domain, Application and Infrastructure layers):
```bash
dotnet test CocktailsApp.Domain.Tests
dotnet test CocktailsApp.Application.Tests
dotnet test CocktailsApp.Infrastructure.Tests
```

2. Run API Integration Tests:
```bash
dotnet test CocktailsApp.API.Tests
```

---

## License
This project is licensed under the MIT License – see the [LICENSE](./LICENSE.txt) file for details.

---

## Contributing
Contributions are welcome! Feel free to open issues or submit pull requests.
