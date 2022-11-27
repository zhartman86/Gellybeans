# Gellybeans

Gellybeans is a C# library, containing two parts:

- `Gellybeans.Expressions` contains an integer-based math parser built primarily for Pathfinder 1e. It has typical mathematical operators, as well as syntax for modifying stats and their bonuses.

- `Gellybeans.Pathfinder` contains many data classes, including a `StatBlock` class that interfaces with the expression engine's `IContext`
