# ChatApp
# React Vite + ASP.NET Web API Project

https://chatapp-argkatgff8aqakfz.westeurope-01.azurewebsites.net/

This project is a web application with a frontend and backend.  
A key feature of this application is the integration of **Cognitive Services** to analyze messages and determine their context as positive, neutral, or negative.

![image](https://github.com/user-attachments/assets/58d07dc2-64ea-4b98-ae46-834440e55ca0)  
![image](https://github.com/user-attachments/assets/82292b31-cdba-46d1-acfb-6028f061cea8)  

---

## 🧪 Test Users  
If you want to test the app without creating an account, you can log in using the following credentials:  

- **Username:** `Johnny` | **Password:** `pa$$w0rd`  
- **Username:** `Sara` | **Password:** `pa$$w0rd`  

---

## 🛠 Technology Stack  
- **Frontend:** React.js (Vite)  
- **Backend:** .NET Web API  
- **State Management:** Zustand  
- **Styling:** Tailwind CSS  
- **Routing:** React Router  
- **Real-time Communication:** Azure SignalR Service (integrated through Azure Cloud)  
- **AI Integration:** Azure Cognitive Service  
- **Database:** MSSQL  

---

## ⚙️ Setup
Follow these steps to set up the application locally:

1. **Clone the Repository**
   ```bash
   git clone https://github.com/nix1707/ChatApp.git
   cd ChatApp
   ```

2. **Run Backend**
   ```bash
   cd API/API
   dotnet run
   ```

3. **Run Frontend**
   ```bash
   cd client-app
   npm run dev
   ```
