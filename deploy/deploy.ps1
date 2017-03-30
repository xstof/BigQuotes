#
# Pre-requisites: make sure you have logged into Azure via Login-AzureRmAccount
# This powershell script deploys ARM template which creates an API Management instance, one App service plan and a Web app to host an API
#

[CmdletBinding()]
Param(
  # Params required for App Service Plan creation:
  [Parameter(Mandatory=$True)]
  [string]$SubscriptionName,
  
  [Parameter(Mandatory=$True)]
  [string]$RGName,
  
  [Parameter(Mandatory=$True)]
  [string]$Location,

  [Parameter(Mandatory=$True)]
  [string]$AppServicePlanName,

  [Parameter(Mandatory=$True)]
  [string]$WebAppName,

   # Params required for API Management instance creation:
  [Parameter(Mandatory=$True)]
  [string]$ApimPublisherName,
 
  [Parameter(Mandatory=$True)]
  [string]$ApimPublisherEmail

)

$ErrorActionPreference = "Stop"

Select-AzureRmSubscription -SubscriptionName $SubscriptionName
Write-Host "Selected subscription: $SubscriptionName"

# Find existing or deploy new Resource Group:
$rg = Get-AzureRmResourceGroup -Name $RGName -ErrorAction SilentlyContinue
if (-not $rg)
{
    New-AzureRmResourceGroup -Name "$RGName" -Location "$Location"
    Write-Host "New resource group deployed: $RGName"   
}
else{ Write-Host "Resource group found: $RGName"}

$scriptDir = Split-Path $MyInvocation.MyCommand.Path 


#============================
# Deploy ARM template

  New-AzureRmResourceGroupDeployment -Verbose -Force -ErrorAction Stop `
    -Name "webapim" `
    -ResourceGroupName $RGName `
    -TemplateFile "$scriptDir/templates/webandapim.template.json" `
    -publisherName  $ApimPublisherName `
    -publisherEmail $ApimPublisherEmail `
    -appServicePlanName $AppServicePlanName `
    -webAppName $WebAppName



#============================
