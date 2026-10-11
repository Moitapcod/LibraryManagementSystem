# 📊 WEEK 1 PROGRESS REPORT: ANALYZE & DESIGN

* **Course:** Object-Oriented Programming (OOP) — Semester 1, 2026–2027
* **Instructor:** Huỳnh Xuân Phụng
* **Project Title:** Library Management System
* **Group Repository:** [https://github.com/Moitapcod/LibraryManagementSystem.git](https://github.com/Moitapcod/LibraryManagementSystem.git)

---

## 📋 TABLE OF CONTENTS
- [1. SHARED SECTION (GROUP PROGRESS & ARCHITECTURE DESIGN)](#1-shared-section-group-progress--architecture-design)
  - [1.1. Domain Analysis in Own Words](#11-domain-analysis-in-own-words)
  - [1.2. Requirements to Candidate Classes (CRC Cards)](#12-requirements-to-candidate-classes-crc-cards)
  - [1.3. Draft Class Diagram (UML)](#13-draft-class-diagram-uml)
  - [1.4. Planned OOP Principles & Design Justification](#14-planned-oop-principles--design-justification)
  - [1.5. Initial Test Plan](#15-initial-test-plan)
  - [1.6. Syllabus Mapping (Appendix A)](#16-syllabus-mapping-appendix-a)
  - [1.7. AI Usage Log (Week 1 Brainstorming)](#17-ai-usage-log-week-1-brainstorming)
- [2. INDIVIDUAL TEACH-BACK SECTION](#2-individual-teach-back-section)
  - [2.1. Class Sketch & Decision Analysis](#21-class-sketch--decision-analysis)
  - [2.2. Inheritance vs. Composition Rationale](#22-inheritance-vs-composition-rationale)

---

## 1. SHARED SECTION (GROUP PROGRESS & ARCHITECTURE DESIGN)

### 1.1. Domain Analysis in Own Words
A modern library system manages two primary types of catalog items with distinct operational lifecycles:
1. **PhysicalBook:** Represents physical copies with finite inventory, assigned shelf locations, mandatory due dates, and daily monetary overdue fines if returned late.
2. **EBook:** Represents digital publications accessible via digital download or streaming. They do not occupy physical shelf space and **never incur overdue fines** because access expires automatically upon loan termination.
3. **Member:** Borrowers who hold active loan limits (e.g., maximum 3 items simultaneously).
4. **Loan:** Tracks the transaction lifecycle (borrow date, target due date, actual return date, and active state).

---

### 1.2. Requirements to Candidate Classes (CRC Cards)

* **CRC Card 1: `LibraryItem` (Abstract Base Class)**
  * **Responsibilities:** Holds core metadata (ID, Title, Author, ISBN); defines abstract signature `CalculateOverdueFee(int daysLate)`.
  * **Collaborators:** None.
* **CRC Card 2: `PhysicalBook` & `EBook` (Subclasses)**
  * **Responsibilities:** `PhysicalBook` stores shelf placement and calculates daily late fees. `EBook` stores file size in MB and returns a fee of 0.
  * **Collaborators:** Inherits from `LibraryItem`.
* **CRC Card 3: `Member`**
  * **Responsibilities:** Stores profile information, tracks active `Loan` instances, enforces borrowing limits.
  * **Collaborators:** `Loan`.
* **CRC Card 4: `Loan`**
  * **Responsibilities:** Captures checkout timestamps, due dates, return timestamps, and checks late status.
  * **Collaborators:** `Member`, `LibraryItem`.

---

### 1.3. Draft Class Diagram (UML)

<img width="552" height="841" alt="image" src="https://github.com/user-attachments/assets/55f453f6-1267-4719-b5aa-0f3ef0f6417b" />

---

### 1.4. Planned OOP Principles & Design Justification
* **Encapsulation:** Hide internal mutable state (`private` active loan lists, private setters for dates) to guarantee invariants cannot be broken by caller code.
* **Inheritance:** `LibraryItem` provides a clean base abstraction. `PhysicalBook` and `EBook` inherit core properties, avoiding duplicated code.
* **Polymorphism:** `CalculateOverdueFee()` provides polymorphic behavior without requiring procedural type checks (`if-else` or `is/as`).
* **Single Responsibility Principle (SRP):** Entities are cleanly segregated (`LibraryItem` handles catalog data, `Member` handles user limits, `Loan` manages lifecycle). No single "God Class" exists.

---

### 1.5. Initial Test Plan
1. **Happy Path:** Valid member borrows a `PhysicalBook` -> `Loan` created -> Returned on time -> Overdue fee is 0.
2. **Error / Boundary Validation:** Member attempts to borrow beyond their quota limit (e.g., > 3 items) -> System rejects transaction with exception.
3. **Polymorphic Behavior:** Test 5-day overdue calculation on `PhysicalBook` (returns fee) vs `EBook` (returns 0).

---

### 1.6. Syllabus Mapping (Appendix A)
* **Mapped Concepts:** Classes & Encapsulation, Inheritance & Abstract Classes, Polymorphism, Collections (`List<T>`), File I/O (JSON/CSV).

---

### 1.7. AI Usage Log (Week 1 Brainstorming)
* **Tool:** Gemini / ChatGPT
* **Prompt:** `how to write a readme for library management c# with class diagram`
* **Output Summary:** AI produced a standard markdown template with placeholder class diagrams.
* **Verification & Modification:** Refactored the diagram manually to fit the project's exact requirements (`PhysicalBook` vs `EBook` distinction) and wrote the C# core code manually.

---

## 2. INDIVIDUAL TEACH-BACK SECTION

### 2.1. Class Sketch & Decision Analysis
* **Initial Prototype Setup:** Created `Book.cs`, `Member.cs`, and `Program.cs`.
* **Design Rationale:** Starting with a simple prototype allowed testing basic instantiation and property assignments before refactoring `Book` into an abstract `LibraryItem` hierarchy in Week 2.

### 2.2. Inheritance vs. Composition Rationale
* **`LibraryItem` -> `PhysicalBook` / `EBook`:** Uses **Inheritance** because it represents an "IS-A" relationship (`PhysicalBook` IS A `LibraryItem`).
* **`Member` -> `Loan`:** Uses **Composition / Aggregation** because a member "HAS" loans (`Member` HAS-A list of `Loan` instances). A member is not a subtype of a loan transaction.
