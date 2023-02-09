az login
$appId = az ad app create --display-name "MSGraph Blazor App" --public-client-redirect-uris "https://localhost:5001/signin-oidc" --query appId -o tsv
((Get-Content -path Program.cs -Raw) -replace 'CLIENT_ID',$appId) | Set-Content -Path Program.cs
