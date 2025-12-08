# Features
Main features that the project needs to provide:
- Account management
- Creating slideshows
- Image management
    - FrameImage class (so to not confuse with WPF Image)

# Code structure
Separate folders for each of the main features:
- Account Management -- delegate to Tori
- Frame management -- Julcia?
- Photo show management -- me

# Classes
- PhotoControl
    - provides a toggleable checkbox in the upper right corner
    - provides rounded corner
    - inherited by Frame, Photo and Effect classes
- Dynamic image rendering -- we will see lol
- PageManager
    - To switch pages
    - Have each page provide a AskBeforeExiting, for screens that the user can edit