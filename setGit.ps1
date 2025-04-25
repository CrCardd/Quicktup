cmdkey /delete:LegacyGeneric:target=git:https://github.com

$Proxy = ""

git config --global http.proxy "http://" + $Proxy 

git config --global https.proxy "https://" + $Proxy 

git config --global user.name "user"
git config --global user.email "email"