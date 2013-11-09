-- phpMyAdmin SQL Dump
-- version 4.0.4.1
-- http://www.phpmyadmin.net
--
-- 主機: 127.0.0.1
-- 產生日期: 2013 �?11 ??09 ??09:51
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
  `roleslot_1_rolebox_index` int(11) NOT NULL COMMENT '����� 1 ����O���ި� ����box�� ���@�Ӵ��� ',
  `roleslot_1_weapon_cardbox_index` int(11) NOT NULL COMMENT '����� 1 ���⪺ �Z�� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_1_skill_1_cardbox_index` int(11) NOT NULL COMMENT '����� 1 ���⪺ �� M �ޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_1_skill_2_cardbox_index` int(11) NOT NULL COMMENT '����� 1 ���⪺ �� M �ޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_1_passiveskill_1_cardbox_index` int(11) NOT NULL COMMENT '����� 1 ���⪺ �� M �Q�ʧޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_1_passiveskill_2_cardbox_index` int(11) NOT NULL COMMENT '����� 1 ���⪺ �� M �Q�ʧޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_2_rolebox_index` int(11) NOT NULL COMMENT '����� 2 ����O���ި� ����box�� ���@�Ӵ��� ',
  `roleslot_2_weapon_cardbox_index` int(11) NOT NULL COMMENT '����� 2 ���⪺ �Z�� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_2_skill_1_cardbox_index` int(11) NOT NULL COMMENT '����� 2 ���⪺ �� M �ޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_2_skill_2_cardbox_index` int(11) NOT NULL COMMENT '����� 2 ���⪺ �� M �ޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_2_passiveskill_1_cardbox_index` int(11) NOT NULL COMMENT '����� 2 ���⪺ �� M �Q�ʧޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_2_passiveskill_2_cardbox_index` int(11) NOT NULL COMMENT '����� 2 ���⪺ �� M �Q�ʧޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_3_rolebox_index` int(11) NOT NULL COMMENT '����� 3 ����O���ި� ����box�� ���@�Ӵ��� ',
  `roleslot_3_weapon_cardbox_index` int(11) NOT NULL COMMENT '����� 3 ���⪺ �Z�� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_3_skill_1_cardbox_index` int(11) NOT NULL COMMENT '����� 3 ���⪺ �� M �ޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_3_skill_2_cardbox_index` int(11) NOT NULL COMMENT '����� 3 ���⪺ �� M �ޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_3_passiveskill_1_cardbox_index` int(11) NOT NULL COMMENT '����� 3 ���⪺ �� M �Q�ʧޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `roleslot_3_passiveskill_2_cardbox_index` int(11) NOT NULL COMMENT '����� 3 ���⪺ �� M �Q�ʧޯ� �O���ި� cardbox�� ���@�Ӵ���',
  `rolebox_1_role_id` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 1 ���⪺ ID',
  `rolebox_1_role_exp` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 1 ���⪺ Exp',
  `rolebox_2_role_id` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 2 ���⪺ ID',
  `rolebox_2_role_exp` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 2 ���⪺ Exp',
  `rolebox_3_role_id` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 3 ���⪺ ID',
  `rolebox_3_role_exp` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 3 ���⪺ Exp',
  `rolebox_4_role_id` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 4 ���⪺ ID',
  `rolebox_4_role_exp` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 4 ���⪺ Exp',
  `rolebox_5_role_id` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 5 ���⪺ ID',
  `rolebox_5_role_exp` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 5 ���⪺ Exp',
  `rolebox_6_role_id` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 6 ���⪺ ID',
  `rolebox_6_role_exp` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 6 ���⪺ Exp',
  `rolebox_7_role_id` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 7 ���⪺ ID',
  `rolebox_7_role_exp` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 7 ���⪺ Exp',
  `rolebox_8_role_id` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 8 ���⪺ ID',
  `rolebox_8_role_exp` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 8 ���⪺ Exp',
  `rolebox_9_role_id` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 9 ���⪺ ID',
  `rolebox_9_role_exp` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 9 ���⪺ Exp',
  `rolebox_10_role_id` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 10 ���⪺ ID',
  `rolebox_10_role_exp` int(11) NOT NULL COMMENT '�H���x�s�椤index�� 10 ���⪺ Exp',
  `cardbox_1_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 1 �i�d�� ID',
  `cardbox_1_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 1 �i�d�� Exp',
  `cardbox_2_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 2 �i�d�� ID',
  `cardbox_2_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 2 �i�d�� Exp',
  `cardbox_3_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 3 �i�d�� ID',
  `cardbox_3_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 3 �i�d�� Exp',
  `cardbox_4_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 4 �i�d�� ID',
  `cardbox_4_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 4 �i�d�� Exp',
  `cardbox_5_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 5 �i�d�� ID',
  `cardbox_5_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 5 �i�d�� Exp',
  `cardbox_6_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 6 �i�d�� ID',
  `cardbox_6_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 6 �i�d�� Exp',
  `cardbox_7_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 7 �i�d�� ID',
  `cardbox_7_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 7 �i�d�� Exp',
  `cardbox_8_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 8 �i�d�� ID',
  `cardbox_8_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 8 �i�d�� Exp',
  `cardbox_9_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 9 �i�d�� ID',
  `cardbox_9_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 9 �i�d�� Exp',
  `cardbox_10_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 10 �i�d�� ID',
  `cardbox_10_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 10 �i�d�� Exp',
  `cardbox_11_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 11 �i�d�� ID',
  `cardbox_11_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 11 �i�d�� Exp',
  `cardbox_12_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 12 �i�d�� ID',
  `cardbox_12_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 12 �i�d�� Exp',
  `cardbox_13_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 13 �i�d�� ID',
  `cardbox_13_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 13 �i�d�� Exp',
  `cardbox_14_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 14 �i�d�� ID',
  `cardbox_14_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 14 �i�d�� Exp',
  `cardbox_15_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 15 �i�d�� ID',
  `cardbox_15_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 15 �i�d�� Exp',
  `cardbox_16_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 16 �i�d�� ID',
  `cardbox_16_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 16 �i�d�� Exp',
  `cardbox_17_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 17 �i�d�� ID',
  `cardbox_17_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 17 �i�d�� Exp',
  `cardbox_18_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 18 �i�d�� ID',
  `cardbox_18_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 18 �i�d�� Exp',
  `cardbox_19_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 19 �i�d�� ID',
  `cardbox_19_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 19 �i�d�� Exp',
  `cardbox_20_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 20 �i�d�� ID',
  `cardbox_20_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 20 �i�d�� Exp',
  `cardbox_21_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 21 �i�d�� ID',
  `cardbox_21_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 21 �i�d�� Exp',
  `cardbox_22_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 22 �i�d�� ID',
  `cardbox_22_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 22 �i�d�� Exp',
  `cardbox_23_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 23 �i�d�� ID',
  `cardbox_23_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 23 �i�d�� Exp',
  `cardbox_24_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 24 �i�d�� ID',
  `cardbox_24_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 24 �i�d�� Exp',
  `cardbox_25_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 25 �i�d�� ID',
  `cardbox_25_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 25 �i�d�� Exp',
  `cardbox_26_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 26 �i�d�� ID',
  `cardbox_26_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 26 �i�d�� Exp',
  `cardbox_27_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 27 �i�d�� ID',
  `cardbox_27_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 27 �i�d�� Exp',
  `cardbox_28_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 28 �i�d�� ID',
  `cardbox_28_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 28 �i�d�� Exp',
  `cardbox_29_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 29 �i�d�� ID',
  `cardbox_29_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 29 �i�d�� Exp',
  `cardbox_30_card_id` int(11) NOT NULL COMMENT '�d�]��index�� 30 �i�d�� ID',
  `cardbox_30_card_exp` int(11) NOT NULL COMMENT '�d�]��index�� 30 �i�d�� Exp',
  `create_time` timestamp NULL DEFAULT NULL,
  `last_login_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`serial`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=64 ;

-- --------------------------------------------------------

--
-- 表的結構 `player_battle`
--

CREATE TABLE IF NOT EXISTS `player_battle` (
  `account_serial` int(11) NOT NULL COMMENT '該玩家 序號',
  `account_battle_id` int(11) NOT NULL COMMENT '戰鬥屬於哪個關卡',
  `account_battle_status` smallint(2) NOT NULL COMMENT '此戰鬥狀態:0=戰鬥結束, 1=戰鬥中',
  `account_battle_exp` int(11) NOT NULL COMMENT '可獲得經驗',
  `account_battle_coin` int(11) NOT NULL COMMENT '可獲得金錢',
  `account_battle_item_1` int(11) NOT NULL COMMENT '可獲得道具(1)',
  `account_battle_item_2` int(11) NOT NULL,
  `account_battle_item_3` int(11) NOT NULL,
  `account_battle_item_4` int(11) NOT NULL,
  `account_battle_item_5` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- 表的結構 `system_log`
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
