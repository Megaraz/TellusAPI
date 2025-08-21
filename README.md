# E-commerce API (SQL Server + ADO.NET)

## 📌 Description
This project is a database-driven e-commerce solution with a strong focus on **database design and SQL development**.  
The database is entirely built in **SQL Server**, including tables, relationships, constraints, and **stored procedures** for core business logic.  

On top of the database, an API layer was implemented in **C# (.NET, ADO.NET)** to handle communication with the database and expose data operations.  

The project does not include a GUI — the main focus is on the **backend and data access layer**.

---

## 🚀 Features
- Relational database for e-commerce (customers, products, orders, etc.).  
- Business logic implemented with **stored procedures**.  
- API layer in **C# (ADO.NET)** to perform CRUD operations.  
- All SQL scripts required to create the database are included.  

---

## 🛠️ Technologies
- **Database:** SQL Server (T-SQL, Stored Procedures)  
- **Backend:** C# (.NET), ADO.NET  
- **Other:** API layer for database interaction  

---

## 📂 Project Structure
- `SQLCode.sql` → contains all scripts to create the database schema and stored procedures.  
- `Models and Repositories` → C# code with ADO.NET for database access and CRUD operations.  

---

## 📖 How to Use
1. Create a new database in **SQL Server**.  
2. Run the scripts in the `SQLCode.sql` file to generate the schema, tables, and stored procedures.  
3. Open the `Test` project in **Visual Studio** (or similar IDE).  
4. Update the database connection string in Test-project if needed
5. Run the project and interact with the API layer.  

---

## 📌 Notes
- This project was developed as a **learning exercise** with a primary focus on **SQL-first development** and ADO.NET.  
- Future improvements could include an ASP.NET Core API and a simple front-end client for demonstration.  
