$resourceGroupName = "venus-iot"

$today=Get-Date -Format "yyyyMMddhhmmss"
$deploymentName = $resourceGroupName + "-deployment-" + $today

$templateFile = "./iot-template.json"
$templateParameterFile = "./iot-template.parameters.json"

New-AzResourceGroupDeployment `
-Name $deploymentName `
-ResourceGroupName $resourceGroupName `
-Mode "Incremental" `
-TemplateParameterFile $templateParameterFile `
-TemplateFile $templateFile