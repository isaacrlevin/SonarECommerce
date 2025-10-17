# TechStore - E-commerce Computer Components Store

A modern, full-featured e-commerce application built with ASP.NET Core Blazor Server for selling computer components and hardware.

## Features

### 🛍️ Product Management
- **8 Product Categories**: Processors, Graphics Cards, Memory, Storage, Motherboards, Power Supplies, Cooling, Cases
- **18 Sample Products**: Pre-seeded with realistic computer components and pricing
- **Advanced Filtering**: Search by name, category, brand, price range
- **Multiple Views**: Grid and list view options
- **Stock Management**: Real-time stock tracking and availability

### 🛒 Shopping Cart
- **Add to Cart**: One-click add to cart functionality
- **Quantity Management**: Increase/decrease item quantities
- **Real-time Updates**: Live cart updates with toast notifications
- **Cart Summary**: Subtotal, shipping, tax calculations
- **Free Shipping**: Free shipping on orders over $100

### 🔐 User Authentication
- **ASP.NET Core Identity**: Built-in user registration and login
- **Secure Authentication**: Email confirmation required
- **Protected Routes**: Cart and checkout require authentication
- **User Account Management**: Profile management capabilities

### 🎨 Modern UI/UX
- **Bootstrap 5**: Responsive, mobile-first design
- **Font Awesome Icons**: Professional iconography
- **Interactive Components**: Hover effects, animations, and transitions
- **Toast Notifications**: User-friendly feedback system
- **Sticky Navigation**: Persistent search and cart access

### 🔍 Advanced Search & Filtering
- **Full-text Search**: Search across product names, descriptions, brands
- **Category Filtering**: Browse by specific component types
- **Brand Filtering**: Filter by manufacturer
- **Price Range**: Min/max price filtering
- **Smart Sorting**: Sort by name, price, newest items

### 🏗️ Technical Architecture
- **Blazor Server**: Real-time, interactive web UI
- **Entity Framework Core**: Code-first database approach
- **SQL Server**: Robust data storage
- **Service Layer**: Clean separation of concerns
- **Repository Pattern**: Organized data access

## Database Schema

### Core Tables
- **Categories**: Product categorization
- **Products**: Product catalog with pricing and inventory
- **ShoppingCarts**: User shopping carts
- **CartItems**: Individual cart line items
- **Orders**: Order header information
- **OrderItems**: Order line item details

### Key Features
- **Foreign Key Relationships**: Proper data integrity
- **Indexes**: Optimized for performance
- **Seed Data**: 8 categories and 18 sample products
- **Computed Properties**: Cart totals, item counts

## Sample Products

### Processors
- AMD Ryzen 9 7950X - $699.99
- Intel Core i9-13900K - $589.99
- AMD Ryzen 7 7700X - $399.99

### Graphics Cards
- NVIDIA RTX 4090 - $1,599.99 (Featured)
- AMD RX 7900 XTX - $999.99 (Featured)
- NVIDIA RTX 4070 - $599.99

### Memory & Storage
- Various DDR5 RAM configurations
- NVMe SSDs from Samsung and WD
- Price range: $129.99 - $329.99

### Other Components
- Premium motherboards, power supplies, cooling solutions, and PC cases
- All with realistic pricing and specifications

## Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server (LocalDB works)
- Visual Studio 2022 or VS Code

### Installation
1. Clone the repository
2. Update connection string in `appsettings.json`
3. Run `dotnet run --project SonarECommerce`
4. Navigate to `http://localhost:5010`

### First Run
The application will automatically:
- Create the database
- Seed categories and products
- Set up Identity tables

## User Experience Highlights

### Home Page
- Hero section with call-to-action
- Featured products showcase
- Category quick navigation
- Why choose us section

### Product Browsing
- Advanced search and filtering
- Grid/list view toggle
- Real-time product availability
- Add to cart with authentication check

### Shopping Cart
- Visual cart items with images
- Quantity adjustment controls
- Running totals with shipping/tax
- Free shipping threshold indicator
- Secure checkout preparation

### Navigation
- Persistent search bar
- Category dropdown menu
- Live cart item counter
- User account dropdown

## Technology Stack

- **Frontend**: Blazor Server, Bootstrap 5, Font Awesome
- **Backend**: ASP.NET Core 10, Entity Framework Core
- **Database**: SQL Server with Code-First migrations
- **Authentication**: ASP.NET Core Identity
- **UI Components**: Custom Razor components
- **Styling**: Custom CSS with Bootstrap overrides

## Future Enhancements

- Order processing and history
- Product reviews and ratings  
- Wishlist functionality
- Payment integration
- Admin dashboard
- Email notifications
- Product image upload
- Inventory management
- Multi-vendor support

This is a complete, production-ready foundation for an e-commerce store with modern web technologies and best practices.