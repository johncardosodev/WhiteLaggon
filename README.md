# Cardoso Luxury Villa Rental CRM

The Cardoso Luxury Villa Rental CRM project is a sophisticated web application designed for the management of a luxury villa rental service. It aims to enhance user experience and streamline the management process for both customers and administrators. Below is an overview of its key features and components:

## Key Features

### 1. Villa Management
The application facilitates the creation and management of villas, which are the primary rental properties offered. This functionality is supported by a `VillaController` and associated migration files for creating villa-related tables in the database.

### 2. Extras Management
WhiteLaggon supports the addition of extra amenities or services, such as pools, Jacuzzis, family rooms, etc., that can be associated with each villa. This is managed through the `ExtrasController` and backed by a dedicated database schema.

### 3. User Accounts and Roles
The application implements a comprehensive user management system, including registration and role assignment functionalities. This is handled by the `ContaController` (Account Controller), utilizing ASP.NET Identity for user authentication and role management (e.g., Administrator and User roles).

### 4. Reservation System
A core feature of WhiteLaggon is its reservation system, which allows users to book villas for specific dates. The `ReservaController` manages reservation creation and includes integration with Stripe for payment processing, indicating seamless interaction with external payment gateways.

### 5. Database and Infrastructure
Entity Framework Core is used for database operations, as evidenced by various migration files. These migrations detail the creation of tables for villas, extras, reservations, and identity (users and roles), showcasing a well-structured database schema.

### 6. Role-Based Access Control
The application implements role-based access control to ensure that only authorized users can perform specific operations, such as making payments for reservations. This is facilitated using ASP.NET Core's authorization features.

### 7. Integration with External Services
Payment functionality within WhiteLaggon integrates with Stripe for processing transactions, demonstrating the application's ability to interact with external APIs and services.

## Conclusion

Overall, the Cardoso Luxury Villa Rental Service project offers a comprehensive solution for managing luxury villa rentals. It features a rich set of functionalities for property management, user authentication and authorization, reservation booking, and payment processing. Leveraging modern web development frameworks and technologies, including ASP.NET Core, Entity Framework Core, and Stripe, WhiteLaggon provides a robust and user-friendly platform for both customers and administrators.
## Images
![image](https://github.com/user-attachments/assets/86dc80d5-52a9-48ab-a6d7-f6ff412a2987)

![image](https://github.com/user-attachments/assets/383bc61b-8d11-480e-8d98-c0990f5420e2)

