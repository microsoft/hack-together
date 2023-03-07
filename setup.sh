az login
appId=$(az ad app create --display-name "MSGraph Blazor App" --web-redirect-uris "https://localhost:5001/signin-oidc" --query appId -o tsv)
clientSecret=$(az ad app credential reset --id $appId --query password -o tsv)
sed -i '' -e "s/CLIENT_ID/$appId/g" appsettings.json
sed -i '' -e "s/CLIENT_SECRET/$clientSecret/g" appsettings.json