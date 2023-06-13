# AccountManagement-AspNetCore
-<b>git clone https://github.com/your-username/FinalProject-Authentication.git <br></b>
-<b>update-database</b>
# AccountController

The `AccountController` is a controller class that handles user authentication and account management functionalities for a web application. It includes methods for user login, registration, logout, and updating user profile information.

## Dependencies

The following dependencies are required to use the `AccountController`:

- `AhmetBAYRAM_FinalProject.Entities`: Contains the entity classes representing the database tables.
- `AhmetBAYRAM_FinalProject.Models`: Contains the view model classes used for data transfer between the views and the controller.
- `Microsoft.AspNetCore.Authentication`: Provides authentication middleware for ASP.NET Core applications.
- `Microsoft.AspNetCore.Authentication.Cookies`: Provides cookie authentication support for ASP.NET Core applications.
- `Microsoft.AspNetCore.Mvc`: Provides the base classes for creating MVC controllers and handling HTTP requests.
- `NETCore.Encrypt.Extensions`: A library for data encryption and decryption in .NET Core applications.
- `System.Security.Claims`: Represents the claims-based identity system in .NET Core.

## Controller Actions

The `AccountController` includes the following controller actions:

### Login

- `Login()` (GET): Renders the login view.
- `Login(LoginViewModel model)` (POST): Handles the login form submission. Performs user authentication and signs in the user if the credentials are valid. If successful, redirects to the home page; otherwise, displays an error message.

### Register

- `Register()` (GET): Renders the registration view.
- `Register(RegisterViewModel model)` (POST): Handles the registration form submission. Creates a new user account if the provided username is not already taken. If successful, redirects to the login page; otherwise, displays an error message.

### Logout

- `LogoutAsync()` (GET): Handles user logout. Signs out the currently authenticated user and redirects to the login page.

### Profile

- `Profile()` (GET): Renders the user profile view. Retrieves the user information based on the currently authenticated user and displays it.

### Update

- `Update()` (GET): Renders the password update view.
- `Update(UpdateViewModel model)` (POST): Handles the password update form submission. Verifies the user's current password and updates the password if it matches. If successful, the user is signed out and redirected to the login page; otherwise, displays an error message.

## Configuration

The `AccountController` requires the following configuration settings to be specified:

- `AppSetting:MD5Salt`: Specifies the salt value to be used for hashing passwords with MD5.

Ensure that the `DatabaseContext` and `IConfiguration` instances are correctly provided to the `AccountController` constructor.

Feel free to modify and extend this code as needed for your application.
