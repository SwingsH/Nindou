<html>
什麼是 BOM (Byte-Order Mark)？
<p>
在一些平台上，是把代表數值較大的 byte 放在前面，這稱為 Big Endian (BE) 的系統；有些平台則相反，是把代表數值較小的 byte 放在前面，稱為 Little Endian (LE) 的系統。
<p>
若採 LE 方式編碼，BOM 會表示為 0xFF 0xFE，而在 Unicode 的定義中是不存在 U+FFFE 這個字元的.
<p>
若採 BE 方式編碼，BOM 會表示為 0xFE 0xFF，而 U+FEFF 剛好是在 Unicode 中的有效字元，代表的是一個不佔空間的 space 符號，所以即使沒被解釋為 BOM，也不會對閱覽者產生錯誤的訊息. 
<p>
如何移除？ (使用 PHP)
<p>
BOM信息是文件開頭的一串隱藏的字符，用於讓某些編輯器識別這是個UTF-8編碼的文件。但PHP在讀取文件時會把這些字符讀出，從而形成了文件開頭含有一些無法識別的字符的問題。
要檢測一個UTF-8文件是否含有BOM信息，就是檢測文件開頭的字三個符，是否為0xEF, 0xBB, 0xBF。
<p>
下方有個小程式，使用者可以搜尋某個目錄下所有文件，並檢測是否加了BOM。
<p>

<?php 
//此文件用於快速測試UTF8編碼的文件是不是加了BOM，並可自動移除 
//By Bob Shen 

$basedir="."; //修改此行為需要檢測的目錄，點表示當前目錄 
$auto=1; //是否自動移除發現的BOM信息。1為是，0為否。 

//以下不用改&#21160; 

if ($dh = opendir($basedir)) { 
while (($file = readdir($dh)) !== false) { 
if ($file!='.' && $file!='..' && !is_dir($basedir."/".$file)) echo "<br/>filename: $file ".checkBOM("$basedir/$file")." 
"; 
} 
closedir($dh); 
} 

function checkBOM ($filename) { 
global $auto; 
$contents=file_get_contents($filename); 
$charset[1]=substr($contents, 0, 1); 
$charset[2]=substr($contents, 1, 1); 
$charset[3]=substr($contents, 2, 1); 
if (ord($charset[1])==239 && ord($charset[2])==187 && ord($charset[3])==191) { 
if ($auto==1) { 
$rest=substr($contents, 3); 
rewrite ($filename, $rest); 
return ("BOM found, automatically removed."); 
} else { 
return ("BOM found."); 
} 
} 
else return ("BOM Not Found."); 
} 

function rewrite ($filename, $data) { 
$filenum=fopen($filename,"w"); 
flock($filenum,LOCK_EX); 
fwrite($filenum,$data); 
fclose($filenum); 
} 
?> 

<p>
-------------------
<p>
2005-11-1 修正
<p>
本次測試結果：
<p>
1. UltraEdit 10
缺點：商業軟體。
優點：完全符合需求。
<p>
2. EditPlus
缺點：無法移除 BOM 碼，借助其他方式移除之後中文變成亂碼。商業軟體。
優點：可自訂程式語法格式、中文化。
<p>
2.1 EditPlus 2.20
缺點：商業軟體。
優點：完全符合需求。
<p>
3. PSPad editor
缺點：無法移除 BOM 碼
優點：具有 16 進位編輯模式、中文化.....如果不是要移除 BOM 碼，以後我就會改用了。
<p>
4. Zend Studio Client
缺點：商業軟體、很慢 (測試機器不夠力)。
優點：似乎也可以符合需求，不過因為太慢了，沒有仔細測試過。
<p>
5. Notepad++
缺點：無法移除 BOM 碼。
優點：具有摺疊層次功能、可自訂程式語法格式、中文化。 
</html>