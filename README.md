# Threat-o-tron 9000 Obstacle Avoidance System
![MainMenu](/assets/screenshots/MainMenu.png)
A sophisticated pathfinding application built with C# and .NET 8.0 that calculates safe navigation routes through obstacle-laden environments using the A* pathfinding algorithm.

## Project Overview

This command-line application provides intelligent route planning for agents navigating through secure facilities with various obstacle types. The system dynamically calculates optimal paths while avoiding detection from guards, fences, sensors, and security cameras.

### Key Features

- **Intelligent Pathfinding**: Implements the A* algorithm for optimal route calculation
- **Multiple Obstacle Types**: Supports guards, fences, sensors, and cameras with unique detection patterns
- **Dynamic Map Visualization**: Generates ASCII-based maps of obstacle configurations
- **Safety Assessment**: Evaluates directional safety from any coordinate position
- **Scalable Architecture**: Handles arbitrarily large coordinate spaces (32-bit integer range)

## Architecture & Design

### Object-Oriented Design Principles

This project demonstrates advanced OOP concepts:

- **Polymorphism**: Abstract `MapObject` base class with specialized obstacle implementations
- **Encapsulation**: Private fields with controlled public property access
- **Inheritance**: Hierarchical class structure for obstacle types
- **Abstraction**: Separation of concerns across `Map`, `Path`, and `MapObject` classes

### Class Structure

```
Map (Core System)
├── MapObject (Abstract Base Class)
│   ├── Guard
│   ├── Fence
│   ├── Sensor
│   └── Camera
├── Path (Pathfinding Algorithm)
│   └── Node (Pathfinding Node)
└── Utility (Helper Methods)

Program (Main Application Loop)
```

### Key Design Decisions

1. **Static Obstacle List**: Centralized obstacle management through `Map.MapObject_List`
2. **Inheritance Chain**: `MapObject`, `Path`, and `Node` all extend `Map` for shared access to obstacle data
3. **Node-Based Pathfinding**: Custom `Node` class with automatic G and H score calculation in constructor
4. **Command Pattern**: Main program loop parses commands and delegates to appropriate classes
5. **Euclidean Distance Calculation**: Precise sensor range detection using Pythagorean theorem
6. **Utility Class**: Static helper methods for common operations (e.g., Help display)

## Technical Implementation

### Pathfinding Algorithm

The application uses the **A* (A-Star) algorithm** for optimal pathfinding:

- **G Score**: Distance traveled from start node
- **H Score**: Heuristic (Manhattan distance to goal)
- **F Score**: Total cost (G + H)

The algorithm maintains open and closed lists, exploring nodes with the lowest F score first to guarantee an optimal path.

### Obstacle Detection Systems

#### Guard
- **Pattern**: Single point obstruction
- **Implementation**: Coordinate-based blocking

#### Fence
- **Pattern**: Linear obstruction (north/east orientation)
- **Implementation**: Directional length-based blocking

#### Sensor
- **Pattern**: Circular detection radius
- **Implementation**: Euclidean distance calculation
- **Formula**: `√((x₂-x₁)² + (y₂-y₁)²) ≤ range`

#### Camera
- **Pattern**: Infinite cone of vision (45° spread)
- **Implementation**: Diagonal-bounded directional detection

## Usage Examples

### Adding Obstacles
```
Enter command: add guard 2 1
Successfully added guard obstacle.

Enter command: add fence 3 3 east 5
Successfully added fence obstacle.

Enter command: add sensor 5 5 2.5
Successfully added sensor obstacle.

Enter command: add camera 7 7 north
Successfully added camera obstacle.
```
![AddObjects](/assets/screenshots/AddObjects/png)

### Checking Safety
```
Enter command: check 2 2
You can safely take any of the following directions:
North
East
West
```
![CheckSafety](/assets/screenshots/Check.png)
### Finding Paths
```
Enter command: path 3 5 7 1
The following path will take you to the objective:
Head south for 1 klick.
Head east for 5 klicks.
Head south for 3 klicks.
Head west for 1 klick.
```
![CheckPath](/assets/screenshots/path.png)
### Map Visualization
```
Enter command: map 0 -5 9 8
Here is a map of obstacles in the selected region:
.........
.........
....G.G..
.........
...FFFFF.
.........
.........
.........
```
![Display map]()/assets/screenshots/map.png)

## Technologies Used

- **Language**: C# 10
- **Framework**: .NET 8.0
- **Development**: Visual Studio 2022
- **Testing**: Gradescope automated testing platform

## Command Reference

| Command | Parameters | Description |
|---------|-----------|-------------|
| `add guard` | `<x> <y>` | Register a guard obstacle |
| `add fence` | `<x> <y> <orientation> <length>` | Register a fence (east/north) |
| `add sensor` | `<x> <y> <radius>` | Register a sensor with detection radius |
| `add camera` | `<x> <y> <direction>` | Register a camera (north/south/east/west) |
| `check` | `<x> <y>` | Check safe directions from location |
| `map` | `<x> <y> <width> <height>` | Display obstacle map |
| `path` | `<agent_x> <agent_y> <goal_x> <goal_y>` | Calculate safe path |
| `help` | - | Display command list |
| `exit` | - | Close application |

## Code Highlights

### Efficient Neighbor Discovery
```csharp
static List<Node> GetWalkableAdjacentSquares(int x, int y)
{
    var proposedLocations = new List<Node>()
    {
        new Node(x, y - 1), // South
        new Node(x, y + 1), // North
        new Node(x - 1, y), // West
        new Node(x + 1, y), // East
    };
    
    return proposedLocations.Where(node => IsCoordSafe(node.X, node.Y)).ToList();
}
```

### Smart Node Construction
```csharp
public Node(int x, int y)
{
    X = x;
    Y = y;
    // Automatically calculate distances using inherited Path properties
    G = Math.Abs(Start_X - x) + Math.Abs(Start_Y - y);
    H = Math.Abs(Goal_X - x) + Math.Abs(Goal_Y - y);
}
```

### Sensor Range Detection
```csharp
public bool IsOccupied(int x, int y)
{
    float fx = x - this.X;
    float fy = y - this.Y;
    double DistanceFromSensor = Math.Sqrt(fx * fx + fy * fy);
    
    return DistanceFromSensor <= this.Range;
}
```

## Skills Demonstrated

- Algorithm implementation (A*, pathfinding)
- Object-oriented design patterns
- Data structures (lists, queues, nodes)
- Coordinate geometry and spatial reasoning
- Input validation and error handling
- Code documentation and maintainability
- Problem decomposition and abstraction
- Test case analysis and debugging

## License

Academic project - Queensland University of Technology (QUT)

---
