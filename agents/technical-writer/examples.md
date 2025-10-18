# Technical Writer Agent Examples

## Example 1: Write User Guide

### Input
```
Task: Write User Guide for Password Reset Feature
Feature: Self-service password reset
Audience: End users (non-technical)
Format: Web documentation
Includes: Email-based reset flow
```

### Output
```markdown
# Password Reset Guide

## Overview

If you've forgotten your password, you can easily reset it yourself using
our self-service password reset feature. This guide will walk you through
the process step by step.

**Estimated time:** 5 minutes

---

## Before You Begin

Make sure you have:
- ✓ Access to the email address associated with your account
- ✓ Ability to receive emails (check spam folder if needed)

**Note:** Password reset links expire after 1 hour for security reasons.

---

## How to Reset Your Password

### Step 1: Request a Password Reset

1. Go to the login page at [www.example.com/login](https://www.example.com/login)
2. Click the **Forgot Password?** link below the login button
3. Enter your email address in the provided field
4. Click **Send Reset Link**

![Password reset request screen](images/password-reset-request.png)

**What happens next:** You'll see a confirmation message that an email has
been sent to your address.

---

### Step 2: Check Your Email

1. Open your email inbox
2. Look for an email from `noreply@example.com` with the subject 
   "Reset Your Password"
3. If you don't see it within 5 minutes:
   - Check your spam or junk folder
   - Verify you entered the correct email address
   - Try requesting another reset link

![Password reset email](images/password-reset-email.png)

---

### Step 3: Create a New Password

1. Open the password reset email
2. Click the **Reset Password** button in the email
   - Or copy and paste the reset link into your browser
3. You'll be taken to the password reset page
4. Enter your new password in the **New Password** field
5. Re-enter your password in the **Confirm Password** field
6. Click **Reset Password**

![Password reset form](images/password-reset-form.png)

---

### Step 4: Log In with Your New Password

1. After resetting your password, you'll be redirected to the login page
2. Enter your email address
3. Enter your new password
4. Click **Log In**

**Success!** You're now logged in with your new password.

---

## Password Requirements

Your new password must meet the following requirements:

- ✓ At least 12 characters long
- ✓ Contains at least one uppercase letter (A-Z)
- ✓ Contains at least one lowercase letter (a-z)
- ✓ Contains at least one number (0-9)
- ✓ Contains at least one special character (!@#$%^&*)
- ✓ Cannot be one of your last 5 passwords

**Tip:** Use a passphrase like "MyDog!sNamed@Charlie2024" - it's both 
secure and easier to remember than random characters.

---

## Troubleshooting

### I didn't receive the reset email

**Possible solutions:**
1. Check your spam or junk folder
2. Wait a few more minutes (sometimes emails are delayed)
3. Make sure you entered the correct email address
4. Try requesting a new reset link
5. Contact support if you still don't receive it

### The reset link says it's expired

**Solution:** Reset links expire after 1 hour for security. Request a 
new reset link and complete the process within the hour.

### My new password isn't being accepted

**Check that your password:**
- Meets all requirements listed above
- Doesn't contain spaces at the beginning or end
- Isn't one of your previous 5 passwords

### I'm still having trouble

**Contact Support:**
- Email: support@example.com
- Phone: 1-800-123-4567 (Mon-Fri, 9am-5pm EST)
- Live Chat: Available on our website

---

## Security Tips

**Keep your account secure:**
- ✓ Never share your password with anyone
- ✓ Use a unique password (don't reuse passwords from other sites)
- ✓ Consider using a password manager
- ✓ Enable two-factor authentication (if available)
- ✓ Don't use easily guessable passwords like "Password123!"

**Suspicious activity?**
If you didn't request a password reset but received an email:
1. Don't click the link in the email
2. Log into your account using your current password
3. Change your password immediately
4. Contact our security team at security@example.com

---

## Frequently Asked Questions

### How often can I reset my password?
You can reset your password as often as needed, but there's a limit of 
5 reset requests per hour to prevent abuse.

### Can I use my old password again?
No, you cannot reuse any of your last 5 passwords. This security measure 
helps protect your account.

### Will I be logged out of other devices?
Yes, for security reasons, resetting your password will log you out of 
all devices. You'll need to log in again with your new password.

### How long does the reset link last?
Password reset links expire after 1 hour. If yours has expired, simply 
request a new one.

---

## Related Guides

- [How to Change Your Password](./change-password.md)
- [Setting Up Two-Factor Authentication](./two-factor-auth.md)
- [Account Security Best Practices](./security-best-practices.md)
- [Managing Your Account Settings](./account-settings.md)

---

**Last updated:** October 17, 2025  
**Version:** 1.0  
**Was this guide helpful?** [Yes](#) | [No](#) | [Send Feedback](mailto:docs@example.com)
```

---

## Example 2: Write API Documentation

### Input
```
Task: Write API Documentation for User Endpoint
API: GET /api/users/{id}
Purpose: Retrieve user information by ID
Authentication: Bearer token required
```

### Output
```markdown
# Get User by ID

Retrieve detailed information about a specific user by their unique identifier.

## Endpoint

```
GET /api/users/{id}
```

## Authentication

This endpoint requires authentication. Include a valid access token in the 
Authorization header.

```http
Authorization: Bearer {access_token}
```

## Path Parameters

| Parameter | Type   | Required | Description |
|-----------|--------|----------|-------------|
| `id`      | string | Yes      | Unique identifier of the user to retrieve |

## Query Parameters

This endpoint does not accept query parameters.

## Request Headers

| Header          | Value            | Required | Description |
|-----------------|------------------|----------|-------------|
| Authorization   | Bearer {token}   | Yes      | Valid access token |
| Content-Type    | application/json | No       | Response format preference |

## Request Example

```http
GET /api/users/usr_1234567890 HTTP/1.1
Host: api.example.com
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

```bash
# cURL
curl -X GET "https://api.example.com/api/users/usr_1234567890" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

```javascript
// JavaScript (fetch)
const response = await fetch('https://api.example.com/api/users/usr_1234567890', {
  method: 'GET',
  headers: {
    'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'
  }
});
const user = await response.json();
```

```python
# Python (requests)
import requests

url = "https://api.example.com/api/users/usr_1234567890"
headers = {
    "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}

response = requests.get(url, headers=headers)
user = response.json()
```

```csharp
// C# (HttpClient)
using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = 
    new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...");

var response = await client.GetAsync("https://api.example.com/api/users/usr_1234567890");
var user = await response.Content.ReadAsStringAsync();
```

## Response

### Success Response (200 OK)

```json
{
  "id": "usr_1234567890",
  "email": "john.doe@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "displayName": "John Doe",
  "role": "user",
  "status": "active",
  "profilePictureUrl": "https://cdn.example.com/avatars/usr_1234567890.jpg",
  "createdAt": "2025-01-15T10:30:00Z",
  "updatedAt": "2025-10-17T14:22:33Z",
  "lastLoginAt": "2025-10-17T09:15:22Z",
  "emailVerified": true,
  "preferences": {
    "language": "en",
    "timezone": "America/New_York",
    "notifications": {
      "email": true,
      "push": false
    }
  }
}
```

### Response Fields

| Field               | Type    | Description |
|---------------------|---------|-------------|
| `id`                | string  | Unique user identifier |
| `email`             | string  | User's email address |
| `firstName`         | string  | User's first name |
| `lastName`          | string  | User's last name |
| `displayName`       | string  | User's display name |
| `role`              | string  | User role: `user`, `admin`, `moderator` |
| `status`            | string  | Account status: `active`, `inactive`, `suspended` |
| `profilePictureUrl` | string  | URL to user's profile picture (nullable) |
| `createdAt`         | string  | ISO 8601 timestamp of account creation |
| `updatedAt`         | string  | ISO 8601 timestamp of last update |
| `lastLoginAt`       | string  | ISO 8601 timestamp of last login (nullable) |
| `emailVerified`     | boolean | Whether email is verified |
| `preferences`       | object  | User preferences object |

## Error Responses

### 400 Bad Request

```json
{
  "error": {
    "code": "INVALID_USER_ID",
    "message": "The provided user ID format is invalid",
    "details": "User ID must start with 'usr_' followed by 10 digits"
  }
}
```

### 401 Unauthorized

```json
{
  "error": {
    "code": "UNAUTHORIZED",
    "message": "Invalid or expired access token",
    "details": "Please obtain a new access token and try again"
  }
}
```

### 403 Forbidden

```json
{
  "error": {
    "code": "FORBIDDEN",
    "message": "You don't have permission to view this user",
    "details": "Users can only view their own profile unless they have admin role"
  }
}
```

### 404 Not Found

```json
{
  "error": {
    "code": "USER_NOT_FOUND",
    "message": "User with the specified ID does not exist",
    "details": "Please verify the user ID and try again"
  }
}
```

### 429 Too Many Requests

```json
{
  "error": {
    "code": "RATE_LIMIT_EXCEEDED",
    "message": "Rate limit exceeded",
    "details": "Maximum 100 requests per minute allowed",
    "retryAfter": 45
  }
}
```

### 500 Internal Server Error

```json
{
  "error": {
    "code": "INTERNAL_ERROR",
    "message": "An unexpected error occurred",
    "details": "Please try again later or contact support if the issue persists",
    "requestId": "req_abc123xyz"
  }
}
```

## Status Codes

| Code | Description |
|------|-------------|
| 200  | Success - User retrieved |
| 400  | Bad Request - Invalid user ID format |
| 401  | Unauthorized - Missing or invalid token |
| 403  | Forbidden - Insufficient permissions |
| 404  | Not Found - User does not exist |
| 429  | Too Many Requests - Rate limit exceeded |
| 500  | Internal Server Error - Server error |

## Rate Limiting

This endpoint is subject to rate limiting:
- **Limit:** 100 requests per minute per access token
- **Header:** `X-RateLimit-Remaining` shows remaining requests
- **Header:** `X-RateLimit-Reset` shows reset time (Unix timestamp)

## Permissions

- **Users** can retrieve their own user information
- **Admins** can retrieve any user's information
- **Moderators** can retrieve any user's information (read-only)

## Notes

- The `profilePictureUrl` field may be `null` if no picture is set
- The `lastLoginAt` field may be `null` for newly created accounts
- Suspended users can still be retrieved but will have `status: "suspended"`
- Deleted users will return a 404 error

## Related Endpoints

- [List Users](./list-users.md) - `GET /api/users`
- [Update User](./update-user.md) - `PATCH /api/users/{id}`
- [Delete User](./delete-user.md) - `DELETE /api/users/{id}`
- [Get Current User](./get-current-user.md) - `GET /api/users/me`

---

**Last updated:** October 17, 2025  
**API Version:** v2.1
```
# Business Analyst Agent

## Role
Business Analyst responsible for requirements analysis and bridging business and technical teams.

## Overview
The Business Analyst Agent analyzes business requirements, documents processes, creates specifications, and ensures that technical solutions align with business objectives. This agent translates business needs into actionable requirements.

## Primary Responsibilities
- Analyze business requirements and processes
- Create functional specifications
- Document business workflows
- Identify and manage stakeholders
- Facilitate communication between business and technical teams
- Validate requirements completeness
- Perform impact analysis
- Create use cases and user scenarios

## Skills & Expertise
- Business process analysis
- Requirements elicitation and documentation
- Stakeholder management
- Process modeling (BPMN, flowcharts)
- Use case development
- Gap analysis
- Impact assessment
- Domain knowledge

## Collaboration
- **Receives from**: Product Owner (business goals), Stakeholders (requirements), Coder (technical constraints)
- **Sends to**: Product Owner (refined requirements), Architect (technical needs), Technical Writer (process documentation)

## Success Criteria
- Clear and complete requirements
- Stakeholder alignment achieved
- Business processes well-documented
- Requirements traceable to business goals
- Minimal requirement changes during development

## Related Files
- [Rules](./rules.md)
- [Task Types](./task-types.md)
- [Examples](./examples.md)

