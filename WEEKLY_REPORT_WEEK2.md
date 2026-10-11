# 📊 WEEK 2 PROGRESS REPORT: CORE CLASSES & INVARIANTS

* **Course:** Object-Oriented Programming (OOP) — Semester 1, 2026–2027
* **Instructor:** Huỳnh Xuân Phụng
* **Project Title:** Library Management System
* **Group Repository:** [https://github.com/Moitapcod/LibraryManagementSystem.git](https://github.com/Moitapcod/LibraryManagementSystem.git)

---

## 📋 TABLE OF CONTENTS
- [1. SHARED SECTION (IMPLEMENTATION & PROGRESS)](#1-shared-section-implementation--progress)
  - [1.1. Core Classes & Inheritance Hierarchy Implementation](#11-core-classes--inheritance-hierarchy-implementation)
  - [1.2. Runtime Invariants & Verification](#12-runtime-invariants--verification)
  - [1.3. Critical AI Review (Over-Engineering Detection)](#13-critical-ai-review-over-engineering-detection)
  - [1.4. AI Usage Log (Week 2 Prompts & Verification)](#14-ai-usage-log-week-2-prompts--verification)
- [2. INDIVIDUAL TEACH-BACK SECTION](#2-individual-teach-back-section)
  - [2.1. Encapsulation & Protected State Breakdown](#21-encapsulation--protected-state-breakdown)
  - [2.2. Impact Analysis: What breaks if fields are made public?](#22-impact-analysis-what-breaks-if-fields-are-made-public)
  - [2.3. Executed Test Output & Proofs](#23-executed-test-output--proofs)

---

## 1. SHARED SECTION (IMPLEMENTATION & PROGRESS)

### 1.1. Core Classes & Inheritance Hierarchy Implementation
In Week 2, the core domain model was implemented and pushed to the GitHub repository:
* **GitHub Repository:** [https://github.com/Moitapcod/LibraryManagementSystem.git](https://github.com/Moitapcod/LibraryManagementSystem.git)
* **Core Classes Created:** `Book.cs`, `Member.cs`, and `Program.cs`.
* **State Management:** Established proper constructors, backing fields, and encapsulated properties across all domain entities.

---

### 1.2. Runtime Invariants & Verification
The codebase enforces two critical runtime business invariants:
1. **Invariant 1 (Max Borrowing Quota):** A `Member` instance can never hold more active `Loan` records than their assigned quota limit (`MaxAllowedItems`, default = 3).
2. **Invariant 2 (Valid State Transition):** A book marked as borrowed (`IsBorrowed = true`) cannot be checked out a second time until properly returned, preventing double-booking state corruption.

---

### 1.3. Critical AI Review (Over-Engineering Detection)
* **AI Suggested Design:** When prompted for borrowing rule architectures, the AI suggested generating a `BorrowingRulesEngineFactory` coupled with the Strategy and Abstract Factory patterns.
* **Flaw / Over-engineering Identified:** The AI solution created 5 extra classes and interfaces for a simple quota check, obscuring core domain logic and making the system over-engineered.
* **Refactoring Decision:** Rejected the AI suggestion. Kept the validation logic directly encapsulated inside domain methods (`Member.BorrowItem()`), respecting the Single Responsibility Principle while maintaining high code readability.

---

### 1.4. AI Usage Log (Week 2 Prompts & Verification)
* **Tool:** Gemini / ChatGPT
* **Prompt:** `how to write a readme for library management c# with class diagram`
* **Output Summary:** Suggested C# code snippets for exception handling and markdown formatting templates.
* **Verification Method:** Manually reviewed and tested the code in Visual Studio, ensuring exceptions are thrown properly when invariant limits are breached.

---

## 2. INDIVIDUAL TEACH-BACK SECTION

### 2.1. Encapsulation & Protected State Breakdown

Encapsulation protects the core entities as follows:

1. **`Member` Class:**
   * Holds member identity (`MemberId`, `Name`).
   * Internal loan state is stored privately. Modifying active loans must be performed via dedicated methods (`BorrowItem()`), ensuring quota validations are always triggered.

2. **`Book` / `LibraryItem` Class:**
   * Protects core metadata (`ISBN`, `Title`, `Author`).
   * The `IsBorrowed` state flag is managed internally through checkout/return operations rather than direct public mutation.

---

### 2.2. Impact Analysis: What breaks if fields are made public?

If field access modifiers were made `public`:

* **Violation of Quota Invariant:**
  If an active loans collection were `public`, external code could bypass validation and call `.Add()` directly:
  ```csharp
  // Dangerous bypass if active loans list is public:
  member.ActiveLoans.Add(new Loan(book4)); // Quota of 3 is breached without warning!
  ```
* **Corrupted Inventory State:**
  If `IsBorrowed` had a public setter, any caller could run `book.IsBorrowed = false;` without recording return dates or calculating late fees, leading to mismatched inventory records.

---

### 2.3. Executed Test Output & Proofs

Console test output executed in `Program.cs`:

<img width="1447" height="417" alt="image" src="https://github.com/user-attachments/assets/50c3c9ba-af8c-4291-960f-0ae779c59d89" />
