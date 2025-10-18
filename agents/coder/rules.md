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

### Coverage Measurement
**Tools by Technology:**
- **.NET/C#**: Use `dotnet-coverage` or `coverlet`
  ```bash
  # Install coverlet
  dotnet add package coverlet.collector
  
  # Run tests with coverage
  dotnet test --collect:"XPlat Code Coverage"
  
  # Generate report
  dotnet tool install -g dotnet-reportgenerator-globaltool
  reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coverage-report"
  ```

- **JavaScript/TypeScript**: Use `jest` with coverage or `nyc`
  ```bash
  # Jest
  npm test -- --coverage --coverageThreshold='{"global":{"lines":80,"branches":80,"functions":80,"statements":80}}'
  
  # NYC with other test runners
  nyc --reporter=html --reporter=text --check-coverage --lines 80 npm test
  ```

- **Python**: Use `pytest-cov` or `coverage.py`
  ```bash
  # Install
  pip install pytest-cov
  
  # Run with coverage
  pytest --cov=src --cov-report=html --cov-report=term --cov-fail-under=80
  ```

- **Java**: Use `JaCoCo` or `Cobertura`
  ```xml
  <!-- Maven plugin -->
  <plugin>
    <groupId>org.jacoco</groupId>
    <artifactId>jacoco-maven-plugin</artifactId>
    <configuration>
      <rules>
        <rule>
          <element>BUNDLE</element>
          <limits>
            <limit>
              <counter>LINE</counter>
              <value>COVEREDRATIO</value>
              <minimum>0.80</minimum>
            </limit>
          </limits>
        </rule>
      </rules>
    </configuration>
  </plugin>
  ```

**Coverage Verification Process:**
1. Run unit tests locally with coverage before committing
2. Review coverage report (HTML output) to identify untested code
3. Add tests for uncovered critical paths
4. Ensure CI/CD pipeline includes coverage checks
5. Block PRs that fall below 80% coverage threshold

**What Counts Toward Coverage:**
- ✅ New code written for the feature
- ✅ Modified existing code
- ❌ Auto-generated code (migrations, DTOs)
- ❌ Configuration files
- ❌ Third-party libraries

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
