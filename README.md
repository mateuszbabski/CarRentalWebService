# CarRentalWebService

## Table of content:
* [Project description](#project-description)
* [Technologies](#technologies)
* [Setup](#setup)
* [Features](#features)
* [Architecture](#architecture)
* [Contribution](#contribution)


## Project description
The purpose of the project was to create a simple car rental aggregator. Rental companies share their cars to rent that can be listed and booked by a customer. Project create while learning different architectures used in software engineering.

## Technologies

- C# 10
- .NetCore 6
- EntityFrameworkCore 6.0.8
- MSQLServer

## Setup

#### Clone to repository
```
$ git clone https://github.com/mateuszbabski/CarRentalWebService
```

#### Go to the folder you cloned
```
$ cd CarRentalWebService
```

#### Install dependencies
```
$ dotnet restore
```

#### Create empty database, create migration and update database

#### Set API Layer as startup project

#### Run application
```
$ dotnet run
```

## Features

- Register/Login for Customer and Rental Company separately
- Forgot, reset password feature
- Adding, updating and deleting vehicles available to rent
- Creating and sending invoices
- As a customer listing rental companies, listing and searching vehicles, making reservations for vehicles
- Unit tests

## Architecture

Project is based on Clean architecture principles, following separation of layers. I followed CQRS approach with using MediatR library - except custom authentication system.

## Contribution

Feel free to fork project and work on it with me. I am open to any suggestions how to make it better.
