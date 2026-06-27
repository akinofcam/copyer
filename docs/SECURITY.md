# Security Policy

## Reporting a Vulnerability

We take security seriously. If you discover a security vulnerability in Copyer, please report it responsibly.

### How to Report

**Do NOT** create a public GitHub issue for security vulnerabilities.

Instead, please email your report to: **[copyer-security@example.com]**

Include the following information:

- Type of vulnerability
- Location of the vulnerability
- Description of the vulnerability
- Steps to reproduce (if applicable)
- Potential impact
- Suggested fix (optional)

### Response Timeline

- **Initial Response:** Within 24 hours
- **Status Updates:** Every 48 hours
- **Fix Release:** Within 7-14 days (depending on severity)

## Supported Versions

| Version | Status | Support Until |
|---------|--------|----------------|
| 2.x     | Active | Current + 1 year |
| 1.x     | Legacy | 2026-12-31 |

## Security Considerations

### File Operations

Copyer performs local file system operations. When using Copyer:

- **Permissions:** Ensure you have proper permissions for source and destination directories
- **Antivirus:** Some antivirus software may slow down operations; consider whitelisting
- **Verification:** Use `--verify` flag for critical operations to ensure data integrity

### Data Privacy

- **No External Communication:** Copyer performs all operations locally
- **No Data Collection:** We collect no personal information or usage data
- **No Telemetry:** Copyer contains no telemetry or tracking
- **Optional Logging:** Logging is strictly opt-in via `--log` flag

### Dependency Security

Copyer uses well-maintained dependencies:

- **Spectre.Console** - Terminal formatting library
- **Serilog** - Structured logging framework

All dependencies are checked for security vulnerabilities regularly. We use:
- Dependabot for automated dependency updates
- GitHub security alerts
- NuGet package verification

### File Verification

When using `--verify`:

- **SHA256 Hashing** - Industry-standard cryptographic hashing
- **File Size Comparison** - Ensures complete file transfer
- **Bit-Accurate Copying** - Verifies every byte

## Secure Usage Guidelines

### Best Practices

1. **Run as Administrator** - Grant proper permissions for sensitive operations
2. **Verify Backups** - Use `--verify` for critical data
3. **Monitor Logs** - Check logs for unusual activity with `--log`
4. **Keep Updated** - Install security updates promptly
5. **Validate Paths** - Double-check source and destination paths

### Unsafe Practices

❌ Don't run with more permissions than necessary
❌ Don't disable antivirus protection
❌ Don't copy untrusted source directories
❌ Don't ignore verification errors
❌ Don't disable logging for critical operations

## Vulnerability History

Copyer maintains a public record of security issues and resolutions:

- [Security Advisories](https://github.com/akinofcam/copyer/security/advisories)
- [Security Alerts](https://github.com/akinofcam/copyer/security)

## Compliance

Copyer is designed to comply with:

- Windows file system security standards
- .NET security best practices
- Industry-standard cryptography (SHA256)
- OWASP guidelines for local tools

## Contact

For security-related questions or concerns:

**Email:** copyer-security@example.com  
**GitHub:** https://github.com/akinofcam/copyer/security

---

Thank you for helping keep Copyer and our community secure!
