# MessageLogger Microservice - Used to log message from external service

Requirement is to build a Microservice exposing API for making call to external service and log response in text file.

I have created a REST service for processing message received frm external service using .net Core and Log4Net. Added Swagger for consumers to easily integrate with their application.

## Prerequisites/Things to know
- Used Log4Net for logging
- Left placeholdre for external service URL in ServiceHelper class

## How to run
### Endpoint and test details
- There is swagger implementation for the API to know the Request and Response with status code in details. Available in Json and can also access after running the application.
- The local url for swagger - https://localhost:44340/swagger/index.html

## Unit Test
Used `MS Unite test` and `Moq` for writing unit test.

## Improvements/Extra miles
Below improvements can be implemented if I get more time,
 - I can build token based authentication.
