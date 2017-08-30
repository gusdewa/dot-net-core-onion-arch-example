This solution is built on top of **.NET Core**. The **.NET Core** is a cross-platform framework. It can be run not only on Windows, but also on Linux and MacOS.

## Install .NET Core on Linux or Mac

Please visit this page:
`https://www.microsoft.com/net/core#linuxredhat`

Please choose your **MacOS** or **Linux** distribution, then follow the instructions.

## Build the Source Code

1. Go to `~/ParkingLot/` folder
2. Run CLI `dotnet restore`
3. Run CLI `dotnet build`

## Run the program

1. Go to `~/ParkingLot/ParkingLot.ConsoleApp`
2. Run CLI `dotnet run`
3. Type `parking_lot` or `parking_lot filename.txt`. In case of opening a file, it needs to be placed in the same folder with the Console App.

## Run the unit test project

1. Go to any folder having name pattern `*.Tests`, e.g., `~/ParkingLot/ParkingLot.ApplicationService.Tests`
2. Run CLI `dotnet test`

## Source Code Patterns and Structure
The structure follow **Domain Driven Design** principle. It placed the **Domain** as a core dependency. The **Domain** is wrapped inside the **Application Service**. All interaction to the infrastructure, e.g., Console or File, is maintained in the **Infrasturcture** project. Through *interface segragetation* (the interfaces are placed in the **Domain**), the **Application Service** can be aware of it, but not having a direct dependency to it. It can be then injected by a Dependency Resolver library.

- `ParkingLot.ApplicationService\`:
  - Has dependency to **Domain**
  - Command and Factory pattern artifacts.
  - Command-Query Segregation artifacts.
    - Command: Methods to mutate the domain state
    - Query: Methods to retrieve the domain state
- `ParkingLot.ApplicationService.Tests\`:
  - Test project for `ParkingLot.ApplicationService`
- `ParkingLot.ConsoleApp\`:
  - Has dependency to **Application Service** and **Infrastructure**
  - Starting point to run the Command program.
  - Implements dependency injection for Application Service through the constructor.
- `ParkingLot.Domain\`:
  - Domain is a core. It must not have any dependencies to other projects
  - Domain Models, e.g., Car, CarSlotManager, etc.
  - Core Utils Models, e.g., IScreenWriter, IFileReader
- `ParkingLot.Domain.Tests\`:
  - Test project for `ParkingLot.Domain`
- `ParkingLot.Infrastructure\`:
  - Has dependency to **Infrastucture**
  - Implementation that is tightly-coupled to infrastuctore, like Console and File