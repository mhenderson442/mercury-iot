$resourceGroupName = "venus-iot"

$today=Get-Date -Format "yyyyMMddhhmmss"
$deploymentName = $resourceGroupName + "-deployment-" + $today

$templateFile = "./templates/iot-template.json"
$templateParameterFile = "./templates/iot-template.parameters.json"

New-AzResourceGroupDeployment `
-Name $deploymentName `
-ResourceGroupName $resourceGroupName `
-Mode "Incremental" `
-TemplateParameterFile $templateParameterFile `
-TemplateFile $templateFile `
-DeploymentDebugLogLevel "ResponseContent"