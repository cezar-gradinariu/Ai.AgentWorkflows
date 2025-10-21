# Coder Agent Rules

## Project Structure Rules

### Folder Organization
- ✅ All production code must be placed in a `src` folder
- ✅ All test code must be placed in a `tests` folder
- ✅ Solution files (.sln) should be at the root level
- ✅ Each project should have its own subfolder within `src` or `tests`
- ✅ Follow clean architecture or layered architecture patterns

### Project Setup (Must Complete BEFORE Writing Code)
- ✅ Create the solution file (.sln) first
- ✅ Create all required project files (.csproj) with proper structure
- ✅ Add all projects to the solution file
- ✅ Set up project references between dependent projects
- ✅ Install all required NuGet packages
- ✅ Verify the solution builds successfully (even if empty)
- ❌ Do NOT write any implementation code until the solution structure is complete and verified

**Project Setup Order:**
1. Create solution file at root
2. Create `src` and `tests` folders
3. Create all project files (.csproj) in their respective folders
4. Add projects to solution
5. Configure project references (e.g., API → Application → Domain)
6. Install NuGet dependencies
7. Build solution to verify structure
8. **ONLY THEN** start writing implementation code

### Code File Management
- ✅ Remove all template/scaffold files (e.g., Class1.cs, UnitTest1.cs) after project creation
- ✅ Ensure every .cs file contains actual implementation code
- ✅ Delete or replace empty files and empty classes
- ✅ Every class must have at least one member (property, method, or field)
- ✅ Each .cs file must contain ONLY the code intended for that specific file
- ✅ One class per file (except for nested classes)
- ✅ File name must match the primary class/interface name (e.g., WeatherService.cs contains WeatherService class)
- ✅ Verify that namespaces match the project structure
- ✅ Remove all unused `using` statements from every file
- ❌ Do NOT leave empty or placeholder files in the codebase
- ❌ Do NOT leave stub classes without implementation
- ❌ Do NOT put code for multiple unrelated classes in a single file
- ❌ Do NOT accidentally place code meant for File A into File B
- ❌ Do NOT leave unused `using` directives in any file

**Clean Code Practices:**
1. After writing code, remove all unused `using` statements
2. Only include `using` directives that are actually needed by the code
3. Organize `using` statements (System namespaces first, then external, then project namespaces)
4. Use IDE features or `dotnet format` to clean up unused imports

**Example - CORRECT:**
```csharp
using System;
using WeatherApi.Domain.Entities;

namespace WeatherApi.Application
{
    public class WeatherService
    {
        // Only the using statements that are actually used
    }
}
```

**Example - INCORRECT:**
```csharp
using System;
using System.Collections.Generic;  // ❌ Not used
using System.Linq;                  // ❌ Not used
using Microsoft.Extensions.Logging; // ❌ Not used
using WeatherApi.Domain.Entities;

namespace WeatherApi.Application
{
    public class WeatherService
    {
        // Multiple unused using statements
    }
}
```

**Example Structure:**
```
YourProject/
├── YourProject.sln
├── src/
│   ├── YourProject.Domain/
│   ├── YourProject.Application/
│   ├── YourProject.Infrastructure/
│   └── YourProject.Api/
└── tests/
    ├── YourProject.UnitTests/
    └── YourProject.IntegrationTests/
```

## Code Quality Rules

### Must Follow
- ��� Follow SOLID principles
- ✅ Write self-documenting code with clear naming
- ✅ Include XML documentation for public APIs
- ✅ Ensure all code is testable
- ✅ Handle errors and exceptions appropriately
- ✅ Follow the project's coding style guide
- ✅ Use dependency injection for loose coupling
- ✅ Keep methods small and focused (single responsibility)

### Must Not Do
- ❌ Commit code without tests
- ❌ Hard-code configuration values
- ❌ Ignore compiler warnings
- ❌ Leave commented-out code in commits
- ❌ Use magic numbers or strings
- ❌ Create God objects or classes
- ❌ Bypass code review process

## Testing Rules
- Minimum 80% code coverage for new code
- All public methods must have unit tests
- Integration tests for complex workflows
- Test edge cases and error conditions
- Mock external dependencies

### Test Project Dependencies
- ✅ Use **Moq** to mock interfaces (IMemoryCache, ILogger, etc.)
- ✅ Reference production projects to access interfaces and DTOs
- ✅ Only add test-specific packages (xUnit, Moq, FluentAssertions, TestContainers, etc.)
- ❌ Do NOT add NuGet packages that are already available through project references
- ❌ Do NOT create real instances of infrastructure components (MemoryCache, DbContext, etc.) - mock them instead

**Example - CORRECT:**
```csharp
// Test project references Application project
// IMemoryCache is available through the reference
var cacheMock = new Mock<IMemoryCache>();
var handler = new MyHandler(cacheMock.Object);
```

**Example - INCORRECT:**
```csharp
// Adding Microsoft.Extensions.Caching.Memory to test project
// Creating real MemoryCache instead of mocking
var cache = new MemoryCache(new MemoryCacheOptions()); // ❌ WRONG!
```

## Performance Rules
- Consider Big O complexity for algorithms
- Avoid N+1 query problems
- Use async/await for I/O operations
- Implement caching where appropriate
- Profile code for performance bottlenecks

## Security Rules
- Validate all input data
- Use parameterized queries to prevent SQL injection
- Never log sensitive data
- Follow secure coding practices
- Implement proper authentication and authorization

## Documentation Rules
- Document all public APIs
- Include usage examples for complex code
- Document assumptions and design decisions
- Keep documentation up-to-date with code changes
- Use clear and concise language

## Constraints
- Maximum method length: 50 lines
- Maximum class length: 300 lines
- Maximum cyclomatic complexity: 10
- Maximum task duration: 4 hours
- Maximum concurrent tasks: 3

## Dependencies
- Must wait for architecture approval before major changes
- Must have requirements defined before implementation
- Must coordinate with QA for testability requirements
