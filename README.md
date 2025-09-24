## CT SYNCSPACE Management System by ResolveIQ

CT SYNCSPACE Management System by ResolveIQ is a .NET web application designed to help managers and their colleagues efficiently manage tasks. The system enables users to create, assign, and track tasks, promoting better collaboration and productivity within teams. Users watching specific tasks receive push notifications for updates, ensuring everyone stays informed in real time.

### Features

- Task creation, assignment, and tracking
- User roles for managers and team members
- Push notifications for task watchers
- Centralized user directory integration for authentication, authorization, and role management
- Secure authentication and authorization

### Getting Started

1. Clone the repository.
3. Configure the application's user directory integration:
    - The application utilizes a centralized User Directory (such as Active Directory, LDAP, or a custom user store) to manage user roles, authentication, and access control.
    - Ensure your User Directory is set up and accessible from the application environment.
    - Update the application's configuration files (e.g., `appsettings.json`) with the necessary connection details for your User Directory.
    - The system automatically synchronizes user accounts and roles from the directory, allowing managers to assign tasks and permissions based on directory roles (e.g., Manager, Team Member).
    - User authentication and authorization are handled via the directory, ensuring secure and consistent access management.
    - Any changes to user roles or group memberships in the directory are reflected in the application in real time.
    - For custom directory setups, refer to the documentation in the `/docs/user-directory-integration.md` for detailed configuration steps.
3. Build and run the application using Visual Studio or `dotnet run`.
4. Access the web app at `http://localhost:5000`.

### Requirements

- .NET 6.0 or later
- MySQL database

### Contributing

Contributions are welcome! Please submit issues or pull requests for improvements.

### License

This project is licensed under the MIT License.
