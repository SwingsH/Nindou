<?php

/***************************************************************************
*	ID			: nindou.query.string.php
*	Date		: 2013.10.10
*	Coder		: SwingsHuang
*
*	Specification: 
*		Table column name 只能在這裡出現, 有利於 db schema 更改
**************************************************************************/

// Table相關
//define('TABLE_ACCOUNT', 'player_account');


 //取得玩家帳號
function db_insert_log( $message )
 {
 	$generator 	= &new QueryGenerator( 'system_log' );
	$query 		= $generator->insert(	array( 'log_message' ) , 
										array( $message ),
										array( true ) 
										);
 	return  $query ;
}

/*************************************************
 * 相關 Table : player_account
 ************************************************/
 
 //取得玩家帳號
function db_get_account( $device_id )
 {
 	$query = " SELECT * FROM player_account WHERE unique_device =   '" . $device_id . "' " ;
 	
 	return  $query ;
}

 //新增玩家帳號
function db_insert_account( $device_id, $session, $inherit_id )
 {
 	$query = ' SELECT * FROM player_account WHERE unique_device = ' . $device_id ;
 	
 	$generator 	= &new QueryGenerator( 'player_account' );
 	
 	// 玩家尚未索取 引繼 password 時先不處理 ?
	$query 		= $generator->insert(	array( 'unique_device' , 'current_login_session' , 'current_inherit_id' , 'current_inherit_password' ) , 
										array( $device_id , $session , $inherit_id ,  '' ),
										array( true , true , true , true  ) 
										);
										
 	return  $query ;
}

 //新增玩家帳號
function db_update_account( $device_id, $session )
 {
 	$generator 	= &new QueryGenerator( 'player_account' );
	$query = $generator->update( array( 'current_login_session' => $session ),
								 array( true ) ,  
								"  WHERE unique_device = '" . $device_id ."' " );
										
 	return  $query ;
}

/*************************************************
 * 相關 Table : prs_user_favor
 ************************************************/
 function prsdb_get_userfavor( $store_seq , $request = 'by_store' , $uid = 1 ){
 	
 	if( $request == 'by_store' ){
	 	$query = ' SELECT * FROM prs_user_favor puv WHERE puv.store_seq = ' . $store_seq ;
 	}
 	else if( $request == 'by_store_and_uid' ){
 		$query = ' SELECT * FROM prs_user_favor puv WHERE puv.store_seq = ' . $store_seq . 
 					' AND puv.uid = ' . $uid ;
 	}
 	
 	return  $query ;
}

/*************************************************
 * 相關 Table : prs_product , prs_product_top
 *************************************************/
 function prsdb_get_product( $request = 'all' , $prod_seq = 0 , $where_query = "" , $limit_start = 0 , $limit = 9 ) {
 	switch( $request ){
 	case 'all' :
 		$query = " SELECT * FROM prs_product pp " ;
 		
 		if( $where_query != "" ){
 			$query = $query . ' WHERE pp.state = "on" AND storage >= 1 AND  ' . $where_query ;	
 		}
 		$query = $query . " ORDER BY orderby ASC " . 
 					' LIMIT ' . $limit_start . ' , ' . $limit ;		
 	break ;
 	
 	case 'count' :
 		$query = " SELECT COUNT( prod_seq ) AS total FROM prs_product pp" ;
 		
 		if( $where_query != "" ){
 			$query = $query . ' WHERE pp.state = "on" AND storage >= 1 AND ' . $where_query ;	
 		}
 		$record = db_getrow( $query ) ;
 		return $record[ 'total' ] ;
 	break ;
 	
 	case 'one' :
 		$query = " SELECT * FROM prs_product pd WHERE pd.state = 'on' AND pd.prod_seq = " . $prod_seq ;
 		return db_getrow( $query ) ;
 	break ;
 	
 	default :
 		
 	}
 	
 	return  $query ;
}

/***************************************************
 *	$type = 1 (人氣商品廣告) , $type = 2 (兌換排行榜)
 ***************************************************/
 function prsdb_get_producttop( $type = 1 , $request = 'rank' , $limit_start = 0 , $limit = 30 , $order_by = 'change_point' ) {
 	
 	if(  $type == 0  )	$type_query = "  " ; 	
 	else if( $type == 1 ){
 		$type_query = ' ppt.type = 1 ' ;
 		 $group_query = " GROUP BY playway " . " ORDER BY playway  " ;
 	}
 	else if( $type == 2 ){
 		 $type_query = ' ppt.type = 2 ' ;
 		 $group_query = " GROUP BY rank " . " ORDER BY ppt.rank " ;
 	}
 	switch( $request ){
 	
 	case 'rank' :
 		$query = " SELECT * FROM prs_product_top ppt JOIN prs_product pp ON ppt.prod_seq = pp.prod_seq " .
 					" WHERE  pp.state = 'on' AND pp.storage >= 1 AND " . $type_query .
 					$group_query .
 					" LIMIT " . $limit_start . ' , ' . $limit ;
 	default :
 		
 	}

 	return  $query ;
}
 
/*******************************************************
 * 相關 Table : prs_discount , prs_discount_log_order , prs_discount_log_flow
 *******************************************************/
function prsdb_get_discount( $order = 'rank' , $limit = 10  , $start_date = false , $end_date = false , $monthly_date = false ){
	
	if( $order == 'download_times' ){
		$query = prsdb_query_discount(  $order , $limit ) ;
	}
	else if( $order == 'billboard_download_times' ){
		$query = prsdb_query_discount( 'download_times'  ,  25 , $start_date , $end_date , 'BY_DISCOUNT' , 'BY_COUNT' ) ;
	}
	else if( $order == 'rank' ){
		$query = " SELECT *  FROM prs_discount pd JOIN prs_store ps ON ps.store_seq = pd.store_seq " .
					" ORDER BY pd.discount_rank ASC" .
					" LIMIT 0 , " . $limit ;	
	}
	else if( $order == 'billboard' ){
		$query = " SELECT DISTINCT( pd.discount_rank ), pd.discount_seq ,  pd.title , pd.store_seq , pd.title_color , pd.discount_rank_lastweek , pd.discount_rank " .
					" FROM prs_discount pd JOIN prs_store ps ON ps.store_seq = pd.store_seq " .
					" WHERE discount_rank <> 0  " .
					" ORDER BY pd.discount_rank ASC" .
					" LIMIT 0 , " . $limit ;
	}
	else if( $order = 'monthly' ){
		$query = " SELECT ps.store_seq , discount_seq , LEFT( discount_create_time , 10 ) AS discount_create_time , title" . 
					" FROM prs_discount pd JOIN prs_store ps ON ps.store_seq = pd.store_seq " .
					" WHERE discount_state = 1 AND LEFT( discount_create_time , 7 ) = '" . $monthly_date ."' " .
					" ORDER BY LEFT( discount_create_time , 10 ) ASC ";
	}
	
	return  $query ;
}
/*******************************************************
 * 相關 Table : prs_discount , prs_discount_log_order , prs_discount_log_flow
 * 單一 Store 之多筆  discount 資料
 * 或單一  discount 資料
 *******************************************************/
function prsdb_get_dis( $seq , $request = 'nums' ){
	if( $request == 'nums' ){
		$query = " SELECT * " .
					" FROM prs_discount pd " .
					" WHERE pd.store_seq = " . $seq ;	
	}
	else if( $request == 'single' ){
		$query = " SELECT * " .
					" FROM prs_discount pd " .
					" WHERE pd.discount_seq = " . $seq ;		
	}
	
	return  $query  ;
}

/*************************************************
 * 取出多筆 Store 資料
 * 相關 Table : prs_store ,  prs_store_view
 *************************************************/
function prsdb_get_stores( $order = 'rank' ,  $limit = 10 ){
	
	if( $order == 'visit' ){
		$query = "SELECT psv.store_seq, ps.store_name, count( psv.store_seq ) AS view " .
					" FROM prs_store ps LEFT JOIN prs_store_view psv " .
					" ON ps.store_seq = psv.store_seq " .
					" WHERE ps.state = 1 " .
					" GROUP BY psv.store_seq " .
					" ORDER BY view " .
					" LIMIT 0 , " . $limit ;
	}
	else if( $order == 'rank' ){
		$query = " SELECT  * " . 
					" FROM prs_store ps" .
					" WHERE ps.state = 1 " .
					" LIMIT 0 , " . $limit ;		
	}
	else if( $order == 'rank_has_grade_views' ){
		$query = " SELECT ps.store_seq, ps.store_name , ps.store_rank , ps.store_rank_lastweek , AVG( pa.appreciation_value ) AS grade , COUNT( psv.store_seq ) AS view  " . 
					" FROM prs_store ps LEFT JOIN prs_appreciation pa ON ps.store_seq = pa.store_seq , prs_store_view psv " .
					" WHERE ps.store_seq = psv.store_seq AND ps.store_rank <> 0 AND ps.state = 1 " .
						sprintf( " AND ( psv.create_date BETWEEN  '%s' AND '%s'  ) " , $start_date , $end_date ) .
					" GROUP BY ps.store_seq " .
					" ORDER BY ps.store_rank ASC " .
					" LIMIT 0 , " . $limit ;	
	}
	else if( $order == 'rank_has_grade' ){
		$query = " SELECT ps.store_seq, ps.store_name , ps.store_rank , ps.store_rank_lastweek , AVG( pa.appreciation_value ) AS grade , COUNT( psv.store_seq ) AS view  " . 
					" FROM prs_store ps LEFT JOIN prs_appreciation pa ON ps.store_seq = pa.store_seq , prs_store_view psv " .
					" WHERE ps.store_seq = psv.store_seq  AND ps.store_rank <> 0  AND ps.state = 1 " . // AND ps.store_rank <> 0
					" GROUP BY ps.store_seq " .
					" ORDER BY ps.store_rank ASC " .
					" LIMIT 0 , " . $limit ;	
	}
	return  $query ;
}


/*************************************************
 * 取出單筆 Store 資料
 * 相關 Table : prs_store ,  prs_store_picture , prs_store_view
 *************************************************/
function prsdb_get_store( $store_seq , $order = 'picture' ,  $limit = 1 ){
	
	// Store info and picture 
	// if no picture 會有 null ...
	if( $order == 'mixed'  ){
		$query = 	" SELECT psp.pic_seq , ps.store_seq , psp.uid , psp.photo , psp.title , psp.upload_time , " . 
						" psp.store_order , psp.member_order , psp.cover , psp.mode , " .
						" ps.post_uid , ps.owner_uid, ps.apply_uid , ps.class_seq , ps.arc_seq , ps.store_name , ps.state , ps.state_note , ps.store_contacts, ps.intro , ps.type_tag, ps.commend_prod , ps.phone , ps.city , ps.addr , ps.wapurl, ps.appreciation_value , ps.notice_state1 , ps.notice_state2 , ps.notice_state3 , ps.keyword , ps.store_create_time , ps.store_rank , ps.store_rank_lastweek , ps.modify_time , ps.modify_text , ps.position , ps.lat , ps.lng " .
						" up.nick AS post_nickname " .
					" FROM prs_store ps LEFT JOIN prs_store_picture psp ON ps.store_seq = psp.store_seq  , user_profile up " . 
					" WHERE ps.store_seq = " . $store_seq .
					" AND up.uid = ps.post_uid  " ; 
	}
	else if( $order == 'picture' ){
		$query = 	" SELECT psp.pic_seq , ps.store_seq , psp.uid , psp.photo , psp.title , psp.upload_time , " . 
						" psp.store_order , psp.member_order , psp.cover , psp.mode , " .
						" ps.post_uid , ps.owner_uid, ps.apply_uid , ps.class_seq , ps.arc_seq , ps.store_name , ps.state , ps.state_note , ps.store_contacts, ps.intro , ps.type_tag, ps.commend_prod , ps.phone , ps.city , ps.addr , ps.wapurl, ps.appreciation_value , ps.notice_state1 , ps.notice_state2 , ps.notice_state3 , ps.keyword , ps.store_create_time , ps.store_rank , ps.store_rank_lastweek , ps.modify_time , ps.modify_text , ps.position , ps.lat , ps.lng " .
					" FROM prs_store ps LEFT JOIN prs_store_picture psp ON ps.store_seq = psp.store_seq  " .
					" WHERE ps.store_seq = " . $store_seq ;
					 // " AND psp.cover = 1 " ; // 商家封面 
					 // " AND psp.mode = 1 " ; 	//商家上傳
	}
	else if( $order == 'store' ){
		$query = 	" SELECT ps.store_seq , ps.post_uid , ps.owner_uid, ps.apply_uid , ps.class_seq , ps.arc_seq , ps.store_name , ps.state , ps.state_note , " . 
					" ps.store_contacts, ps.intro , ps.type_tag, ps.commend_prod , ps.city , ps.addr , ps.wapurl, ps.appreciation_value , ps.notice_state1 , ps.notice_state2 , ps.notice_state3 , ps.keyword , ps.store_create_time , ps.store_rank , ps.store_rank_lastweek , ps.modify_time , ps.modify_text , ps.position , ps.lat , ps.lng " .
					" FROM prs_store ps " .
					" WHERE ps.store_seq = " . $store_seq ;
	}
	else if( $order == 'store_picnums' ){
		$query = 	" SELECT count( psp.pic_seq ) AS pics" .
						"  ps.post_uid , ps.owner_uid, ps.apply_uid , ps.class_seq , ps.arc_seq , ps.store_name , ps.state , ps.state_note , ps.store_contacts, ps.intro , ps.type_tag, ps.commend_prod , ps.city , ps.addr , ps.wapurl, ps.appreciation_value , ps.notice_state1 , ps.notice_state2 , ps.notice_state3 , ps.keyword , ps.store_create_time , ps.store_rank , ps.store_rank_lastweek , ps.modify_time , ps.modify_text , ps.position , ps.lat , ps.lng " .
					" FROM prs_store ps LEFT JOIN prs_store_picture psp ON psp.store_seq = ps.store_seq " . 
					" WHERE psp.store_seq = " . $store_seq ;
	}
	
	return  $query ;
}

// 整合舊 appre 資料至 MAPPING TABLE , APPRE_COMMEND / APPRE_TYPETAGs
function test_store_appre(){
	$query = " SELECT * FROM prs_appreciation " ;
	// echo $query ;
	$record = db_getallrows( $query ) ;
	
	// print_r( $record ) ;
	foreach( $record as $row ){
		$query = " SELECT * FROM prs_appreciation_commend_prod WHERE app_seq  = " . $row[ 'app_seq' ] ; 
		$rec = db_getrow( $query ) ;
		if( intval( $rec ) == 0 ){
			$Quicker = &new DBQuicker( 'prs_appreciation_commend_prod' ) ;
			$query = $Quicker->insert( array( 'app_seq' , 'commend_prod' ) , array( $row[ 'app_seq' ] , $row[ 'commend_prod' ] ) ,
									array( false , true ) ) ;
			db_execute( $query ) ;
			echo $row[ 'app_seq' ] .' -commend_prod 更新成功<br/> ' ;
		}
		else{
			echo $row[ 'app_seq' ] .' -commend_prod 已有資料<br/>' ;
		}
		
		$query = " SELECT * FROM prs_appreciation_type_tag WHERE app_seq  = " . $row[ 'app_seq' ] ; 
		$rec = db_getrow( $query ) ;
		if( intval( $rec ) == 0 ){
			$Quicker = &new DBQuicker( 'prs_appreciation_type_tag' ) ;
			$query = $Quicker->insert( array( 'app_seq' , 'type_tag' ) , array( $row[ 'app_seq' ] , $row[ 'type_tag' ] ) ,
									array( false , true ) ) ;
			db_execute( $query ) ;
			echo $row[ 'app_seq' ] .' - ' .$row[ 'type_tag' ]. ' -type_tag更新<br/> ' ;
		}
		else{
			echo $row[ 'app_seq' ] .'-type_tag已有資料<br/>' ;
		}
	}
}
// 整合舊 user_favor 資料至 mapping table prs_user_favor_type_tag
function test_store_user_favor(){
	$query = " SELECT * FROM prs_user_favor" ;
	$record = db_getallrows( $query ) ;
	
	foreach( $record as $row ){
		$uf_seq = $row[ 'uf_seq' ] ;
		
		if( intval( db_getrow( " SELECT * FROM prs_user_favor_type_tag WHERE uf_seq = " . $uf_seq ) ) == 0  ){ 
			$type_tag = $row[ 'type_tag' ]  == "" ? '未填推薦商品' :  $row[ 'type_tag' ] ;
			$Quicker = &new DBQuicker( 'prs_user_favor_type_tag' ) ;
			$query = $Quicker->insert( array( 'uf_seq' , 'type_tag' ) , array( $uf_seq , $type_tag ) , array( false , true )) ;
			
			db_execute( $query ) ;
			echo $uf_seq .' - ' .$type_tag. ' -user favor type_tag更新<br/> ' ;
		}
		else{
			echo $uf_seq .' - 已有資料<br/> ' ;	
		}
	}
}
//
//if( $_GET[ 'test' ] ){
//	require_once( "../config.inc.php") ;
//	//print_r( db_getallrows( ' SELECT * FROM prs_store_picture WHERE store_seq = 1 ' ) ) ;
//	//print_r( prsdb_get_joywap_store_user( 18 ) ) ;
//	//print_r(prsdb_get_store_all_commendprod( 2 ) ) ;
//	//prsdb_get_store_type_tag( 2 ) ;
//	//print_r( db_getallrows( ' SELECT * FROM prs_appreciation ' ) ) ;
//	//print_r( db_getallrows( ' SELECT * FROM prs_appreciation_type_tag ' ) ) ;
//	//print_r( db_getallrows( ' SELECT * FROM user_area ' ) ) ;
//	//print_r( db_getallrows( ' SELECT * FROM user_hint ' ) ) ;
//	//print_r( db_getallrows( ' SELECT * FROM user_income ' ) ) ;
//	//echo '<br/>' . prsdb_get_userarea( '基隆市' ) ;
//	// print_r( db_getallrows( ' SELECT * FROM user_profile ORDER BY create_time DESC LIMIT 0 , 30  ' ) ) ;
//	//test_store_user_favor();
//	//test_store_appre();
//	// prsdb_get_appreciation( 0 , 'by_store_nums' , 1  )
//	// print_r( prsdb_get_appreciation_type_tag( 3 ) ) ;
//	// print_r( prsdb_get_appreciation_commend_prod( 3 ) ) ;
//	echo prsdb_get_stores( 'rank_has_grade_views' ) ;
//}

?>