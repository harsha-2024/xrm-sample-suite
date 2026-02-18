# Variables for PAC scripts
$global:SolutionName = $env:SOLUTION_NAME; if(-not $global:SolutionName){$global:SolutionName="ContosoSolution"}
$global:DevUrl=$env:DEV_ENV_URL;$global:TestUrl=$env:TEST_ENV_URL;$global:ProdUrl=$env:PROD_ENV_URL