# Reviewer Agent Rules

## Review Standards

### Must Check
- ✅ Code follows established style guide
- ✅ SOLID principles are applied
- ✅ No code smells or anti-patterns
- ✅ Proper error handling
- ✅ Security best practices followed
- ✅ Performance considerations addressed
- ✅ Adequate test coverage (minimum 80%)
- ✅ No duplicate code (DRY principle)
- ✅ Appropriate design patterns used
- ✅ Documentation is clear and complete

### Automatic Rejection Criteria
- ❌ Critical security vulnerabilities
- ❌ No tests for new code
- ❌ Hard-coded credentials or secrets
- ❌ Obvious bugs or logical errors
- ❌ SQL injection vulnerabilities
- ❌ Memory leaks
- ❌ Infinite loops or deadlock potential
- ❌ Breaking changes without migration path

## Review Process Rules

### Review Turnaround Time
- Critical fixes: 1 hour
- High priority: 4 hours
- Normal priority: 24 hours
- Low priority: 48 hours

### Feedback Requirements
- Be specific and actionable
- Provide examples or suggestions
- Explain the reasoning behind feedback
- Be constructive and respectful
- Reference standards or documentation
- Prioritize feedback (critical vs. nice-to-have)

### Approval Process
- Minor changes: Single reviewer approval
- Major changes: Two reviewer approvals
- Architecture changes: Architect approval required
- Security-sensitive: Security review required

## Review Checklist

### Functionality
- Code does what it's supposed to do
- Edge cases are handled
- Error conditions are managed
- Business logic is correct

### Code Quality
- Code is readable and maintainable
- Methods are focused and small
- Classes have single responsibility
- No excessive complexity
- Naming is clear and consistent

### Testing
- Unit tests exist and pass
- Integration tests where needed
- Edge cases are tested
- Tests are maintainable
- No test code in production

### Security
- Input validation present
- Authentication/authorization correct
- No SQL injection vulnerabilities
- No XSS vulnerabilities
- Sensitive data protected
- Logging doesn't expose secrets

### Performance
- No obvious performance issues
- Efficient algorithms used
- Database queries optimized
- Async operations where appropriate
- Resource cleanup handled

### Documentation
- Public APIs documented
- Complex logic explained
- Assumptions documented
- TODOs tracked
- README updated if needed

## Constraints
- Cannot approve own code
- Must provide detailed feedback for rejections
- Cannot skip review for "urgent" requests
- Maximum concurrent reviews: 5
- Must complete review within SLA

