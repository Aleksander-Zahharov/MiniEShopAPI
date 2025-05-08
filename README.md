# 🛒 Mini E-Shop API
## 📖 Overview
This project is an intern test task implementing a complete ASP.NET Core Web API for managing products and orders in a simple e-shop. It showcases clean architecture, SOLID principles, dependency injection, and a switchable caching mechanism, all runnable via Docker Compose.

## ✅ Functional Endpoints
- **Products**  
  • GET    /api/products          — List all products  
  • GET    /api/products/{id}     — Get product by ID  
  • POST   /api/products          — Create a new product  
  • PUT    /api/products/{id}     — Update an existing product  
  • DELETE /api/products/{id}     — Delete a product

- **Orders**  
  • GET    /api/orders            — List all orders  
  • GET    /api/orders/{id}       — Get order by ID  
  • POST   /api/orders            — Create a new order with product IDs  
  • DELETE /api/orders/{id}       — Cancel an order

## ⚙️ Technical Stack
- **Framework**: ASP.NET Core Web API (.NET 6+ / .NET 9)  
- **Database**: PostgreSQL via EF Core with automatic migrations  
- **Caching**: Factory-based switch between In-Memory and Simulated Redis  
- **Architecture**: Controllers, Services, Repositories, Models (clean code & DI)  
- **API Docs**: Swagger / Swashbuckle  
- **Deployment**: Docker & docker-compose (API + PostgreSQL)

## 🧪 Testing
- Automated tests with xUnit covering Docker setup, caching, and API endpoints  
- Example requests in `Tests/curl_tests.http` and `Tests/postman_tests.json`  
- Run all tests:
  ```bash
  dotnet test
  ```

--------------------------------------------------------------------

## 📂 Project structure  
- 🗂️ **MiniEShopAPI** (API project)  
  - 📁 **Controllers**: ProductController, OrderController (HTTP endpoints)  
  - 📁 **Data**: ApplicationDbContext (EF Core context)  
  - 📁 **Migrations**: EF Core migration files for database schema  
  - 📁 **Models**: Product.cs, Order.cs (entity definitions)  
  - 📁 **Repositories**: Data access via IProductRepository & implementations  
  - 📁 **Services**: Business logic & caching (InMemory, SimulatedRedis)  
  - 📁 **Properties**: launchSettings.json (environment profiles)  
- 🗂️ **Tests** (unit & integration tests)  
  - 🧪 CacheTests.cs, DockerTests.cs, GeneralTests.cs (xUnit tests)  
  - 🔧 curl_tests.http, postman_tests.json (manual API tests)  
- 📋 **setup.bat**: Windows setup script for prerequisites  
- 📦 **Dockerfile**: Container image definition for API  
- 🐳 **docker-compose.yml**: Docker Compose config for API + PostgreSQL  
- 📄 **README.md**: Project overview and instructions  

--------------------------------------------------------------------

# 🚀 How to run it

### 🖥️ **Install all necessary packages**:
- (If you have just downloaded this project) Run the setup script:
	`setup.bat`

### 🐳 **Run using Docker**

- Open Docker Desktop

- Start Docker in terminal:
`docker compose up --build` occupies the terminal (Press `CTRL+C` to stop)
`docker compose down && docker compose up --build -d ` will run in the background
	
- If Docker was already connected you can run it just by pressing run button in the Docker Desktop App

- Check that containers are active and Docker is running:
`docker ps`

- Next line will show if you are connected to database
`docker logs minieshopapi-api-1`
- Or you can connect by hand
`docker exec -it minieshopapi-db-1 psql -U postgres`

- Use this SQL querry to check if database works
`\l`
 Press `CTRL+Z` to escape the editor

- There are tests in Tests/DockerTests.cs, but we can just run them all at once
`dotnet test`

##### If you run into DB issues:

- Migrate DB
` dotnet ef database update --project MiniEShopAPI/MiniEShopAPI.csproj`

- Test if table is created
`docker exec -it minieshopapi-db-1 psql -U postgres -c "\dt"`	 
	

### 👽 **Run via dotnet**
	


- Build and Run the application:
`dotnet build`
`dotnet run --project MiniEShopAPI/MiniEShopAPI.csproj`

##### Occupied ports

- If you are running the program for the second time, the ports might still be occupied. Use this command to check what is using port 5000:
`netstat -ano | findstr :5000`

  - Then use the following command to terminate the processes:
	`taskkill /PID <PID> /F`
- Replace `<PID>` with the process ID.

- Then clean and build the project:
`dotnet clean`
`dotnet build`


### 🔍 **Manually run tests**:

- Test the database connection:
`curl http://localhost:8080/api/products/test-db-connection`

- Now run all **tests**:
`dotnet test`

- Optionally, test API endpoints manually:
`curl -X GET "http://localhost:8080/api/products/4"` to get product by ID
and to create new product:
    ```bash
     curl -X POST http://localhost:8080/api/products -H "Content-Type: application/json" -d "{\"name\":\"Product 4\", \"description\":\"Description for Product 4\", \"price\":40.99}" 
     ```

- Run Postman tests yourself:
`newman run MiniEShopAPI/Tests/postman_tests.json`

	 
#### 🗄️ **Don't forget to migrate the database from time to time**:

`dotnet ef database update --project MiniEShopAPI/MiniEShopAPI.csproj`

---

## 📬 Contact
If you have any questions or suggestions, contact the developer via GitHub or email.

