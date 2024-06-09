# ConnectFour
Simulating Connect 4 Game

# Prerequsites
Requires .Net SDK 7.0 or greater

# Install
- Clone the repo
- Navigate to folder ConnectFour
- Run the following commands 
  - `dotnet restore`
  - `dotnet build`
  - `dotnet test`
# Running the game
From command line
- `dotnet run ./ConnectFour/ConnectFour.csproj` 

Enjoy!

Some possible improvements
- Tests are not extensive and lack some cases as highlighted in the test file too.
- The winning strategy implemenation can be moved out of Board class, for extensibility
- The GamePlay class has not been tested.
