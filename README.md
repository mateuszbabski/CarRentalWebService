# CarRentalWebService

In this project I'd implement register/authentication option for a customer and for a rental company separately. 

Customer will be able to check companies, available vehicles and make a reservation for some one them. After returning a car customer gets invoice to pay. Customer keeps Reservation and Invoice history. 

Rental company as a web service user will be able to manipulate its vehicles (add, delete, change), keep invoices, vehicle list, and history of reservations.

I implemented my own custom authentication. Options available:
- login user/company
- register as a user or a company
- jwttoken as part of authentication proccess
- generating reset password token, send it to mail then change password

Project is based on Clean/Onion Architecture with partially implemented CQRS (except Auth part).
