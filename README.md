# File Organizer App

## Overview
The File Organizer App is a Windows desktop application designed to help users efficiently organize their files and folders. It allows users to scan specified folders or disk drives, categorize files, and schedule regular scans to maintain organization.

## Features
- **Folder and Disk Drive Scanning**: Users can select which folders or disk drives to scan for files.
- **Scheduling**: Users can set up a schedule for regular scanning of selected folders or drives.
- **Category Management**: Users can create target folders or categories to organize their files effectively.
- **User-Friendly Interface**: The application provides a simple and intuitive interface for managing files and settings.

## Project Structure
```
file-organizer-app
├── src
│   ├── main.cs
│   ├── Models
│   │   ├── ScanTarget.cs
│   │   ├── Category.cs
│   │   └── Schedule.cs
│   ├── Services
│   │   ├── FileScanner.cs
│   │   ├── FileOrganizer.cs
│   │   └── ScheduleManager.cs
│   ├── Views
│   │   ├── MainWindow.xaml
│   │   ├── MainWindow.xaml.cs
│   │   ├── ScanSettingsWindow.xaml
│   │   ├── ScanSettingsWindow.xaml.cs
│   │   ├── CategoryManagerWindow.xaml
│   │   └── CategoryManagerWindow.xaml.cs
│   └── ViewModels
│       ├── MainViewModel.cs
│       ├── ScanSettingsViewModel.cs
│       └── CategoryManagerViewModel.cs
├── Resources
│   └── Strings.resx
├── file-organizer-app.csproj
└── README.md
```

## Setup Instructions
1. Clone the repository to your local machine.
2. Open the project in your preferred IDE.
3. Build the project to restore dependencies.
4. Run the application to start organizing your files.

## Future Enhancements
- Implement additional file filtering options.
- Add support for cloud storage integration.
- Enhance the user interface for better usability.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.