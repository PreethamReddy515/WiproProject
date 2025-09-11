RealTimeChatApp Setup Instructions
Prerequisites

Before running the project, make sure you have the following installed:

.NET 7 SDK (or the version your backend uses) – Download here

Node.js (v18+) and npm – Download here

SQL Server (LocalDB or full SQL Server)

Optional: Postman for API testing

Backend Setup

Clone the repository

git clone <repo-url>
cd RealTimeChatApp.Backend


Configure Database

Open appsettings.json

Update the DefaultConnection string to point to your SQL Server instance.

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=RealTimeChatDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}


Apply Migrations

dotnet ef database update


Configure JWT (Optional)

Update appsettings.json → Jwt section if you want to use your own key/issuer/audience.

Run Backend

dotnet run


Default URL: http://localhost:5000

Frontend Setup

Navigate to frontend folder

cd ../frontend


Install dependencies

npm install


Create environment file

Create frontend/.env with:

VITE_API_BASE_URL=http://localhost:5000/api
VITE_SIGNALR_URL=http://localhost:5000/hubs/chat


Run frontend

npm run dev


Default URL: http://localhost:5173

Notes / Tips

Uploaded files are stored in wwwroot/uploads on the backend. Make sure the folder exists.