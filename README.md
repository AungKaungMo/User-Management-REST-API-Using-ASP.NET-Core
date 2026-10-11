# User Management API — ASP.NET Core

Course project implementing user CRUD operations, request validation, logging middleware, exception handling, and OpenAPI/Swagger documentation.

## Requirements
- .NET 8 SDK

## Run
```bash
dotnet restore
dotnet run --launch-profile http
```
Open http://localhost:5080/swagger to test the API.

## API endpoints
| Method | Endpoint | Description | Success status |
|---|---|---|---|
| GET | `/api/users` | List users | 200 |
| GET | `/api/users/{id}` | Get user | 200 |
| POST | `/api/users` | Create user | 201 |
| PUT | `/api/users/{id}` | Update user | 204 |
| DELETE | `/api/users/{id}` | Delete user | 204 |

Sample POST/PUT body:
```json
{
  "name": "Aung Kaung",
  "email": "aung@example.com",
  "age": 23
}
```

Validation: required name (2–100 characters), valid email (up to 254 characters), age 1–120. `[ApiController]` automatically returns HTTP 400 for invalid models. Duplicate emails return 409. Missing users return 404.

Middleware:
- `RequestLoggingMiddleware`: logs HTTP method, request path, response status and elapsed time (without request bodies or sensitive values).
- `ExceptionHandlingMiddleware`: logs unexpected errors and returns a generic HTTP 500 problem response.

**Storage:** Thread-safe in-memory dictionary. Data is lost when the app restarts; no database or authentication is included. Intended for the rubric's basic CRUD/validation/middleware requirements, not production use.

## Example curl tests
```bash
curl -i -X POST http://localhost:5080/api/users \
  -H 'Content-Type: application/json' \
  -d '{"name":"Aung Kaung","email":"aung@example.com","age":23}'

curl -i http://localhost:5080/api/users

# Validation: expect 400
curl -i -X POST http://localhost:5080/api/users \
  -H 'Content-Type: application/json' \
  -d '{"name":"X","email":"not-an-email","age":0}'
```
Use the `id` returned from POST to try GET by ID, PUT, and DELETE in Swagger.

## Publish to GitHub
```bash
git init
git add .
git commit -m "Build ASP.NET Core user management API"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/user-management-api.git
git push -u origin main
```
Create the empty `user-management-api` repository on GitHub first, then replace `YOUR_USERNAME`.

## Copilot debugging evidence (complete this yourself)
The assignment asks whether **you used GitHub Copilot to debug code**. Open the project in VS Code/Visual Studio with Copilot enabled. Ask Copilot to review validation, duplicate-email handling, and HTTP status codes. Verify any fixes, then record what you tested or changed in your submission. Do not claim Copilot was used unless you actually used it.

## Rubric checklist
- [ ] GitHub repository created and pushed (you must do this)
- [x] GET, POST, PUT, DELETE user endpoints present
- [ ] Copilot used to debug and evidence recorded (you must do this)
- [x] User model validation implemented
- [x] Logging and exception-handling middleware implemented
