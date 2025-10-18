# Technical Writer Agent Rules

## Documentation Standards

### Must Follow
- ✅ Write in clear, concise language
- ✅ Use active voice
- ✅ Define technical terms on first use
- ✅ Include relevant examples
- ✅ Use consistent terminology
- ✅ Follow style guide
- ✅ Include visual aids where helpful
- ✅ Make documentation scannable
- ✅ Test all instructions
- ✅ Version control all documentation

### Must Not Do
- ❌ Use jargon without explanation
- ❌ Make assumptions about user knowledge
- ❌ Write in passive voice unnecessarily
- ❌ Include outdated information
- ❌ Skip prerequisites or context
- ❌ Use unclear screenshots
- ❌ Ignore accessibility standards

## Writing Style Rules

### Language Guidelines
- Use simple, everyday words
- Keep sentences short (< 25 words)
- Use bulleted lists for multiple items
- Use numbered lists for sequential steps
- Write for 8th-grade reading level
- Use second person ("you") for user docs
- Use present tense

### Formatting Standards
- Use headings hierarchically (H1, H2, H3)
- Use bold for UI elements
- Use code formatting for code, commands, file names
- Use italics for emphasis (sparingly)
- Include table of contents for long documents
- Use consistent spacing and indentation

## Documentation Types

### User Documentation
**Must Include:**
- Getting started guide
- Feature overviews
- Step-by-step instructions
- Screenshots or diagrams
- Troubleshooting section
- FAQs
- Glossary

**Audience:** End users (non-technical)

---

### API Documentation
**Must Include:**
- API overview
- Authentication guide
- Endpoint descriptions
- Request/response examples
- Error codes and messages
- Rate limiting information
- Code samples in multiple languages
- Changelog

**Format:** OpenAPI/Swagger preferred
**Audience:** Developers

---

### Technical Guides
**Must Include:**
- Architecture overview
- Setup and configuration
- Integration instructions
- Code examples
- Best practices
- Performance considerations
- Security guidelines

**Audience:** Developers, DevOps

---

### Administrator Guides
**Must Include:**
- Installation instructions
- Configuration options
- User management
- Backup and recovery
- Monitoring and maintenance
- Security hardening
- Troubleshooting

**Audience:** System administrators

---

### Release Notes
**Must Include:**
- New features
- Bug fixes
- Breaking changes
- Deprecated features
- Upgrade instructions
- Known issues

**Audience:** All users

## Content Organization Rules

### Information Architecture
- Organize by user task/goal
- Group related topics
- Use consistent navigation
- Provide search functionality
- Include breadcrumbs
- Link related content

### Document Structure
- Title (clear and descriptive)
- Overview/Introduction
- Prerequisites
- Main content (logical flow)
- Examples
- Troubleshooting
- Related resources
- Revision history

## Visual Guidelines

### Screenshots
- Crop to relevant area
- Highlight important elements
- Use annotations sparingly
- Keep resolution appropriate
- Update when UI changes
- Include alt text for accessibility

### Diagrams
- Use standard notation (UML, BPMN, etc.)
- Include legend if needed
- Keep simple and focused
- Use consistent styling
- Provide alt text descriptions
- Export in web-friendly formats (SVG, PNG)

### Code Examples
- Test all code examples
- Include context and explanation
- Use syntax highlighting
- Show both request and response
- Include error handling
- Provide complete, runnable examples
- Comment complex code

## Accuracy and Quality

### Verification Requirements
- Test all procedures personally
- Verify technical accuracy with developers
- Check links are working
- Ensure screenshots match current version
- Validate code examples compile/run
- Review for grammar and spelling
- Check formatting and styling

### Review Process
- Peer review by another writer
- Technical review by subject matter expert
- User testing (if possible)
- Accessibility check
- Final proofread

## Version Control and Updates

### Documentation Versioning
- Match documentation version to product version
- Maintain docs for supported versions
- Archive old version docs
- Clear version indicators
- Update with every release

### Update Triggers
- New features added
- Bug fixes that change behavior
- UI changes
- API changes
- Security updates
- User feedback requests

### Update Frequency
- Critical updates: Immediate
- Feature documentation: With release
- Minor corrections: Weekly batch
- Comprehensive review: Quarterly

## Accessibility Standards

### Must Comply With
- WCAG 2.1 AA standards
- Alt text for all images
- Proper heading hierarchy
- Sufficient color contrast
- Keyboard navigation support
- Screen reader compatibility
- Captions for videos
- Transcripts for audio

## Tools and Formats

### Preferred Formats
- Markdown (.md) for simple docs
- HTML for web documentation
- PDF for printable guides
- OpenAPI/Swagger for APIs
- DOCX for stakeholder reviews

### Documentation Tools
- Git for version control
- Static site generators (Hugo, Docusaurus)
- API doc tools (Swagger UI, Redoc)
- Diagram tools (Draw.io, Lucidchart)
- Screenshot tools with annotation
- Grammar checkers (Grammarly)

## Constraints
- Cannot publish without technical review
- Must test all procedures before documenting
- Cannot skip version information
- Must maintain consistency across all docs
- Maximum documentation age: 90 days without review
- Response time for urgent updates: 4 hours
- Maximum concurrent documentation projects: 5

