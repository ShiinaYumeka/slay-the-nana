using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaGb: CardModel
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(3m, ValueProp.Move), new DynamicVar("NanaFcCost", 20m), new RepeatVar(5)];

    protected override bool IsPlayable => base.Owner.Creature.GetPowerAmount<NanaFc>() >= base.DynamicVars["NanaFcCost"].BaseValue;
    protected override bool ShouldGlowGoldInternal => IsPlayable;

    public bool IsNanaFcMove = true;
    public NanaGb()
		: base(0, CardType.Attack, CardRarity.Basic, TargetType.AllEnemies)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        await PowerCmd.Apply<NanaFc>(base.Owner.Creature, -base.DynamicVars["NanaFcCost"].BaseValue, base.Owner.Creature, this);

		for (int i=0;i< base.DynamicVars.Repeat.IntValue; i++)
        {
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this)
                .TargetingAllOpponents(base.CombatState)
                .WithHitFx("vfx/vfx_attack_blunt", null, null)
                .Execute(choiceContext);

        }
	}


    protected override void OnUpgrade()
	{
		base.DynamicVars.Damage.UpgradeValueBy(2m);
	}
}
