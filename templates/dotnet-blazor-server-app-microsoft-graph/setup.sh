az login
appId=$(az ad app create --display-name "MSGraph Blazor App" --public-client-redirect-uris "https://localhost:5001/signin-oidc" --query appId -o tsv)
sed -i '' -e "s/CLIENT_ID/$appId/g" Program.cs