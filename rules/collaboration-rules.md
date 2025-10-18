# Collaboration Rules

## Purpose

Define how agents work together effectively within workflows.

## Core Collaboration Principles

### 1. Clear Ownership
- Every task has ONE primary responsible agent
- Ownership explicitly assigned
- Accountability tracked
- Handoffs documented

### 2. Explicit Handoffs
When passing work to another agent:
- ✅ Confirm work is complete
- ✅ Provide all necessary context
- ✅ Document what's been done
- ✅ Specify what's needed next
- ✅ Notify the receiving agent
- ✅ Confirm receipt

### 3. Dependency Management
- Identify dependencies early
- Communicate blockers immediately
- Don't work around blockers silently
- Keep dependency tracking updated

## Agent Interaction Patterns

### Sequential Handoff
Agent A completes → Agent B begins
```
Product Owner → Business Analyst → Architect → Coder
```

### Parallel Work
Multiple agents work simultaneously
```
Coder (Implementation) || Technical Writer (Draft Docs)
```

### Review Loop
Work → Review → Revise → Review
```
Coder → Reviewer → Coder → Reviewer → Approved
```

### Collaborative
Multiple agents work together
```
Architect + Coder: Technical spike
QA + Coder: Bug investigation
```

## Communication Requirements

### Status Updates
**Daily:**
- What was completed
- What's in progress
- Any blockers

**Weekly:**
- Summary of progress
- Upcoming milestones
- Risk assessment

### Handoff Communication
When handing off work:
```markdown
## Handoff to: [Agent Name]
**From:** [Your Agent Role]
**Task:** [Task Description]
**Status:** [Complete/Blocked/Partial]

**Work Completed:**
- Item 1
- Item 2

**Next Steps Required:**
- Action 1
- Action 2

**Context/Notes:**
- Important info
- Decisions made
- Known issues

**Artifacts:**
- Link to document
- Link to code
- Link to designs
```

### Blocker Communication
When blocked:
```markdown
🚨 BLOCKED: [Task Name]

**Blocked By:** [What's blocking you]
**Agent/Team:** [Who can unblock]
**Impact:** [High/Medium/Low]
**Urgency:** [Immediate/Soon/Can Wait]

**Details:**
[Explanation of blocker]

**Requested Action:**
[What you need to proceed]

**Workaround:**
[If any workaround exists]
```

## Collaboration Scenarios

### Scenario 1: Requirement Clarification
```
Coder needs clarification
  ↓
Coder → Product Owner: Question
  ↓
Product Owner → Coder: Answer
  ↓
If architectural impact:
  Product Owner → Architect → Coder
```

### Scenario 2: Design Review
```
Architect creates design
  ↓
Architect → Reviewer: Request review
  ↓
Reviewer → Architect: Feedback
  ↓
If major changes needed: Loop back
If minor changes: Architect updates
If approved: → Coder
```

### Scenario 3: Bug Found in Testing
```
QA finds bug
  ↓
QA → Coder: Bug report
  ↓
Coder → QA: Fix ready for verification
  ↓
QA → Product Owner: Verification results
```

## Conflict Resolution

### When Agents Disagree
1. **Document both positions** clearly
2. **Identify the decision maker** (usually PO or Architect)
3. **Present options** with pros/cons
4. **Decision maker decides** and documents rationale
5. **All agents accept** and move forward

### Priority Conflicts
If multiple agents need the same resource:
1. Check priority levels (P0 > P1 > P2 > P3)
2. Check business value
3. Escalate to Product Owner if unclear
4. Product Owner makes final call

## Collaboration Anti-Patterns

### ❌ Don't Do This

**Silent Failures**
- Don't fail silently
- Always communicate blockers

**Assumption Making**
- Don't assume what others want
- Ask for clarification

**Skipping Handoff**
- Don't just move on
- Properly hand off work

**Working in Silos**
- Don't isolate yourself
- Communicate progress

**Incomplete Handoffs**
- Don't hand off half-done work
- Ensure completeness

## Collaboration Best Practices

### ✅ Do This

**Overcommunicate**
- More communication is better
- Status updates are valuable
- Share context liberally

**Ask Questions**
- No question is stupid
- Clarify early and often
- Confirm understanding

**Document Decisions**
- Write down what was decided
- Record the rationale
- Share with relevant parties

**Give Context**
- Explain WHY, not just WHAT
- Share relevant background
- Link to related work

**Be Responsive**
- Reply within 24 hours
- Acknowledge receipt
- Set expectations if delayed

## Meeting Guidelines

### Workflow Kickoff
**Attendees:** All assigned agents  
**Duration:** 30-60 minutes  
**Purpose:**
- Understand requirements
- Clarify roles
- Identify dependencies
- Agree on timeline

### Daily Standups (Virtual)
**Format:** Async status updates  
**Each Agent Reports:**
- Yesterday's progress
- Today's plan
- Blockers

### Workflow Retrospective
**Attendees:** All involved agents  
**Duration:** 45-60 minutes  
**Purpose:**
- What went well
- What could improve
- Action items

## Tools and Channels

### Synchronous
- Video calls (when needed)
- Instant messaging (urgent only)

### Asynchronous (Preferred)
- Feature documents
- Status updates in files
- Documented handoffs
- Shared documentation

## Metrics

Track collaboration effectiveness:
- Handoff clarity (feedback from receiving agent)
- Blocker resolution time
- Rework due to miscommunication
- Overall workflow duration

## Emergency Protocols

### Critical Issue
1. **Immediately notify** all affected agents
2. **Document** the issue clearly
3. **Assess impact** and urgency
4. **Coordinate response** (who does what)
5. **Update status** frequently
6. **Post-mortem** after resolution

### When to Escalate
- Blocker > 24 hours unresolved
- Conflict between agents unresolved
- Risk to delivery timeline
- Quality standards at risk
- Security or compliance issue

