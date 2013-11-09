<?php
/***************************************************************************
*	ID			: nindou.common.func.php
*	Licence		: prs
*	Date		: 2009.03.04
*	Coder		: SwingsHuang
*	Specification: 
*			Nindou 通用函式
*	
**************************************************************************/

// 用 device id + time 製作 login session
function get_current_time_session($device_id)
{
	$current_time = time();
	$seed = $device_id . strval($current_time);
	
	return sha1($seed);
}

function get_item_data( $item_id )
{
	$json = json_decode(file_get_contents(JSON_FILE_ITEM_DATA), true);
	foreach( $json as $row )
	{
		if($row['ID'] == $item_id)
		{
			return $row;	
		}
	}
}

// todo: 取得引繼 id
function get_inherited_id($device_id)
{
	return 'zduUT56200';
}

function get_calling_method_name(){
    $e = new Exception();
    $trace = $e->getTrace();
    //position 0 would be the line that called this function so we ignore it
    $last_call = $trace[1];
    //print_r($last_call);
    
    if(isset($last_call['function']))
    	return $last_call['function'];
    return '';
}

/* This function will return the name string of the function that called $function. To return the
    caller of your function, either call get_caller(), or get_caller(__FUNCTION__).
*/
function get_caller($function = NULL, $use_stack = NULL) {
    if ( is_array($use_stack) ) {
        // If a function stack has been provided, used that.
        $stack = $use_stack;
    } else {
        // Otherwise create a fresh one.
        $stack = debug_backtrace();
        echo "\nPrintout of Function Stack: \n\n";
        print_r($stack);
        echo "\n";
    }

    if ($function == NULL) {
        // We need $function to be a function name to retrieve its caller. If it is omitted, then
        // we need to first find what function called get_caller(), and substitute that as the
        // default $function. Remember that invoking get_caller() recursively will add another
        // instance of it to the function stack, so tell get_caller() to use the current stack.
        $function = get_caller(__FUNCTION__, $stack);
    }

    if ( is_string($function) && $function != "" ) {
        // If we are given a function name as a string, go through the function stack and find
        // it's caller.
        for ($i = 0; $i < count($stack); $i++) {
            $curr_function = $stack[$i];
            // Make sure that a caller exists, a function being called within the main script
            // won't have a caller.
            if ( $curr_function["function"] == $function && ($i + 1) < count($stack) ) {
                return $stack[$i + 1]["function"];
            }
        }
    }

    // At this stage, no caller has been found, bummer.
    return "";
    
    // TEST CASE
//	function woman() {
//	    $caller = get_caller(); // No need for get_caller(__FUNCTION__) here
//	    if ($caller != "") {
//	        echo $caller , "() called " , __FUNCTION__ , "(). No surprises there.\n";
//	    } else {
//	        echo "no-one called ", __FUNCTION__, "()\n";
//	    }
//	}
//	
//	function man() {
//	    // Call the woman.
//	    woman();
//	}
//	
//	// Don't keep him waiting
//	man();
}

?>