# TaskFlow

TaskFlow is a **task management application** built using **Domain-Driven Design (DDD)** principles with a **React** frontend and **ASP.NET Core** backend. It allows users to manage their tasks, track progress, and collaborate effectively.

This project is deployed on **AWS** and designed to be easily scalable, with modern web technologies ensuring a fast and responsive user experience.

## Features
- **Create and manage tasks** with different statuses (e.g., "To Do", "In Progress", "Completed").
- **Assign tasks** to specific users and track progress.
- **REST API** built using **ASP.NET Core** for task management operations.
- **Responsive frontend** built using **React**.
- **Authentication** and **authorization** to restrict access to certain features.

---

## Technologies Used

- **Frontend**: React, TypeScript, Axios (for API calls), CSS (or styled-components)
- **Backend**: ASP.NET Core (Web API), Entity Framework Core (for database operations)
- **Database**: SQL Server (or any RDBMS supported by EF Core)
- **Authentication**: JWT (JSON Web Tokens)
- **Deployment**: AWS (using services like EC2, S3, and RDS)

---

## Project Structure

The project is organized using **Domain-Driven Design (DDD)** principles, which separates the solution into different layers for better maintainability and scalability.

```yaml
📂 TaskFlow
│── 📂 src
│   │── 📂 Domain            # Core business logic (Entities, Value Objects, Interfaces)
│   │── 📂 Application       # Use cases and DTOs
│   │── 📂 Infrastructure    # Persistence (EF Core), External services
│   │── 📂 API               # ASP.NET Core Web API (Controllers, Middleware)
│── 📂 tests                 # Unit & Integration Tests
│   │── 📂 Domain.Tests      # Domain layer tests
│   │── 📂 Application.Tests # Application layer tests
│   │── 📂 API.Tests         # API layer tests (Integration)
│── 📂 client                # React Frontend (TypeScript, Vite or CRA)
│── 📂 deployment            # Deployment configs (AWS Lambda, Docker, Terraform)
│── 📂 .github               # GitHub Actions (CI/CD)
│── README.md                # Project documentation
│── TaskFlow.sln             # .NET Solution file
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
git clone https://github.com/Wizni4/TaskFlow.git
cd TaskFlow
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

The TaskFlow API provides the following endpoints:

### Tasks
- `GET /api/tasks` – Retrieve a list of tasks.
- `POST /api/tasks` – Create a new task.
- `GET /api/tasks/{id}` – Retrieve a single task by ID.
- `PUT /api/tasks/{id}` – Update a task.
- `DELETE /api/tasks/{id}` – Delete a task.

### Authentication
- `POST /api/auth/login` – Authenticate a user and obtain a JWT token.
- `POST /api/auth/register` – Register a new user.

---

## Testing
The project includes unit and integration tests for each layer of the application.

1. Run Unit Tests (Domain and Application layers):
```bash
dotnet test TaskFlow.Domain.Tests
dotnet test TaskFlow.Application.Tests
```

2. Run API Integration Tests:
```bash
dotnet test TaskFlow.API.Tests
```

---

## License
This project is licensed under the MIT License – see the [LICENSE](./LICENSE.txt) file for details.

---

## Contributing
Contributions are welcome! Feel free to open issues or submit pull requests.
