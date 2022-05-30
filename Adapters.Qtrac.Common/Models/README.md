# How to re-generate the models in this folder

## Overview
The model classes in this folder are auto-generated from QTrac database, and then renamed to the right shape.
To re-generate those, you'll need to go through the same excercise the original developers did:

- Scaffold the DbContext from the QTrac database (QTrac_Test was used originally)
- Remove the references to the Indexes from the DbContext
- Rename the Class names and Field names to match the naming convention of the project.

## Scaffolding process

This article [Getting Started with EF Core on ASP.NET Core with an Existing Database](https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db)
explains how to scaffold the EF DB Context from within Visual Studio's package manager console.

This article [Generating a model from an existing database](https://www.learnentityframeworkcore.com/walkthroughs/existing-database)
explains how to do the same with the `dotnet ef` tool in the command line.

If you need to re-generate only few models, you can scaffold the database context in a separate project/folder, do all the renaming there, 
and then copy the required classes into the QtracDbContext and into this folder.

Here's the command we've used to generate models in a separate folder:
`dotnet ef dbcontext scaffold "Server=<QTrac Server>;Database=QTrac_Test;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Model -c "QtracDbContext"

There's an optional `-d` flag tells the scaffolder to apply data annotation attributes (like table names and column names) to the models.
It's actually not a recommended approach currently. The recommended approach is to keep all this information inside the DbContext's 'OnModelCreating' method.
This way, I guess, it's easier to rename the fields whenever it needed, or it enables reuse of the models for different scenarios outside of the database. 
It makes, however, the code less readable, since I now need to go to a separate class to see what is the table name where the information is populated from.

Right now, we're using the recommended approach of not using the data annotation attributes, and instead configuring all the relations inside the DbContext code.