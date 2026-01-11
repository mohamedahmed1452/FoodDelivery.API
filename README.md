# 🍔 Food Delivery Platform (Full Stack)

[![Live Demo](https://img.shields.io/badge/Demo-Online-brightgreen?style=for-the-badge&logo=google-chrome&logoColor=white)](http://deliveryfood.runasp.net/)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Angular](https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Redis](https://img.shields.io/badge/redis-%23DD0031.svg?style=for-the-badge&logo=redis&logoColor=white)

A scalable, full-stack food delivery application architected with **ASP.NET Core (Web API)** and **Angular**. This project demonstrates enterprise-level development practices, focusing on performance, scalability, and maintainability using **Clean Architecture**.

## 🌐 Live Demo

🚀 **Check out the live application here:** 👉 **[http://deliveryfood.runasp.net/](http://deliveryfood.runasp.net/)**

---

## 🚀 Key Features

* **Clean Architecture:** Strict separation of concerns divided into API, Application, Core, and Infrastructure layers.
* **Performance Optimization:** implemented **Redis Caching** strategies for high-traffic data (Products/Menus).
* **Advanced Authentication:** Secure system using **JWT (JSON Web Tokens)** with **Refresh Token** rotation mechanisms.
* **Payment Integration:** Fully functional payment gateway integration using **Stripe**.
* **Order Management:** Complex workflow handling for basket management, order creation, and history.
* **Design Patterns:** Implementation of **Repository Pattern**, **Unit of Work**, and **Specification Pattern** for flexible data querying.
* **Error Handling:** Centralized global exception handling using custom Middleware.

## 🏗️ Architecture & Project Structure

The solution is organized into four main layers following Clean Architecture principles:

* **📂 FoodDelivery.Core:** The center of the onion. Contains Domain Entities (Basket, Order, Identity), Interfaces (Repository contracts), and Specifications.
* **📂 FoodDelivery.Infrastructure:** Implementation details. Contains EF Core `StoreContext`, Data Migrations, Repositories (`BasketRepository`, `GenericRepository`), and Data Seeding.
* **📂 FoodDelivery.Application:** Contains business logic services (`AuthService`, `OrderService`, `PaymentService`, `ResponseCacheService`) and DTOs.
* **📂 FoodDelivery.API:** The entry point. Contains Controllers, Middleware, and Extensions.

## 🛠️ Tech Stack

**Backend:**
* **Framework:** ASP.NET Core 8 Web API
* **Language:** C#
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core
* **Identity:** ASP.NET Core Identity (User Management)
* **Caching:** Redis
* **Object Mapping:** AutoMapper

**Frontend:**
* **Framework:** Angular
* **Styling:** Bootstrap / SCSS

## 🔌 API Endpoints Overview

The API manages **40+ RESTful endpoints**. Key controllers include:

| Controller | Functionality |
| :--- | :--- |
| `AccountController` | User Login, Registration, Address Management. |
| `ProductsController` | Fetching products with Pagination, Sorting, and Filtering (using Specifications). |
| `BasketController` | Redis-backed shopping cart management. |
| `OrdersController` | Creating and retrieving orders. |
| `PaymentController` | Handling Stripe intents and webhooks. |
| `BuggyController` | Testing error responses and middleware behavior. |

## ⚙️ Getting Started

Follow these steps to set up the project locally.

### Prerequisites
* .NET SDK 8.0+
* Node.js (for Angular)
* SQL Server
* Redis Server (Ensure it is running)

### Installation

1.  **Clone the repository**
    ```bash
    git clone [https://github.com/your-username/food-delivery-app.git](https://github.com/your-username/food-delivery-app.git)
    cd food-delivery-app
    ```

2.  **Configure Backend**
    * Navigate to `FoodDelivery.API`.
    * Update `appsettings.json` (or use User Secrets) with your connection strings and API keys:
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Server=.;Database=FoodDeliveryDb;Trusted_Connection=True;MultipleActiveResultSets=true",
          "Redis": "localhost"
        },
        "StripeSettings": {
          "PublishableKey": "pk_test_...",
          "SecretKey": "sk_test_..."
        }
        ```

3.  **Apply Migrations**
    ```bash
    dotnet ef database update --project FoodDelivery.Infrastructure --startup-project FoodDelivery.API
    ```

4.  **Run the Backend**
    ```bash
    dotnet run --project FoodDelivery.API
    ```

5.  **Run the Frontend (Angular)**
    ```bash
    cd client
    npm install
    ng serve
    ```

## 🤝 Contributing

Contributions are welcome! Please create a Pull Request for any features or bug fixes.

## 📄 License

This project is licensed under the MIT License.
