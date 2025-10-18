# Feature Development Workflow

## Overview

The Feature Development Workflow orchestrates all agents to take a feature from initial concept through to production deployment. This is the most comprehensive workflow, involving all agent types.

## Workflow Purpose

Transform a feature idea into a fully implemented, tested, documented, and deployed capability.

## Agents Involved

1. **Product Owner** - Define requirements and priorities
2. **Business Analyst** - Analyze requirements and create specifications
3. **Architect** - Design technical solution
4. **Coder** - Implement the feature
5. **Reviewer** - Review code quality
6. **QA** - Test the feature
7. **Technical Writer** - Create documentation

## Workflow Duration

**AI Agent Processing Time:** 1-3 hours (total sequential execution)  
**Calendar Time (with reviews & iterations):** 2-5 days

### Understanding Durations
The durations in this workflow represent **AI agent processing time**, not human work time:
- **Minutes**: Simple tasks (e.g., creating a user story: 2-5 minutes)
- **Hours**: Complex tasks (e.g., writing comprehensive tests: 15-30 minutes)
- **Sequential vs Parallel**: Many tasks can run in parallel to reduce total time

**Note:** The original "2-6 weeks" estimate was based on human team workflows. With AI agents, the same work completes in hours or days, depending on:
- Feature complexity
- Number of review iterations required
- Waiting for human approvals (if any)
- Parallel vs sequential execution

## Prerequisites

- Feature concept or idea defined
- Business value identified
- Basic feasibility assessment done
- Resources available

## Success Criteria

- All acceptance criteria met
- Code reviewed and approved
- All tests passing
- Documentation complete
- Feature deployed to production
- Stakeholder sign-off obtained

## Workflow States

1. **Initiated** - Feature request received
2. **Requirements** - Requirements being defined
3. **Design** - Technical design in progress
4. **Development** - Code implementation
5. **Review** - Code review
6. **Testing** - QA testing
7. **Documentation** - Docs creation
8. **Approval** - Final validation
9. **Deployment** - Production release
10. **Completed** - Feature live

## Detailed Workflow

See [workflow-definition.md](./workflow-definition.md) for step-by-step details.

## Related Files

- [Workflow Definition](./workflow-definition.md) - Detailed steps
- [Agent Sequence](./agent-sequence.md) - Agent collaboration flow
- [Inputs & Outputs](./inputs-outputs.md) - Data flow
- [Example](./example.md) - Real execution example
