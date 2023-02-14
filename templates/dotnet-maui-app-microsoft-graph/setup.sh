az login
appId=$(az ad app create --display-name "MSGraph MAUI app" --public-client-redirect-uris "https://login.microsoftonline.com/common/oauth2/nativeclient" --query appId -o tsv)
sed -i '' -e "s/CLIENT_ID/$appId/g" ./MAUIwithMSGRaph/GraphService.cs