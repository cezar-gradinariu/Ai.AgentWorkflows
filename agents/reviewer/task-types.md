# Reviewer Agent Task Types

## Task Types the Reviewer Agent Can Handle

### 1. Review Code
**Description**: Review code changes for quality and standards
**Input Required**:
- Code changes/pull request
- Context and purpose
- Related requirements
- Coding standards reference

**Output Produced**:
- Review comments
- Approval or rejection decision
- List of issues (critical, major, minor)
- Suggestions for improvement

**Typical Duration**: 15 minutes - 2 hours

---

### 2. Review Architecture
**Description**: Review architectural design and decisions
**Input Required**:
- Architecture diagrams
- Design documents
- Technical specifications
- Requirements

**Output Produced**:
- Architecture review report
- Identified risks
- Recommendations
- Approval status

**Typical Duration**: 1-3 hours

---

### 3. Security Review
**Description**: Review code for security vulnerabilities
**Input Required**:
- Code changes
- Security requirements
- Threat model (if available)
- Compliance requirements

**Output Produced**:
- Security assessment
- Vulnerability report
- Risk ratings
- Remediation recommendations

**Typical Duration**: 30 minutes - 2 hours

---

### 4. Performance Review
**Description**: Review code for performance issues
**Input Required**:
- Code changes
- Performance requirements
- Benchmarks (if available)
- Expected load

**Output Produced**:
- Performance analysis
- Bottleneck identification
- Optimization suggestions
- Approval status

**Typical Duration**: 30 minutes - 2 hours

---

### 5. Review Tests
**Description**: Review test coverage and quality
**Input Required**:
- Test code
- Coverage reports
- Test strategy
- Tested functionality

**Output Produced**:
- Test quality assessment
- Coverage analysis
- Missing test scenarios
- Recommendations

**Typical Duration**: 30 minutes - 1 hour

---

### 6. Review Documentation
**Description**: Review technical documentation
**Input Required**:
- Documentation files
- Documentation standards
- Target audience
- Related code

**Output Produced**:
- Documentation feedback
- Clarity assessment
- Missing information
- Approval status

**Typical Duration**: 15 minutes - 1 hour

---

## Review Priorities

### Critical (Immediate)
- Production hotfixes
- Security vulnerabilities
- Critical bug fixes

### High (Same Day)
- Feature completions
- Important bug fixes
- Release-blocking changes

### Normal (Within 24 hours)
- Standard features
- Refactoring
- Documentation updates

### Low (Within 48 hours)
- Minor improvements
- Code cleanup
- Non-urgent updates

## Review Limitations
- Cannot review own code
- Requires complete context to review
- Cannot approve without proper testing
- May require additional expert review for complex changes

