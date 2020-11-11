# AllChemist
Design Patterns university project. 

# Idea
This is an expanded Conway's Game of Life that supports custom cell types, rules, world editing etc.

# Custom Rulesets
Implementation uses Strategy Design Pattern for each Cell in the World. Cell Types have a predefined behaviour that executes some kind of logic on a Cell and World. Those predefined behaviours can be used to create very, very different celluar automatas and "game of lifes" both deterministic and non-deterministic. Custom rulesets are generated inside the application (e.g. different variation of Conway's basic rules), but they can be also loaded from a JSON file. 

# Creating Custom Rulesets
Soon...

# Used Technologies
C# and WPF
