using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaElemental : CardModel
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(20m, ValueProp.Move), new DynamicVar("NanaFcCost", 50m), new DynamicVar("NanaElemental", 3m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<PoisonPower>()), (HoverTipFactory.FromPower<WeakPower>()), (HoverTipFactory.FromPower<FrailPower>()), (HoverTipFactory.FromPower<VulnerablePower>())];

    protected override bool IsPlayable => base.Owner.Creature.GetPowerAmount<NanaFc>() >= base.DynamicVars["NanaFcCost"].BaseValue;
    protected override bool ShouldGlowGoldInternal => IsPlayable;

    public bool IsNanaFcMove = true;
    public NanaElemental()
		: base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        await PowerCmd.Apply<NanaFc>(base.Owner.Creature, -base.DynamicVars["NanaFcCost"].BaseValue, base.Owner.Creature, this);
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this)
            .TargetingAllOpponents(base.CombatState)
            .WithHitFx("vfx/vfx_attack_slash", null, null)
            .Execute(choiceContext);
        await PowerCmd.Apply<PoisonPower>(base.CombatState.HittableEnemies, base.DynamicVars["NanaElemental"].BaseValue, base.Owner.Creature, this);
        await PowerCmd.Apply<WeakPower>(base.CombatState.HittableEnemies, base.DynamicVars["NanaElemental"].BaseValue, base.Owner.Creature, this);
        await PowerCmd.Apply<FrailPower>(base.CombatState.HittableEnemies, base.DynamicVars["NanaElemental"].BaseValue, base.Owner.Creature, this);
        await PowerCmd.Apply<VulnerablePower>(base.CombatState.HittableEnemies, base.DynamicVars["NanaElemental"].BaseValue, base.Owner.Creature, this);

    }


    protected override void OnUpgrade()
	{
        base.DynamicVars["NanaElemental"].UpgradeValueBy(1m);
	}
}
