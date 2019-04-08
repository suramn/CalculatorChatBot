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

# Getting the bot Icon
$iconUrl = Read-Host -Prompt "Enter a URL for the bot which resolves to a PNG file"

# Ensuring that this is in-fact a multi-tenant app because of the fact that we need to automate
# the registration with Bot Framework
$isMultiTenant = $true;

# Now having the registration of the app in AAD through the cmdlet
$appVars = New-AzureADApplication -DisplayName $appName -AvailableToOtherTenants $isMultiTenant

# Now making sure to have the Password generated
$pwdVars = New-AzureADApplicationPasswordCredential -ObjectId $appVars.ObjectId

# Get the app logo
$appLogoFolder = Get-Location
$appLogoLocation = -Join($appLogoFolder, '\Assets\logo.jpg')

# Setting our logo for the Azure Application
Set-AzureADApplicationLogo -ObjectId $appVars.ObjectId -FilePath $appLogoLocation

# Get the Repo URL and branch
$repoUrl = Read-Host -Prompt "Please provide the GitHub URL for the source code"
$branch = Read-Host -Prompt "Enter the branch name (i.e. master)"

# Creating the parameters.json file
$schemaLink = "https://schema.management.azure.com/schemas/2015-01-01/deploymentParameters.json#"
$contentVer = "1.0.0.0"

$parameters = @{
    '$schema' = $schemaLink;
    contentVersion = $contentVer;
    parameters = @{
        botAppID = @{
            "value" = $appVars.AppId
        }
        botAppPwd = @{
            "value" = $pwdVars.Value
        }
        botDisplayName = @{
            "value" = $appName
        }
        botDescription = @{
            "value" = $appDescription
        }
        botIconUrl = @{
            "value" = $iconUrl
        }
        repoUrl = @{
            "value" = $repoUrl
        }
        repoBranch = @{
            "value" = $branch
        }
    }
}

# Generating the output parameters.json file
$outputFolder = Get-Location
$outputFileLoc = -Join($outputFolder, '\parameters.json');

$parameters | ConverTo-Json -Depth 5 | Out-File $outputFileLoc

<#
    Now the deployment will happen
#>

# Getting the resource group name
$resourceGroupName = Read-Host -Prompt "Please enter the name of the Resource Group"

# Running to an initial check to see if the resource group actually exists
Get-AzureRmResourceGroup -Name $resourceGroupName -ErrorVariable isPresent -ErrorAction SilentlyContinue

if ($isPresent)
{
    Write-Host "Cannot find a Resource Group with the name "$resourceGroupName
    Write-Host "That's okay! Create the new Resource Group named "$resourceGroupName

    $rgLocation = Read-Host -Prompt "Enter a location (i.e. centralus) from the location of the Resource Group"

    $deploymentName = Read-Host -Prompt "Enter a name for this deployment"

    # Ensuring to have the necessary information for the deployment
    $folderLocation = $outputFolder
    $templateFile = -Join($folderLocation, '\azuredeploy.json')
    $templateParamFile = $outputFileLoc

    # Create the new Resource Group
    New-AzureRmResourceGroup -Name $resourceGroupName -Location $rgLocation

    # Kicking off the deployment
    New-AzureRmResourceGroupDeployment -ResourceGroupName $resourceGroupName -Name $deploymentName -TemplateFile $templateFile -TemplateParameterFile $templateParamFile
}
else
{
    Write-Host "Found the Resource Group "$resourceGroupName
    Write-Host "Continuing the deployment to "$resourceGroupName

    $deploymentName = Read-Host -Prompt "Enter a name for the deployment"

    $folderLocation = $outputFolder
    $templateFile = -Join($folderLocation, '\azuredeploy.json')
    $templateParamFile = $outputFileLoc

    # Continuing with the deployment now
    New-AzureRmResourceGroupDeployment -ResourceGroupName $resourceGroupName -Name $deploymentName -TemplateFile $templateFile -TemplateParameterFile $templateParamFile
}