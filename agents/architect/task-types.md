# Architect Agent Task Types

## Task Types the Architect Agent Can Handle

### 1. Design System Architecture
**Description**: Create overall system architecture design
**Input Required**:
- Business requirements
- Non-functional requirements
- Constraints (budget, timeline, team skills)
- Existing system context

**Output Produced**:
- Architecture diagrams (C4 model)
- Component specifications
- Technology recommendations
- Architecture decision records
- Implementation roadmap

**Typical Duration**: 1-5 days

---

### 2. Create Technical Specification
**Description**: Write detailed technical specs for features
**Input Required**:
- Feature requirements
- Business context
- User stories
- Quality requirements

**Output Produced**:
- Technical specification document
- API contracts
- Data models
- Integration points
- Technical constraints

**Typical Duration**: 4-8 hours

---

### 3. Make Technical Decision
**Description**: Evaluate options and make architectural decision
**Input Required**:
- Decision context
- Available options
- Constraints
- Evaluation criteria

**Output Produced**:
- Architecture Decision Record (ADR)
- Chosen solution with rationale
- Implementation guidelines
- Risk assessment
- Migration plan (if applicable)

**Typical Duration**: 2-6 hours

---

### 4. Review Architecture Change
**Description**: Review proposed architectural changes
**Input Required**:
- Change proposal
- Current architecture
- Rationale for change
- Impact assessment

**Output Produced**:
- Review report
- Approval or rejection decision
- Recommendations
- Risk identification
- Alternative suggestions

**Typical Duration**: 2-4 hours

---

### 5. Design API
**Description**: Design RESTful or GraphQL APIs
**Input Required**:
- API requirements
- Use cases
- Client needs
- Security requirements

**Output Produced**:
- API specification (OpenAPI/Swagger)
- Endpoint definitions
- Request/response schemas
- Authentication approach
- Rate limiting strategy
- Versioning strategy

**Typical Duration**: 3-6 hours

---

### 6. Design Database Schema
**Description**: Design database structure and relationships
**Input Required**:
- Data requirements
- Access patterns
- Performance requirements
- Scalability needs

**Output Produced**:
- ERD (Entity Relationship Diagram)
- Table definitions
- Indexes strategy
- Partitioning approach
- Migration scripts outline

**Typical Duration**: 3-8 hours

---

### 7. Evaluate Technology
**Description**: Assess new technology or framework
**Input Required**:
- Technology to evaluate
- Use case requirements
- Current stack context
- Constraints

**Output Produced**:
- Evaluation report
- Pros and cons analysis
- POC recommendations
- Adoption plan (if approved)
- Training needs

**Typical Duration**: 1-3 days

---

### 8. Create Architecture Decision Record (ADR)
**Description**: Document important architectural decision
**Input Required**:
- Decision context
- Options considered
- Decision made
- Consequences

**Output Produced**:
- Formal ADR document
- Decision rationale
- Trade-offs documented
- Implementation guidance
- Review and approval status

**Typical Duration**: 1-2 hours

---

### 9. Performance Architecture Review
**Description**: Review system for performance optimization
**Input Required**:
- Current performance metrics
- Performance requirements
- System architecture
- Bottleneck analysis

**Output Produced**:
- Performance assessment
- Optimization recommendations
- Architecture improvements
- Caching strategy
- Scalability plan

**Typical Duration**: 4-8 hours

---

### 10. Security Architecture Review
**Description**: Review system security architecture
**Input Required**:
- Current architecture
- Security requirements
- Threat model
- Compliance needs

**Output Produced**:
- Security assessment
- Vulnerability identification
- Security improvements
- Architecture changes needed
- Compliance validation

**Typical Duration**: 4-8 hours

---

## Deliverable Standards

### Architecture Diagrams
- Use C4 model (Context, Container, Component, Code)
- Include legends and descriptions
- Version controlled
- Kept up-to-date

### Technical Specifications
- Clear and detailed
- Include diagrams where helpful
- Define interfaces and contracts
- Specify error handling
- Include examples

### Decision Records
- Follow ADR template
- Numbered and dated
- Immutable (new ADR to change)
- Stored in version control
- Linked to related decisions

