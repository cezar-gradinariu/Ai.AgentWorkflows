# QA Agent Examples

## Example 1: Create Test Plan

### Input
```
Task: Create Test Plan for User Authentication Feature
Feature: User login, registration, password reset
Timeline: 2 weeks
Environment: Staging
Compliance: GDPR, security standards
```

### Output
```
Test Plan: User Authentication Feature
Version: 1.0
Date: October 17, 2025
QA Lead: QA Agent

=== 1. INTRODUCTION ===

Purpose:
Validate that the user authentication system works correctly, securely,
and meets all functional and non-functional requirements.

Scope:
- User registration
- User login
- Password reset
- Session management
- Security compliance

Out of Scope:
- Social login (future release)
- Two-factor authentication (separate test plan)

=== 2. TEST STRATEGY ===

Testing Types:
✓ Functional Testing - Core authentication flows
✓ Security Testing - Vulnerability assessment
✓ Performance Testing - Login/registration under load
✓ Usability Testing - User experience validation
✓ Compatibility Testing - Cross-browser, mobile
✓ Regression Testing - Existing functionality

Testing Levels:
- Unit Tests: Developer responsibility (80% coverage required)
- Integration Tests: QA + Developer collaboration
- System Tests: QA responsibility
- Acceptance Tests: Product Owner + QA

=== 3. TEST ENVIRONMENT ===

Environment: Staging
URL: https://staging.example.com
Database: Staging PostgreSQL
Email Service: MailHog (test email server)

Prerequisites:
- Staging environment deployed
- Test user accounts created
- Email service configured
- Browser/device farm access

=== 4. TEST DATA ===

Required Test Data:
- Valid user accounts (10 users)
- Invalid email formats (5 variations)
- Weak/strong passwords (10 variations)
- Expired password reset tokens
- Active sessions

Data Reset:
- Database reset before each test run
- Session cleanup between tests

=== 5. TEST CASES ===

5.1 User Registration (REG-001 to REG-015)
- Happy path registration
- Duplicate email handling
- Password validation rules
- Email verification flow
- Field validation (15 test cases)

5.2 User Login (LOGIN-001 to LOGIN-020)
- Valid credentials login
- Invalid credentials handling
- Account lockout after failed attempts
- Session creation
- Remember me functionality (20 test cases)

5.3 Password Reset (PWD-001 to PWD-012)
- Request password reset
- Email delivery
- Token validation
- Password update
- Token expiration (12 test cases)

5.4 Security Tests (SEC-001 to SEC-010)
- SQL injection attempts
- XSS attempts
- CSRF protection
- Password hashing verification
- Session hijacking prevention (10 test cases)

5.5 Performance Tests (PERF-001 to PERF-005)
- Concurrent registrations
- Login response time
- Session management load
- Database connection pooling (5 test cases)

Total Test Cases: 62

=== 6. ENTRY CRITERIA ===

✓ Feature development completed
✓ Code review passed
✓ Unit tests passing (80%+ coverage)
✓ Staging environment stable
✓ Test data prepared
✓ Test cases reviewed and approved

=== 7. EXIT CRITERIA ===

✓ All test cases executed
✓ Pass rate ≥ 98%
✓ No critical (P0) bugs
✓ No high (P1) bugs
✓ All security tests passed
✓ Performance requirements met
✓ Acceptance criteria validated
✓ Test report completed

=== 8. TEST SCHEDULE ===

Week 1:
- Days 1-2: Test case preparation
- Days 3-4: Functional testing
- Day 5: Security testing

Week 2:
- Days 1-2: Performance testing
- Day 3: Regression testing
- Day 4: Bug fix verification
- Day 5: Final validation & sign-off

=== 9. RESOURCES ===

QA Team:
- 1 QA Engineer (functional testing)
- 1 Test Automation Engineer
- 0.5 Security Tester

Tools:
- Test Management: TestRail
- Bug Tracking: Jira
- Automation: Selenium, Postman
- Performance: JMeter
- Security: OWASP ZAP

=== 10. RISKS & MITIGATION ===

Risk 1: Email service issues in staging
- Impact: Cannot test email flows
- Mitigation: Use MailHog, fallback to manual verification

Risk 2: Environment instability
- Impact: Test execution delays
- Mitigation: Daily environment health checks

Risk 3: Late bug fixes
- Impact: Insufficient time for verification
- Mitigation: Daily testing cycles, early bug reporting

=== 11. DEFECT MANAGEMENT ===

Severity Definitions:
- P0 (Critical): Cannot login, system crash, data loss
- P1 (High): Major feature broken, security issue
- P2 (Medium): Feature partially working
- P3 (Low): Minor UI issues, typos

Bug Reporting:
- Use standard bug template
- Report within 2 hours of discovery
- Include reproduction steps and screenshots
- Tag with priority and component

=== 12. ACCEPTANCE CRITERIA ===

From Product Owner:
✓ Users can register with email/password
✓ Users can login with valid credentials
✓ Invalid credentials show appropriate errors
✓ Password reset works via email
✓ Sessions expire after 24 hours
✓ Passwords are hashed (not plain text)
✓ System handles 1000 concurrent users

=== 13. DELIVERABLES ===

- Test plan (this document)
- Test cases (62 cases)
- Test execution report
- Bug reports
- Test metrics dashboard
- Sign-off document

=== 14. APPROVALS ===

Prepared by: QA Agent
Reviewed by: QA Lead
Approved by: Product Owner
Date: October 17, 2025

Status: Approved ✓
```

---

## Example 2: Bug Report

### Input
```
Task: Report Bug Found During Testing
Feature: User Login
Issue: Login fails with correct credentials after 5 attempts
Environment: Staging
```

### Output
```
BUG REPORT

Bug ID: BUG-247
Title: Login Lockout Occurs After 5 Attempts Even With Correct Credentials
Reported By: QA Agent
Date: October 17, 2025 14:32 UTC
Environment: Staging

=== SEVERITY & PRIORITY ===

Severity: High (P1)
Priority: High
Reason: Blocks legitimate users from accessing accounts

=== DESCRIPTION ===

The account lockout mechanism incorrectly locks users out after 5 login
attempts, even when they use correct credentials. This prevents valid
users from accessing their accounts.

=== STEPS TO REPRODUCE ===

1. Navigate to https://staging.example.com/login
2. Enter valid credentials (test@example.com / ValidPass123!)
3. Click "Login" button
4. Repeat steps 2-3 five times
5. Observe: "Account locked" error message appears
6. Wait 15 minutes (lockout duration)
7. Try to login with same valid credentials
8. Observe: Still unable to login

Expected Behavior:
- Valid credentials should log user in successfully
- Account lockout should only occur after FAILED login attempts
- Account should unlock after lockout period

Actual Behavior:
- Account gets locked after 5 attempts regardless of credential validity
- Lockout persists beyond the specified duration
- User cannot login even with correct credentials

=== IMPACT ANALYSIS ===

User Impact: High
- Legitimate users locked out of accounts
- No workaround except contacting support
- Affects all users attempting multiple logins

Business Impact:
- User frustration and support tickets
- Potential revenue loss (if payment blocked)
- Brand reputation damage

Affected Users: All users (100%)

=== ENVIRONMENT DETAILS ===

Environment: Staging
URL: https://staging.example.com
Browser: Chrome 118.0.5993.70
OS: Windows 11
Database: PostgreSQL 15.3
API Version: v2.1.5
Build: 2025.10.15.3

=== ADDITIONAL INFORMATION ===

Related Components:
- AuthenticationService.cs
- LoginController.cs
- AccountLockoutMiddleware.cs

Test Data Used:
- Email: test@example.com
- Password: ValidPass123!
- User ID: usr_7423892

Console Errors:
```
POST /api/auth/login 429 Too Many Requests
{
  "error": "Account locked due to too many attempts",
  "lockoutEndTime": "2025-10-17T15:47:32Z"
}
```

Database State:
- failed_login_attempts: 5
- is_locked: true
- lockout_end_time: 2025-10-17T15:47:32Z

Note: The counter increments even for successful credential validation

=== ATTACHMENTS ===

1. screenshot_login_page.png
2. screenshot_error_message.png
3. network_log.txt
4. database_query_results.sql

=== REPRODUCTION RATE ===

100% reproducible
Tested: 5 times
Failed: 5 times

=== SUGGESTED FIX ===

The lockout logic should:
1. Only increment failed_attempts counter on INVALID credentials
2. Reset counter to 0 on successful login
3. Clear lockout status after lockout period expires

Possible Root Cause:
LoginController increments attempt counter before validating credentials

=== WORKAROUND ===

Manual database update to reset lockout:
```sql
UPDATE users 
SET failed_login_attempts = 0, 
    is_locked = false, 
    lockout_end_time = NULL 
WHERE email = 'affected@user.com';
```

Note: This requires direct database access (not feasible for end users)

=== RELATED BUGS ===

- BUG-198: Lockout period not honoring configured duration
- BUG-223: Failed attempts counter not resetting

=== TESTING NOTES ===

Blocked Tests:
- LOGIN-005: Multiple successful logins
- LOGIN-012: Session persistence
- LOGIN-018: Remember me functionality

Affected Test Cases: 8 test cases blocked

=== ASSIGNMENT ===

Assigned To: Coder Agent
Component: Authentication
Sprint: Sprint 15
Target Fix Date: October 19, 2025

=== STATUS ===

Status: New
Discovered In: v2.1.5
Regression: No (new feature)
Requires Retest: Yes
Requires Regression Test: Yes

=== COMMUNICATION ===

Notification Sent To:
- Development Team Lead
- Product Owner
- Technical Support Team

Urgency: High - blocks user acceptance testing
```
# QA Agent

## Role
Quality Assurance Engineer responsible for testing and ensuring software quality.

## Overview
The QA Agent creates test plans, executes tests, identifies bugs, and ensures that all features meet quality standards before release. This agent acts as the quality gatekeeper.

## Primary Responsibilities
- Create comprehensive test plans
- Write and execute test cases
- Perform various types of testing (unit, integration, E2E)
- Identify and report bugs
- Verify bug fixes
- Ensure quality standards are met
- Test automation
- Performance and load testing

## Skills & Expertise
- Testing methodologies and best practices
- Test automation frameworks
- Performance testing tools
- Security testing
- API testing
- Mobile and web testing
- Bug tracking and reporting

## Collaboration
- **Receives from**: Coder (features to test), Product Owner (acceptance criteria), Architect (quality requirements)
- **Sends to**: Coder (bug reports), Product Owner (quality reports), Technical Writer (test documentation)

## Success Criteria
- All features thoroughly tested
- Bugs identified before production
- Test coverage meets standards
- No critical bugs in production
- Fast feedback to developers

## Related Files
- [Rules](./rules.md)
- [Task Types](./task-types.md)
- [Examples](./examples.md)

