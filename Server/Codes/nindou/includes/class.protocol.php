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
	// S: 1-1 登入, s1:session , i1:登入類型(0=登入失敗,1=新帳號登入,2=快速登入,3=更新session)
	private function handle_login_1()
	{
		global $db;
		
		$deviceid = $this->pop_string();
		$query = db_get_account($deviceid);
		if(IS_DEBUG)
		{
			//die(db_insert_log($query));
			$result = $db->sql_query( db_insert_log($query) );
			$db->sql_freeresult($result);
		}
		$result = $db->sql_query( $query );

		$num_fields = $db->sql_numrows($result);
		$login_result = 0;
		
		if($num_fields > 1) // 不正常   device id  重複
		{
			$this->handle_command_error(get_calling_method_name());
			$login_result = 0;
		}
		else if($num_fields == 0) //  account is empty, 新帳號
		{
			//todo: init account data
			$new_session = get_current_time_session($deviceid);
			$inherited_id = get_inherited_id($deviceid);
			$query = db_insert_account($deviceid, $new_session, $inherited_id);
			$result = $db->sql_query($query);
			$success = $db->sql_affectedrows() == 1 ? true : false;
			$login_result = 1;
			
			if(!$success)
				$this->handle_command_error(get_calling_method_name());	
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

		if(!isset($new_session))
			$this->handle_command_error(get_calling_method_name());
			
		$pack = &new JsonPackage($this->current_mainkind, $this->current_subkind);
		$pack->PushStr($new_session);
		$pack->PushInt($login_result);
		$package_group = &new JsonPackageGroup($this->current_serial, $this->current_mainkind, $this->current_subkind);
		$package_group->AddPackage($pack);
		
		echo json_encode($package_group);

//		while( $row = $db->sql_fetchrow($result) ){
//			echo 'haha' ;
//		}
		
		//print_r($arr_arguments);
		
//		if (!($result = $db->sql_query("SELECT * FROM $table")))
//        {
//                message_die(GENERAL_ERROR, "Failed in get_table_content (select *)", "", __LINE__, __FILE__, "SELECT * FROM $table");
//        }
//
//        // Loop through the resulting rows and build the sql statement.
//        if ($row = $db->sql_fetchrow($result))
//        {
//                $handler("\n#\n# Table Data for $table\n#\n");
//                $field_names = array();
//
//                // Grab the list of field names.
//                $num_fields = $db->sql_numfields($result);    
	}
	
	private function handle_command_error($func_name = '')
	{
		$pack = &new JsonPackage(0, 0); //s error kind 0, 0
		$pack->PushStr( "error in " . $func_name . '()' );
		$package_group = &new JsonPackageGroup($this->current_serial, $this->current_mainkind, $this->current_subkind);
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
}

?>