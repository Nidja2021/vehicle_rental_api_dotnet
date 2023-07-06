
# Vehicle Rental API

This project is a Vehicle Rental API built using .NET 7, ASP.NET Core WebAPI, Entity Framework, PostgreSQL, JWT authentication, Docker, and AutoMapper. The API provides a platform for managing vehicle rental operations, including vehicle listings, reservations, and user authentication.

## Technologies
- .NET 7
- ASP.NET Core Web API
- PostgreSQL
- Entity Framework
- AutoMapper
- JWT
- Docker

## Features

- Vehicle Management: The API allows users to create, retrieve, update, and delete vehicle listings. Each vehicle listing contains information such as make, model, year, rental price, and availability.

- Reservation Handling: Users can make reservations for available vehicles, specifying the desired rental period and additional details. The API handles the reservation process, including checking vehicle availability, calculating rental fees, and storing reservation information.

- User Authentication: The API employs JWT (JSON Web Token) authentication to secure user endpoints. Users can register accounts, log in, and obtain a token for subsequent authenticated requests.

- Database Persistence: The API utilizes Entity Framework along with a PostgreSQL database for persistent data storage. This ensures reliable data management and allows for efficient querying and manipulation of vehicle listings, reservations, and user information.

- Containerization with Docker: The project is containerized using Docker, which provides a consistent and portable environment for running the API. Docker allows for easy deployment and scalability across different platforms and environments.

- Data Mapping with AutoMapper: AutoMapper is employed to simplify and automate the mapping of data models to DTOs (Data Transfer Objects) and vice versa. This helps in reducing manual mapping efforts and improves development productivity.


## API Reference

#### AUTH
*Register user and save to the db
```http
  POST /api/auth/register
```
*Login user and generate JWT token
```http
  POST /api/auth/login
```

#### USER
*Get user's profile - **Bearer token for user (Required)**.
```http
  GET /api/users/profile
```
*Update user's profile - **Bearer token for user (Required)**.
```http
  PATCH /api/users/profile
```


#### VEHICLE
*Get list of vehicles
```http
  GET /api/vehicles
```
*Create a new vehicle - **Bearer token for admin (Required)**. 
```http
  POST /api/vehicles
```

*Get a vehicle by ID
```http
  GET /api/vehicles/{id}
```
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `vehicle_id` | `string` | **Required**. Vehicle ID |



*Update a vehicle by ID - **Bearer token for admin (Required)**.
```http
  PATCH /api/vehicles/{id}
```
*Delete a vehicle by ID - **Bearer token for admin (Required)**.
```http
  DELETE /api/vehicles/{id}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `vehicle_id` | `string` | **Required**. Vehicle ID |


#### RESERVATION
*Get all reservations by logged user - **Bearer token for user (Required)**.
```http
  GET /api/reservations
```
*Create a new reservation by logged user - **Bearer token for user (Required)**.
```http
  POST /api/reservations            
```

*Get a reservation by ID of reservation and logged user - **Bearer token for user (Required)**.
```http
  GET /api/reservations/{id}
```
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `reservation_id` | `string` | **Required**. Reservation ID |

*Update a reservation by ID of reservation and logged user - **Bearer token for user (Required)**.
```http
  PATCH /api/reservations/{id}
```

*Delete a reservation by ID of reservation and logged user - **Bearer token for user (Required)**.
```http
  DELETE /api/reservations/{id}     
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `reservation_id` | `string` | **Required**. Reservation ID |

