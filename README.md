# Gellybeans

Gellybeans is a C# library, containing two parts:

- `Gellybeans.Expressions` contains an integer-based math parser built primarily for Pathfinder 1e. It has typical mathematical operators, as well as syntax for modifying stats and expressions through a given context.

- `Gellybeans.Pathfinder` contains many data classes that pertain to the game itself. `StatBlock` utilizes the `IContext` interface of the parser, providing a context for variables.
