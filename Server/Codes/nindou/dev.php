<?php

/***************************************************************************
*	ID			: protocol.php
*	Licence		: Swings (C) Nindou mobile , T.I.E in Taiwan 
 ***************************************************************************/
require_once( 'includes/nindou.config.php');

if(isset($_GET['dev']))
DevCreate();

function DevCreate()
{
	//DevCreate_PlayerAccount_1();
	//DevCreate_PlayerAccount_Cardbox();
	//DevCreate_PlayerAccount_Rolebox();
	//DevCreate_PlayerAccount_Role_Slot();
	//DevCreate_BattleData_Role_Slot();
	
	//ReadJsonTest();
}	

function ReadJsonTest(){
	$file = "./json/itemdata.json";
	
	$json = json_decode(file_get_contents($file), true);
	
	var_dump($json);
	//echo file_get_contents($file);	
}

function DevCreate_PlayerAccount_1(){
		global $db;
		
		$query = "ALTER TABLE `player_account` 
		ADD `role_2_card_id` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���' AFTER `role_1_passiveskill_2_exp` ,
		ADD `role_2_card_exp` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө��⪺ exp' AFTER `role_2_card_id` ,
		ADD `role_2_card_team` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���d��������s��: 1=�Ĥ@��,2=�ĤG��, 0=���W��' AFTER `role_2_card_exp` ,
		ADD `role_2_ultraskill_card_id` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���d���� �Z���ޯ�' AFTER `role_2_card_team` ,
		ADD `role_2_ultraskill_exp` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���d���� �Z���ޯ� exp ' AFTER `role_2_ultraskill_card_id` ,
		ADD `role_2_skill_1_card_id` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���d���� �� 1 �ӧޯ� ' AFTER `role_2_ultraskill_exp` ,
		ADD `role_2_skill_1_exp` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���d����  �� 1 �ӧޯ�  exp ' AFTER `role_2_skill_1_card_id` ,
		ADD `role_2_skill_2_card_id` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���d���� �� 2 �ӧޯ� ' AFTER `role_2_skill_1_exp` ,
		ADD `role_2_skill_2_exp` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���d����  �� 2 �ӧޯ�  exp ' AFTER `role_2_skill_2_card_id` ,
		ADD `role_2_passiveskill_1_card_id` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���d���� �� 1 �� [�Q��] �ޯ� ' AFTER `role_2_skill_2_exp` ,
		ADD `role_2_passiveskill_1_exp` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���d����  �� 1 �� [�Q��] �ޯ�  exp ' AFTER `role_2_passiveskill_1_card_id` ,
		ADD `role_2_passiveskill_2_card_id` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���d���� �� 2 �� [�Q��] �ޯ� ' AFTER `role_2_passiveskill_1_exp` ,
		ADD `role_2_passiveskill_2_exp` INT( 11 ) NOT NULL COMMENT '�� 2 �Ө���d����  �� 2 �� [�Q��] �ޯ�  exp ' AFTER `role_2_passiveskill_2_card_id` ,
		ADD `role_3_card_id` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���' AFTER `role_2_passiveskill_2_exp` ,
		ADD `role_3_card_exp` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө��⪺ exp' AFTER `role_3_card_id` ,
		ADD `role_3_card_team` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���d��������s��: 1=�Ĥ@��,2=�ĤG��, 0=���W��' AFTER `role_3_card_exp` ,
		ADD `role_3_ultraskill_card_id` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���d���� �Z���ޯ�' AFTER `role_3_card_team` ,
		ADD `role_3_ultraskill_exp` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���d���� �Z���ޯ� exp ' AFTER `role_3_ultraskill_card_id` ,
		ADD `role_3_skill_1_card_id` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���d���� �� 1 �ӧޯ� ' AFTER `role_3_ultraskill_exp` ,
		ADD `role_3_skill_1_exp` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���d����  �� 1 �ӧޯ�  exp ' AFTER `role_3_skill_1_card_id` ,
		ADD `role_3_skill_2_card_id` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���d���� �� 2 �ӧޯ� ' AFTER `role_3_skill_1_exp` ,
		ADD `role_3_skill_2_exp` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���d����  �� 2 �ӧޯ�  exp ' AFTER `role_3_skill_2_card_id` ,
		ADD `role_3_passiveskill_1_card_id` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���d���� �� 1 �� [�Q��] �ޯ� ' AFTER `role_3_skill_2_exp` ,
		ADD `role_3_passiveskill_1_exp` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���d����  �� 1 �� [�Q��] �ޯ�  exp ' AFTER `role_3_passiveskill_1_card_id` ,
		ADD `role_3_passiveskill_2_card_id` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���d���� �� 2 �� [�Q��] �ޯ� ' AFTER `role_3_passiveskill_1_exp` ,
		ADD `role_3_passiveskill_2_exp` INT( 11 ) NOT NULL COMMENT '�� 3 �Ө���d����  �� 2 �� [�Q��] �ޯ�  exp ' AFTER `role_3_passiveskill_2_card_id` ;" ;
		
		$data_res = $db->sql_query( $query );
}

function DevCreate_PlayerAccount_Cardbox(){
		global $db;
		
		$all_query = "ALTER TABLE `player_account` ";
		$current_column = "" ;
		$previous_column = "" ;
		$comma = " , \n";
		$semi = " ; \n";
		
		$column_name_id = "cardbox_%d_card_id" ;
		$column_name_exp = "cardbox_%d_card_exp" ;
		$pattern_id = "ADD `%s` INT( 11 ) NOT NULL COMMENT '�d�]��index�� %d �i�d�� ID' AFTER `%s` ";
		$pattern_exp = "ADD `%s` INT( 11 ) NOT NULL COMMENT '�d�]��index�� %d �i�d�� Exp' AFTER `%s` ";
		$max_cardbox_num = 30;
		
		$previous_column = "current_inherit_password";
		$one_data_query = "";
		for($idx = 1 ; $idx <= $max_cardbox_num ; $idx++)
		{
			$current_column = sprintf($column_name_id, $idx);
			$one_data_query = sprintf($pattern_id, $current_column, $idx, $previous_column) ;
			$one_data_query = $one_data_query . $comma;
			$all_query		= $all_query . $one_data_query;
			
			$previous_column = $current_column;
			
			$current_column = sprintf($column_name_exp, $idx);
			$one_data_query = sprintf($pattern_exp, $current_column, $idx, $previous_column);
			if( $idx == $max_cardbox_num ) //last
			{
				$one_data_query = $one_data_query . $semi;
			}
			else
			{
				$one_data_query = $one_data_query . $comma;
			}
			$all_query		= $all_query . $one_data_query;
			
			$previous_column = $current_column;
		}
		
		echo $all_query	;
		$data_res = $db->sql_query( $all_query );
}

function DevCreate_PlayerAccount_Rolebox()
{
			global $db;
		
		$all_query = "ALTER TABLE `player_account` ";
		$current_column = "" ;
		$previous_column = "" ;
		$comma = " , \n";
		$semi = " ; \n";
		
		$column_name_id = "rolebox_%d_role_id" ;
		$column_name_exp = "rolebox_%d_role_exp" ;
		$pattern_id = "ADD `%s` INT( 11 ) NOT NULL COMMENT '�H���x�s�椤index�� %d ���⪺ ID' AFTER `%s` ";
		$pattern_exp = "ADD `%s` INT( 11 ) NOT NULL COMMENT '�H���x�s�椤index�� %d ���⪺ Exp' AFTER `%s` ";
		$max_rolebox_num = 10;
		
		$previous_column = "current_inherit_password";
		$one_data_query = "";
		for($idx = 1 ; $idx <= $max_rolebox_num ; $idx++)
		{
			$current_column = sprintf($column_name_id, $idx);
			$one_data_query = sprintf($pattern_id, $current_column, $idx, $previous_column) ;
			$one_data_query = $one_data_query . $comma;
			$all_query		= $all_query . $one_data_query;
			
			$previous_column = $current_column;
			
			$current_column = sprintf($column_name_exp, $idx);
			$one_data_query = sprintf($pattern_exp, $current_column, $idx, $previous_column);
			if( $idx == $max_rolebox_num ) //last
			{
				$one_data_query = $one_data_query . $semi;
			}
			else
			{
				$one_data_query = $one_data_query . $comma;
			}
			$all_query		= $all_query . $one_data_query;
			
			$previous_column = $current_column;
		}
		
		echo $all_query	;
		$data_res = $db->sql_query( $all_query );
}


function DevCreate_PlayerAccount_Role_Slot()
{
		global $db;
		
		$all_query = "ALTER TABLE `player_account` ";
		$current_column = "" ;
		$previous_column = "" ;
		$comma = " , \n";
		$semi = " ; \n";
		
		$column_role 		= "roleslot_%d_rolebox_index" ;						//����� N ����O���ި� ����box�� ���@�Ӵ���
		$column_role_weapon = "roleslot_%d_weapon_cardbox_index" ; 				//����� N ���⪺ �Z�� �O���ި� cardbox�� ���@�Ӵ���
		$column_role_skill = "roleslot_%d_skill_%d_cardbox_index" ;					//����� N ���⪺ �� M �ޯ� �O���ި� cardbox�� ���@�Ӵ���
		$column_role_passiveskill = "roleslot_%d_passiveskill_%d_cardbox_index" ;	//����� N ���⪺ �� M �Q�ʧޯ� �O���ި� cardbox�� ���@�Ӵ���
		
		$pattern_role = "ADD `%s` INT( 11 ) NOT NULL COMMENT '����� %d ����O���ި� ����box�� ���@�Ӵ��� ' AFTER `%s` ";
		$pattern_role_weapon = "ADD `%s` INT( 11 ) NOT NULL COMMENT '����� %d ���⪺ �Z�� �O���ި� cardbox�� ���@�Ӵ���' AFTER `%s` ";
		$pattern_role_skill = "ADD `%s` INT( 11 ) NOT NULL COMMENT '����� %d ���⪺ �� M �ޯ� �O���ި� cardbox�� ���@�Ӵ���' AFTER `%s` ";
		$pattern_role_passiveskill = "ADD `%s` INT( 11 ) NOT NULL COMMENT '����� %d ���⪺ �� M �Q�ʧޯ� �O���ި� cardbox�� ���@�Ӵ���' AFTER `%s` ";
		$max_rolebox_num = 3;
		
		$previous_column = "current_inherit_password";
		$one_data_query = "";
		for($idx = 1 ; $idx <= $max_rolebox_num ; $idx++)
		{
			$current_column	= sprintf($column_role, $idx); 
			$one_data_query = sprintf($pattern_role, $current_column, $idx, $previous_column) ;
			$one_data_query = $one_data_query . $comma;
			$all_query		= $all_query . $one_data_query;
			$previous_column= $current_column;
			
			$current_column = sprintf($column_role_weapon, $idx);
			$one_data_query = sprintf($pattern_role_weapon, $current_column, $idx, $previous_column) ;
			$one_data_query = $one_data_query . $comma;
			$all_query		= $all_query . $one_data_query;
			$previous_column= $current_column;
			
			$current_column = sprintf($column_role_skill, $idx, 1);
			$one_data_query = sprintf($pattern_role_skill, $current_column, $idx, $previous_column) ;
			$one_data_query = $one_data_query . $comma;
			$all_query		= $all_query . $one_data_query;
			$previous_column= $current_column;
			
			$current_column = sprintf($column_role_skill, $idx, 2);
			$one_data_query = sprintf($pattern_role_skill, $current_column, $idx, $previous_column) ;
			$one_data_query = $one_data_query . $comma;
			$all_query		= $all_query . $one_data_query;
			$previous_column= $current_column;
			
			$current_column = sprintf($column_role_passiveskill, $idx, 1);
			$one_data_query = sprintf($pattern_role_passiveskill, $current_column, $idx, $previous_column) ;
			$one_data_query = $one_data_query . $comma;
			$all_query		= $all_query . $one_data_query;
			$previous_column= $current_column;
			
			if( $idx == $max_rolebox_num ) //last
			{
				$last = $semi;
			}
			else
			{
				$last = $comma;
			}
			
			$current_column = sprintf($column_role_passiveskill, $idx, 2);
			$one_data_query = sprintf($pattern_role_passiveskill, $current_column, $idx, $previous_column) ;
			$one_data_query = $one_data_query . $last;
			$all_query		= $all_query . $one_data_query;
			$previous_column= $current_column;
		}
		
		echo $all_query	;
		$data_res = $db->sql_query( $all_query );
}

?>