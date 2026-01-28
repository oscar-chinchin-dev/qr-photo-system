# QR-Based Photo Selection System

Web application that allows clients to access a private photo gallery
using a QR code, select images, and manage their commercial status.

##  Context

This project simulates a real-world workflow commonly used in events and
photography sessions, where clients access their images without registrations
or user accounts by using a physical identifier (QR code).  
This type of system is frequently used in tourist environments and
high-traffic events.

##  Features

- Session access via QR code
- Private photo gallery visualization
- Photo selection by the client
- Payment and availability status management
- Frontend-backend communication through a REST API

##  Architecture

- **Frontend**: Angular
- **Backend**: ASP.NET Core Web API
- **Database**: SQL Server (Entity Framework Core)
- **Communication**: DTOs + REST API

The system clearly separates domain entities, input/output contracts (DTOs),
and business logic implemented within the controllers.

##  Main Workflow

1. The system creates a Visit associated with a QR code
2. The client accesses the gallery by scanning the QR code
3. Photos linked to the visit are loaded
4. The client selects the desired images
5. The backend registers the selection and updates the corresponding states

##  Learnings

- Design and consumption of REST APIs
- Use of DTOs to decouple frontend and backend
- Entity modeling and relationship management
- Commercial state handling in a real-world system
- Project documentation and organization

##  Project Status

Functional project under continuous improvement.
Designed with a scalable architecture that allows future extensions,
such as payment platform integration.
Focused on learning and applying good software development practices.
