$u = "using MegaCrit.Sts2.Core.Models.CardPools;"
Get-ChildItem "e:\mc\slay-the-nana\src\cards\*.cs" | ForEach-Object {
    $path = $_.FullName
    $t = [IO.File]::ReadAllText($path)
    if ($t -notmatch "MegaCrit\.Sts2\.Core\.Models\.CardPools") {
        $t = $t -replace "(namespace SlayTheNANA;)", ($u + "`r`n`r`n`$1")
        [IO.File]::WriteAllText($path, $t)
        Write-Host "using OK $($_.Name)"
    }
}
