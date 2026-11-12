# 小鲸鲸 MOD 安装脚本（Chill with You : Lo-Fi Story，Steam 版本号 26586283）
# 不附带任何游戏文件：在你自己的游戏文件上打补丁，原版文件先备份到本文件夹的 backup\。
param([string]$GameData)
$ErrorActionPreference = 'Stop'
$Root = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$Build = '26586283'; $AppId = '3548580'
$Orig = @{ 'sharedassets3.assets' = 'a3966ccb61b48e5cfd367faf61c5ee3f5b1a32818034eb752aafda3fc079f722'; 'level3' = '58d70b902026cfe5e91db05e6c7c1b61ec55909ecea86fbb2b938918ce62ca8c' }
$Mod = Get-Content -Raw -LiteralPath "$Root\tools\hashes.json" | ConvertFrom-Json
function Fail($m) { Write-Host ''; Write-Host "[失败] $m" -ForegroundColor Red; exit 1 }
function Sha($p) { (Get-FileHash -LiteralPath $p -Algorithm SHA256).Hash.ToLower() }
function SaveJson($p, $o) { [IO.File]::WriteAllText($p, ($o | ConvertTo-Json -Depth 6 -Compress), (New-Object Text.UTF8Encoding $false)) }

# 1. 找到游戏
$Manifest = $null
if (-not $GameData) {
  $steam = (Get-ItemProperty -Path 'HKCU:\Software\Valve\Steam' -ErrorAction SilentlyContinue).SteamPath
  $libs = @()
  if ($steam) {
    $libs += $steam
    $vdf = Join-Path $steam 'steamapps\libraryfolders.vdf'
    if (Test-Path $vdf) { $libs += (Select-String -LiteralPath $vdf -Pattern '"path"\s+"([^"]+)"' | ForEach-Object { $_.Matches[0].Groups[1].Value -replace '\\\\', '\' }) }
  }
  foreach ($l in $libs | Select-Object -Unique) {
    $d = Join-Path $l 'steamapps\common\Chill with You Lo-Fi Story\Chill With You_Data'
    if (Test-Path (Join-Path $d 'sharedassets3.assets')) { $GameData = $d; $Manifest = Join-Path $l "steamapps\appmanifest_$AppId.acf"; break }
  }
}
if (-not $GameData -or -not (Test-Path (Join-Path $GameData 'sharedassets3.assets'))) {
  Fail '没有找到游戏。请把游戏的 "Chill With You_Data" 文件夹拖到 install.cmd 上运行。'
}
Write-Host "游戏目录：$GameData"
if (-not $Manifest) { $Manifest = Join-Path (Split-Path (Split-Path (Split-Path (Split-Path $GameData)))) "appmanifest_$AppId.acf" }
if ((Test-Path $Manifest) -and -not (Select-String -LiteralPath $Manifest -Pattern "`"buildid`"\s+`"$Build`"" -Quiet)) {
  Fail "游戏版本不是 $Build，这个 MOD 只适配这个版本。游戏更新后需要等 MOD 更新。"
}
if (Get-Process -Name 'Chill With You' -ErrorAction SilentlyContinue) { Fail '游戏正在运行，请先退出游戏。' }

# 2. 备份并打补丁
$Backup = Join-Path $Root 'backup'; New-Item -ItemType Directory -Force $Backup | Out-Null
foreach ($f in 'sharedassets3.assets', 'level3') {
  $p = Join-Path $GameData $f; $h = Sha $p
  if ($h -eq $Mod.$f) { Write-Host "$f 已经是 MOD 版本，跳过"; continue }
  if ($h -ne $Orig[$f]) { Fail "$f 不是原版文件（可能装过别的 MOD 或文件损坏）。请在 Steam 里右键游戏 → 属性 → 已安装文件 → 验证游戏文件的完整性，然后再运行安装。" }
  if (-not (Test-Path (Join-Path $Backup $f)) -or (Sha (Join-Path $Backup $f)) -ne $Orig[$f]) { Copy-Item -LiteralPath $p -Destination $Backup -Force }
  Write-Host "正在给 $f 打补丁……"
  $tmp = "$p.whale-tmp"; if (Test-Path $tmp) { Remove-Item -LiteralPath $tmp -Force }
  & "$Root\tools\hpatchz.exe" -f "$p" "$Root\patches\$f.hdiff" "$tmp" | Out-Null
  if ($LASTEXITCODE -ne 0 -or -not (Test-Path $tmp) -or (Sha $tmp) -ne $Mod.$f) { if (Test-Path $tmp) { Remove-Item -LiteralPath $tmp -Force }; Fail "$f 打补丁失败，游戏文件没有改动。" }
  Move-Item -LiteralPath $tmp -Destination $p -Force
}

# 3. MOD 程序：放进 Managed，并在 Unity 的两个启动清单里各登记一条（不修改游戏代码）
Copy-Item -LiteralPath "$Root\tools\WhaleFingerProbe.dll" -Destination (Join-Path $GameData 'Managed') -Force
if ((Sha (Join-Path $GameData 'Managed\WhaleFingerProbe.dll')) -ne $Mod.'WhaleFingerProbe.dll') { Fail 'MOD 程序复制失败。' }
foreach ($j in 'ScriptingAssemblies.json', 'RuntimeInitializeOnLoads.json') { if (-not (Test-Path (Join-Path $Backup $j))) { Copy-Item -LiteralPath (Join-Path $GameData $j) -Destination $Backup } }
$sa = Get-Content -Raw -LiteralPath (Join-Path $GameData 'ScriptingAssemblies.json') | ConvertFrom-Json
if ($sa.names -notcontains 'WhaleFingerProbe.dll') { $sa.names = @($sa.names) + 'WhaleFingerProbe.dll'; $sa.types = @($sa.types) + 16; SaveJson (Join-Path $GameData 'ScriptingAssemblies.json') $sa }
$ri = Get-Content -Raw -LiteralPath (Join-Path $GameData 'RuntimeInitializeOnLoads.json') | ConvertFrom-Json
if (-not ($ri.root | Where-Object { $_.assemblyName -eq 'WhaleFingerProbe' })) {
  $ri.root = @($ri.root) + [pscustomobject]@{ assemblyName = 'WhaleFingerProbe'; nameSpace = ''; className = 'WhaleFingerProbe'; methodName = 'Init'; loadTypes = 1; isUnityClass = $false }
  SaveJson (Join-Path $GameData 'RuntimeInitializeOnLoads.json') $ri
}

# 4. 中文配音（可选）：本文件夹里有 WhaleVoice 就一起装
$voice = Join-Path $Root 'WhaleVoice'
if (Test-Path $voice) {
  Write-Host '正在安装中文配音……'
  robocopy "$voice" (Join-Path $GameData 'WhaleVoice') *.wav /MIR /NFL /NDL /NJH /NJS /NP | Out-Null
  if ($LASTEXITCODE -ge 8) { Fail '中文配音复制失败。' }
  $n = (Get-ChildItem -LiteralPath (Join-Path $GameData 'WhaleVoice') -Filter *.wav).Count; $want = (Get-ChildItem -LiteralPath $voice -Filter *.wav).Count
  if ($n -ne $want) { Fail "中文配音只复制了 $n / $want 个文件。" }
  Write-Host "中文配音：$n 条"
} else { Write-Host '没有找到 WhaleVoice 文件夹，跳过中文配音（只装模型）。' }

Write-Host ''
Write-Host '[完成] 小鲸鲸 MOD 已安装。启动游戏即可。' -ForegroundColor Green
Write-Host '提示：游戏设置里的语音音量需要打开才能听到配音。'
