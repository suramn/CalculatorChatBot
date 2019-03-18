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