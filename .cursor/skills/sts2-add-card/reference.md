# sts2-add-card 参考清单

## 可复制执行清单

```markdown
新增卡牌任务清单
- [ ] 1. 明确卡牌输入（类名、费用、类型、稀有度、目标、效果、升级、文案）
- [ ] 2. 新建 `src/cards/<ClassName>.cs` 并实现 `CardModel`
- [ ] 3. 在 `src/character/nanadummy/CardPool.cs` 添加 `ModelDb.Card<<ClassName>>()`
- [ ] 4. 在 `SlayTheNANA/localization/zhs/cards.json` 添加 `NANA_<UPPER_SNAKE>.title/.description`
- [ ] 5. 对齐资源：`nana_<lower_snake>.png` + `nana_<lower_snake>.tres`
- [ ] 6. 检查 atlas `.tres` 是否引用正确 `res://...png`
- [ ] 7. 最小验证（编译、注册、本地化、资源命名一致）
```

## 命名对照表

- `NanaBoneSpecial` -> `NANA_BONE_SPECIAL` -> `nana_bone_special`
- `NanaTearTheRoom2` -> `NANA_TEAR_THE_ROOM2` -> `nana_tear_the_room2`
- `NanaGb` -> `NANA_GB` -> `nana_gb`

## 关键文件对照

- 卡牌代码：`src/cards/`
- 卡池注册：`src/character/nanadummy/CardPool.cs`
- 角色绑定卡池：`src/character/nanadummy/baselib.cs`
- 卡牌本地化：`SlayTheNANA/localization/zhs/cards.json`
- 卡图 portrait：`images/packed/card_portraits/nana_dummy/`
- 卡图 atlas：`images/packed/card_atlas.sprites/nana_dummy/`
- 自定义标签定义：`src/tags/CustomCardTag.cs`

## 常见坑与修复

1. **卡牌文件已创建但游戏里不出现**
   - 原因：未加入 `NanaDummyCardPool.GenerateAllCards()`
   - 修复：在 `CardPool.cs` 添加 `ModelDb.Card<<ClassName>>()`

2. **卡名或描述不显示（显示键名）**
   - 原因：`cards.json` 缺失键或键名与类名映射不一致
   - 修复：按 `NANA_<UPPER_SNAKE>.title/.description` 重新补齐

3. **动态变量显示异常**
   - 原因：文案占位符与 `CanonicalVars` 名称不一致
   - 修复：统一变量名与占位符，例如 `DynamicVar("DexterityLoss", ...)` 对应 `{DexterityLoss:diff()}`

4. **卡图不显示**
   - 原因：`png` 与 `tres` stem 不一致，或 atlas `.tres` 引用错误路径
   - 修复：确保两者同名，并在 `.tres` 指向 `res://images/packed/card_portraits/nana_dummy/nana_<lower_snake>.png`

5. **误改现有卡池内容**
   - 原因：编辑时覆盖/删除旧条目
   - 修复：仅追加新卡，保持已有卡条目不变

## 最小代码骨架（示例）

```csharp
namespace SlayTheNANA;

public sealed class NanaExample : CardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(6m, ValueProp.Move)];

    public NanaExample() : base(1, CardType.Attack, CardRarity.Common, TargetType.Enemy) {}

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        // 按需求实现
    }

    protected override void OnUpgrade()
    {
        // 仅处理升级增量
    }
}
```
