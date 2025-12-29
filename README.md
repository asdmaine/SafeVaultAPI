# SafeVault - Secure API Capstone

This project demonstrates a secure backend API with robust defenses against common OWASP vulnerabilities.

## 🛡️ Vulnerability Report & Fixes

### 1. SQL Injection (SQLi)
* **Vulnerability:** Initial database queries relied on string concatenation, allowing attackers to manipulate queries.
* **Fix:** Implemented Entity Framework Core with LINQ. This ensures all database queries are **parameterized** by default, neutralizing injection attempts.

### 2. Cross-Site Scripting (XSS)
* **Vulnerability:** User input was echoed back to the client without sanitation.
* **Fix:** 1. Added strict **Input Validation** (`[RegularExpression]`) to the DTOs to reject scripts.
    2. Applied **HTML Encoding** on the server side before storing content.

### 3. Broken Access Control
* **Vulnerability:** Sensitive endpoints were public.
* **Fix:** Implemented **Role-Based Access Control (RBAC)** using the `[Authorize(Roles = "Admin")]` attribute, ensuring only privileged users can access the full vault.

---

## 🤖 Copilot Reflection
**How Microsoft Copilot Assisted in Development:**

* **Generating Secure Regex:** I used Copilot to generate the Regular Expression for the `Title` field. I prompted: *"Create a Regex that allows only alphanumeric characters and spaces to prevent injection."* It provided the secure pattern used in `SecureNoteDto.cs`.
* **Writing Unit Tests:** To ensure my validation was working, I asked Copilot: *"Write an xUnit test that checks if a model with XSS tags fails validation."* It generated the `SecurityTests.cs` file, which helped me confirm that my defenses were active.
* **Debugging RBAC:** I encountered a 403 Forbidden error during testing. Copilot explained that I needed to ensure my JWT token claims matched the specific Role string expected by the `[Authorize]` attribute.