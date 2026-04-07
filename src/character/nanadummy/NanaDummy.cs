//using Godot;
//using MegaCrit.Sts2.Core.Entities.Characters;
//using MegaCrit.Sts2.Core.Models;
//using MegaCrit.Sts2.Core.Models.CardPools;
//using MegaCrit.Sts2.Core.Models.Characters;
//using MegaCrit.Sts2.Core.Models.PotionPools;
//using MegaCrit.Sts2.Core.Models.RelicPools;
//using SlayTheNANA;
//using System.Collections.Generic;

//public sealed class NanaDummy : CharacterModel
//{
//    public const string energyColorName = "nana_dummy"; // 能量显示的颜色名称

//    public override CharacterGender Gender => CharacterGender.Neutral; // 性别

//    protected override CharacterModel? UnlocksAfterRunAs => null;

//    public override Color NameColor => new Color("#EFEFD9"); // 名称显示的颜色
//    public override string CharacterSelectSfx =>
//    ModelDb.Character<Ironclad>().CharacterSelectSfx;

//    //如果没有对应的音效，我们可以在角色的C#类中添加：
//    public override string CharacterTransitionSfx =>
//        "event:/sfx/ui/wipe_ironclad";

//    public override int StartingHp => 100; // 角色初始生命值

//    public override int StartingGold => 99; // 角色初始拥有的金钱

//    public override CardPoolModel CardPool => ModelDb.CardPool<NanaDummyCardPool>();
//    public override PotionPoolModel PotionPool => ModelDb.PotionPool<IroncladPotionPool>();

//    public override RelicPoolModel RelicPool => ModelDb.RelicPool<IroncladRelicPool>();

//    public override List<CardModel> StartingDeck => [
//        ModelDb.Card<NanaStrikeDummy>(),
//        ModelDb.Card<NanaStrikeDummy>(),
//        ModelDb.Card<NanaStrikeDummy>(),
//        ModelDb.Card<NanaStrikeDummy>(),
//        ModelDb.Card<NanaDefendDummy>(),
//        ModelDb.Card<NanaDefendDummy>(),
//        ModelDb.Card<NanaDefendDummy>(),
//        ModelDb.Card<NanaDefendDummy>(),
//        ModelDb.Card<NanaTrial>(),
//        ModelDb.Card<NanaStick>()
//    ]; // 初始卡组

//    public override List<RelicModel> StartingRelics => [
//        ModelDb.Relic<NanaRedTridentRelic>()
//    ];

//    // 角色播放攻击动作后，到真正出手/命中前的延迟时间
//    public override float AttackAnimDelay => 0.15f;

//    // 角色播放施法动作后，到效果实际触发前的延迟时间
//    public override float CastAnimDelay => 0.25f;

//    // 卡牌左上角费用数字的描边颜色
//    public override Color EnergyLabelOutlineColor => Colors.DarkGoldenrod;

//    // 角色相关对白、气泡、事件发言等文本的颜色
//    public override Color DialogueColor => Colors.DarkGoldenrod;

//    // 地图上该角色绘制连线时使用的颜色
//    public override Color MapDrawingColor => Colors.DarkGoldenrod;

//    // 联机状态下，这个角色的指向线主体颜色
//    public override Color RemoteTargetingLineColor => Colors.DarkGoldenrod;

//    // 联机指向线的外描边颜色
//    public override Color RemoteTargetingLineOutline => Colors.DarkGoldenrod;

//    public override List<string> GetArchitectAttackVfx()
//    {
//        return new List<string>();
//    }
//}