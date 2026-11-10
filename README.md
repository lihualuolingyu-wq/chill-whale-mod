# 小鲸鲸 MOD · Chill with You : Lo-Fi Story

把《Chill with You : Lo-Fi Story》的女主替换成「小鲸鲸」（DeepSeek 角色形象，形象作者 B 站 [@ZipZipPipe](https://space.bilibili.com/4168597)；3D 模型作者 B 站 UP 主 [@大鹅追鸭](https://space.bilibili.com/23574630)），并附带肥鱼声线的中文配音。

**完全免费，仅限个人非商业使用。** 本 MOD 为玩家自制，与游戏开发商 Nestopi 无关，需要正版游戏。

<!-- 宣传视频链接：发布后填写 B 站地址 -->

## 功能

- **模型替换**：女主换成小鲸鲸，身体、手指、全部动作与表情（眨眼、口型、视线、眉毛）都保留。
- **头发物理**：长发和呆毛随动作自然摆动。
- **中文配音**：1286 条台词按游戏官方简体中文字幕合成，声线为肥鱼。笑声、语气声等约 100 条保持游戏原声。配音为机器合成，个别句子可能不自然。

## 下载与安装

在右侧 [Releases](../../releases) 下载：

| 文件 | 内容 |
|------|------|
| `WhaleMOD-v1.0-full.zip` | 模型 + 中文配音 |
| `WhaleMOD-v1.0-model-only.zip` | 只有模型，保留游戏原声 |

1. 退出游戏，把压缩包解压到任意位置（之后不要删除，卸载要用里面的备份）。
2. 双击 `install.cmd`。脚本会自动找到 Steam 里的游戏；找不到时，把游戏的 `Chill With You_Data` 文件夹拖到 `install.cmd` 上。
3. 看到「完成」后启动游戏。想听配音，请在游戏设置里打开语音音量。

卸载：退出游戏，双击 `uninstall.cmd`。

**适用版本**：Steam 版 Build ID 26291741。游戏更新后补丁可能失效，安装脚本会检查版本。

## 安全与原理

- 压缩包里**不包含任何游戏文件**。安装时在你自己的游戏文件上打二进制补丁（[HDiffPatch](https://github.com/sisong/HDiffPatch)），打补丁前后都校验 SHA256，原版文件先备份。
- **不修改游戏代码**。MOD 程序 `WhaleFingerProbe.dll` 通过 Unity 的两个启动清单（`ScriptingAssemblies.json`、`RuntimeInitializeOnLoads.json`）各登记一条来加载，源码见 [`src/`](src/)。
- 修改的文件：`sharedassets3.assets`、`level3`（补丁）；`Managed\WhaleFingerProbe.dll`（新增）；上述两个 JSON；`WhaleVoice` 文件夹（配音）。

## 致谢与许可

- **角色形象**：DeepSeek 小鲸鲸，作者 B 站 [@ZipZipPipe](https://space.bilibili.com/4168597)。
- **3D 模型**：B 站 UP 主 [@大鹅追鸭](https://space.bilibili.com/23574630)（GitHub [xxxxxxur](https://github.com/xxxxxxur/deepseek-blender-model)，模型介绍视频 [BV1zZWE6UE6Y](https://www.bilibili.com/video/BV1zZWE6UE6Y/)），许可 [CC BY-NC-SA 4.0](https://creativecommons.org/licenses/by-nc-sa/4.0/deed.zh-hans)。
  本 MOD 的修改：重新绑定到游戏骨骼并按游戏动画调整比例；删除衣服下不可见的皮肤；裙摆坐姿跟随大腿；贴图拼成图集；调整下巴与嘴部轮廓；补描边；限制视线幅度；增加头发物理。修改后的模型数据同样以 CC BY-NC-SA 4.0 发布。
- **声线**：[肥鱼 RVC 声库](https://huggingface.co/Esmerubphire/FeiyuVoice-RVC)（发布者 Esmerubphire），原声来自 B 站 UP 主 [星落落_oi](https://space.bilibili.com/30584746/)。本 MOD 只包含生成好的台词语音，不包含声库；仅限非商业使用。
- **语音合成**：bilibili [IndexTTS-2.5](https://github.com/index-tts/index-tts)，遵守《bilibili 模型使用许可协议》（见压缩包内 `LICENSES`）。中文配音为使用该模型生成的衍生内容，对原模型所作的任何改动与原模型原始权利人无关，原始权利人对该衍生品不承担任何责任。生成过程中还用到 Amphion MaskGCT 语义编码器（CC BY-NC 4.0）、facebook/w2v-bert-2.0（MIT）、CAMPPlus（Apache-2.0）、NVIDIA BigVGAN（MIT）。
- **MOD 程序源码**（`src/`、`installer/`）同样以 CC BY-NC-SA 4.0 发布（源码内含模型的骨骼比例数据）。
- **补丁工具**：HDiffPatch `hpatchz.exe`，作者 housisong，MIT。
- **游戏**：Chill with You : Lo-Fi Story © Nestopi。游戏本身、台词文本及翻译的权利归原权利人所有。

禁止将本 MOD 或其中任何内容用于商业用途（付费下载、付费转售等）。
