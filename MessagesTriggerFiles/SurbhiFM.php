r<?php
$home="http://surbhihospital.www.pilesfissurefistula.com/Account/FollowUpMessage";
$c=curl_init();
curl_setopt($c,CURLOPT_URL, $home);
curl_exec($c);
?>