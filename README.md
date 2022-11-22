# Initializer

## What is it?
*Initializer* helps you run scripts when the game is started in the *editor and build* with the help of *Addressables package*. This tool can be, for example, used to load an *(Addressable)* "Initialization" scene.

## Getting started
1. Clone or download repository.
2. Copy **Initializer** folder from **Plugins** folder to your project.
3. Take a look at the included *Demo*.
4. Done!

Developed in **Unity 2020.3.41f1**, but I don't see why it wouldn't work in any Unity version **as long as Addressables package is supported**.

## How To Use
1. Make sure **Addressables package** is added to your project.
2. **Right-click** in a folder and navigate to **Create** -> **Initializer** -> **Default Initializer** to create a new *Initializer asset*.
3. **Mark** your new *Initializer asset* **as Addressable** and set the addressable name as **Initializer**.
4. **Create a new scene** (named *Initialization*, for example) and **mark it as Addressable**. The included *Default Initializer* script will load this scene when the game is started.
5. **Select** the **Initializer asset** you created in *step 2* and use the inspector to select the **Initialization scene** in the **Scene field**.
3. Press play!

Remember: If something doesn't work in build, try rebuilding Addressables (**Window** -> **Asset Management** -> **Addressables** -> **Groups**) and build the game again.

## Demos
### 1. Basic
A basic example that loads an Initialization scene when the game is started.
