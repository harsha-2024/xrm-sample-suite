param([string]$ZipPath,[string]$Folder)
. "$PSScriptRoot/variables.ps1"
if(-not $ZipPath){$ZipPath="out/$($global:SolutionName).zip"}
if(-not $Folder){$Folder="solutions/$($global:SolutionName)"}
pac solution unpack --zipfile $ZipPath --folder $Folder --packagetype Both