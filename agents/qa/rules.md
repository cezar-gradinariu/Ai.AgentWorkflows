# QA Agent Rules

## Testing Standards

### Must Test
- ✅ All acceptance criteria
- ✅ Happy path scenarios
- ✅ Edge cases and boundary conditions
- ✅ Error handling and validation
- ✅ Security vulnerabilities
- ✅ Performance under load
- ✅ Cross-browser compatibility (web)
- ✅ Mobile responsiveness
- ✅ Accessibility standards
- ✅ Integration points

### Test Coverage Requirements
- Unit tests: 80% minimum code coverage
- Integration tests: All critical workflows
- E2E tests: All user journeys
- API tests: All endpoints
- Regression tests: All previous bugs

### Cannot Release With
- ❌ Critical (P0) bugs
- ❌ Security vulnerabilities
- ❌ Data loss scenarios
- ❌ Performance degradation > 20%
- ❌ Broken core functionality
- ❌ Failing acceptance criteria

## Test Planning Rules

### Test Plan Must Include
- Scope and objectives
- Test strategy (types of testing)
- Entry and exit criteria
- Test environment requirements
- Test data requirements
- Schedule and milestones
- Resources needed
- Risk assessment

### Test Case Requirements
- Clear preconditions
- Step-by-step instructions
- Expected results
- Actual results (during execution)
- Pass/fail criteria
- Priority level
- Test data specified

## Bug Reporting Rules

### Bug Report Must Include
- ✅ Clear, descriptive title
- ✅ Steps to reproduce
- ✅ Expected behavior
- ✅ Actual behavior
- ✅ Severity and priority
- ✅ Environment information
- ✅ Screenshots/videos if applicable
- ✅ Impact assessment

### Severity Levels
- **Critical (P0)**: System crash, data loss, security breach
- **High (P1)**: Major functionality broken, no workaround
- **Medium (P2)**: Important feature impaired, workaround exists
- **Low (P3)**: Minor issue, cosmetic problem

### Priority Guidelines
- P0: Fix immediately, block release
- P1: Fix before release
- P2: Fix in next release
- P3: Fix when time permits

## Testing Types

### Functional Testing
- Feature functionality
- Business logic
- User workflows
- Data validation
- Error handling

### Non-Functional Testing
- Performance testing
- Load testing
- Security testing
- Usability testing
- Compatibility testing
- Accessibility testing

### Regression Testing
- Test after every change
- Automate where possible
- Cover all critical paths
- Include previous bug fixes

### Exploratory Testing
- Ad-hoc testing
- Unscripted exploration
- Edge case discovery
- User perspective testing

## Test Automation Rules

### Should Automate
- Regression tests
- Repetitive test cases
- Data-driven tests
- API tests
- Performance tests
- Smoke tests

### Should Not Automate
- One-time tests
- Exploratory testing
- Usability testing
- Tests that change frequently
- Visual design validation

### Automation Standards
- Use standard frameworks
- Maintain test code quality
- Document test purpose
- Keep tests independent
- Fast execution (< 30 min full suite)

## Test Environment Rules

### Environment Requirements
- Match production as closely as possible
- Isolated from development
- Test data properly managed
- Reset between test runs
- Monitoring enabled

### Test Data Management
- Use realistic test data
- Protect sensitive data
- Create reusable datasets
- Document data setup
- Clean up after tests

## Quality Gates

### Cannot Proceed Without
- All test plans reviewed and approved
- Test environment ready
- Test data prepared
- Automation framework set up

### Release Criteria
- ✅ All test cases executed
- ✅ Pass rate > 98%
- ✅ No critical bugs
- ✅ No high-priority bugs
- ✅ Regression tests passed
- ✅ Performance tests passed
- ✅ Security scan passed

## Constraints
- Must test all acceptance criteria
- Cannot skip regression testing
- Must report all bugs found
- Cannot approve release with critical bugs
- Test results must be documented
- Maximum concurrent test suites: 3
- Bug report SLA: Within 4 hours of discovery

