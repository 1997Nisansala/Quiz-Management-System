-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 06, 2023 at 04:01 PM
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
(30, 'Test15', 'T001', '2023-02-17', '16:40:00', 20, 1, 3),
(31, '', 'T001', '2023-07-23', '11:00:00', 34, 1, 3),
(33, 'Test17', 'T001', '2023-03-09', '12:00:00', 12, 1, 3),
(34, 'Test16', 'T001', '2023-03-24', '14:00:00', 10, 1, 2);

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
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `exam`
--
ALTER TABLE `exam`
  MODIFY `ExamID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=35;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `exam`
--
ALTER TABLE `exam`
  ADD CONSTRAINT `exam_ibfk_1` FOREIGN KEY (`TeacherID`) REFERENCES `teacher` (`TeacherID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
