<?php

/***************************************************************************
 *   modify               : Swings (C) T.I.E in Taiwan 
 ***************************************************************************/

/***************************************************************************
 *   Json 回傳內容的資料版型
 ***************************************************************************/
class JsonPackage
{
	var $MainKind; //協定主版號
	var $SubKind;	//協定副版號
	var $Ints;
	var $Strs;
	
	function __construct($main, $sub)
	{
		$this->MainKind = $main;
		$this->SubKind = $sub;
		$this->Ints = array();
		$this->Strs = array();
	}
	
	public function PushInt($value)
	{
		array_push($this->Ints, $value);
	}
	
	public function PushStr($value)
	{
		array_push($this->Strs, $value);
	}
}

class JsonPackageGroup
{
	var $Serial; // c 端傳來的序號
	var $RequestMain; // c 端請求協定號 main kind
	var $RequestSub; // c 端請求協定號 sub kind
	var $Packages;
	
	function __construct($Serial, $RequestMain, $RequestSub)
	{
		$this->Serial = $Serial;
		$this->RequestMain = $RequestMain;
		$this->RequestSub = $RequestSub;
		$this->Packages = array();	
	}
	
	public function Add($main, $sub)
	{
		$newpack = &new JsonPackage($main, $sub);
		array_push( $this->Packages, $newpack ) ;
	}
	
	public function AddPackage(JsonPackage $package)
	{
		if (!($package instanceof JsonPackage)) {
			return;
		}
		array_push( $this->Packages, $package ) ;
	}
}

?>