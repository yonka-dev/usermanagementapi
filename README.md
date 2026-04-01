## UserManagementAPI

A lightweight ASP.NET Core Web API for managing user records.
This project demonstrates clean API architecture, CRUD operations, and in‑memory data handling without a database.


### 🚀 Features

- Retrieve all users
- Retrieve a user by ID
- Create new users
- Update existing users
- Delete users
- In‑memory data store (no database required)
- Swagger UI for quick local testing
- Postman collection included for reviewers


### 📦 Technologies Used

- .NET 9
- C#
- Controllers + Dependency Injection
- In‑memory service layer


### 📁 Project Structure

UserManagementAPI
│
├── Controllers
│     └── UsersController.cs
│
├── Models
│     └── User.cs
│
├── Services
│     └── IUserService.cs
│     └── UserService.cs
│
└── Program.cs


### ▶️ Running the API Locally

1. Clone the repository
2. Open the solution in Visual Studio or VS Code
3. Run the project (F5)

The API runs on fixed ports:
- HTTPS: https://localhost:5001
- HTTP:  http://localhost:5000


### 🧪 Testing the API
You can test the API using Swagger or Postman.


#### ✔ Swagger UI (local development only)
Navigate to: https://localhost:5001/swagger
Swagger provides interactive documentation and allows you to test endpoints directly.


#### ✔ Postman (recommended for reviewers)
This project includes a Postman collection: UserManagementAPI.postman_collection.json


### Importing the collection (Postman Desktop)

1. Open Postman Desktop
2. Click Import
3. Select the JSON file
4. The collection will appear in the sidebar


### Setting the base URL

The collection uses a variable: {{baseUrl}}

To set it:
1. Click the collection name
2. Open the Variables tab
3. Set: baseUrl = https://localhost:5001

Now all requests will work automatically.


### 🔧 API Endpoints

GET /users
Returns all users.

GET /users/{id}
Returns a specific user.

POST /users
Creates a new user.

Example body:
```json
{
  "firstName": "Tester",
  "lastName": "Tester",
  "email": "tester@example.com"
}
``` 

PUT /users/{id}
Updates an existing user.

DELETE /users/{id}
Deletes a user by ID.


### 🤖 How Microsoft Copilot Assisted in Building This API

Copilot supported development by:

1. Project Setup & Architecture
Suggested a clean structure for controllers, services, and models.
Helped refine Program.cs for clarity and maintainability.

2. Code Generation
Generated CRUD endpoints following REST best practices.
Provided an in‑memory service implementation.

3. Testing Guidance
Recommended using Postman for cross‑platform testing.
Provided example request bodies and expected responses.

4. Documentation
Assisted in writing this README and explaining API behavior clearly.

Copilot acted as a development assistant, improving speed, structure, and 
clarity while keeping full control in the hands of the developer.