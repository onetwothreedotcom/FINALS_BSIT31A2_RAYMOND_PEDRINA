# Pet Adoption Forum

A complete ASP.NET Core MVC application for pet adoption management.

## Features

### User Features (No Login Required)
- ✅ View all available pets with pictures
- ✅ View all adopted pets with pictures
- ✅ Click on any pet to see full details (name, age, breed, description, full picture)
- ✅ Submit unlimited adoption requests without logging in
- ✅ Fill out adoption form with: Full Name, Phone Number, Age, and Email

### Admin Features (Login Required)
- ✅ Secure login with session management
- ✅ Add new pets to the system
- ✅ Edit existing pets (both available and adopted)
- ✅ View all pending adoption requests
- ✅ Approve adoption requests (automatically moves pet to adopted section)
- ✅ View separate lists of available pets and adopted pets
- ✅ When approving an adoption, other pending requests for the same pet are automatically rejected

## Tech Stack
- **Framework**: ASP.NET Core 8.0 MVC
- **Database**: SQLite with Entity Framework Core
- **Session Management**: In-memory distributed cache
- **UI**: Bootstrap 5

## Setup Instructions

### 1. Prerequisites
- .NET 8.0 SDK installed

### 2. Database Setup
The database is already configured with migrations. To reset or update the database:

```powershell
# Navigate to the project directory
cd "C:\Users\RYZEN\source\repos\Pet Adoption Forum\Pet Adoption Forum"

# Apply migrations (if needed)
dotnet ef database update
```

### 3. Run the Application
```powershell
# From the project directory
dotnet run
```

The application will start at: `http://localhost:5210`

## Default Admin Credentials
- **Username**: `admin`
- **Password**: `password`

⚠️ **Important**: Change these credentials in production!

## Usage Guide

### For Users
1. Visit the home page
2. Click "View Available Pets" to see all pets available for adoption
3. Click "View Adopted Pets" to see pets that have been adopted
4. Click on any pet card to view full details
5. On the details page, click "Adopt" if the pet is available
6. Fill out the adoption form and submit

### For Admins
1. Click "Admin" in the navigation bar
2. Login with the credentials above
3. **Dashboard Overview**:
   - Pending adoption requests (with approve button)
   - Available pets (with edit buttons)
   - Adopted pets (with view/edit buttons)
4. **Add New Pet**:
   - Click "Add New Pet" button
   - Fill in pet details (Name, Age, Breed, Description, Image URL)
   - Submit to add to available pets
5. **Edit Pet**:
   - Click "Edit" on any pet card
   - Modify pet information
   - Save changes
6. **Approve Adoption**:
   - Review pending requests in the dashboard
   - Click "Approve" for the selected adopter
   - Pet automatically moves to adopted section
   - Other pending requests for that pet are rejected

## Project Structure
```
Pet Adoption Forum/
├── Controllers/
│   ├── AdminController.cs      # Admin management
│   ├── HomeController.cs       # Home page
│   └── PetsController.cs       # Pet viewing and adoption
├── Models/
│   ├── Pet.cs                  # Pet entity
│   ├── AdoptionRequest.cs      # Adoption request entity
│   └── ErrorViewModel.cs
├── Views/
│   ├── Admin/
│   │   ├── Dashboard.cshtml    # Admin main page
│   │   ├── Login.cshtml        # Admin login
│   │   ├── AddPet.cshtml       # Add new pet form
│   │   └── EditPet.cshtml      # Edit pet form
│   ├── Pets/
│   │   ├── Index.cshtml        # Available pets
│   │   ├── Adopted.cshtml      # Adopted pets
│   │   ├── Details.cshtml      # Pet details
│   │   └── Adopt.cshtml        # Adoption form
│   └── Shared/
│       └── _Layout.cshtml      # Main layout with navigation
├── Data/
│   └── ApplicationDbContext.cs # EF Core context
└── Migrations/                 # Database migrations
```

## Key Features Implementation

### Session-Based Authentication
- Admin sessions last 30 minutes
- Automatic logout functionality
- Protected admin routes

### Database Relations
- One-to-Many: Pet → AdoptionRequests
- Proper foreign key constraints
- Cascade delete protection

### User Experience
- Bootstrap responsive design
- Alert messages for actions
- Empty state handling
- Proper validation on all forms

## Sample Pet URLs for Testing
Use these URLs when adding pets:
- `https://images.unsplash.com/photo-1543466835-00a7907e9de1`
- `https://images.unsplash.com/photo-1587300003388-59208cc962cb`
- `https://images.unsplash.com/photo-1514888286974-6c03e2ca1dba`
- `https://images.unsplash.com/photo-1517849845537-4d257902454a`

## Troubleshooting

### Database Issues
If you encounter database errors:
```powershell
# Delete the database file
Remove-Item "PetAdoption.db", "PetAdoption.db-shm", "PetAdoption.db-wal"

# Recreate it
dotnet ef database update
```

### Port Already in Use
If port 5210 is busy, the app will automatically use a different port. Check the console output.

## Future Enhancements
- Image upload functionality (currently uses URLs)
- Email notifications for adoption approvals
- Search and filter functionality
- User accounts for tracking adoption history
- Admin user management
- Pet categories (dogs, cats, birds, etc.)

## License
MIT License - Free to use and modify
