# Architect Agent Examples

## Example 1: Design System Architecture

### Input
```
Task: Design E-Commerce Platform Architecture
Requirements:
- Support 100,000 concurrent users
- Multi-region deployment
- 99.9% uptime SLA
- Real-time inventory updates
- Payment processing integration
- Mobile and web clients
- Budget: Medium to High
- Team: 15 developers
- Timeline: 9 months
```

### Output
```
E-Commerce Platform Architecture Design
Version: 1.0
Date: October 17, 2025
Architect: Architecture Agent

=== EXECUTIVE SUMMARY ===

Proposed architecture uses microservices pattern with event-driven
communication to achieve scalability and resilience requirements.

Key Decisions:
- Microservices architecture for scalability
- Event-driven communication for loose coupling
- CQRS for read/write optimization
- Multi-region active-active deployment
- API Gateway for client access
- Kubernetes for container orchestration

=== ARCHITECTURE OVERVIEW ===

High-Level Architecture (C4 Level 1 - Context):
┌─────────────────────────────────────────────┐
│         E-Commerce Platform                 │
│                                             │
│  ┌─────────┐  ┌──────────┐  ┌───────────┐ │
│  │   Web   │  │  Mobile  │  │   Admin   │ │
│  │  Client │  │   App    │  │  Portal   │ │
│  └─────────┘  └──────────┘  └───────────┘ │
│       │            │              │         │
│       └────────────┴──────────────┘         │
│                    │                        │
│            ┌───────▼───────┐               │
│            │  API Gateway  │               │
│            └───────┬───────┘               │
└────────────────────┼─────────────────────────┘
                     │
         ┌───────────┴───────────┐
         │                       │
    ┌────▼────┐          ┌──────▼──────┐
    │ Service │          │   Service   │
    │  Mesh   │          │  Discovery  │
    └────┬────┘          └─────────────┘
         │
    ┌────▼────────────────────────────┐
    │      Microservices Layer        │
    ├─────────────────────────────────┤
    │ • Catalog Service               │
    │ • Order Service                 │
    │ • Inventory Service             │
    │ • Payment Service               │
    │ • User Service                  │
    │ • Notification Service          │
    │ • Cart Service                  │
    │ • Review Service                │
    └─────────────────────────────────┘

=== CORE SERVICES ===

1. Catalog Service
   - Product information management
   - Search and filtering
   - Category management
   - Database: PostgreSQL + Elasticsearch

2. Order Service
   - Order creation and management
   - Order status tracking
   - Order history
   - Database: PostgreSQL
   - Events: OrderCreated, OrderCompleted

3. Inventory Service
   - Real-time stock management
   - Reservation system
   - Multi-warehouse support
   - Database: PostgreSQL
   - Cache: Redis for real-time counts

4. Payment Service
   - Payment processing integration
   - Transaction management
   - Refund handling
   - PCI compliance
   - Integration: Stripe, PayPal

5. User Service
   - Authentication (JWT)
   - Authorization (RBAC)
   - Profile management
   - Database: PostgreSQL

6. Cart Service
   - Shopping cart management
   - Cart persistence
   - Database: Redis (session-based)

7. Notification Service
   - Email notifications
   - SMS notifications
   - Push notifications
   - Event-driven (consumes events)

8. Review Service
   - Product reviews
   - Ratings
   - Moderation
   - Database: MongoDB

=== TECHNICAL STACK ===

Backend:
- Language: C# / .NET 8
- Framework: ASP.NET Core
- API: RESTful + GraphQL (for mobile)

Data:
- Primary: PostgreSQL
- Search: Elasticsearch
- Cache: Redis
- Document: MongoDB
- Message Queue: RabbitMQ
- Event Store: EventStore

Infrastructure:
- Container: Docker
- Orchestration: Kubernetes
- Service Mesh: Istio
- API Gateway: Kong
- Cloud: AWS (multi-region)

Monitoring:
- Logging: ELK Stack
- Metrics: Prometheus + Grafana
- Tracing: Jaeger
- APM: DataDog

=== ARCHITECTURE PATTERNS ===

1. Microservices
   - Independent deployment
   - Technology flexibility
   - Fault isolation

2. Event-Driven Architecture
   - Loose coupling
   - Asynchronous communication
   - Event sourcing for orders

3. CQRS
   - Separate read/write models
   - Optimized queries
   - Scalability

4. API Gateway Pattern
   - Single entry point
   - Authentication/Authorization
   - Rate limiting
   - Request routing

5. Database per Service
   - Data independence
   - Technology choice per service
   - Scalability

=== SCALABILITY STRATEGY ===

Horizontal Scaling:
- All services stateless
- Auto-scaling based on CPU/memory
- Load balancing (ALB)

Caching Strategy:
- Redis for hot data
- CDN for static assets
- Query result caching

Database Scaling:
- Read replicas for read-heavy services
- Sharding for large datasets
- Connection pooling

=== HIGH AVAILABILITY ===

Multi-Region Setup:
- Active-Active deployment (US-East, US-West, EU)
- Global load balancing (Route53)
- Data replication across regions

Resilience:
- Circuit breakers (Polly)
- Retry policies
- Fallback mechanisms
- Health checks
- Graceful degradation

Disaster Recovery:
- RTO: 15 minutes
- RPO: 5 minutes
- Automated backups (hourly)
- Cross-region backup storage

=== SECURITY ARCHITECTURE ===

Authentication:
- JWT tokens
- OAuth 2.0 / OpenID Connect
- MFA for admin access

Authorization:
- Role-Based Access Control (RBAC)
- Claims-based authorization
- API-level permissions

Data Security:
- Encryption at rest (AES-256)
- Encryption in transit (TLS 1.3)
- PCI DSS compliance for payment data
- GDPR compliance

Network Security:
- VPC with private subnets
- Security groups
- WAF (Web Application Firewall)
- DDoS protection

=== DEPLOYMENT ARCHITECTURE ===

Environment Strategy:
- Development
- Staging
- Production (multi-region)

CI/CD Pipeline:
- GitHub Actions
- Automated testing
- Container scanning
- Blue-green deployment
- Canary releases

Infrastructure as Code:
- Terraform for infrastructure
- Helm charts for Kubernetes
- GitOps approach

=== PERFORMANCE TARGETS ===

- API Response Time: p95 < 200ms
- Search Response Time: < 100ms
- Order Processing: < 500ms
- Concurrent Users: 100,000
- Throughput: 10,000 requests/second
- Database Queries: < 50ms

=== RISKS & MITIGATION ===

Risk 1: Microservices Complexity
- Mitigation: Strong DevOps, monitoring, service mesh

Risk 2: Data Consistency
- Mitigation: Eventual consistency with saga pattern

Risk 3: Network Latency (Multi-region)
- Mitigation: CDN, caching, read replicas

Risk 4: Learning Curve
- Mitigation: Training, documentation, gradual adoption

=== IMPLEMENTATION ROADMAP ===

Phase 1 (Months 1-3): Foundation
- Infrastructure setup
- Core services (User, Catalog, Cart)
- API Gateway
- Basic CI/CD

Phase 2 (Months 4-6): Core Features
- Order Service with CQRS
- Payment integration
- Inventory management
- Event-driven architecture

Phase 3 (Months 7-9): Scale & Polish
- Multi-region deployment
- Advanced monitoring
- Performance optimization
- Security hardening

=== COST ESTIMATION ===

Infrastructure (Monthly):
- Kubernetes Cluster: $2,000
- Databases: $1,500
- Cache/Message Queue: $800
- Monitoring: $500
- CDN: $300
- Total: ~$5,100/month

Development:
- Team (15 devs x 9 months): ~$1.35M
- Infrastructure setup: $50K
- Training: $30K
- Total: ~$1.43M

=== DECISION RECORDS ===

See related ADRs:
- ADR-001: Microservices vs Monolith
- ADR-002: Event-Driven Communication
- ADR-003: CQRS for Order Service
- ADR-004: Multi-Region Active-Active
- ADR-005: Technology Stack Selection

=== APPROVAL STATUS ===

Status: Pending Review
Reviewers: Product Owner, Tech Lead, CTO
Next Steps: POC for event-driven architecture
```

---

## Example 2: Architecture Decision Record

### Input
```
Task: Create ADR for Database Choice
Context: Selecting database for user profile service
Options: PostgreSQL, MongoDB, DynamoDB
Decision Needed: Which database to use
```

### Output
```
Architecture Decision Record: User Profile Service Database

ADR-007: Database Selection for User Profile Service
Date: 2025-10-17
Status: Accepted
Deciders: Architect Agent, Tech Lead, DBA

=== CONTEXT ===

We need to select a database for the User Profile Service that will
store user information, preferences, and authentication data.

Requirements:
- Store structured user data (name, email, preferences)
- Support ACID transactions for critical operations
- Handle 1M+ users
- Provide fast read access (< 10ms)
- Support complex queries for admin dashboard
- Ensure data consistency
- GDPR compliance (data encryption, deletion)

Current System Context:
- Most services use PostgreSQL
- Team has strong SQL expertise
- Infrastructure team supports PostgreSQL well
- No document database in current stack

=== DECISION DRIVERS ===

1. Data Structure: Mostly structured with some flexibility needed
2. Query Patterns: Mix of simple lookups and complex joins
3. Consistency: Strong consistency required for auth data
4. Team Expertise: Strong in SQL, limited in NoSQL
5. Operational Complexity: Prefer fewer technologies
6. Performance: Read-heavy workload
7. Cost: Infrastructure and operational costs

=== OPTIONS CONSIDERED ===

Option 1: PostgreSQL
Option 2: MongoDB
Option 3: DynamoDB

=== EVALUATION ===

| Criteria           | PostgreSQL | MongoDB | DynamoDB |
|--------------------|------------|---------|----------|
| Data Model Fit     | Excellent  | Good    | Good     |
| ACID Transactions  | Yes        | Limited | Limited  |
| Query Flexibility  | Excellent  | Good    | Limited  |
| Performance        | Excellent  | Excellent| Excellent|
| Scalability        | Good       | Excellent| Excellent|
| Team Expertise     | High       | Low     | Low      |
| Operational Cost   | Medium     | Medium  | Low      |
| Infrastructure Fit | Excellent  | Poor    | Good     |
| GDPR Compliance    | Excellent  | Good    | Good     |

=== DECISION ===

Selected: PostgreSQL

Rationale:
PostgreSQL is the best fit for the user profile service based on:

1. Data Structure Match
   - User profile data is highly structured
   - Relationships between users, roles, permissions
   - SQL schema provides clear data model

2. Strong Consistency
   - Authentication data requires ACID guarantees
   - User state changes must be immediately consistent
   - PostgreSQL provides full ACID compliance

3. Query Flexibility
   - Admin dashboard needs complex queries and joins
   - Full SQL support enables rich reporting
   - Indexes provide excellent read performance

4. Team Expertise
   - Team has deep PostgreSQL knowledge
   - Reduces learning curve and mistakes
   - Faster development and troubleshooting

5. Operational Simplicity
   - Already in our infrastructure
   - No new technology to learn and operate
   - Existing backup and monitoring systems

6. Performance
   - With proper indexing, meets < 10ms read requirement
   - Connection pooling handles high concurrency
   - Read replicas for scaling reads

=== CONSEQUENCES ===

Positive:
+ Leverages existing team expertise
+ Consistent with other services (operational simplicity)
+ Strong ACID guarantees
+ Excellent query flexibility
+ Mature tooling and ecosystem
+ Good performance with proper tuning

Negative:
- Slightly more complex to scale than DynamoDB
- Requires schema migrations for changes
- Need to manage read replicas manually

Neutral:
~ Standard relational database trade-offs
~ Need proper connection pooling configuration

=== IMPLEMENTATION NOTES ===

Database Configuration:
- Use connection pooling (Npgsql)
- Set up read replicas for scaling
- Create indexes on frequently queried fields
- Enable query performance monitoring

Schema Design:
- Normalize to 3NF where appropriate
- Use foreign keys for referential integrity
- Add audit columns (created_at, updated_at)
- Implement soft deletes for GDPR

Performance Optimization:
- Index on email, user_id, and common query fields
- Use EXPLAIN ANALYZE for query optimization
- Monitor slow query log
- Set up pg_stat_statements

Security:
- Use separate read-only user for replicas
- Encrypt connections (SSL/TLS)
- Enable data-at-rest encryption
- Implement row-level security if needed

Backup Strategy:
- Automated daily backups
- Point-in-time recovery enabled
- 30-day retention
- Cross-region backup storage

=== ALTERNATIVES RECONSIDERED ===

MongoDB:
- Reconsider if we need flexible schema
- Good fit if document-oriented data grows
- Would require team training

DynamoDB:
- Reconsider for extreme scale (10M+ users)
- Good fit if AWS-native solution preferred
- Consider for future services with simple access patterns

=== REVIEW & APPROVAL ===

Reviewed by: Tech Lead ✓
Reviewed by: DBA ✓
Reviewed by: Product Owner ✓
Approved by: CTO ✓

Status: Accepted and Implemented
Implementation Date: 2025-10-20

=== RELATED DECISIONS ===

- ADR-003: Service Data Ownership
- ADR-005: Technology Stack Selection
- ADR-012: Data Encryption Strategy

=== REVISION HISTORY ===

v1.0 - 2025-10-17 - Initial decision
```
# Architect Agent

## Role
Software Architect responsible for system design, technical decisions, and architectural standards.

## Overview
The Architect Agent designs the overall system architecture, makes critical technical decisions, and ensures that the codebase follows sound architectural principles. This agent provides technical leadership and maintains the long-term technical vision.

## Primary Responsibilities
- Design system architecture and components
- Make technical decisions and trade-offs
- Define technical standards and best practices
- Review architectural changes
- Ensure scalability and maintainability
- Evaluate new technologies
- Create technical specifications
- Manage technical debt

## Skills & Expertise
- Deep knowledge of architectural patterns
- Understanding of distributed systems
- Performance and scalability expertise
- Security architecture knowledge
- Database design
- Cloud architecture
- System integration
- Technology evaluation

## Collaboration
- **Receives from**: Product Owner (feature requests), Business Analyst (requirements), Coder (technical questions)
- **Sends to**: Coder (technical designs), Reviewer (architectural standards), QA (quality requirements)

## Success Criteria
- Scalable and maintainable architecture
- Technical decisions well-documented
- System meets non-functional requirements
- Minimal technical debt accumulation
- Successful technology adoption

## Related Files
- [Rules](./rules.md)
- [Task Types](./task-types.md)
- [Examples](./examples.md)

