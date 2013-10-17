<?php

/***************************************************************************
 *   modify               : Swings (C) T.I.E in Taiwan 
 ***************************************************************************/

/***************************************************************************
 *   Json �^�Ǥ��e����ƪ���
 ***************************************************************************/
class JsonPackage
{
	var $MainKind; //��w�D����
	var $SubKind;	//��w�ƪ���
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
	var $Serial; // c �ݶǨӪ��Ǹ�
	var $RequestMain; // c �ݽШD��w�� main kind
	var $RequestSub; // c �ݽШD��w�� sub kind
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