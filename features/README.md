# Features

This directory contains all feature requests and their current status throughout the development lifecycle.

## Feature States

Features move through these directories based on their current state:

### 📋 Backlog
Features that are defined but not yet started. Prioritized and ready for development when resources are available.

### 🚧 In Progress
Features currently being worked on by one or more agents through workflows.

### ✅ Completed
Successfully implemented, tested, and deployed features.

## Feature Structure

Each feature is stored as a markdown file with a unique ID:
```
FEATURE-{NUMBER}-{short-name}.md
```

Example: `FEATURE-001-user-authentication.md`

## Feature Template

Every feature file should include:
- **Feature ID**: Unique identifier
- **Title**: Clear, descriptive name
- **Description**: What the feature does
- **Business Value**: Why it's needed
- **Priority**: P0/P1/P2/P3
- **Status**: Current state
- **Assigned Workflow**: Which workflow is handling it
- **Requirements**: Detailed requirements
- **Acceptance Criteria**: Success criteria
- **Dependencies**: Other features or systems
- **Estimated Effort**: Time estimate
- **Actual Effort**: Time spent
- **Notes**: Additional information
- **History**: Status changes

## Creating a New Feature

1. Copy template from `../templates/features/feature-template.md`
2. Assign unique feature ID
3. Fill in all required sections
4. Save in `backlog/` directory
5. Add to feature tracking system

## Moving Features

When feature status changes:
```
backlog/ → in-progress/ → completed/
```

Maintain feature history in the file itself.

## Feature Priorities

- **P0 (Critical)**: Must have, blocking other work
- **P1 (High)**: Important for current release
- **P2 (Medium)**: Planned for near future
- **P3 (Low)**: Nice to have

## Related Documentation

- [Feature Template](../templates/features/feature-template.md)
- [Workflow Definitions](../workflows/README.md)
- [Agent Guidelines](../agents/README.md)

