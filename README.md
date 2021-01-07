# Payment Gateway Exercise

## Preface

In every exercise I like to set myself in a strict "hackathon-like" schedule.
By setting a strict schedule I force myself to make hard design decisions, accept tradeoffs, simulate a real world scenario with a fixed deadline while also keeping myself focused and committed.
In addition, I select a new technology or an idea that I would like to learn more about, explore and experiment with, to make things more interesting and learn something along the way.

For this project I would like to learn more about Rebus and how to use it for domain events (and maybe be ready to divide the application up in microservices).

* Time alotted: max 3 days.
* Architecture: Domain centric (with some tradeoffs and bypasses mostly for brevity).
* Some TDD.
* Auth: IS4
* New tech to learn: Rebus
* Persistence: InMemory with EFCore
* Tests: In different projects.
* CQRS: No, but split into read / write for future segregation.

## Requirements

Design and code a Payment Gateway that receives payments from merchants and fulfills them with the help of an external acquiring bank (faked).

## Assumptions

A number of assumptions were made (in no particular order):

* Gateway has 60 seconds to process a payment.
* Card details are persisted in their entirety. PCI prohibits this. Maybe gateways are different(?).
* Card holder information is not required.
* Card expiration year is set to 100 years from now (min 1950).
* Card number contains only digits (12 to 30 chars long).
* System allows expired cards (bank will fortunately validate).
* Maximum allowed requested amount 99999.99.

## Notes

### Projects

The solution consists of:

1. **Core**
    * **Domain** - contains the domain models, ddd objects and some helper functions.
    * **Application** - contains the two main services (ProcessPayment & PaymentDetails) as well as DTOs, and interfaces for storage and infrastructure.
1. **Persistence**
    * **InMemory** - Implements the storage interfaces defined in the application project. Provides separate repositories for reading and writing to the in-memory db. DB is seeded with sample currencies and merchants (to assist with tests).
1. **API** - provides the restful connection to the outside world. Authenticates and authorizes users via the IS4. Relies on view model validation with data annotations. Serves as the composition root.
1. **IdentityServer** - Used as a token service. One client (merchant) is hardcoded.
1. **Tests**
    * **Unit** - Mostly domain model validations and helper functions tests.
    * **Integration** - Basic services tests.
    * **ApiTester** - Console application to test the API externaly.

        *(Two postman tests are also provided in the root of the solution)*

### Entities

* Domain models

    Domain entities ended up rather anemic. This is due to the fact that most processing happens outside our system. Domain entities will validate though. As the application matures more logic will be pushed into the domain models.

* DTO models

    DTO models are used for the communication between the Application layer / services and UI or Infra.

* Data Models

    In the Persistance project different set of models are introduced for the relational database. These models are technology-specific.

* View Models

    Finally, the API project uses it's own models to communicate with the outside world.

*All mapping is done manually in implicit operators and translate methods. (I regret not using automapper).*
