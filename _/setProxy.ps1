$User = ""
$Password = ""
$Proxy = ""

# SET PROXY
[System.Environment]::SetEnvironmentVariable("http_proxy", "http://" + $User + ":" + $Password + "@" + $Proxy, "User");
[System.Environment]::SetEnvironmentVariable("https_proxy", "http://" + $User + ":" + $Password + "@" + $Proxy, "User");


