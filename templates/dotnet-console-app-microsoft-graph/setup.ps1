az login
$appId = az ad app create --display-name "MSGraph console app" --public-client-redirect-uris "http://localhost" --query appId -o tsv
((Get-Content -path Program.cs -Raw) -replace 'CLIENT_ID',$appId) | Set-Content -Path Program.cs