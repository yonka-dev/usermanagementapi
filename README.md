# UserManagementAPI

A lightweight ASP.NET Core Web API for managing user records.  
This project demonstrates clean API architecture, DTO‑based validation, a service layer with business rules, and in‑memory data handling without a database.

---

## 🚀 Features

- Retrieve all users  
- Retrieve a user by ID  
- Create new users  
- Update existing users  
- Delete users  
- Input validation using DTOs  
- Business rule validation in the service layer  
- In‑memory data store (no database required)  
- Global error handling returning RFC 7807 JSON responses  
- Swagger UI for quick local testing  
- Postman collection included for reviewers  

---

## 📦 Technologies Used

- .NET 9  
- C#  
- Controllers + Dependency Injection  
- DTOs for request/response shaping  
- In‑memory service layer  
- Swagger / OpenAPI  

---

## 📁 Project Structure

```
UserManagementAPI
│
├── Controllers
│     └── UsersController.cs
│
├── DTOs
│     ├── UserCreateDto.cs
│     ├── UserUpdateDto.cs
│     └── UserReadDto.cs
│
├── Models
│     └── User.cs
│
├── Services
│     ├── IUserService.cs
│     └── UserService.cs
│
├── ErrorController.cs
└── Program.cs
```

---

## ▶️ Running the API Locally

1. Clone the repository  
2. Open the solution in Visual Studio or VS Code  
3. Run the project (F5)

The API runs on fixed ports:

- HTTPS: **https://localhost:5001**  
- HTTP:  **http://localhost:5000**

---

## 🧪 Testing the API

You can test the API using **Swagger** or **Postman**.

### ✔ Swagger UI (local development only)

Navigate to:

```
https://localhost:5001/swagger
```

Swagger provides interactive documentation and allows you to test endpoints directly.

---

### ✔ Postman (recommended for reviewers)

This project includes a Postman collection:

```
UserManagementAPI.postman_collection.json
```

### Importing the collection

1. Open Postman Desktop  
2. Click **Import**  
3. Select the JSON file  
4. The collection will appear in the sidebar  

### Setting the base URL

The collection uses a variable: `{{baseUrl}}`

To set it:

1. Click the collection name  
2. Open the **Variables** tab  
3. Set:  
   ```
   baseUrl = https://localhost:5001
   ```

Now all requests will work automatically.

---

## 🔧 API Endpoints

### GET /users  
Returns all users.

### GET /users/{id}  
Returns a specific user.

### POST /users  
Creates a new user.

Example body:
```json
{
  "firstName": "Tester",
  "lastName": "Tester",
  "email": "tester@example.com"
}
```

### PUT /users/{id}  
Updates an existing user.

### DELETE /users/{id}  
Deletes a user by ID.

---

## 🛡 Global Error Handling

The API uses:

```csharp
app.UseExceptionHandler("/error");
```

All unhandled exceptions are routed to `ErrorController`, which returns a standardized **Problem Details (RFC 7807)** JSON response.

This ensures consistent error output across the entire API.

---

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

---

## 🛠 Summary of Bugs Fixed and Optimizations Made

During development, several issues were identified and resolved to ensure the API behaved correctly, followed modern .NET best practices, and met the assignment requirements. Below is a summary of the key fixes and optimizations.

### **1. Incorrect Email Update Logic**
**Bug:**  
The `UserService.Update()` method always removed and re‑added the email in the HashSet, even when the email had not changed.

**Fix:**  
Added a conditional check to update the email index only when the email actually changes.

**Result:**  
- Prevented unnecessary HashSet operations  
- Eliminated potential inconsistencies in email uniqueness tracking  
- Improved performance for repeated updates  

---

### **2. Missing Validation on DTOs**
**Bug:**  
Validation attributes existed only on the domain model (`User`), not on the DTOs.  
This meant invalid client input could reach the service layer.

**Fix:**  
Added `[Required]`, `[StringLength]`, and `[EmailAddress]` to `UserCreateDto` and `UserUpdateDto`.

**Result:**  
- Ensured invalid input is rejected early  
- Protected the service layer from malformed data  
- Matched modern ASP.NET Core validation patterns  

---

### **3. Controllers Returning Domain Models Instead of DTOs**
**Bug:**  
Controllers returned `User` objects directly, exposing internal fields and breaking API encapsulation.

**Fix:**  
Mapped all responses to `UserReadDto`.

**Result:**  
- Clear separation between internal models and API contracts  
- More stable and predictable API responses  
- Prevented accidental over‑posting or leaking internal fields  

---

### **4. Incorrect HTTP Response for POST**
**Bug:**  
The POST endpoint returned `200 OK` instead of the correct REST response.

**Fix:**  
Replaced `Ok()` with `CreatedAtAction()`.

**Result:**  
- Proper REST‑compliant behavior  
- Location header included in responses  
- Improved API clarity for clients and reviewers  

---

### **5. Redundant Logging and Branching**
**Bug:**  
Controllers contained duplicated logging and unnecessary `else` blocks.

**Fix:**  
Simplified logging and used early returns.

**Result:**  
- Cleaner, more readable controllers  
- Reduced noise in logs  
- More idiomatic ASP.NET Core code  

---

### **6. Global Error Handling**
**Bug:**  
Missing error handling implementation.

**Fix:**  
Implemented a proper API‑focused error handler returning RFC 7807 Problem Details.

**Result:**  
- Consistent JSON error responses  
- Modern, standards‑compliant error pipeline  
- Clear separation between API errors and potential future UI errors  

---

## ⚡ How Copilot Streamlined the Debugging Process

- **Identified subtle logic bugs** (e.g., email update logic) that are easy to overlook.
- **Suggested best‑practice patterns** for controllers, DTOs, and service layers.
- **Clarified validation flow** between DTOs and domain models.
- **Improved REST correctness** by recommending `CreatedAtAction`.
- **Helped structure documentation** to make the project reviewer‑friendly.
- **Provided architectural guidance** on error handling, routing, and project layout.

Overall, Copilot accelerated debugging, improved code quality, and ensured the final API followed modern ASP.NET Core conventions while keeping full control in the hands of the developer.