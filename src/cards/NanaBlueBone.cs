using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using SlayTheNANA.src.cardtags;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MegaCrit.Sts2.Core.Models.CardPools;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaBlueBone : NanaCardModel
{
	protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CustomCardTag.Bone };
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(10m, ValueProp.Move), new BlockVar(10m, ValueProp.Move), new DynamicVar("DexterityLoss", 2m)];
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<DexterityPower>())];

	public NanaBlueBone()
		: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
	{
	}


	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this)
			.TargetingAllOpponents(base.CombatState)
			.WithHitFx("vfx/vfx_flying_slash", null, null)
			.Execute(choiceContext);
		await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
		await PowerCmd.Apply<DexterityPower>(choiceContext, base.CombatState.HittableEnemies, -base.DynamicVars["DexterityLoss"].BaseValue, base.Owner.Creature, this);

	}

	protected override void OnUpgrade()
	{
		base.DynamicVars.Damage.UpgradeValueBy(4m);
		base.DynamicVars.Block.UpgradeValueBy(4m);
	}
}
