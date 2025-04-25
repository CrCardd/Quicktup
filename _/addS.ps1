# ADD S:// FOLDER
$RootPath = ""

New-PSDrive -Name "S" -Root $RootPath -Persist -PSProvider "FileSystem" -Scope "Global"
