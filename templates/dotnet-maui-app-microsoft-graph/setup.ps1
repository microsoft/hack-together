az login
$appId = az ad app create --display-name "MSGraph MAUI app" --public-client-redirect-uris "https://login.microsoftonline.com/common/oauth2/nativeclient" --query appId -o tsv
((Get-Content -path .\MAUIwithMSGRaph\GraphService.cs -Raw) -replace 'CLIENT_ID',$appId) | Set-Content -Path .\MAUIwithMSGRaph\GraphService.cs
