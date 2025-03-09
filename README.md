# Challenge

Create project using concepts CQRS with separate write and read database
Event was used to perform synchronization between the write and read database. 
Was created a base arquitecture thinking in scalability and maintenance
Was aplied solid principals to separate layes and responsabilities

# Objective

This project aims to demonstrate the implementation of the following technologies and standards:
- API (.net)
- Standards: DDD, EDA, CQRS
- MassTransit (rabbit mq)
- SinalR
- Fluent
- Mediator
- Entity
- Angular
- BlazorWasm
- Postgres

## Projetct

The architecture follows the pattern CQRS with DDD and Event-Driven, where:

Write side (Product.Write) handles commands (create, update, delete).

Read side (Product.Read) handles queries (retrieve data).

Event side (Product.ReadEvent) handles event publishing and subscription for synchronization between write and read models.

Shared contracts (SharedContracts) define common DTOs and interfaces.

Base projects (Base.*) provide reusable infrastructure and domain logic.

## Communication 

![alt text](comunicacao.png)
