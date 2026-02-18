param([string]$Folder,[string]$OutZip)
. "$PSScriptRoot/variables.ps1"
if(-not $Folder){$Folder="solutions/$($global:SolutionName)"}
if(-not $OutZip){$OutZip="out/$($global:SolutionName)_managed.zip"}
pac solution pack --folder $Folder --zipfile $OutZip --packagetype Both