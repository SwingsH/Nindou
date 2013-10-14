-- phpMyAdmin SQL Dump
-- version 4.0.4.1
-- http://www.phpmyadmin.net
--
-- 主機: 127.0.0.1
-- 產生日期: 2013 �?10 ??14 ??05:59
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

DROP TABLE IF EXISTS `player_account`;
CREATE TABLE IF NOT EXISTS `player_account` (
  `serial` int(8) NOT NULL AUTO_INCREMENT,
  `unique_device` varchar(64) NOT NULL,
  `current_login_session` varchar(16) NOT NULL,
  `player_name` varchar(32) NOT NULL,
  `max_action_point` smallint(6) NOT NULL,
  `current_action_point` smallint(6) NOT NULL,
  `current_inherit_id` varchar(16) NOT NULL,
  `current_inherit_password` varchar(16) NOT NULL,
  `create_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `last_login_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`serial`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=20 ;

--
-- 轉存資料表中的資料 `player_account`
--

INSERT INTO `player_account` (`serial`, `unique_device`, `current_login_session`, `player_name`, `max_action_point`, `current_action_point`, `current_inherit_id`, `current_inherit_password`, `create_time`, `last_login_time`) VALUES
(19, 'a36ec54e961ee79e8d92247f8a081b47a4c52e55', 'a8ee369a716e12e6', '', 0, 0, 'zduUT56200', '', '2013-10-13 23:41:45', '2013-10-13 23:41:45');

-- --------------------------------------------------------

--
-- 表的結構 `system_log`
--

DROP TABLE IF EXISTS `system_log`;
CREATE TABLE IF NOT EXISTS `system_log` (
  `serial` int(11) NOT NULL AUTO_INCREMENT,
  `log_message` varchar(512) NOT NULL,
  `time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`serial`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=5 ;

--
-- 轉存資料表中的資料 `system_log`
--

INSERT INTO `system_log` (`serial`, `log_message`, `time`) VALUES
(2, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-13 23:39:12'),
(3, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-13 23:41:45'),
(4, ' SELECT * FROM player_account WHERE unique_device =   ''a36ec54e961ee79e8d92247f8a081b47a4c52e55'' ', '2013-10-13 23:41:55');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
