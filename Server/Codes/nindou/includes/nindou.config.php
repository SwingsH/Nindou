<?php
// ** Path Setting ** //
define('ROOT_PATH', $_SERVER['DOCUMENT_ROOT'] . '/nindou/') ;
define('INCLUDE_PATH', ROOT_PATH . 'includes/' ) ;

// ** MySQL settings ** //
define('DB_NAME', 'nindou_mobile');    // The name of the database
define('DB_USER', 'nindou');     	// Your MySQL username
define('DB_PASSWORD', '20130717'); // ...and password
define('DB_HOST', 'localhost');    // 99% chance you won't need to change this value

// ** SystemL settings ** //
define('IPDOMAIN','http://127.0.0.1/');
define('SLASH','/');
define('UNDERLINE','_');

define('POST_PARAMETER_MAIN_KIND','mainkind');
define('POST_PARAMETER_SUB_KIND','subkind');
define('POST_PARAMETER_DEVICE_ID','deviceid');
define('POST_PARAMETER_PROTOCOL_SERIAL','serial');
define('POST_PARAMETER_STRING','s%d');
define('POST_PARAMETER_INTEGER','i%d');

define('IS_DEBUG', true) ;

define('STR_ERROR', 'No Data in this Page ! 請由正常管道進入');

require_once( INCLUDE_PATH . 'class.json.package.php');
require_once( INCLUDE_PATH . 'class.protocol.php');
require_once( INCLUDE_PATH . 'class.default.php');
require_once( INCLUDE_PATH . 'class.mysqldb.php');
require_once( INCLUDE_PATH . 'nindou.query.string.php');
require_once( INCLUDE_PATH . 'nindou.common.func.php');

global $db;
init_db_connect();
function init_db_connect(){
	global $db;
	$db = new MySqlDB( DB_HOST , DB_USER , DB_PASSWORD , DB_NAME , false );
	$db->sql_query("SET NAMES 'UTF8'");
	//return $db ;
}

?>