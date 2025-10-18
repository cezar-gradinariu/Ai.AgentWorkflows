# Workflows

## Overview

This directory contains **complete workflow definitions** that orchestrate multiple AI agents to accomplish complex tasks. Each workflow is a end-to-end process from start to finish.

## Available Workflows

### 1. Feature Development Workflow
**Path:** [feature-development/](./feature-development/)

**Purpose:** Take a feature from initial concept through to production deployment

**Agents Used:** All 7 (Product Owner, Business Analyst, Architect, Coder, Reviewer, QA, Technical Writer)

**Duration:**
- AI Processing: 3.5-16 hours (depending on complexity)
- Calendar Time: 2-5 days (with reviews and iterations)

**Use When:**
- Building new features
- Adding functionality to existing systems
- Complete end-to-end development needed

**Outputs:**
- User stories and requirements
- Technical design and architecture
- Working code with tests (80%+ coverage)
- Code review approval
- Test results and QA sign-off
- User documentation
- Production deployment

[→ View workflow details](./feature-development/README.md)  
[→ See step-by-step definition](./feature-development/workflow-definition.md)

---

## How to Use Workflows

### Step 1: Choose a Workflow
Select the workflow that matches your task. Each workflow README explains its purpose and when to use it.

### Step 2: Review Prerequisites
Check that you have everything needed to start (defined in each workflow's README).

### Step 3: Follow the Phases
Each workflow is divided into phases. Complete each phase in order.

### Step 4: Execute Agent Steps
Within each phase, execute the individual agent steps using one of these methods:

**Manual Execution:**
- Copy agent prompts to your AI
- Provide the required inputs
- Save outputs for next step

**Automated Execution:**
- Use scripts to call AI APIs
- Pass data between steps automatically
- Handle loops and iterations programmatically

**Orchestration Tools:**
- Use workflow engines (n8n, Zapier, etc.)
- Visual workflow design
- Built-in error handling

### Step 5: Handle Approvals
Some steps require approval before proceeding. If rejected, loop back to fix issues.

### Step 6: Complete Workflow
When all phases are done, you'll have all deliverables ready.

## Quick Start Example

```
# Starting the Feature Development Workflow

1. "Act as Product Owner: Define requirements for [feature]"
   → Get: User story + acceptance criteria

2. "Act as Business Analyst: Analyze these requirements: [paste from step 1]"
   → Get: Functional specification

3. "Act as Architect: Design solution for: [paste spec]"
   → Get: Technical design

4. "Act as Coder: Implement: [paste design]"
   → Get: Code + tests

5. "Act as Reviewer: Review this code: [paste code]"
   → Get: Review feedback (approve or request changes)

6. "Act as QA: Test this feature: [paste requirements + code]"
   → Get: Test results

7. "Act as Technical Writer: Document: [paste feature details]"
   → Get: User documentation

8. "Act as Product Owner: Validate: [paste all outputs]"
   → Get: Final approval

Done! Feature complete.
```

For detailed execution instructions, see [Getting Started Guide](../docs/getting-started/README.md).

## Workflow Structure

Each workflow directory contains:

```
workflow-name/
├── README.md                 # Overview and quick reference
├── workflow-definition.md    # Detailed step-by-step process
├── inputs-outputs.md        # Data flow documentation
├── agent-sequence.md        # Agent collaboration diagram
└── example.md              # Real execution example
```

## Understanding Workflow Phases

### Requirements Phase
**Agents:** Product Owner, Business Analyst  
**Purpose:** Define and analyze what needs to be built  
**Outputs:** Requirements, specifications, use cases

### Design Phase
**Agents:** Architect, Reviewer  
**Purpose:** Create technical design and validate approach  
**Outputs:** Architecture, API contracts, ADRs

### Development Phase
**Agents:** Coder  
**Purpose:** Implement the solution with tests  
**Outputs:** Source code, unit tests, documentation

### Review Phase
**Agents:** Reviewer  
**Purpose:** Ensure quality and standards  
**Outputs:** Review feedback, approval/rejection

### Testing Phase
**Agents:** QA  
**Purpose:** Validate functionality and quality  
**Outputs:** Test plans, test results, bug reports

### Documentation Phase
**Agents:** Technical Writer  
**Purpose:** Create user-facing documentation  
**Outputs:** User guides, API docs, tutorials

### Validation Phase
**Agents:** Product Owner  
**Purpose:** Confirm business value delivered  
**Outputs:** Validation report, sign-off

### Deployment Phase
**Agents:** Coder (with DevOps)  
**Purpose:** Release to production  
**Outputs:** Live feature, monitoring setup

## Workflow Best Practices

### ✅ Do's

- **Follow the sequence** - Steps are ordered for a reason
- **Save all outputs** - You'll need them for later steps
- **Provide complete context** - Give agents all relevant information
- **Validate at gates** - Don't skip quality checks
- **Iterate when needed** - Loop back if quality isn't met
- **Track progress** - Know where you are in the workflow
- **Document decisions** - Keep Architecture Decision Records

### ❌ Don'ts

- **Don't skip steps** - Each has a purpose
- **Don't rush approvals** - Quality gates prevent issues
- **Don't lose context** - Keep full history available
- **Don't mix agent roles** - Each agent has specific responsibilities
- **Don't ignore feedback** - Review comments are important
- **Don't skip testing** - Quality is non-negotiable

## Customizing Workflows

You can adapt workflows for your needs:

### Simplify for Small Tasks
For simple tasks, you might skip some phases:
- Simple bug fix: Skip BA and Architect phases
- Documentation update: Only Technical Writer needed
- Minor refactor: Coder → Reviewer → QA

### Extend for Complex Projects
For complex projects, add extra steps:
- Security review before deployment
- Performance testing phase
- Multiple review cycles
- Stakeholder demos

### Parallel Execution
Speed up by running steps in parallel:
- Test plan creation during code review
- Documentation during testing
- Multiple test suites simultaneously

## Workflow Metrics

Track these metrics to measure success:

| Metric | Target | Why It Matters |
|--------|--------|---------------|
| Total duration | < 16 hours AI time | Efficiency |
| Review cycles | 1-2 per phase | Quality on first attempt |
| Test coverage | ≥ 80% | Code quality |
| Bugs found in QA | < 5 | Implementation quality |
| Requirements met | 100% | Completeness |
| Documentation coverage | 100% of features | Usability |

## Coming Soon

More workflows in development:

- **Bug Fix Workflow** - Fast track for defect resolution
- **Documentation Update** - Update docs for existing features
- **Architecture Review** - Validate system design
- **Technical Debt Reduction** - Systematic refactoring
- **API Design** - Design-first API development
- **Database Migration** - Safe schema changes

## Support

### Getting Help

- **New to workflows?** Start with [Getting Started Guide](../docs/getting-started/README.md)
- **Execution questions?** See detailed examples in each workflow directory
- **Agent questions?** Review [Agent Documentation](../agents/README.md)
- **Integration issues?** Check [Collaboration Rules](../rules/collaboration-rules.md)

### Examples

See real examples:
- Each workflow has an `example.md` showing complete execution
- Agent directories have `examples.md` with role-specific examples
- Getting Started guide has step-by-step walkthrough

---

**Ready to start?** Choose a workflow above or follow the [Getting Started Guide](../docs/getting-started/README.md) 🚀

