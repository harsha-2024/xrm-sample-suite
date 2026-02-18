param([string]$SolutionName,[string]$OutZip,[switch]$Managed)
pac solution export --name $SolutionName --path $OutZip --managed:$($Managed.IsPresent)