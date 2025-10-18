# Coder Agent Examples

## Example 1: Implement Feature Task

### Input
```
Task Type: Implement Feature
Feature: User Authentication
Description: Implement user login and registration functionality

Requirements:
- Users can register with email and password
- Users can log in with credentials
- Passwords must be hashed and secured
- JWT tokens for session management
- Email validation required

Acceptance Criteria:
- Given valid credentials, user can log in successfully
- Given invalid credentials, user sees appropriate error
- Passwords are never stored in plain text
- JWT token expires after 24 hours
```

### Output
```
Implementation Complete:

Files Created:
- Services/AuthenticationService.cs
- Controllers/AuthController.cs
- Models/LoginRequest.cs
- Models/RegistrationRequest.cs
- Tests/AuthenticationServiceTests.cs

Key Features Implemented:
- Password hashing using BCrypt
- JWT token generation and validation
- Email format validation
- Error handling for invalid credentials

Test Coverage: 92%
All acceptance criteria met ✓
```

---

## Example 2: Fix Bug Task

### Input
```
Task Type: Fix Bug
Bug ID: BUG-123
Title: Memory leak in data processing service

Description:
The data processing service consumes increasing amounts of memory over time,
eventually causing the application to crash.

Steps to Reproduce:
1. Start the application
2. Process 1000 records
3. Monitor memory usage
4. Memory continues to grow without being released

Expected: Memory should be released after processing
Actual: Memory grows indefinitely

Priority: High
```

### Output
```
Bug Fix Complete:

Root Cause:
Event handlers were not being properly unsubscribed, causing references
to accumulate and preventing garbage collection.

Changes Made:
- Modified: Services/DataProcessingService.cs
  - Added proper IDisposable implementation
  - Unsubscribe from events in Dispose method
  - Added using statements for disposable resources

- Added: Tests/DataProcessingServiceMemoryTests.cs
  - Memory leak regression test
  - Proper disposal verification test

Verification:
- Processed 10,000 records
- Memory usage remains stable
- No memory leaks detected in profiler

Test Coverage: 95%
Bug verified fixed ✓
```

---

## Example 3: Refactor Code Task

### Input
```
Task Type: Refactor Code
Target: UserService.cs
Reason: Method too long and complex (250 lines, cyclomatic complexity 18)

Goals:
- Reduce method length
- Improve readability
- Maintain existing functionality
- Improve testability
```

### Output
```
Refactoring Complete:

Changes Summary:
- Split CreateUser method into 5 smaller methods:
  - ValidateUserData()
  - CheckUserExists()
  - HashPassword()
  - SaveUser()
  - SendWelcomeEmail()

Improvements:
- Average method length: 15 lines (was 250)
- Cyclomatic complexity: 4 (was 18)
- Improved testability (can test each step independently)
- Added XML documentation to each method

Files Modified:
- Services/UserService.cs (refactored)
- Tests/UserServiceTests.cs (updated and expanded)

Test Results:
- All existing tests pass ✓
- Added 8 new unit tests
- Coverage increased from 72% to 94%

No functionality changes - behavior preserved ✓
```

---

## Example 4: Write Unit Tests Task

### Input
```
Task Type: Write Unit Tests
Target: OrderProcessor.cs
Current Coverage: 45%
Required Coverage: 85%

Focus Areas:
- Edge cases
- Error handling
- Boundary conditions
- Integration points
```

### Output
```
Unit Tests Created:

New Test File: Tests/OrderProcessorTests.cs

Test Cases Added (23 total):
- Happy path scenarios (5 tests)
- Edge cases (8 tests)
  - Empty order
  - Null customer
  - Invalid product IDs
  - Negative quantities
  - Duplicate items
- Error handling (6 tests)
  - Database errors
  - Network timeouts
  - Invalid data
- Boundary conditions (4 tests)
  - Maximum order size
  - Minimum values
  - Currency precision

Coverage Results:
- Line coverage: 89% (was 45%)
- Branch coverage: 87% (was 38%)
- Method coverage: 100% (was 62%)

All tests passing ✓
Requirements exceeded ✓
```
# AI Agent Workflows - Project Structure

A comprehensive framework for orchestrating multiple AI agents to collaborate on software development projects.

## 📁 Project Structure Overview

```
Ai.AgentWorkflows/
├── agents/                      # Agent definitions and rules
│   ├── coder/
│   ├── reviewer/
│   ├── product-owner/
│   ├── architect/
│   ├── qa/
│   ├── business-analyst/
│   └── technical-writer/
├── workflows/                   # Workflow definitions
│   ├── feature-development/
│   ├── bug-fix/
│   ├── architecture-review/
│   └── documentation/
├── features/                    # Feature requests and implementations
│   ├── backlog/
│   ├── in-progress/
│   └── completed/
├── templates/                   # Templates for tasks and documents
│   ├── agent-tasks/
│   ├── workflows/
│   └── features/
├── rules/                       # Global rules and policies
└── docs/                        # Documentation
    ├── getting-started/
    ├── agent-guides/
    └── workflow-guides/
```

## 🎯 Purpose

This structure allows you to:
- Define multiple specialized agents with specific roles
- Create reusable workflows that orchestrate agent collaboration
- Manage features from conception to completion
- Define rules and constraints for agent behavior
- Track work through different stages

## 🚀 Quick Start

1. Review agent definitions in `agents/` to understand each agent's role
2. Explore workflows in `workflows/` to see how agents collaborate
3. Add new features to `features/backlog/`
4. Use templates in `templates/` to create consistent documentation

## 📖 Key Concepts

- **Agents**: Specialized AI workers with specific roles and responsibilities
- **Workflows**: Orchestrated sequences of agent tasks
- **Features**: Work items that flow through workflows
- **Rules**: Constraints and guidelines for agent behavior
- **Templates**: Reusable formats for common tasks

## 🔗 Navigation

- [Agent Definitions](./agents/README.md)
- [Workflow Definitions](./workflows/README.md)
- [Features Guide](./features/README.md)
- [Rules & Policies](./rules/README.md)
- [Documentation](./docs/README.md)

