-- phpMyAdmin SQL Dump
-- version 4.0.4.1
-- http://www.phpmyadmin.net
--
-- ä¸»æ©Ÿ: 127.0.0.1
-- ç”¢ç”Ÿæ—¥æœŸ: 2013 å¹?11 ??09 ??09:51
-- ä¼ºæœå™¨ç‰ˆæœ¬: 5.6.11
-- PHP ç‰ˆæœ¬: 5.5.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- è³‡æ–™åº«: `nindou_mobile`
--
CREATE DATABASE IF NOT EXISTS `nindou_mobile` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `nindou_mobile`;

-- --------------------------------------------------------

--
-- è¡¨çš„çµæ§‹ `player_account`
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
  `roleslot_1_rolebox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 1 ¨¤¦â¬O¯Á¤Ş¨ì ¨¤¦âboxªº ­ş¤@­Ó´¡¼Ñ ',
  `roleslot_1_weapon_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 1 ¨¤¦âªº ªZ¾¹ ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_1_skill_1_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 1 ¨¤¦âªº ²Ä M §Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_1_skill_2_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 1 ¨¤¦âªº ²Ä M §Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_1_passiveskill_1_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 1 ¨¤¦âªº ²Ä M ³Q°Ê§Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_1_passiveskill_2_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 1 ¨¤¦âªº ²Ä M ³Q°Ê§Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_2_rolebox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 2 ¨¤¦â¬O¯Á¤Ş¨ì ¨¤¦âboxªº ­ş¤@­Ó´¡¼Ñ ',
  `roleslot_2_weapon_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 2 ¨¤¦âªº ªZ¾¹ ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_2_skill_1_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 2 ¨¤¦âªº ²Ä M §Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_2_skill_2_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 2 ¨¤¦âªº ²Ä M §Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_2_passiveskill_1_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 2 ¨¤¦âªº ²Ä M ³Q°Ê§Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_2_passiveskill_2_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 2 ¨¤¦âªº ²Ä M ³Q°Ê§Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_3_rolebox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 3 ¨¤¦â¬O¯Á¤Ş¨ì ¨¤¦âboxªº ­ş¤@­Ó´¡¼Ñ ',
  `roleslot_3_weapon_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 3 ¨¤¦âªº ªZ¾¹ ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_3_skill_1_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 3 ¨¤¦âªº ²Ä M §Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_3_skill_2_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 3 ¨¤¦âªº ²Ä M §Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_3_passiveskill_1_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 3 ¨¤¦âªº ²Ä M ³Q°Ê§Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `roleslot_3_passiveskill_2_cardbox_index` int(11) NOT NULL COMMENT '¶¤¥î¤¤²Ä 3 ¨¤¦âªº ²Ä M ³Q°Ê§Ş¯à ¬O¯Á¤Ş¨ì cardboxªº ­ş¤@­Ó´¡¼Ñ',
  `rolebox_1_role_id` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 1 ¨¤¦âªº ID',
  `rolebox_1_role_exp` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 1 ¨¤¦âªº Exp',
  `rolebox_2_role_id` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 2 ¨¤¦âªº ID',
  `rolebox_2_role_exp` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 2 ¨¤¦âªº Exp',
  `rolebox_3_role_id` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 3 ¨¤¦âªº ID',
  `rolebox_3_role_exp` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 3 ¨¤¦âªº Exp',
  `rolebox_4_role_id` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 4 ¨¤¦âªº ID',
  `rolebox_4_role_exp` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 4 ¨¤¦âªº Exp',
  `rolebox_5_role_id` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 5 ¨¤¦âªº ID',
  `rolebox_5_role_exp` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 5 ¨¤¦âªº Exp',
  `rolebox_6_role_id` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 6 ¨¤¦âªº ID',
  `rolebox_6_role_exp` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 6 ¨¤¦âªº Exp',
  `rolebox_7_role_id` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 7 ¨¤¦âªº ID',
  `rolebox_7_role_exp` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 7 ¨¤¦âªº Exp',
  `rolebox_8_role_id` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 8 ¨¤¦âªº ID',
  `rolebox_8_role_exp` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 8 ¨¤¦âªº Exp',
  `rolebox_9_role_id` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 9 ¨¤¦âªº ID',
  `rolebox_9_role_exp` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 9 ¨¤¦âªº Exp',
  `rolebox_10_role_id` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 10 ¨¤¦âªº ID',
  `rolebox_10_role_exp` int(11) NOT NULL COMMENT '¤Hª«Àx¦s®æ¤¤index²Ä 10 ¨¤¦âªº Exp',
  `cardbox_1_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 1 ±i¥dªº ID',
  `cardbox_1_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 1 ±i¥dªº Exp',
  `cardbox_2_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 2 ±i¥dªº ID',
  `cardbox_2_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 2 ±i¥dªº Exp',
  `cardbox_3_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 3 ±i¥dªº ID',
  `cardbox_3_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 3 ±i¥dªº Exp',
  `cardbox_4_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 4 ±i¥dªº ID',
  `cardbox_4_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 4 ±i¥dªº Exp',
  `cardbox_5_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 5 ±i¥dªº ID',
  `cardbox_5_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 5 ±i¥dªº Exp',
  `cardbox_6_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 6 ±i¥dªº ID',
  `cardbox_6_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 6 ±i¥dªº Exp',
  `cardbox_7_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 7 ±i¥dªº ID',
  `cardbox_7_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 7 ±i¥dªº Exp',
  `cardbox_8_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 8 ±i¥dªº ID',
  `cardbox_8_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 8 ±i¥dªº Exp',
  `cardbox_9_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 9 ±i¥dªº ID',
  `cardbox_9_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 9 ±i¥dªº Exp',
  `cardbox_10_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 10 ±i¥dªº ID',
  `cardbox_10_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 10 ±i¥dªº Exp',
  `cardbox_11_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 11 ±i¥dªº ID',
  `cardbox_11_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 11 ±i¥dªº Exp',
  `cardbox_12_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 12 ±i¥dªº ID',
  `cardbox_12_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 12 ±i¥dªº Exp',
  `cardbox_13_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 13 ±i¥dªº ID',
  `cardbox_13_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 13 ±i¥dªº Exp',
  `cardbox_14_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 14 ±i¥dªº ID',
  `cardbox_14_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 14 ±i¥dªº Exp',
  `cardbox_15_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 15 ±i¥dªº ID',
  `cardbox_15_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 15 ±i¥dªº Exp',
  `cardbox_16_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 16 ±i¥dªº ID',
  `cardbox_16_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 16 ±i¥dªº Exp',
  `cardbox_17_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 17 ±i¥dªº ID',
  `cardbox_17_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 17 ±i¥dªº Exp',
  `cardbox_18_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 18 ±i¥dªº ID',
  `cardbox_18_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 18 ±i¥dªº Exp',
  `cardbox_19_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 19 ±i¥dªº ID',
  `cardbox_19_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 19 ±i¥dªº Exp',
  `cardbox_20_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 20 ±i¥dªº ID',
  `cardbox_20_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 20 ±i¥dªº Exp',
  `cardbox_21_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 21 ±i¥dªº ID',
  `cardbox_21_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 21 ±i¥dªº Exp',
  `cardbox_22_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 22 ±i¥dªº ID',
  `cardbox_22_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 22 ±i¥dªº Exp',
  `cardbox_23_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 23 ±i¥dªº ID',
  `cardbox_23_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 23 ±i¥dªº Exp',
  `cardbox_24_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 24 ±i¥dªº ID',
  `cardbox_24_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 24 ±i¥dªº Exp',
  `cardbox_25_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 25 ±i¥dªº ID',
  `cardbox_25_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 25 ±i¥dªº Exp',
  `cardbox_26_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 26 ±i¥dªº ID',
  `cardbox_26_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 26 ±i¥dªº Exp',
  `cardbox_27_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 27 ±i¥dªº ID',
  `cardbox_27_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 27 ±i¥dªº Exp',
  `cardbox_28_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 28 ±i¥dªº ID',
  `cardbox_28_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 28 ±i¥dªº Exp',
  `cardbox_29_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 29 ±i¥dªº ID',
  `cardbox_29_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 29 ±i¥dªº Exp',
  `cardbox_30_card_id` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 30 ±i¥dªº ID',
  `cardbox_30_card_exp` int(11) NOT NULL COMMENT '¥d¥]¤¤index²Ä 30 ±i¥dªº Exp',
  `create_time` timestamp NULL DEFAULT NULL,
  `last_login_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`serial`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=64 ;

-- --------------------------------------------------------

--
-- è¡¨çš„çµæ§‹ `player_battle`
--

CREATE TABLE IF NOT EXISTS `player_battle` (
  `account_serial` int(11) NOT NULL COMMENT 'è©²ç©å®¶ åºè™Ÿ',
  `account_battle_id` int(11) NOT NULL COMMENT 'æˆ°é¬¥å±¬æ–¼å“ªå€‹é—œå¡',
  `account_battle_status` smallint(2) NOT NULL COMMENT 'æ­¤æˆ°é¬¥ç‹€æ…‹:0=æˆ°é¬¥çµæŸ, 1=æˆ°é¬¥ä¸­',
  `account_battle_exp` int(11) NOT NULL COMMENT 'å¯ç²å¾—ç¶“é©—',
  `account_battle_coin` int(11) NOT NULL COMMENT 'å¯ç²å¾—é‡‘éŒ¢',
  `account_battle_item_1` int(11) NOT NULL COMMENT 'å¯ç²å¾—é“å…·(1)',
  `account_battle_item_2` int(11) NOT NULL,
  `account_battle_item_3` int(11) NOT NULL,
  `account_battle_item_4` int(11) NOT NULL,
  `account_battle_item_5` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- è¡¨çš„çµæ§‹ `system_log`
--

CREATE TABLE IF NOT EXISTS `system_log` (
  `serial` int(11) NOT NULL AUTO_INCREMENT,
  `log_message` varchar(512) NOT NULL,
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`serial`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=575 ;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
