<?php

class ErrorLog{
	var $file_path ;
	var $file_name ;
	var $time_enable ;
	var $time_label ;
	var $arr_vars = array();

	function __construct( $path ,  $file = null , $time_enable = true ){
		$this->file_path = $path ;
		$this->file_name = $file ;
		$this->time_enable = $time_enable ;
		$this->time_label = "" ;
	}
	
	function save( $error ){
		$this->file_name = $this->file_path . date("Y_m") . ".dmlog";
		
		$fp = fopen( $this->file_name ,'a+');
		$content = $error . $this->get_time() ;
		fwrite($fp, $content . "\n");
	}
	
	function open_file(){
	
	}
	
	function get_time(){
		$date = getdate();
		$this->time_label = " ".$date["year"]."/".$date["mon"]."/".$date["mday"]."  ".$date["hours"].":".$date["minutes"].":".$date["seconds"]." - ".$date["weekday"];
		return $this->time_label ;
	}
	
	function cache_by_param(){
		if(file_exists($this->file_cache)){
			return false;
		}
		$fp = fopen( $this->file_cache , 'a+');
		//echo current($this->arr_vars) ;
		for( ; $value = current($this->arr_vars); next($this->arr_vars) ){
			fwrite($fp, key($this->arr_vars).'='.$value.'&');
		}
	}
}

 // 表單 Form 產生器
class FormGenerator{
	var $arr_hidden_buttons = array();
	var $html = "" ;
	
	// hidden input format
	var $fmt_hidden_button = '<input type="hidden" ' ; 
	
	function __construct(){
		
	}
	
	public function set_button_type( $type ){
		if( $type == 'text' ){
			$this->fmt_hidden_button =  '<br/><input type="text" ' ;
		}
		else if( $type == 'hidden' ){
			$this->fmt_hidden_button =  '<input type="hidden" ' ;
		}
	}
	
	function set_hidden_button( $name , $value){
		$button = array('name'=>$name,'value'=>$value );
		array_push( $this->arr_hidden_buttons , $button );
	}
	
	// 將陣列所有元素製作成 Hidden Button
	function batch_hidden_buttons( $arr_form ){
		foreach( $arr_form as $key => $value){
			$button = array('name'=>$key, 'value'=>$value );
			array_push( $this->arr_hidden_buttons , $button );
		}
	}
	
	//輸出所有隱藏按鈕
	function get_hidden_buttons( $flush = true){
		while($this->arr_hidden_buttons){
			$btn = array_shift( $this->arr_hidden_buttons );
			$this->html = $this->html . 
			$this->fmt_hidden_button . ' name="'. $btn['name'] .'" '
			.' value="'.$btn['value'].'" >' ;
		}
		return $this->html ;
	}
}

// 快速產生 SQL 指令用
class QueryGenerator{
	var $table ;
	var $sql_command ;
	
	function __construct( $tb_name ){
		$this->table = $tb_name ;
	}
	function insert( $arr_col , $arr_val , $arr_isstr){
		$pre = "INSERT INTO " .$this->table ;
		$col = " (" ;
		$val = " VALUES ( " ;
		for( $i=0 ; $i < count($arr_col)-1 ; $i++ ){
			// Generate Column
			$col = $col . $arr_col[ $i ] . " , " ;
			// Generate Values
			$val = $val . ( $arr_isstr[$i] ?  " \"".$arr_val[ $i ]."\" ," : $arr_val[ $i ] . " , ") ;
		}
		$col = $col . $arr_col[ $i ] . ") " ;
		$val = $val . ($arr_isstr[$i] ?  " \"".$arr_val[ $i ]."\" " : $arr_val[ $i ])  . ' ) ' ;
		
		$this->sql_command = $pre . $col  . $val;
		return $this->sql_command ;
	}
	
	function select( $arr_columns , $where_query = null ){
		$select_query = "" ;
		if( !is_array( $arr_columns ) && $arr_columns == '*' ){
			$select_query = 'SELECT * ';
		}
		$this->sql_command = $select_query . " FROM " . $this->table ;
		if( !empty($where_query) )
			$this->sql_command = $this->sql_command . $where_query ;
		
		return $this->sql_command ;
	}
	
	function delete( $where_query  ){
		$where_query = " WHERE " . $where_query ;
		
		$this->sql_command = "DELETE FROM " . $this->table . $where_query ;
		
		return $this->sql_command ;
	}
	
	// arg1 mixed array , arg2 boolean array check if the column is string
	// $arr_coldata require column=>data , key=>value
	function update( $arr_coldata , $arr_isstr , $where_query){
		$set_query = ' SET ' ;
		$count = 0 ; $max = count($arr_coldata);
		foreach( $arr_coldata as $key=>$value ){
			if( $arr_isstr[ $count++ ] == true ){ // is string
				$set_query = $set_query . $key ." = '".$value. "' " ;
			}
			else{
				$set_query = $set_query . $key ." = ".$value ;
			}
			$set_query = $set_query . ( $count < $max ? ', ' : ' ' ) ;
		}
		
		$this->sql_command = 'UPDATE '.$this->table. $set_query . $where_query ;
		return $this->sql_command ;
	}
}

?>