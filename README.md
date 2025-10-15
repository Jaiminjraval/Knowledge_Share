# KnowledgeShare - A Peer-to-Peer Learning Platform

## Project Overview
KnowledgeShare is a web application built with **ASP.NET Core MVC** that functions as a marketplace for skills and knowledge. It connects users who want to learn a specific subject with users who are willing to teach it. The platform manages user roles, profiles, subjects, and a complete session request workflow.

The project is built on a robust backend using C# and leverages Entity Framework Core to communicate with a SQL Server LocalDB database. User authentication and management are handled by the powerful and secure ASP.NET Core Identity framework.

**Features Implemented**
The application includes a full suite of features for both "Tutor" and "Learner" user roles.

## User & Authentication Features 
User Registration: A custom registration page with fields for Name, Email, Password, and a dropdown for selecting a Role.

**Role-Based System**: Users register as either a Tutor (can teach) or a Learner (can request sessions).

**User Login/Logout**: Secure user authentication managed by ASP.NET Core Identity.

**Authorization**: Key pages and actions are protected, requiring users to be logged in.

## Tutor-Specific Features 
**Profile Management**: Tutors have a personal profile page where they can write and update a Bio about themselves.

**Subject Management**: Tutors can add a list of subjects they are proficient in and can remove them at any time.

**Request Management**: Tutors have a dedicated "My Requests" page where they can view all incoming session requests and choose to Approve or Decline them.

## Learner-Specific Features
**Tutor Directory**: Learners can view a list of all available tutors on the platform.

**Search by Subject**: A powerful search functionality allows learners to filter the tutor list by a specific subject.

**Session Requests**: Learners can send a session request to any tutor directly from the tutor list.

**Status Tracking**: Learners have a "My Sessions" page to track the status (Pending, Approved, Declined) of all their outgoing requests.

## Security & Business Logic
**Data Validation**: User inputs (email format, password length, etc.) are validated on the server.

**Ownership Constraints**: Tutors can only manage their own profiles and requests. They cannot interfere with another tutor's data.

**Duplicate Request Prevention**: A learner cannot send a new request to a tutor if they already have one pending.

## Setup Instructions (How to Run the Project)
Follow these steps to set up and run the project on a local machine.

## Prerequisites
1. Visual Studio 2019 or later with the ASP.NET and web development workload installed.
2. .NET Core 3.1 SDK.
3. SQL Server LocalDB, which is typically installed automatically with Visual Studio.


## Step-by-Step Guide
1. Clone or Download the Project:
   Get the project files onto your local machine.
2. Open the Project in Visual Studio:
   Open the KnowledgeShare.sln solution file in Visual Studio.
3. Check the Database Connection:

   Open the appsettings.json file.
   Verify that the DefaultConnection string is pointing to your local SQL Server LocalDB instance. The default should work for most Visual Studio installations:

  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=KnowledgeShareDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }

4. Create the Database via Migrations:
   In Visual Studio, go to the top menu: Tools > NuGet Package Manager > Package Manager Console.

   In the console window that opens, run the following command to create the database based on the project's models. This only needs to be done once.
  
   Update-Database

5. Run the Application:
   Press F5 or click the green "Play" button (IIS Express) in the Visual Studio toolbar.

The application will open in your default web browser.
