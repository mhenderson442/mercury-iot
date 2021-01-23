$resourceGroupName = "mars"

$today=Get-Date -Format "yyyyMMddhhmmss"
$deploymentName = $resourceGroupName + "-deployment-" + $today

$templateFile = "templates/resource-groups/mars-resource-group.json"
$templateParameterFile = "templates/resource-groups/mars-resource-group.parameters.json"

New-AzResourceGroupDeployment `
-Name $deploymentName `
-ResourceGroupName $resourceGroupName `
-Mode "Incremental" `
-TemplateParameterFile $templateParameterFile `
-TemplateFile $templateFile `
-DeploymentDebugLogLevel "ResponseContent"