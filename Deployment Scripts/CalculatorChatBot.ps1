<#
    Author      :   Pranav S. Krishnamurthy

    Date        :   2019-01-31

    File Name   :   CalculatorChatBot.ps1

    Version History

    Version             Date              Who       Comments
    -------             ----              ---       --------
    1.0                 2019-03-18        PKR       Original version
#>

# Allowing for custom written scripts to properly run
Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope CurrentUser

# Install necessary modules
Install-Module AzureAD -AllowClobber -Scope CurrentUser
Install-Module AzureRM -AllowClobber -Scope CurrentUser

# Prompting the user to authenticate and log in before moving forward
Connect-AzureAD

# Getting the name of the application/bot
$appName = Read-Host -Prompt "Enter the name of your app"

# Getting the description of our bot
$appDescription = Read-Host -Prompt "Enter the description for your app"

# Ensuring that this is in-fact a multi-tenant app because of the fact that we need to automate
# the registration with Bot Framework
$isMultiTenant = $true;

# Now having the registration of the app in AAD through the cmdlet
$appVars = New-AzureADApplication -DisplayName $appName -AvailableToOtherTenants $isMultiTenant

# Get the app logo
$appLogoFolder = Get-Location

# Make sure to have the logo later
$appLogoLocation = -Join($appLogoFolder, '\Assets\logo.jpg')