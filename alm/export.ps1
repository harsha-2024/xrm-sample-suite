param([string]$SolutionName,[string]$OutZip,[switch]$Managed)
. "$PSScriptRoot/variables.ps1"
if(-not $SolutionName){$SolutionName=$global:SolutionName}
if(-not $OutZip){$OutZip="out/$SolutionName.zip"}
pac solution export --name $SolutionName --path $OutZip --managed:$($Managed.IsPresent)