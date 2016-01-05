# TODO

## General
* [x] Map command line arguments to a method to execute.
* [x] Handle parsing errors in a friendly manner.
* [x] Support boolean actions and report proper exit code.
* [x] Seek consistent interfaces between commands, actions, and parameterValues, especially with respect to names and aliases.
* [x] Paginate should honor line breaks.

## Command Line Options

* [x] Support boolean switches
  * [x] Support --switch and --no-switch argument styles for boolean arguments
* [x] Support implicit ordered parameters
* [x] Support the concept of a default action
* [x] Support sub-comamnds
* [x] Support aliases on controllers
* [x] Support aliases on methods
* [x] Support aliases on parameters
* [x] Support argument parsing for primitive types
  * [x] bool
  * [x] int
  * [x] long
  * [x] DateTime
  * [x] decimal
  * [x] double
  * [x] Enum
* [x] Support argument parsing for nullable primitive types
  * [x] bool
  * [x] int
  * [x] long
  * [x] DateTime
  * [x] decimal
  * [x] double
  * [x] Enum
* [x] Support custom argument parsers
    * [x] Support custom argument parsers with aliases
* [x] Support array arguments
  * [x] string
  * [x] bool
  * [x] bool?
  * [x] int
  * [x] int?
  * [x] long
  * [x] long?
  * [x] DateTime
  * [x] DateTime?
  * [x] decimal
  * [x] decimal?
  * [x] double
  * [x] double?
  * [x] Enum
  * [x] Enum?

## Extensibility

* [x] Support an injectable Logger
* [x] Support customizable conventions
  * [x] provide /argment:value style convention
    * [x]  negative arguments should be in the form /no-argument
  * [x] provide /argument=value style convention
    * [x]  negative arguments should be in the form /no-argument

## Help

* [x] Generate help using reflection
  * [x] Aliases should be emitted by the help
  * [x] Help generation should be customizable
  * [x] Actions
      * [x] default actions should be prefixed with a *
      * [x] get help for action by alias.
  * [x] Parameters
      * [x] Help should display available enum options
      * [x] Display default values using reflection
      * [x] Display negative argument style for negative booleans.
  * [x] SubCommands

## Testability
* [x] Provide testability of command structure separate from execution of command structure.
* [x] Validation
  * [x] Validate no more than one default action per command
  * [x] Detect conflicts between subcommands and action names
  * [x] Validate no more than one of the same alias on a method
  * [ ] Validate no duplicate identifiers for parameter methods.

## Deployment
* [x] Create a nuget package
  * [ ] create xml comments for public methods
  * [ ] include xml comments in nuget package
  * [ ] emit xml comments and wiki pages.
* [x] Setup AppVeyor Build

## Documentation
* [x] Create a demo project to show how to use the library.
