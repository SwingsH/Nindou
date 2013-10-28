-- phpMyAdmin SQL Dump
-- version 4.0.4.1
-- http://www.phpmyadmin.net
--
-- 主機: 127.0.0.1
-- 產生日期: 2013 �?10 ??22 ??14:01
-- 伺服器版本: 5.6.11
-- PHP 版本: 5.5.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- 資料庫: `nindou_mobile`
--
CREATE DATABASE IF NOT EXISTS `nindou_mobile` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `nindou_mobile`;

-- --------------------------------------------------------

--
-- 表的結構 `player_account`
--

CREATE TABLE IF NOT EXISTS `player_account` (
  `serial` int(8) NOT NULL AUTO_INCREMENT,
  `unique_device` varchar(64) NOT NULL,
  `current_login_session` varchar(16) NOT NULL,
  `player_name` varchar(32) NOT NULL,
  `max_action_point` smallint(6) NOT NULL,
  `current_action_point` smallint(6) NOT NULL,
  `current_inherit_id` varchar(16) NOT NULL,
  `current_inherit_password` varchar(16) NOT NULL,
  `create_time` timestamp NULL DEFAULT NULL,
  `last_login_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`serial`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=43 ;

--
-- 轉存資料表中的資料 `player_account`
--

INSERT INTO `player_account` (`serial`, `unique_device`, `current_login_session`, `player_name`, `max_action_point`, `current_action_point`, `current_inherit_id`, `current_inherit_password`, `create_time`, `last_login_time`) VALUES
(19, 'zzzzzzzzz', 'c36bfff267f6073b', '如果這不是遊戲 HAHA', 0, 0, 'zduUT56200', '', '2013-10-13 15:41:45', '2013-10-13 15:41:45'),
(21, '9cbefdac', 'd146519f8b2adc99', '那甚麼還叫遊戲', 0, 0, 'zduUT56200', '', '2013-10-14 04:58:22', '2013-10-14 04:58:22'),
(36, 'd73596ffe78e429d27a362534e9242196745c14a', '442f11a552f31ae3', 'test1234', 0, 0, 'zduUT56200', '', '2013-10-21 06:32:13', '2013-10-21 06:32:13'),
(37, 'b8da7a1425e7cf9e73c296728e0ac4c9008eec67', 'cf010ce934dd2ba8', 'Ucc', 0, 0, 'zduUT56200', '', '2013-10-21 06:33:10', '2013-10-21 06:33:10'),
(38, '06f4499e3e970b42887b08a86bfd579c0605d371', '3caf64f6830cedc7', '上面的', 0, 0, 'zduUT56200', '', '2013-10-21 06:35:14', '2013-10-21 06:35:14'),
(39, 'f804a8d35d390238eff7f3e59d9a8b0f', '0bdfded98aa97acd', '我哈', 0, 0, 'zduUT56200', '', '2013-10-21 06:38:05', '2013-10-21 06:38:05'),
(41, 'a36ec54e961ee79e8d92247f8a081b47a4c52e55', 'fea67472dd9795ce', '11233', 0, 0, 'zduUT56200', '', '2013-10-21 16:01:51', '2013-10-21 16:01:51'),
(42, '91bfbe35ac320facc0d6a74a7b6ec78a', 'f520c710c1bd5372', '滋茲', 0, 0, 'zduUT56200', '', '2013-10-22 09:21:43', '2013-10-22 09:21:43');

-- --------------------------------------------------------

--
-- 表的結構 `system_log`
--

CREATE TABLE IF NOT EXISTS `system_log` (
  `serial` int(11) NOT NULL AUTO_INCREMENT,
  `log_message` varchar(512) NOT NULL,
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`serial`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=346 ;

--
-- 轉存資料表中的資料 `system_log`
--

INSERT INTO `system_log` (`serial`, `log_message`, `time`) VALUES
(165, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 12:29:18'),
(166, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 12:30:50'),
(167, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 12:38:34'),
(168, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 12:38:56'),
(169, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 12:39:02'),
(170, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "a36ec54e961ee79e8d92247f8a081b47a4c52e55" , "17f989bf687dcf77551b9445e8021235ecce0332" , "zduUT56200" , "" , "uouo"  ) ', '2013-10-20 12:39:02'),
(171, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 12:40:00'),
(172, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 12:40:02'),
(173, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "a36ec54e961ee79e8d92247f8a081b47a4c52e55" , "942be5415f0afb91cc451b92e2b7c0a7c80a0afc" , "zduUT56200" , "" , "gr3g3g"  ) ', '2013-10-20 12:40:03'),
(174, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 13:48:25'),
(175, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 13:48:55'),
(176, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 13:52:00'),
(177, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 13:52:17'),
(178, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 13:53:02'),
(179, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 13:53:40'),
(180, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 14:02:57'),
(181, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 14:19:17'),
(182, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 14:40:35'),
(183, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 14:42:21'),
(184, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:06:32'),
(185, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:06:52'),
(186, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:07:33'),
(187, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:12:04'),
(188, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:14:21'),
(189, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:20:53'),
(190, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:22:56'),
(191, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:24:13'),
(192, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:25:17'),
(193, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:26:52'),
(194, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:28:57'),
(195, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:29:28'),
(196, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:31:59'),
(197, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:38:48'),
(198, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:40:45'),
(199, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:43:52'),
(200, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:48:34'),
(201, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:54:55'),
(202, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:57:30'),
(203, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 15:59:09'),
(204, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 16:02:20'),
(205, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 16:39:54'),
(206, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 16:40:50'),
(207, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:33:19'),
(208, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:33:38'),
(209, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:33:42'),
(210, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:34:40'),
(211, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:34:46'),
(212, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:35:16'),
(213, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:35:46'),
(214, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:37:27'),
(215, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:37:48'),
(216, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:38:08'),
(217, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "a36ec54e961ee79e8d92247f8a081b47a4c52e55" , "f6d7835d36cde2ed6a21324d7bb47400d2899fbb" , "zduUT56200" , "" , "h中文"  ) ', '2013-10-20 20:38:08'),
(218, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:39:23'),
(219, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:42:55'),
(220, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:43:33'),
(221, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:47:58'),
(222, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:50:34'),
(223, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:52:38'),
(224, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 20:54:35'),
(225, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:03:23'),
(226, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:14:19'),
(227, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:16:10'),
(228, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:18:22'),
(229, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:19:02'),
(230, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:19:58'),
(231, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:27:07'),
(232, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:27:36'),
(233, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:28:53'),
(234, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:29:44'),
(235, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:32:32'),
(236, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:33:02'),
(237, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:40:41'),
(238, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:42:05'),
(239, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:43:29'),
(240, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:44:13'),
(241, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:45:24'),
(242, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:46:04'),
(243, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:47:48'),
(244, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:49:06'),
(245, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:57:22'),
(246, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 21:58:21'),
(247, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:05:06'),
(248, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:15:02'),
(249, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:22:37'),
(250, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:23:28'),
(251, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:24:19'),
(252, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:27:52'),
(253, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:28:18'),
(254, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:49:13'),
(255, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:49:32'),
(256, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:52:22'),
(257, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:54:29'),
(258, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 22:54:52'),
(259, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 23:28:05'),
(260, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 23:28:33'),
(261, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 23:28:39'),
(262, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 23:29:20'),
(263, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 23:29:45'),
(264, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 23:29:57'),
(265, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "a36ec54e961ee79e8d92247f8a081b47a4c52e55" , "37aa59d82328e366a7e3897e2faec1378b967686" , "zduUT56200" , "" , "yeahhh..."  ) ', '2013-10-20 23:29:57'),
(266, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-20 23:30:50'),
(267, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 03:34:40'),
(268, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 03:34:43'),
(269, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 04:03:46'),
(270, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 04:20:26'),
(271, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 04:28:56'),
(272, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 05:50:27'),
(273, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 05:50:56'),
(274, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 05:51:36'),
(275, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "a36ec54e961ee79e8d92247f8a081b47a4c52e55" , "33b7b0e02437bc31c6f908a946580e876e03a034" , "zduUT56200" , "" , "哈哈@FBB"  ) ', '2013-10-21 05:51:36'),
(276, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 05:51:57'),
(277, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 05:52:17'),
(278, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 06:03:05'),
(279, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 06:03:40'),
(280, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 06:11:59'),
(281, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 06:12:18'),
(282, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "a36ec54e961ee79e8d92247f8a081b47a4c52e55" , "4196f10d0fb1e1acc695d4eab455931036002a22" , "zduUT56200" , "" , "im what"  ) ', '2013-10-21 06:12:18'),
(283, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 06:20:08'),
(284, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 06:32:06'),
(285, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 06:32:13'),
(286, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "d73596ffe78e429d27a362534e9242196745c14a" , "f49b1f277f6fe6c839349434d4fb1a33832844a8" , "zduUT56200" , "" , "test1234"  ) ', '2013-10-21 06:32:13'),
(287, ' SELECT * FROM player_account WHERE unique_device =   ''b8da7a1425e7cf9e73c296728e0ac4c9008eec67'' ', '2013-10-21 06:33:04'),
(288, ' SELECT * FROM player_account WHERE unique_device =   ''b8da7a1425e7cf9e73c296728e0ac4c9008eec67'' ', '2013-10-21 06:33:09'),
(289, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "b8da7a1425e7cf9e73c296728e0ac4c9008eec67" , "cf010ce934dd2ba89ee0a51568e8af360037304d" , "zduUT56200" , "" , "Ucc"  ) ', '2013-10-21 06:33:09'),
(290, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 06:33:11'),
(291, ' SELECT * FROM player_account WHERE unique_device =   ''06f4499e3e970b42887b08a86bfd579c0605d371'' ', '2013-10-21 06:34:39'),
(292, ' SELECT * FROM player_account WHERE unique_device =   ''06f4499e3e970b42887b08a86bfd579c0605d371'' ', '2013-10-21 06:35:14'),
(293, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "06f4499e3e970b42887b08a86bfd579c0605d371" , "c892c3bc5ab58a1173095f5ac282d0678fba1f5b" , "zduUT56200" , "" , "上面的"  ) ', '2013-10-21 06:35:14'),
(294, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-21 06:37:34'),
(295, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-21 06:38:05'),
(296, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "f804a8d35d390238eff7f3e59d9a8b0f" , "9d0a22252536f203fc68437be7faf5aa9dc45c29" , "zduUT56200" , "" , "我哈"  ) ', '2013-10-21 06:38:05'),
(297, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-21 06:38:54'),
(298, ' SELECT * FROM player_account WHERE unique_device =   ''06f4499e3e970b42887b08a86bfd579c0605d371'' ', '2013-10-21 06:39:35'),
(299, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 07:04:33'),
(300, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-21 07:53:24'),
(301, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-21 08:01:05'),
(302, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 08:56:02'),
(303, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 08:58:11'),
(304, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:03:23'),
(305, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:04:45'),
(306, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:06:02'),
(307, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:07:37'),
(308, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:09:29'),
(309, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:10:18'),
(310, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:12:24'),
(311, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:13:13'),
(312, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:15:28'),
(313, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:18:23'),
(314, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:18:58'),
(315, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-21 09:22:15'),
(316, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:23:42'),
(317, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:28:06'),
(318, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:30:34'),
(319, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:32:34'),
(320, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-21 09:40:01'),
(321, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:41:33'),
(322, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 09:47:25'),
(323, ' SELECT * FROM player_account WHERE unique_device =   ''d73596ffe78e429d27a362534e9242196745c14a'' ', '2013-10-21 10:02:17'),
(324, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-21 10:03:33'),
(325, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-21 10:07:50'),
(326, ' SELECT * FROM player_account WHERE unique_device =   ''06f4499e3e970b42887b08a86bfd579c0605d371'' ', '2013-10-21 10:36:03'),
(327, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 10:43:28'),
(328, ' SELECT * FROM player_account WHERE unique_device =   ''06f4499e3e970b42887b08a86bfd579c0605d371'' ', '2013-10-21 10:57:09'),
(329, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 10:58:49'),
(330, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 10:59:19'),
(331, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 10:59:27'),
(332, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "a36ec54e961ee79e8d92247f8a081b47a4c52e55" , "902b7e31bfc4b5cd7dfad7d069212809c92a202a" , "zduUT56200" , "" , "哈哈哈 "  ) ', '2013-10-21 10:59:27'),
(333, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 15:56:01'),
(334, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 15:56:30'),
(335, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 16:00:38'),
(336, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 16:01:44'),
(337, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-21 16:01:51'),
(338, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "a36ec54e961ee79e8d92247f8a081b47a4c52e55" , "fea67472dd9795cebed7b835679ea14f5a373f3f" , "zduUT56200" , "" , "11233"  ) ', '2013-10-21 16:01:51'),
(339, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-22 08:51:10'),
(340, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-22 08:51:51'),
(341, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-22 08:58:17'),
(342, ' SELECT * FROM player_account WHERE unique_device =   ''f804a8d35d390238eff7f3e59d9a8b0f'' ', '2013-10-22 09:11:45'),
(343, ' SELECT * FROM player_account WHERE unique_device =   ''91bfbe35ac320facc0d6a74a7b6ec78a'' ', '2013-10-22 09:21:23'),
(344, ' SELECT * FROM player_account WHERE unique_device =   ''91bfbe35ac320facc0d6a74a7b6ec78a'' ', '2013-10-22 09:21:43'),
(345, 'INSERT INTO player_account (unique_device , current_login_session , current_inherit_id , current_inherit_password , player_name)  VALUES (  "91bfbe35ac320facc0d6a74a7b6ec78a" , "f520c710c1bd537289c3948b502b7c4837afc9bc" , "zduUT56200" , "" , "滋茲"  ) ', '2013-10-22 09:21:43');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
