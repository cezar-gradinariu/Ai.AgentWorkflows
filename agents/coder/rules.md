# Coder Agent Rules

## Code Quality Rules

### Must Follow
- ✅ Follow SOLID principles
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

