# VERSION: 240416

param(
    [switch]$help,
    [switch]$xp,
    [switch]$g,
    [System.Object[]]$fi,
    [System.Object[]]$se,
    [System.Object[]]$th
)

if ($help) {
    Write-Host "-xp = experience"
    Write-Host "-g = gold"
    Write-Host "-fi = first quest <gold, xp, time>"
    Write-Host "-se = first quest <gold, xp, time>"
    Write-Host "-th = first quest <gold, xp, time>"
    return
}

function Average {
    param (
        [object[]]$a
    )
    $sum = $a | ForEach-Object {$local:total=0} {$local:total += $_} {$local:total}
    return ([decimal]$sum / [decimal]$a.Length)
}


function Parse {
    param (
        [System.Object[]]$a,
        [Int16]$ttag = 0
    )
    if ($xp) {
        $xpe = $a[0]
        $d = $a[1].split(":", 3)
        $gold = 0
    } elseif ($g) {
        $gold = $a[0]
        $d = $a[1].split(":", 3)
        $xpe = 0
    } else {
        $gold = $a[0]
        $xpe = $a[1]
        $d = if ($a[2] -is [string]) {$a[2].Split(":", 3)} else {$a[2]}
    }
    if ($d -is [double] -or $d -is [int]) {
        $time = $d * 60
    } else {
        [int]$s = if ($d.Length -ge 1) { $d[$d.Length - 1] } else { 0 }
        [int]$m = if ($d.Length -ge 2) { $d[$d.Length - 2] } else { 0 }
        [int]$h = if ($d.Length -ge 3) { $d[$d.Length - 3] } else { 0 }
        [int]$time = $s + $m * 60 + $h * 3600
    }
    [double]$gold = [System.Math]::Round($gold / $time, 4)
    [double]$xpe = [System.Math]::Round($xpe / $time, 2)
    return [hashtable]@{gold = $gold; xp = $xpe; tag = $ttag}
}
$fi_p = ${gold = 0; xp = 0; tag = 1}
$se_p = ${gold = 0; xp = 0; tag = 2}
$th_p = ${gold = 0; xp = 0; tag = 3}

if ($fi) { $fi_p = Parse -a $fi -ttag 1 }
if ($se) { $se_p = Parse -a $se -ttag 2 }
if ($th) { $th_p = Parse -a $th -ttag 3 }

if (-not ($xp -or $g)) {
    $xp = $true
    $g = $true
}


if ($fi -or $se -or $th) {
    [hashtable[]]$storage = $fi_p, $se_p, $th_p
    $avg_gold = Average($storage | ForEach-Object {$_.gold})
    $avg_xp = Average($storage | ForEach-Object {$_.xp})

    $high_gold = $storage
    | ForEach-Object {@{t = $_.tag; g =  $_.gold; var = ($_.gold - $avg_gold) * ($_.gold - $avg_gold)}}
    | Sort-Object {$_["g"]} -Descending

    $high_xp = $storage
    | ForEach-Object {@{t = $_.tag; x = $_.xp; var = ($_.xp - $avg_xp) * ($_.xp - $avg_xp)}}
    | Sort-Object {$_["x"]} -Descending

    $high_var = $high_gold
    | ForEach-Object {
        $outer = $_
        $var = $_.var + $($high_xp
        | Where-Object {$_.t -eq $outer.t}
        | Select-Object -ExpandProperty var)
        @{t = $_.t; var = $var}
    } | Sort-Object {$_["var"]}

    if($xp) {
        Write-Host "Best xp`t`t=> quest: $($high_xp[0].t)" -ForegroundColor Green
    }

    if ($g) {
        Write-Host "Best gold`t=> quest: $($high_gold[0].t)" -ForegroundColor Green
    }

    if ($g -and $xp) {
        Write-Host "Best overall`t=> quest: $($high_var[0].t)" -ForegroundColor Green
    }
    $Table = $storage | ForEach-Object {
        $b = ""
        if ($_.tag -eq $high_xp[0].t) {$b = "XP"}
        elseif ($_.tag -eq $high_gold[0].t) {$b = "Gold"}
        elseif ($_.tag -eq $high_var[0].t) {$b = "Average"}
        [string]$gld = $(if ($_.gold) {$_.gold.ToString()} {0})
        [PSCustomObject]@{
            Quest = $_.tag
            Gold = $gld
            Experience = $_.xp
            Best = $b
        }
    }
    $Table
    return
}
