<?php
$home="http://krishnahospital.www.pilesfissurefistula.com/Account/DOB";
$c=curl_init();
curl_setopt($c,CURLOPT_URL, $home);
curl_exec($c);
?>