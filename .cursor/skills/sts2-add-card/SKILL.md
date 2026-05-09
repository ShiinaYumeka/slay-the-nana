---
name: sts2-add-card
description: Add a new SlayTheNANA card end-to-end in this repository, including CardModel class, NanaDummy card pool registration, zhs localization keys, portrait/atlas naming alignment, and minimal validation. Use when the user asks to add a new card, 新增卡牌, 新卡, 注册卡池, or card localization/assets.
disable-model-invocation: true
---

# SlayTheNANA 新增卡牌

## 适用范围
仅用于当前仓库 `E:/mc/slay-the-nana` 的 `NanaDummy` 角色卡牌流程。

## Quick Start
按以下顺序执行，不跳步：

1. 在 `src/cards/` 新建 `Nana*.cs` 卡牌类（继承 `CardModel`）。
2. 在 `src/character/nanadummy/CardPool.cs` 的 `GenerateAllCards()` 添加 `ModelDb.Card<NewCard>()`。
3. 在 `SlayTheNANA/localization/zhs/cards.json` 添加 `NANA_<UPPER_SNAKE>.title` 与 `NANA_<UPPER_SNAKE>.description`。
4. 对齐资源命名：
   - `images/packed/card_portraits/nana_dummy/nana_<lower_snake>.png`
   - `images/packed/card_atlas.sprites/nana_dummy/nana_<lower_snake>.tres`
5. 执行最小校验并汇报修改文件与原因。

## 标准工作流

### Step 0: 先确认输入
若用户未明确以下信息，先询问后再改动：
- 卡牌类名（PascalCase，例如 `NanaBoneSpecial`）
- 费用、类型、稀有度、目标
- 核心效果（OnPlay）
- 升级变化（OnUpgrade）
- 中文标题与描述
- 是否需要自定义标签（如 `CustomCardTag.Bone`）

### Step 1: 创建卡牌类
在 `src/cards/<ClassName>.cs`：
- 使用 `namespace SlayTheNANA;`
- 类定义为 `public sealed class <ClassName> : CardModel`
- 构造器调用 `base(cost, CardType.*, CardRarity.*, TargetType.*)`
- 通过 `CanonicalVars` 声明动态数值（如 `DamageVar`, `BlockVar`, `DynamicVar`）
- 在 `OnPlay` 中实现效果
- 在 `OnUpgrade` 中只处理升级增量逻辑

优先参考已有实现：
- `src/cards/NanaBlueBone.cs`
- `src/cards/NanaGb.cs`

### Step 2: 注册进 NanaDummy 卡池
编辑 `src/character/nanadummy/CardPool.cs`：
- 在 `GenerateAllCards()` 返回数组中新增 `ModelDb.Card<<ClassName>>()`
- 保持现有格式与顺序风格（不要替换或删除已有卡）

### Step 3: 增加本地化键
编辑 `SlayTheNANA/localization/zhs/cards.json`：
- 新增 `NANA_<UPPER_SNAKE>.title`
- 新增 `NANA_<UPPER_SNAKE>.description`
- 动态变量占位符需与 `CanonicalVars` 一致（如 `{Damage:diff()}`）

### Step 4: 对齐资源
检查并确保以下 stem 一致：
- 类名：`NanaBoneSpecial`
- 本地化键：`NANA_BONE_SPECIAL.*`
- 文件 stem：`nana_bone_special`

资源要求：
- portrait: `images/packed/card_portraits/nana_dummy/nana_<lower_snake>.png`
- atlas: `images/packed/card_atlas.sprites/nana_dummy/nana_<lower_snake>.tres`
- atlas `.tres` 应为 `AtlasTexture` 并引用对应 png 的 `res://` 路径

### Step 5: 最小验证
至少完成以下检查：
- 新卡类可编译（无明显类型/命名错误）
- `CardPool.cs` 已注册新卡
- `cards.json` 键完整且 JSON 结构合法
- 资源命名与键名映射一致

## 命名映射规则
- PascalCase 类名：`NanaBoneSpecial`
- UPPER_SNAKE 本地化：`NANA_BONE_SPECIAL`
- lower_snake 文件 stem：`nana_bone_special`

通用转换：
1. 去掉前缀 `Nana` 后按单词切分
2. 本地化键保留 `NANA_` 前缀并转大写下划线
3. 资源名使用 `nana_` 前缀并转小写下划线

## 失败回退策略
- **漏注册卡池**：补 `CardPool.cs` 的 `ModelDb.Card<<ClassName>>()`。
- **本地化键不匹配**：统一按类名重算 `NANA_<UPPER_SNAKE>` 并修正。
- **资源缺失**：先报告缺失文件，再创建占位路径或请求用户提供素材。
- **atlas 指向错误**：修正 `.tres` 中 `path="res://images/packed/card_portraits/nana_dummy/nana_<lower_snake>.png"`。

## 输出模板
执行完成后按此格式汇报：

```markdown
已完成新增卡牌 `<ClassName>`，共修改 N 个文件。

- `src/cards/<ClassName>.cs`: 新增卡牌逻辑（费用/类型/效果/升级）
- `src/character/nanadummy/CardPool.cs`: 注册到 NanaDummy 卡池
- `SlayTheNANA/localization/zhs/cards.json`: 新增标题与描述键
- `images/packed/card_portraits/nana_dummy/...` 与 `images/packed/card_atlas.sprites/nana_dummy/...`: 对齐资源命名与引用

验证结果：
- 编译/静态检查：<结果>
- 资源与命名一致性：<结果>
```

## 附加参考
详细清单与常见坑见 [reference.md](reference.md)。
