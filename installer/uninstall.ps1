# 蓝色大肥鱼 MOD 卸载脚本：用 backup\ 里的原版文件恢复游戏，并删除 MOD 程序与中文配音。
param([string]$GameData)
$ErrorActionPreference = 'Stop'
$Root = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$Orig = @{ 'sharedassets3.assets' = 'a3966ccb61b48e5cfd367faf61c5ee3f5b1a32818034eb752aafda3fc079f722'; 'level3' = '58d70b902026cfe5e91db05e6c7c1b61ec55909ecea86fbb2b938918ce62ca8c' }
function Fail($m) { Write-Host ''; Write-Host "[失败] $m" -ForegroundColor Red; exit 1 }
function Sha($p) { (Get-FileHash -LiteralPath $p -Algorithm SHA256).Hash.ToLower() }
function SaveJson($p, $o) { [IO.File]::WriteAllText($p, ($o | ConvertTo-Json -Depth 6 -Compress), (New-Object Text.UTF8Encoding $false)) }
if (-not $GameData) {
  $steam = (Get-ItemProperty -Path 'HKCU:\Software\Valve\Steam' -ErrorAction SilentlyContinue).SteamPath; $libs = @()
  if ($steam) { $libs += $steam; $vdf = Join-Path $steam 'steamapps\libraryfolders.vdf'; if (Test-Path $vdf) { $libs += (Select-String -LiteralPath $vdf -Pattern '"path"\s+"([^"]+)"' | ForEach-Object { $_.Matches[0].Groups[1].Value -replace '\\\\', '\' }) } }
  foreach ($l in $libs | Select-Object -Unique) { $d = Join-Path $l 'steamapps\common\Chill with You Lo-Fi Story\Chill With You_Data'; if (Test-Path (Join-Path $d 'sharedassets3.assets')) { $GameData = $d; break } }
}
if (-not $GameData -or -not (Test-Path (Join-Path $GameData 'sharedassets3.assets'))) { Fail '没有找到游戏。请把游戏的 "Chill With You_Data" 文件夹拖到 uninstall.cmd 上运行。' }
if (Get-Process -Name 'Chill With You' -ErrorAction SilentlyContinue) { Fail '游戏正在运行，请先退出游戏。' }
$Backup = Join-Path $Root 'backup'; $needVerify = $false
foreach ($f in 'sharedassets3.assets', 'level3') {
  $b = Join-Path $Backup $f
  if ((Test-Path $b) -and (Sha $b) -eq $Orig[$f]) { Copy-Item -LiteralPath $b -Destination $GameData -Force } elseif ((Sha (Join-Path $GameData $f)) -ne $Orig[$f]) { $needVerify = $true }
}
foreach ($j in 'ScriptingAssemblies.json', 'RuntimeInitializeOnLoads.json') {
  $b = Join-Path $Backup $j
  if (Test-Path $b) { Copy-Item -LiteralPath $b -Destination $GameData -Force; continue }
  $p = Join-Path $GameData $j; $o = Get-Content -Raw -LiteralPath $p | ConvertFrom-Json
  if ($j -eq 'ScriptingAssemblies.json') { $i = [array]::IndexOf(@($o.names), 'WhaleFingerProbe.dll'); if ($i -ge 0) { $o.names = @($o.names | Where-Object { $_ -ne 'WhaleFingerProbe.dll' }); $t = [Collections.ArrayList]@($o.types); $t.RemoveAt($i); $o.types = @($t); SaveJson $p $o } }
  else { $o.root = @($o.root | Where-Object { $_.assemblyName -ne 'WhaleFingerProbe' }); SaveJson $p $o }
}
$dll = Join-Path $GameData 'Managed\WhaleFingerProbe.dll'; if (Test-Path $dll) { Remove-Item -LiteralPath $dll -Force }
$v = Join-Path $GameData 'WhaleVoice'; if (Test-Path $v) { Remove-Item -LiteralPath $v -Recurse -Force }
Write-Host ''
if ($needVerify) { Write-Host '[注意] 没有找到备份的原版文件。请在 Steam 里验证游戏文件的完整性，把游戏恢复成原版。' -ForegroundColor Yellow }
else { Write-Host '[完成] 已恢复原版游戏。' -ForegroundColor Green }
