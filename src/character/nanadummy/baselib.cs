using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using System.Collections.Generic;

namespace SlayTheNANA;

public class NanaDummy : PlaceholderCharacterModel
{
	public const string CharacterId = "NanaDummy";
	public static readonly Color Color = new("ffffff");

	// 角色名称颜色
	public override Color NameColor => new(0.8f, 0.8f, 0.7f);
	// 能量图标轮廓颜色
	public override Color EnergyLabelOutlineColor => new(0.4f, 0.4f, 0.3f);

	// 人物性别（男女中立）
	public override CharacterGender Gender => CharacterGender.Neutral;

	// 初始血量
	public override int StartingHp => 99;

	// 人物模型tscn路径。要自定义见下。
	public override string CustomVisualPath => "res://scenes/creature_visuals/nana_dummy.tscn";
	// 卡牌拖尾路径。
	public override string CustomTrailPath => "res://scenes/vfx/card_trail_nana_dummy.tscn";
	// 人物头像路径。
	public override string CustomIconTexturePath => "res://images/ui/top_panel/character_icon_nana_dummy.png";
	// 人物头像2号。
	public override string CustomIconPath => "res://scenes/ui/character_icons/nana_dummy_icon.tscn";
	// 能量表盘tscn路径。要自定义见下。
	public override string CustomEnergyCounterPath => "res://scenes/combat/energy_counters/nana_dummy_energy_counter.tscn";
	// 篝火休息动画。
	public override string CustomRestSiteAnimPath => "res://scenes/rest_site/characters/nana_dummy_rest_site.tscn";
	// 商店人物动画。
	public override string CustomMerchantAnimPath => "res://scenes/merchant/characters/nana_dummy_merchant.tscn";
	// 多人模式-手指。
	public override string CustomArmPointingTexturePath => "res://images/ui/hands/multiplayer_hand_nana_dummy_point.png";
	// 多人模式剪刀石头布-石头。
	public override string CustomArmRockTexturePath => "res://images/ui/hands/multiplayer_hand_nana_dummy_rock.png";
	// 多人模式剪刀石头布-布。
	public override string CustomArmPaperTexturePath => "res://images/ui/hands/multiplayer_hand_nana_dummy_paper.png";
	// 多人模式剪刀石头布-剪刀。
	public override string CustomArmScissorsTexturePath => "res://images/ui/hands/multiplayer_hand_nana_dummy_scissors.png";

	// 人物选择背景。
	public override string CustomCharacterSelectBg => "res://scenes/screens/char_select/char_select_bg_nana_dummy.tscn";
	// 人物选择图标。
	public override string CustomCharacterSelectIconPath => "res://images/packed/character_select/char_select_nana_dummy.png";
	// 人物选择图标-锁定状态。
	public override string CustomCharacterSelectLockedIconPath => "res://images/packed/character_select/char_select_nana_dummy_locked.png";
	// 人物选择过渡动画。
	public override string CustomCharacterSelectTransitionPath => "res://materials/transitions/nana_dummy_transition_mat.tres";
	// 地图上的角色标记图标、表情轮盘上的角色头像
	public override string CustomMapMarkerPath => "res://images/packed/map/icons/map_marker_nana_dummy.png";
	// 攻击音效
	// public override string CustomAttackSfx => null;
	// 施法音效
	// public override string CustomCastSfx => null;
	// 死亡音效
	// public override string CustomDeathSfx => null;
	// 角色选择音效
	// public override string CharacterSelectSfx => null;
	// 过渡音效。这个不能删。
	public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";

	public override CardPoolModel CardPool => ModelDb.CardPool<NanaDummyCardPool>();
	public override RelicPoolModel RelicPool => ModelDb.RelicPool<NanaDummyRelicPool>();
	public override PotionPoolModel PotionPool => ModelDb.PotionPool<IroncladPotionPool>();

	// 初始卡组
	public override IEnumerable<CardModel> StartingDeck => [
        ModelDb.Card<NanaStrikeDummy>(),
		ModelDb.Card<NanaStrikeDummy>(),
		ModelDb.Card<NanaStrikeDummy>(),
		ModelDb.Card<NanaStrikeDummy>(),
		ModelDb.Card<NanaDefendDummy>(),
		ModelDb.Card<NanaDefendDummy>(),
		ModelDb.Card<NanaDefendDummy>(),
		ModelDb.Card<NanaDefendDummy>(),
		ModelDb.Card<NanaTrial>(),
		ModelDb.Card<NanaGb>()
	];

	// 初始遗物
	public override IReadOnlyList<RelicModel> StartingRelics => [
		ModelDb.Relic<NanaRelicTest>(),
	];

	// 攻击建筑师的攻击特效列表
	public override List<string> GetArchitectAttackVfx() => [
		"vfx/vfx_attack_blunt",
		"vfx/vfx_heavy_blunt",
		"vfx/vfx_attack_slash",
		"vfx/vfx_bloody_impact",
		"vfx/vfx_rock_shatter"
	];
}
