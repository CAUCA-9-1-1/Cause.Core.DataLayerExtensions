# Cause.Core.DataLayerExtensions
[![NuGet version](https://badge.fury.io/nu/Cause.Core.DataLayerExtensions.svg)](https://badge.fury.io/nu/Cause.Core.DataLayerExtensions)

Cause.Core.DataLayerExtensions are a set of extensions for EntityFrameworkCore :
* Mapping classes.
* Mapping auto detection.
* Mapping naming convention.


# Usage

### Mapping classes
If you want to create a mapping class for the foo model
```c#
public class SomeExample 
{
    public Guid Id {get; set;}
    public ICollection<SomeOtherClass> Classes {get;set;}
}
```

You would simply create a mapping class that inherits from `EntityMappingConfiguration<T>` and override the Map function.  From there, it's all normal fluent mapping code.
```c#
public class SomeExampleMapping: EntityMappingConfiguration<SomeExample>
{
    public override Map(EntityTypeBuilder<SomeXample> entity)
    {
        entity.HasKey(m => m.Id);
        entity.HasMany(m => m.Classes)
            .WithOne(m => m.Parent)
            .HasForeignKey(m => m.ParentId);
    }
}
```

Then, in you DbContext class, you can add the mapping from the OnModelCreating function like so:
```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    new CallSubTypeMapping().Map(modelBuilder);
}
```

### Mapping Auto Detection
The project also contains a mapping auto detection feature so you can easily add all of your mapping in one line.  It can be used like this from the OnModelCreating function from you DbContext:

```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    this.UseAutoDetectedMappings(modelBuilder);
}
```

> Note that your mapping classes must be in your DbContext's project to be detected.

### Mapping's naming features
All of these features must be used from the DbContext's OnModelCreating function.

```c#
public class SomeClass
{
    [PrimaryKey]
    public Guid Id {get;set;}
    public string CurrentName {get;set;}
}
```

##### Primary key naming
```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.AddTableNameToPrimaryKey();
}
```

Will add the class name to the primary key. 
  - `Id` would be mapped as `IdSomeClass`

##### Table's prefix
```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.UseTablePrefix("Tbl")
}
```

Add "Tbl" in front of the class name.  
  - `SomeClass` would then be mapped as `TblSomeClass`.

##### Snakecase mapping
You can also easily auto map everything to snake case.
```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.UseAutoSnakeCaseMapping();
}
```
Automatically transform all class and property naming mapping to snake case.
  - `SomeClass` would be mapped as `some_class`.
  - `CurrentName` would be `current_name`.

##### One more example
You can use any or all of the naming feature together like so:
```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.AddTableNameToPrimaryKey();
    modelBuilder.UseAutoSnakeCaseMapping();
    modelBuilder.UseTablePrefix("tbl_");
    this.UseAutoDetectedMappings(modelBuilder);
}
```

That would create a table named like this:
  - tbl_some_class

With these fields :
  - id_some_class
  - current_name
