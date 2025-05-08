# 🛒 Mini E-Shop API

## 📖 Description
Mini E-Shop API is a compact and well-structured ASP.NET Core web application designed for managing products and orders in an online store. The project demonstrates clean code principles, RESTful design, SOLID principles, and Docker-based deployment.

## ✨ Features
- **Products**:
  - 📋 List all products
  - 🔍 Get product by ID
  - ➕ Create a new product
  - ✏️ Update a product
  - ❌ Delete a product

- **Orders**:
  - 📋 List all orders
  - 🔍 Get order by ID
  - ➕ Create a new order
  - ❌ Delete an order

## ⚙️ Technical Requirements
- **Backend**:
  - ASP.NET Core API (.NET 6 or later)
  - Clean architecture (Controllers, Services, Repositories, Models, etc.)
  - Apply SOLID principles
  - Use Dependency Injection

- **Database**:
  - Relational database (PostgreSQL recommended)
  - Use EF Core for database operations

- **Caching**:
  - Implement a Factory Pattern to switch between:
    - 🧠 In-memory cache
    - 🛠️ Simulated Redis

- **Docker**:
  - 🐳 Dockerfile for the API
  - 🛠️ docker-compose.yml to run the API and database

## 🛠️ Installation and Setup
1. Ensure you have the following installed:
   - .NET SDK 6.0 or later
   - Docker and Docker Compose

2. Clone the repository:
   ```bash
   git clone <repository URL>
   cd MiniEShopAPI
   ```

3. Build and run the containers:
   ```bash
   docker-compose up --build
   ```

4. The API will be available at:
   ```
   http://localhost:5000
   ```

## 🧪 Testing
- **Postman**:
  - Import the `Tests/postman_tests.json` file into Postman to test the API.

- **curl**:
  - Use the `Tests/curl_tests.http` file to execute requests via REST Client or command line.

- **Automated Tests**:
  - Run tests using the following command:
    ```bash
    dotnet test
    ```

## 📂 Project Structure
- **Controllers**: Handles HTTP requests.
- **Services**: Application logic and caching.
- **Repositories**: Data access.
- **Models**: Data models.
- **Tests**: Tests to verify functionality.

--------------------------------------------------------------------

# 🚀 How to run it

0. 🖥️ **Open three terminals in the root directory of this project**:

1. 🍍 **First terminal**: For the application.
	
	- (If you have just downloaded this project) Run the setup script:
		```bash
		setup.bat
		```

   - (If you are running the program for the second time, the ports might still be occupied) Use this command to check what is using port 5000:
		```bash
		netstat -ano | findstr :5000
		```
		- Then use the following command to terminate the processes:
		```bash
		taskkill /PID <PID> /F
		```
     - Replace `<PID>` with the process ID.

	- Then clean and build the project:
		```bash
		dotnet clean
		```
		```bash
		dotnet build
		```

	- Run the application:
		```bash
		dotnet run --project MiniEShopAPI/MiniEShopAPI.csproj
		```

2. 🐳 **In the second terminal**:
   
   - Open Docker Desktop

   - Start Docker in terminal:
     ```bash
     docker compose up --build
     ```
   - Optionally, press `v` to open the desktop version of Docker.

   - If you are running the program and Docker for the second time you may stumble uppon an issue in Docker Desktop if you try to stop it `Cannot stop Docker Compose application. Reason: Max retries reached: connect ENOENT...`, then just restart the desktop version
   (mb with CTRL+SHIFT+ESC. Or mb restart your PC).

3. 🔍 **In the third terminal**:

   - Test the database connection:
     ```bash
     curl http://localhost:5000/api/product/test-db-connection
     ```
	 or open this link in your browser

   - Now run **tests**:
     ```bash
     dotnet test
     ```
   - Optionally, test API endpoints manually:
     ```bash
     curl -X GET "http://localhost:5000/api/product?name=Product%204"
     ```
     ```bash
     curl -X POST http://localhost:5000/api/product -H "Content-Type: application/json" -d "{\"Name\":\"Product 4\", \"Description\":\"Description for Product 4\", \"Price\":40.99}"
     ```
   - You can also run Postman tests yourself:
	 ```bash
	 newman run MiniEShopAPI/Tests/postman_tests.json
	 ```
4. 🗄️ **Don't forget to migrate the database from time to time**:

	 ```bash
	dotnet ef database update --project MiniEShopAPI/MiniEShopAPI.csproj
	 ```

5. ✅ **Press CTRL+C to close terminals when finished**

---

## 📬 Contact
If you have any questions or suggestions, contact the developer via GitHub or email.

