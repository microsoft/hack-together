##Switch-AzureMode AzureResourceManager
##Connect-AzAccount
##Update-Module -Name Az
##az login
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process -Force
Install-Module AzureAD
$tenantId = "yourTenantIdGuid"
$appname = "daemon-console"
$displayname = "MSGraph Daemon Console Test App"
## if running the registration a 2nd time, uncomment this line
#. .\Cleanup.ps1 -TenantId $tenantId
. .\Configure.ps1 -TenantId $tenantId -AppName $appname -DisplayName $displayname

