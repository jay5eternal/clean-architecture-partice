# Project README

## 1. Technical Decision Justification

### 1.1 RESTful API vs gRPC
> I chose RESTful APIs because the project involves integration with front-end developers, where UI considerations are crucial. RESTful APIs provide an intuitive way to interface, which I found more suitable than gRPC. gRPC might be better for efficient communication between internal systems, but RESTful APIs are more adaptable for our needs.

### 1.2 MongoDB vs Redis
> I chose MongoDB, a document-oriented database, offers flexible storage for Json-like documents. It supports database scalability and high availability. The richness of its query language, compatibility with LINQ, and the ability to index based on query conditions are significant advantages. MongoDB Transactions also help manage data consistency during high concurrency. Redis, with its key-value storage, is better suited as a cache layer.

## 2. Run & Test Instructions

1. Navigate to the directory containing `docker-compose.yml`.
2. Execute `docker-compose up --build`.
3. Open a browser and go to `http://localhost:5000/swagger/index.html` to view the API specifications.
4. **POST** `/api/Data/initial-data-load`: Loads file to MongoDB for database initialization.
   > If data already exists, it will not be inserted again.
5. **GET** `/api/Shelf`: View current Shelf data.
6. **POST** `/api/Shelf/sku-add`: Add a new SKU to the Shelf.
   > Basic validation implemented: Checks if Sku, Row, or Lane does not exist.
7. **POST** `/api/Shelf/sku-move`: Move an SKU on the Shelf.
   > Basic validation implemented: Checks the initial position before moving.

## 3. Breakdown of Time Spent on Various Tasks

- **Reading documentation**: 1 hour
- **Setting up Clean Architecture Project Template**: 1 hour
- **Dockerfile, docker-compose, and docker testing**: 1.5 hours
- **Implementing MongoDB**: 2 hours
- **Implementation & Debugging**: 10 hours
- **Unit Testing**: 30 minutes

