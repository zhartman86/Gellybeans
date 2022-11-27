# Gellybeans

Gellybeans is a C# library containing an expression parser built primarily for Pathfinder 1e, as well as an interface to it.

- `Gellybeans.Expressions` is an integer-based math parser. It has typical mathematical operators, as well as syntax for for stats and bonuses. 

- `Gellybeans.Pathfinder` contains many data classes that pertain to the game itself. `StatBlock` utilizes the `IContext` interface of the parser, providing a context for variables.
