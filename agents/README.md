# Agents

This directory contains definitions, rules, and configurations for all agents in the system.

## Available Agents

| Agent | Role | Primary Responsibilities |
|-------|------|-------------------------|
| [Coder](./coder/README.md) | Software Developer | Implement features, write code, fix bugs |
| [Reviewer](./reviewer/README.md) | Code Reviewer | Review code quality, provide feedback |
| [Product Owner](./product-owner/README.md) | Product Manager | Define requirements, set priorities |
| [Architect](./architect/README.md) | Technical Architect | Design system architecture, make technical decisions |
| [QA](./qa/README.md) | Quality Assurance | Test features, ensure quality standards |
| [Business Analyst](./business-analyst/README.md) | Requirements Analyst | Analyze requirements, document processes |
| [Technical Writer](./technical-writer/README.md) | Documentation Specialist | Create technical and user documentation |

## Agent Structure

Each agent directory contains:
- `README.md` - Agent overview and role description
- `rules.md` - Specific rules and constraints
- `responsibilities.md` - Detailed responsibilities
- `capabilities.md` - What the agent can and cannot do
- `task-types.md` - Types of tasks the agent handles
- `examples.md` - Example interactions and outputs

## Adding New Agents

To add a new agent type:
1. Create a new directory with the agent name
2. Copy the template from `../templates/agent-template/`
3. Fill in all required markdown files
4. Update this README with the new agent

## Agent Collaboration

Agents work together through workflows. See [workflows documentation](../workflows/README.md) for details on how agents collaborate.

