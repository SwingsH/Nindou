<?php
/***************************************************************************
 *   copyright            : (C) 2001 The phpBB Group
 *   $Id: mysql.php,v 1.16 2002/03/19 01:07:36 psotfx Exp $
 *
 ***************************************************************************/

/***************************************************************************
 *   modify               : Swings (C) T.I.E in Taiwan 
 ***************************************************************************/

require_once( 'includes/nindou.config.php');

class Protocol
{
	var $handle_functions;
	var $current_mainkind;
	var $current_subkind;
	var $current_serial;
	var $current_arguments;
	var $index_args_str;
	var $index_args_int;
		
	//
	// Constructor
	//
	function __construct()
	{
		$this->handle_functions = array();
		
		// 1 - Login
		$this->handle_functions[1] = array();
		$this->handle_functions[1][1] = 'handle_login_1';
		$this->handle_functions[1][3] = 'handle_login_3';	
		$this->handle_functions[5][1] = 'handle_item_1';	
	}
	
	public function handle($serial, $mainkind, $subkind, $arr_arguments)
	{		
		if(!array_key_exists($mainkind, $this->handle_functions))
			die(STR_ERROR);
		if(!array_key_exists($subkind, $this->handle_functions[$mainkind]))
			die(STR_ERROR);
			
		$this->current_mainkind = $mainkind;
		$this->current_subkind = $subkind;
		$this->current_serial =  $serial;
		$this->current_arguments = &$arr_arguments;
		$this->index_args_str = 1;
		$this->index_args_int = 1;
	
		$func_name = $this->handle_functions[$mainkind][$subkind];
		
		call_user_func( array($this, $func_name));
	}
	
	// C: 1-1 玩家快速登入, s1:deviceid
	// S: 1-1 登入, s1:session , s2: 玩家名稱, i1:登入類型(0=登入失敗,1=新帳號登入,2=快速登入,3=更新session)
	private function handle_login_1()
	{
		global $db;
		
		$deviceid = $this->pop_string();
		$query = db_get_account($deviceid);
		
		if(IS_DEBUG)
		{
			$db->sql_query( db_insert_log($query) );
		}
		$data_res = $db->sql_query( $query );
		$num_fields = $db->sql_numrows($data_res);
		$login_result = 0;
		
		if($num_fields > 1) // 不正常   device id  重複
		{
			echo " $num_fields " . $num_fields ;
			$this->handle_command_error(get_calling_method_name());
			$login_result = 0;
		}
		else if($num_fields == 0) //  account is empty, 新帳號
		{
			$new_session = get_current_time_session($deviceid);

			$login_result = 1;
			
//			if(!$success)
//				$this->handle_command_error(get_calling_method_name());	
		}
		else // 帳號已經存在, update
		{
			$new_session = get_current_time_session($deviceid);
			$query = db_update_account($deviceid, $new_session);
			$result = $db->sql_query($query);
			$success = $db->sql_affectedrows() == 1 ? true : false;
			$login_result = 2;
			
			if(!$success)
				die(STR_ERROR);	
		}
		
		$row = $db->sql_fetchrow($data_res);
		if(gettype($row) == 'boolean') //新帳號
		{
			$row = array();
		}
		$account_data = new AccountData($row);
				
		// prepare response data
		switch( $login_result )
		{
			case 1: // 不存在帳號
				$pack = new JsonPackage($this->current_mainkind, $this->current_subkind);
				$pack->PushStr($new_session); // 新的 session
				$pack->PushStr("");
				$pack->PushInt($login_result);
				$package_group = new JsonPackageGroup($this->current_serial, $this->current_mainkind, $this->current_subkind);
				$package_group->AddPackage($pack);
				
				echo json_encode($package_group);
				exit;
			break;
			
			case 2: // 已存在帳號
				$pack = new JsonPackage($this->current_mainkind, $this->current_subkind);
				$pack->PushStr($new_session); // 新的 session
				$pack->PushStr($account_data->player_name);
				$pack->PushInt($login_result);
				
				//背包卡片資料
				$num = count($account_data->cards);
				$pack->PushInt($num);
				for($i = 0 ; $i < $num ; $i++)
				{
					$pack->PushInt($account_data->cards[$i]);
				}
				
				$package_group = new JsonPackageGroup($this->current_serial, $this->current_mainkind, $this->current_subkind);
				$package_group->AddPackage($pack);
				
				echo json_encode($package_group);
			break;

		}  
	}
	
	// S: 1-2 完整帳號資料, s1:AccountData
	private function handle_login_2()
	{
		
	}
	
	// C: 1-3 玩家要求註冊帳號, s1: 裝置ID, s2: 登入session, s3:玩家輸入之名稱
	// S: 1-3 帳號註冊結果, i1:註冊結果(1=成功, 2=失敗)
	private function handle_login_3()
	{
		global $db;
		
		$deviceid 	= $this->pop_string();
		$session 	= $this->pop_string();
		$inputName 	= $this->pop_string();
		
		$query_check_exist = db_get_account($deviceid);
		if(IS_DEBUG)
		{
			$result_check_exist = $db->sql_query( db_insert_log($query_check_exist) );
			$db->sql_freeresult($result_check_exist);
		}
		$result_check_exist = $db->sql_query( $query_check_exist );
		$num_fields = $db->sql_numrows($result_check_exist);
		
		if($num_fields >= 1) // 不正常, 註冊帳號已存在
			die(STR_ERROR  . "2");
		
		//todo: init account data
		$inherited_id = get_inherited_id($deviceid);
		$query_new_account = db_insert_account($deviceid, $session, $inherited_id, $inputName);
		
		if(IS_DEBUG)
		{
			$db->sql_query( db_insert_log($query_new_account) );
		}
		
		$result_new_account = $db->sql_query($query_new_account);
		$success = $db->sql_affectedrows() == 1 ? true : false;
		
		if($success == true)
			$register_result = 1;
		else
			$register_result = 2;
		
		//又是 1-1 協定, 待整理
		$query_fetch_account = db_get_account($deviceid);
		if(IS_DEBUG)
		{
			$db->sql_query( db_insert_log($query_fetch_account) );
		}
		$data_fetch_account = $db->sql_query( $query_fetch_account );
		$row = $db->sql_fetchrow($data_fetch_account);
		$account_data = new AccountData($row);
		//又是 1-1 協定, 待整理 end
						
		$pack = new JsonPackage($this->current_mainkind, $this->current_subkind);
		$pack->PushInt($register_result);
		
		//背包卡片資料
		$num = count($account_data->cards);
		$pack->PushInt($num);
		for($i = 0 ; $i < $num ; $i++)
		{
			$pack->PushInt($account_data->cards[$i]);
		}
				
		$package_group = new JsonPackageGroup($this->current_serial, $this->current_mainkind, $this->current_subkind);
		$package_group->AddPackage($pack);
				
		echo json_encode($package_group);
		exit;
	}
	
	// C: 5-1 玩家要求合成, s1: 裝置ID, i1: 被合成的 card index, i2: 素材 card index, i3: 被合成的 Item ID 卡牌 ID , i4: 素材 Item ID 卡牌 ID
	// S: 5-1 合成結果, i1:合成結果(1=成功, 2=失敗), i2:合成後的 item id
	private function handle_item_1()
	{
		global $db;
				
		$deviceid 				= $this->pop_string();
		$main_item_index 		= $this->pop_integer();
		$material_item_index 	= $this->pop_integer();
		$main_item_id	 		= $this->pop_integer();
		$material_item_id	 	= $this->pop_integer();
		
		$query_fetch_account = db_get_account($deviceid);
		if(IS_DEBUG)
		{
			$db->sql_query( db_insert_log($query_fetch_account) );
		}
		$data_fetch_account = $db->sql_query( $query_fetch_account );
		$row = $db->sql_fetchrow($data_fetch_account);
		$account_data = new AccountData($row);
		if($account_data->cards[ $main_item_index - 1 ] != $main_item_id)
		{
			die(" Main index 欄位與 Main item id 不符合");
		}
		if($account_data->cards[ $material_item_index - 1 ] != $material_item_id)
		{
			die(" Material index 欄位與 Material item id 不符合");
		}
		
		$item_data = get_item_data($main_item_id);
		$upgrade_result_item_id = $item_data[ 'UpgradeResult' ] ;
		$change_set = array( $material_item_index => 0 , $main_item_index => $upgrade_result_item_id );
		$update_query = db_update_account_item_box($deviceid, $change_set);
		
		$db->sql_query($update_query);
		$success = $db->sql_affectedrows() == 1 ? true : false;
		
		if($success == true)
			$result_kind = 1;
		else
			$result_kind = 2;
		
		$pack = new JsonPackage($this->current_mainkind, $this->current_subkind);
		$pack->PushInt($result_kind);
		$pack->PushInt($upgrade_result_item_id);
		
		$package_group = new JsonPackageGroup($this->current_serial, $this->current_mainkind, $this->current_subkind);
		$package_group->AddPackage($pack);
		
		echo json_encode($package_group);
		exit;
	}
	
	private function handle_command_error($func_name = '')
	{
		$pack = new JsonPackage(0, 0); //s error kind 0, 0
		$pack->PushStr( "error in " . $func_name . '()' );
		$package_group = new JsonPackageGroup($this->current_serial, $this->current_mainkind, $this->current_subkind);
		$package_group->AddPackage($pack);
		echo json_encode($package_group);
	}
	
	private function pop_integer()
	{
		$name = sprintf( POST_PARAMETER_INTEGER , $this->index_args_int );
		
		if(!array_key_exists($name, $this->current_arguments))
		{
			$this->handle_command_error(get_calling_method_name());
			exit;
		}
		
		if( !isset($this->current_arguments[$name]) )
		{
			// todo: error log
			$this->handle_command_error(get_calling_method_name());
			exit;
		}
		$result = $this->current_arguments[$name];
		$this->index_args_int++;
		return (int) $result; //intval() is two and a half times slower
	}
	
	private function pop_string()
	{
		$name = sprintf( POST_PARAMETER_STRING , $this->index_args_str );
		if( !isset($this->current_arguments[$name]) )
		{
			// todo: error log
			return '';
		}
		$result = $this->current_arguments[$name];
		$this->index_args_str++;
		return strval($result);
	}
	
	public function Test()
	{
		global $db;
		
		$query = "ALTER TABLE `player_account` 
		ADD `role_2_card_id` INT( 11 ) NOT NULL COMMENT '第 2 個角色' AFTER `role_1_passiveskill_2_exp` ,
		ADD `role_2_card_exp` INT( 11 ) NOT NULL COMMENT '第 2 個角色的 exp' AFTER `role_2_card_id` ,
		ADD `role_2_card_team` INT( 11 ) NOT NULL COMMENT '第 2 個角色卡片的隊伍編排: 1=第一位,2=第二位, 0=未上場' AFTER `role_2_card_exp` ,
		ADD `role_2_ultraskill_card_id` INT( 11 ) NOT NULL COMMENT '第 2 個角色卡片的 武器技能' AFTER `role_2_card_team` ,
		ADD `role_2_ultraskill_exp` INT( 11 ) NOT NULL COMMENT '第 2 個角色卡片的 武器技能 exp ' AFTER `role_2_ultraskill_card_id` ,
		ADD `role_2_skill_1_card_id` INT( 11 ) NOT NULL COMMENT '第 2 個角色卡片的 第 1 個技能 ' AFTER `role_2_ultraskill_exp` ,
		ADD `role_2_skill_1_exp` INT( 11 ) NOT NULL COMMENT '第 2 個角色卡片的  第 1 個技能  exp ' AFTER `role_2_skill_1_card_id` ,
		ADD `role_2_skill_2_card_id` INT( 11 ) NOT NULL COMMENT '第 2 個角色卡片的 第 2 個技能 ' AFTER `role_2_skill_1_exp` ,
		ADD `role_2_skill_2_exp` INT( 11 ) NOT NULL COMMENT '第 2 個角色卡片的  第 2 個技能  exp ' AFTER `role_2_skill_2_card_id` ,
		ADD `role_2_passiveskill_1_card_id` INT( 11 ) NOT NULL COMMENT '第 2 個角色卡片的 第 1 個 [被動] 技能 ' AFTER `role_2_skill_2_exp` ,
		ADD `role_2_passiveskill_1_exp` INT( 11 ) NOT NULL COMMENT '第 2 個角色卡片的  第 1 個 [被動] 技能  exp ' AFTER `role_2_passiveskill_1_card_id` ,
		ADD `role_2_passiveskill_2_card_id` INT( 11 ) NOT NULL COMMENT '第 2 個角色卡片的 第 2 個 [被動] 技能 ' AFTER `role_2_passiveskill_1_exp` ,
		ADD `role_2_passiveskill_2_exp` INT( 11 ) NOT NULL COMMENT '第 2 個角色卡片的  第 2 個 [被動] 技能  exp ' AFTER `role_2_passiveskill_2_card_id` ,
		ADD `role_3_card_id` INT( 11 ) NOT NULL COMMENT '第 3 個角色' AFTER `role_2_passiveskill_2_exp` ,
		ADD `role_3_card_exp` INT( 11 ) NOT NULL COMMENT '第 3 個角色的 exp' AFTER `role_3_card_id` ,
		ADD `role_3_card_team` INT( 11 ) NOT NULL COMMENT '第 3 個角色卡片的隊伍編排: 1=第一位,2=第二位, 0=未上場' AFTER `role_3_card_exp` ,
		ADD `role_3_ultraskill_card_id` INT( 11 ) NOT NULL COMMENT '第 3 個角色卡片的 武器技能' AFTER `role_3_card_team` ,
		ADD `role_3_ultraskill_exp` INT( 11 ) NOT NULL COMMENT '第 3 個角色卡片的 武器技能 exp ' AFTER `role_3_ultraskill_card_id` ,
		ADD `role_3_skill_1_card_id` INT( 11 ) NOT NULL COMMENT '第 3 個角色卡片的 第 1 個技能 ' AFTER `role_3_ultraskill_exp` ,
		ADD `role_3_skill_1_exp` INT( 11 ) NOT NULL COMMENT '第 3 個角色卡片的  第 1 個技能  exp ' AFTER `role_3_skill_1_card_id` ,
		ADD `role_3_skill_2_card_id` INT( 11 ) NOT NULL COMMENT '第 3 個角色卡片的 第 2 個技能 ' AFTER `role_3_skill_1_exp` ,
		ADD `role_3_skill_2_exp` INT( 11 ) NOT NULL COMMENT '第 3 個角色卡片的  第 2 個技能  exp ' AFTER `role_3_skill_2_card_id` ,
		ADD `role_3_passiveskill_1_card_id` INT( 11 ) NOT NULL COMMENT '第 3 個角色卡片的 第 1 個 [被動] 技能 ' AFTER `role_3_skill_2_exp` ,
		ADD `role_3_passiveskill_1_exp` INT( 11 ) NOT NULL COMMENT '第 3 個角色卡片的  第 1 個 [被動] 技能  exp ' AFTER `role_3_passiveskill_1_card_id` ,
		ADD `role_3_passiveskill_2_card_id` INT( 11 ) NOT NULL COMMENT '第 3 個角色卡片的 第 2 個 [被動] 技能 ' AFTER `role_3_passiveskill_1_exp` ,
		ADD `role_3_passiveskill_2_exp` INT( 11 ) NOT NULL COMMENT '第 3 個角色卡片的  第 2 個 [被動] 技能  exp ' AFTER `role_3_passiveskill_2_card_id` ;"	;
		
		$data_res = $db->sql_query( $query );
	}
}

?>