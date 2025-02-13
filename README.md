# Server Control Panel


![daisyUI](https://img.shields.io/badge/daisyUI-1ad1a5?style=for-the-badge&logo=daisyui&logoColor=white)
![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-38B2AC?style=for-the-badge&logo=tailwind-css&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor-512BD4?style=for-the-badge&logo=blazor&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Rider](https://img.shields.io/badge/Rider-000000?style=for-the-badge&logo=Rider&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-323330?style=for-the-badge&logo=javascript&logoColor=F7DF1E)
![JSON](https://img.shields.io/badge/json-5E5C5C?style=for-the-badge&logo=json&logoColor=white)


A modern and sleek Server Control Panel built using **C# Blazor** and **DaisyUI**, featuring a **blurred glass effect** for a visually stunning user interface. This control panel allows administrators to manage server settings, monitor server status, and access other crucial features in an intuitive way.

## Screenshots

### Desktop View
![Desktop Screenshot 1](Images/screenshot_desktop1.png)
![Desktop Screenshot 2](Images/screenshot_desktop2.png)

## Features

- **Modern UI**: Utilizes DaisyUI for a clean, responsive design with a blur-glass effect that gives the panel a contemporary and attractive look.
- **Server Management**: Easily access information about your server, and control virtlib VMs.
- **Responsive Design**: The panel adapts to any screen size, providing a smooth experience on both desktop and mobile devices.
- **User Authentication**: Secure login to ensure only authorized users can access the control panel.
- **Customizable Settings**: Easily modify the control panel settings through the `settings.json` configuration file.

## Technologies Used

- **C#**: Core backend logic for server management.
- **Blazor**: Framework for building the interactive web UI.
- **DaisyUI**: Tailwind CSS plugin for modern and customizable UI components.
- **TailwindCSS**: Utility-first CSS framework used for styling.

## Customizing the Control Panel

The behavior and appearance of the control panel can be customized through the `settings.json` file located in the project root directory.

Here is an example of a typical `settings.json`:

```json
{
  "panel_title": "Server Panel",
  "password": "testpassword",
  "vms": [
    {
      "name": "Gameserver",
      "os": "Ubuntu 24.04 LTS"
    },
    {
      "name": "Ubuntu_VM1",
      "os": "Ubuntu 24.04 LTS"
    }
  ]
}
