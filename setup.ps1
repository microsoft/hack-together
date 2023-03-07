az login
$appId = az ad app create --display-name "MSGraph Blazor App" --web-redirect-uris "https://localhost:5001/signin-oidc" --query appId -o tsv
$clientSecret = az ad app credential reset --id $appId --query password -o tsv
((Get-Content -path appsettings.json -Raw) -replace 'CLIENT_ID',$appId) | Set-Content -Path appsettings.json
((Get-Content -path appsettings.json -Raw) -replace 'CLIENT_SECRET',$clientSecret) | Set-Content -Path appsettings.json
