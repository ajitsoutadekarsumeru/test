# About the refactoring of FileUtilities

## Goals of refactoring
1) Full unit testability.
2) Configurations through standard DotNet
3) Eliminate wrong use of static methods
4) Standardisation of DI
5) Elimination of unnecessary new'ing of objects

## Some details about the refactoring
1) FileUtilities has been split into FileValidationUtilities, FileTransferUtilities and CsvExcelUtilities. Builds fine. Works at unit level fine. Must be automation tested once, since many file references have been touched. 
2) 34 unit tests have been created using xUnit, and all are passing. 
3) All static methods have been made instance methods. All references have been corrected. 
4) DI implemented through Flex way in a standardised manner - OtherApplicationServicesConfig.cs
5) appsettings.json used for Settings. AppConfigManager use entirely avoided. Sets the foundation for the way to define environment specific configurations, that will get automatically picked up depending on where code is deployed. 
6) All Loggers have been made instance based. 
7) MySqlPrimaryAllocation had to be touched because it was new'ing CsvExcelUtilities.