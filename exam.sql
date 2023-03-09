-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 07, 2023 at 03:29 PM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `exam`
--

-- --------------------------------------------------------

--
-- Table structure for table `exam`
--

CREATE TABLE `exam` (
  `ExamID` int(11) NOT NULL,
  `ExamName` varchar(255) NOT NULL,
  `TeacherID` varchar(5) NOT NULL,
  `ExamDate` date DEFAULT NULL,
  `StartTime` time DEFAULT NULL,
  `Duration` int(11) DEFAULT NULL,
  `Randomized` tinyint(1) NOT NULL,
  `QuestionCount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `exam`
--

INSERT INTO `exam` (`ExamID`, `ExamName`, `TeacherID`, `ExamDate`, `StartTime`, `Duration`, `Randomized`, `QuestionCount`) VALUES
(1, 'Math Exam', 'T001', '2023-05-15', '09:00:00', 120, 1, 10),
(2, 'Science Exam', 'T002', '2023-05-20', '10:30:00', 90, 0, 5),
(3, 'History Exam', 'T003', '2023-05-25', '11:00:00', 180, 1, 0),
(8, 'Test7', 'T006', '2023-05-15', NULL, 30, 1, 4),
(9, 'Test_9', 'T001', '2023-02-13', NULL, 4, 1, 12),
(27, 'Test13', 'T004', NULL, NULL, 10, 1, 10),
(30, 'Test15', 'T001', '2023-02-17', '16:40:00', 25, 1, 3),
(31, '', 'T001', '2023-07-23', '11:00:00', 34, 1, 3),
(33, 'Test17', 'T001', '2023-03-09', '12:00:00', 12, 1, 3),
(34, 'Test16', 'T001', '2023-03-24', '14:00:00', 10, 1, 2),
(35, 'Test20', 'T001', '2023-02-05', '08:00:00', 15, 0, 2),
(36, 'Test21', 'T007', '2023-02-08', '14:00:00', 10, 1, 2),
(37, 'Test22', 'T008', '2023-02-08', '11:00:00', 10, 0, 2);

-- --------------------------------------------------------

--
-- Table structure for table `examresult`
--

CREATE TABLE `examresult` (
  `ResultID` int(11) NOT NULL,
  `StudentID` varchar(5) NOT NULL,
  `ExamID` int(11) NOT NULL,
  `Grade` float NOT NULL,
  `Errors` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `examresult`
--

INSERT INTO `examresult` (`ResultID`, `StudentID`, `ExamID`, `Grade`, `Errors`) VALUES
(1, 'S001', 1, 85.5, 'Question 2 was incorrect'),
(2, 'S002', 1, 90, NULL),
(3, 'S003', 2, 75, 'Question 4 and 5 were incorrect'),
(4, 'S001', 3, 95.5, NULL),
(5, 'S001', 1, 10, NULL),
(6, 'S001', 27, 10, NULL),
(7, 'S001', 27, 50, NULL),
(8, 'S001', 27, 50, NULL),
(9, 'S001', 27, 0, NULL),
(10, 'S001', 27, 0, NULL),
(11, 'S001', 27, 0, NULL),
(12, 'S002', 30, 100, NULL),
(13, 'S002', 30, 66.6667, '(1:Berlin),'),
(16, 'S002', 30, 33.3333, '(0:Berlin),(2:Italy),'),
(17, 'S001', 35, 50, '(11:USD),'),
(18, 'S008', 37, 50, '(11:Asia ),'),
(19, 'S001', 37, 50, '(01:Venus),'),
(20, 'S001', 30, 66.6667, '(2:Sydney),');

-- --------------------------------------------------------

--
-- Table structure for table `question`
--

CREATE TABLE `question` (
  `QuestionID` int(11) NOT NULL,
  `ExamID` int(11) NOT NULL,
  `QuestionText` text NOT NULL,
  `Option1` varchar(255) NOT NULL,
  `Option2` varchar(255) NOT NULL,
  `Option3` varchar(255) NOT NULL,
  `Option4` varchar(255) NOT NULL,
  `CorrectOption` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `question`
--

INSERT INTO `question` (`QuestionID`, `ExamID`, `QuestionText`, `Option1`, `Option2`, `Option3`, `Option4`, `CorrectOption`) VALUES
(1, 1, 'What is the value of pi?', '3.1415', '3.142', '3.14159', '3.141592', 3),
(2, 1, 'What is the square root of 144?', '12', '10', '14', '16', 1),
(3, 2, 'Who discovered penicillin?', 'Alexander Fleming', 'Isaac Newton', 'Albert Einstein', 'Marie Curie', 1),
(4, 2, 'What is the largest planet in the solar system?', 'Mars', 'Venus', 'Saturn', 'Jupiter', 4),
(5, 3, 'What was the first successful airplane?', 'Boeing 747', 'Wright Flyer', 'Airbus A380', 'Concorde', 2);

-- --------------------------------------------------------

--
-- Table structure for table `student`
--

CREATE TABLE `student` (
  `StudentID` varchar(5) NOT NULL,
  `StudentName` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `student`
--

INSERT INTO `student` (`StudentID`, `StudentName`) VALUES
('S001', 'Emily Williams'),
('S002', 'David Lee'),
('S003', 'Sarah Johnson'),
('S004', 'Test1'),
('S005', 'Heshan'),
('S008', 'Smith');

-- --------------------------------------------------------

--
-- Table structure for table `teacher`
--

CREATE TABLE `teacher` (
  `TeacherID` varchar(5) NOT NULL,
  `TeacherName` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `teacher`
--

INSERT INTO `teacher` (`TeacherID`, `TeacherName`) VALUES
('T001', 'John Smith'),
('T002', 'Jane Doe'),
('T003', 'Bob Johnson'),
('T004', 'string'),
('T005', 'John Smith'),
('T006', 'John Smith'),
('T007', 'jane'),
('T008', 'John');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `exam`
--
ALTER TABLE `exam`
  ADD PRIMARY KEY (`ExamID`),
  ADD KEY `TeacherID` (`TeacherID`);

--
-- Indexes for table `examresult`
--
ALTER TABLE `examresult`
  ADD PRIMARY KEY (`ResultID`),
  ADD KEY `StudentID` (`StudentID`),
  ADD KEY `ExamID` (`ExamID`);

--
-- Indexes for table `question`
--
ALTER TABLE `question`
  ADD PRIMARY KEY (`QuestionID`),
  ADD KEY `ExamID` (`ExamID`);

--
-- Indexes for table `student`
--
ALTER TABLE `student`
  ADD PRIMARY KEY (`StudentID`);

--
-- Indexes for table `teacher`
--
ALTER TABLE `teacher`
  ADD PRIMARY KEY (`TeacherID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `exam`
--
ALTER TABLE `exam`
  MODIFY `ExamID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- AUTO_INCREMENT for table `examresult`
--
ALTER TABLE `examresult`
  MODIFY `ResultID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `question`
--
ALTER TABLE `question`
  MODIFY `QuestionID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `exam`
--
ALTER TABLE `exam`
  ADD CONSTRAINT `exam_ibfk_1` FOREIGN KEY (`TeacherID`) REFERENCES `teacher` (`TeacherID`);

--
-- Constraints for table `examresult`
--
ALTER TABLE `examresult`
  ADD CONSTRAINT `examresult_ibfk_1` FOREIGN KEY (`StudentID`) REFERENCES `student` (`StudentID`),
  ADD CONSTRAINT `examresult_ibfk_2` FOREIGN KEY (`ExamID`) REFERENCES `exam` (`ExamID`);

--
-- Constraints for table `question`
--
ALTER TABLE `question`
  ADD CONSTRAINT `question_ibfk_1` FOREIGN KEY (`ExamID`) REFERENCES `exam` (`ExamID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;


+94782894891
dananjaya.nisansale@gmail.com


dont talk about this contact thing in fiver
it is prohibited to share them
