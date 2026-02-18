param([string]$Url,[string]$TenantId,[string]$ClientId,[string]$ClientSecret)
pac auth create --url $Url --tenant $TenantId --applicationId $ClientId --clientSecret $ClientSecret
pac auth list