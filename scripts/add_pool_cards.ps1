$dir = "e:\mc\slay-the-nana\src\cards"
if (-not (Test-Path $dir)) { throw "missing $dir" }
$poolUsing = "using MegaCrit.Sts2.Core.Models.CardPools;"
$attr = "[Pool(typeof(NanaDummyCardPool))]"
Get-ChildItem $dir -Filter "*.cs" | ForEach-Object {
    $path = $_.FullName
    $t = [IO.File]::ReadAllText($path)
    if ($t -match [regex]::Escape("NanaDummyCardPool")) { return }
    if ($t -notmatch [regex]::Escape($poolUsing)) {
        $t = $t -replace "^(namespace SlayTheNANA;)", "$poolUsing`r`n`r`n`$1"
    }
    $t = $t -replace "(namespace SlayTheNANA;\s*\r?\n)(\s*)(public (?:sealed )?class )", "`$1`$2$attr`r`n`$2`$3"
    [IO.File]::WriteAllText($path, $t)
    Write-Host "OK $($_.Name)"
}
