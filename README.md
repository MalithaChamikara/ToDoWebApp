# ToDoWebApp


A full-stack To-Do web application built with ASP.NET Core (Web API), React, Redux, PostgreSQL, and Docker.

---

## 📁 Project Structure

```
ToDoWebApp/
│
├── TodoApi/                # ASP.NET Core Web API backend
│   ├── Controllers/
│   ├── Data/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   ├── Program.cs
│   └── ...
│
├── frontend/               # React frontend
│   ├── src/
│   │   ├── components/
│   │   ├── store/
│   │   ├── tests/
│   │   └── ...
│   ├── public/
│   └── package.json
│
├── docker-compose.yml      # Docker Compose file
├── .gitignore
└── README.md
```

---

## 🛠 Tech Stack

- **Backend:** ASP.NET Core Web API
- **Frontend:** React, Redux, Material-UI
- **Database:** PostgreSQL
- **Testing:** xUnit (backend), Jest & React Testing Library (frontend)
- **Containerization:** Docker, Docker Compose

---

## 🗄 Database Design

**Table: Tasks**

| Column      | Type         | Description           |
|-------------|--------------|-----------------------|
| Id          | UUID (PK)    | Unique identifier     |
| Title       | string       | Task title            |
| Description | string       | Task description      |
| IsCompleted | bool         | Completion status     |
| CreatedAt   | DateTime     | Creation timestamp    |

---

## 🏛 System Architecture

```
[ React + Redux Frontend ]
            |
            v
[ ASP.NET Core Web API ]
            |
            v
[ PostgreSQL Database ]
```

- The frontend communicates with the backend via RESTful API endpoints.
- The backend handles business logic, data validation, and database operations.
- All services are containerized and orchestrated using Docker Compose.

---

## 🚀 Getting Started (with Docker)

### Prerequisites

- [Docker](https://www.docker.com/get-started) and Docker Compose installed

### 1. Clone the repository

```bash
git clone https://github.com/MalithaChamikara/ToDoWebApp.git
cd ToDoWebApp
```

### 2. Configure Environment Variables

- Create a `.env` file in the root or use the provided sample.
- Set your PostgreSQL credentials and connection strings as needed.

### 3. Build and Run with Docker Compose

```bash
docker-compose up --build
```

- The backend will be available at `http://localhost:5000`
- The frontend will be available at `http://localhost:3000`

### 4. Stopping the Application

```bash
docker-compose down
```

---

## 🧪 Running Tests

### Backend (xUnit)

```bash
cd TodoApi
dotnet test
```

### Frontend (Jest)

```bash
cd frontend
npm install
npm test
```

---
