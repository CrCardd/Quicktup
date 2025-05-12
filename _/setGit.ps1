cmdkey /generic:LegacyGeneric:target=git:https://github.com /user:username /pass:auth_key

$Proxy = ""

git config --global http.proxy "http://" + $Proxy 

git config --global https.proxy "https://" + $Proxy 

git config --global user.name "user"
git config --global user.email "email"