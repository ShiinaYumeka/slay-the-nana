using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaThreeTrial : CardModel
{
	//public override IEnumerable<CardKeyword> CanonicalKeywords => [(CardKeyword.Exhaust)];
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("NanaKarma", 24m), new DynamicVar("NanaPlantMagic", 6m), new DynamicVar("NanaKeepMoving", 6m), new DynamicVar("NanaFcCost", 50m)];
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<NanaKarma>()), (HoverTipFactory.FromPower<NanaPlantMagic>()), (HoverTipFactory.FromPower<NanaKeepMoving>())];
    protected override bool IsPlayable => base.Owner.Creature.GetPowerAmount<NanaFc>() >= base.DynamicVars["NanaFcCost"].BaseValue;
    protected override bool ShouldGlowGoldInternal => IsPlayable;

    public bool IsNanaFcMove = true;
    
	public NanaThreeTrial()
		: base(0, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        await PowerCmd.Apply<NanaFc>(base.Owner.Creature, -base.DynamicVars["NanaFcCost"].BaseValue, base.Owner.Creature, this);

        await PowerCmd.Apply<NanaKarma>(base.CombatState.HittableEnemies, base.DynamicVars["NanaKarma"].BaseValue, base.Owner.Creature, this);
		await PowerCmd.Apply<NanaPlantMagic>(base.CombatState.HittableEnemies, base.DynamicVars["NanaPlantMagic"].BaseValue, base.Owner.Creature, this);
		await PowerCmd.Apply<NanaKeepMoving>(base.CombatState.HittableEnemies, base.DynamicVars["NanaKeepMoving"].BaseValue, base.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars["NanaFcCost"].UpgradeValueBy(-13m);
    }
}
