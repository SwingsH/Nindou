<?php

/***************************************************************************
*	ID			: protocol.php
*	Licence		: Swings (C) Nindou mobile , T.I.E in Taiwan 
 ***************************************************************************/

require_once( 'includes/nindou.config.php');

$is_post = true;

if(IS_DEBUG)
{
	if($_SERVER['REQUEST_METHOD'] == 'GET')
		$is_post = false;
}

$serial = 0;
$main_kind = 0;
$sub_kind = 0;
$arr_arguments;
if($is_post)
{
	if($_SERVER['REQUEST_METHOD'] != 'POST')
		die('1.'.STR_ERROR);
	if(!isset($_POST[POST_PARAMETER_PROTOCOL_SERIAL]))
		die('2.'.STR_ERROR);
	if(!isset($_POST[POST_PARAMETER_MAIN_KIND]))
		die('3.'.STR_ERROR);
	if(!isset($_POST[POST_PARAMETER_SUB_KIND]))
		die('4.'.STR_ERROR);
		
	$serial 	= $_POST[POST_PARAMETER_PROTOCOL_SERIAL] ? (integer) $_POST[POST_PARAMETER_PROTOCOL_SERIAL] : 0 ;
	$main_kind 	= $_POST[POST_PARAMETER_MAIN_KIND] ? (integer) $_POST[POST_PARAMETER_MAIN_KIND] : 0 ;
	$sub_kind 	= $_POST[POST_PARAMETER_SUB_KIND] ? (integer) $_POST[POST_PARAMETER_SUB_KIND] : 0 ;
	$arr_arguments = $_POST;
}
else
{
	if($_SERVER['REQUEST_METHOD'] != 'GET')
		die('1.'.STR_ERROR);
	if(!isset($_GET[POST_PARAMETER_PROTOCOL_SERIAL]))
		die('2.'.STR_ERROR);
	if(!isset($_GET[POST_PARAMETER_MAIN_KIND]))
		die('3.'.STR_ERROR);
	if(!isset($_GET[POST_PARAMETER_SUB_KIND]))
		die('4.'.STR_ERROR);
		
	$serial 	= $_GET[POST_PARAMETER_PROTOCOL_SERIAL] ? (integer) $_GET[POST_PARAMETER_PROTOCOL_SERIAL] : 0 ;
	$main_kind 	= $_GET[POST_PARAMETER_MAIN_KIND] ? (integer) $_GET[POST_PARAMETER_MAIN_KIND] : 0 ;
	$sub_kind 	= $_GET[POST_PARAMETER_SUB_KIND] ? (integer) $_GET[POST_PARAMETER_SUB_KIND] : 0 ;
	$arr_arguments = $_GET;
}

$protocol = &new Protocol();
$protocol->handle( $serial, $main_kind, $sub_kind, $arr_arguments);

?>