# AllChemist
Design Patterns university project. 

# Idea
This is an expanded Conway's Game of Life that supports custom cell types, rules, world editing etc.

# Custom Rulesets
Implementation uses Strategy Design Pattern for each Cell in the World. Cell Types have a predefined behaviour that executes some kind of logic on a Cell and World. Those predefined behaviours can be used to create very, very different celluar automatas and "game of lifes" both deterministic and non-deterministic. Custom rulesets are generated inside the application (e.g. different variation of Conway's basic rules), but they can be also loaded from a JSON file. 

# Creating Custom Rulesets
In `Data\Rulesets` you will find some examples of JSON formatted Rulesets. They all are classes that implement IRule in `Cell\Rules`. Typically, a Ruleset file has some metadata about creator and name of this ruleset and bank of cell types. The cell with id 0 is a default cell of this Ruleset, meaning the world will fill itself with it each step. 

Normally, rules are executed one by one, from the first to the last.

# Used Technologies
C# and WPF
