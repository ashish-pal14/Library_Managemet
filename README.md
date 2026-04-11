# 📚 Library Management System

A **3‑tier** Library Management System built with **.NET 8 Web API** and a **Bootstrap 5 UI**.  
Designed for easy testing with an **in‑memory database** – no external database setup required.

---

## ✨ Features

- **Books** – Add, edit, delete, list books  
- **Members** – Add, delete, list members  
- **Borrowing** – Borrow a book (decreases available copies), return a book (increases available copies)  
- **Borrowing history** – View all borrowings (active & returned) per member  
- **Web UI** – Responsive interface using Bootstrap 5  
- **REST API** – Full JSON API for integration with other frontends  
- **Swagger** – Interactive API documentation  

---

## 🏗️ Architecture (3‑Tier)

| Layer            | Technology                          |
|------------------|-------------------------------------|
| **Presentation** | ASP.NET Core Controllers + HTML/JS  |
| **Business**     | C# Service classes                  |
| **Data Access**  | Repository pattern + EF Core        |
| **Database**     | In‑Memory (EF Core)                 |

---

## 🚀 Getting Started (Run on Any Machine)

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (Windows, macOS, Linux)

### Clone & Run
```bash
git clone https://github.com/your-username/LibraryManagement.git
cd LibraryManagement
dotnet run --urls "http://0.0.0.0:5000"
Open your browser at http://localhost:5000 – the UI appears immediately.
Swagger docs: http://localhost:5000/swagger

No database installation needed – the app uses an in‑memory database that resets on every restart.

📁 Project Structure
text
LibraryManagement/
├── Controllers/        # API endpoints
├── DTOs/               # Data transfer objects
├── Models/             # Domain entities
├── Services/           # Business logic & interfaces
├── Repositories/       # Data access (generic + specific)
├── Data/               # EF Core DbContext
├── wwwroot/            # Static files (index.html)
├── Program.cs          # App setup & DI
└── appsettings.json    # Configuration
🔌 API Endpoints
All endpoints return JSON. Base URL: http://localhost:5000/api

Books
Method	Endpoint	Description
GET	/books	List all books
GET	/books/{id}	Get book by ID
POST	/books	Add a new book
PUT	/books/{id}	Update a book
DELETE	/books/{id}	Delete a book
Members
Method	Endpoint	Description
GET	/members	List all members
GET	/members/{id}	Get member by ID
POST	/members	Add a new member
PUT	/members/{id}	Update a member
DELETE	/members/{id}	Delete a member
Borrowings
Method	Endpoint	Description
POST	/borrowings/borrow?bookId=X&memberId=Y	Borrow a book
POST	/borrowings/return/{borrowingId}	Return a book
GET	/borrowings/history/{memberId}	History of a member
GET	/borrowings	All borrowings
🖥️ Web UI Preview
The UI is served at the root (/) and provides tabs for:

Books – add, edit, delete, view all

Members – add, delete, view all

Borrow / Return – borrow books, return active borrowings, see currently borrowed books

Borrowing History – pick a member to see their borrowing history

All actions call the API asynchronously – no page reloads.

🗃️ Switching to a Persistent Database
By default the app uses in‑memory (data lost on restart).
For production, you can switch to SQL Server or SQLite.

To use SQL Server
Install SQL Server (local or remote)

Add the provider: dotnet add package Microsoft.EntityFrameworkCore.SqlServer

In Program.cs, replace UseInMemoryDatabase with:

csharp
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
Set the connection string in appsettings.json

Run migrations: dotnet ef migrations add InitialCreate and dotnet ef database update

To use SQLite (file‑based)
dotnet add package Microsoft.EntityFrameworkCore.Sqlite

Change Program.cs to options.UseSqlite("Data Source=library.db")

Run migrations as above

🐧 Deploying on a Linux Server
Install .NET 8 SDK on the server

Clone the repository

(Optional) Switch to SQL Server or SQLite as described

Run dotnet build --configuration Release

Run the app: dotnet run --configuration Release --urls "http://0.0.0.0:5000"

(Recommended) Set up a systemd service to keep the app running after reboot

🧪 Troubleshooting
Issue	Solution
dotnet: command not found	Install .NET 8 SDK
Port 5000 already in use	Change the port in --urls parameter
UI shows no data	Refresh the page; check browser console for errors
Unexpected end of JSON input	The apiCall function in index.html already handles empty responses – refresh after hard reload (Ctrl+F5)
📄 License
This project is open‑source and available under the MIT License.

🙌 Acknowledgments
Bootstrap 5 for the UI components

.NET team for the excellent framework

Entity Framework Core for data access
