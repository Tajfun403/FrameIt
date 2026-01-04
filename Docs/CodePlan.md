# Features
Main features that the project needs to provide:
- Account management
- Creating slideshows
- Image management
    - FrameImage class (so to not confuse with WPF Image)

# Code structure
Separate folders for each of the main features:
- Account Management -- Julcia
- Frame management -- Tori
- Photo show management -- Sara
    - 

# Classes
- PhotoControl
    - provides a toggleable checkbox in the upper right corner
    - provides rounded corner
    - inherited by Frame, Photo and Effect classes
    - provide optional ShowName?
- Dynamic image rendering -- we will see lol
- PageManager
    - To switch pages
    - Have each page provide a AskBeforeExiting, for screens that the user can edit
    - Prop to hide the current user bar if the user's not logged in
- StorageManager
    - Basic serialization of user accounts, photos, shows, etc
    - Will try to use JSON Serializer
        - The built-in one just throws NotSupportedException without any context on what's actually unsupported
            - Great design Microsoft, great design
        - Will try to focus on DTO classes to try skirt its problems?
        - Can also switch to NewtsonSoft serializer which is allegedely ~~ 10x slower, but more reliable

# Guidlines
- We will use Pages for each of the page our app will provide
- At the top menu, an Account bar will be anchored, which can provide quick navigation and display the current user's name and avatar.
    - Height: 70 pixels
- Base page design: height 530 pixels; width 1000 pixels

# Priorities
## Sara
- PhotoControl -- we will reuse it in MANY places, so it is a top priority
- NavigationBar -- required so that we can actually move through the app
- At least one shows list template
- -- At this point, others can join the project --
- Actually making the entire photo shows pipeline can come now