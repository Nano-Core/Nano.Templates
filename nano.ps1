$find = 'Nano.Template'
$replace = $($args[0])

foreach ($sc in dir -recurse -include * -Exclude Nano.ps1 | where { test-path $_.fullname -pathtype leaf} ) {
    select-string -path $sc -pattern $find
       (get-content $sc) | foreach-object { $_ -replace $find, $replace } | set-content $sc
}

Get-ChildItem -File -Recurse -Include $match | Rename-Item -NewName { $_.Name.replace($find, $replace) }
Get-ChildItem -Directory -Recurse -Include *$find* | Rename-Item -NewName { $_.Name.replace($find, $replace) }