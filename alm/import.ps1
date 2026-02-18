param([string]$ZipPath,[string]$EnvironmentUrl)
. "$PSScriptRoot/variables.ps1"
if(-not $ZipPath){$ZipPath="out/$($global:SolutionName)_managed.zip"}
if(-not $EnvironmentUrl){$EnvironmentUrl=$global:TestUrl}
pac solution import --path $ZipPath --environment $EnvironmentUrl --publish-changes true