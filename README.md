# 📚 Library Management System (OOP - Final Project)

## 1. Project Background & Objectives
Designing a system to manage a library catalog, members, and the borrowing/returning lifecycle for both physical and digital items.
- **Objective:** Strictly apply Object-Oriented Programming (OOP) principles such as Encapsulation, Inheritance, Polymorphism, and Single Responsibility to avoid creating a "God class".

## 2. Core Entities Decomposition
At this initial stage, the system is designed with independent entities having clear boundary responsibilities:
- **`Book` (to evolve into `LibraryItem`):** Represents library items, storing identifying information (ISBN, Title, Author) and borrowing status.
- **`Member`:** Manages user/member profiles participating in the library system.
- **`Program` (Driver):** The entry point of the application, acting as a prototype test runner for basic entity instantiation.

## 3. Development Roadmap
- **Week 2:** Apply inheritance (`LibraryItem` -> `PhysicalBook`, `EBook`) and implement the `Loan` transaction class.
- **Week 3:** Implement polymorphism via overdue fee calculation, search features, and file persistence (JSON/CSV).
- **Week 4:** Finalize test scenarios (Happy path + Error validation) and complete the written report.
