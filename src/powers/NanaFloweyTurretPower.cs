using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaFloweyTurretPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("AttackDamage", 0m)];

    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
    {
        if (dealer == base.Owner && props.IsPoweredAttack_())
        {
            base.DynamicVars["AttackDamage"].BaseValue += base.Amount;
            base.DynamicVars["AttackDamage"].PreviewValue = base.DynamicVars["AttackDamage"].BaseValue;
        }
    }
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != base.Owner.Side)
        {
            return;
        }
        IReadOnlyList<Creature> hittableEnemies = base.CombatState.HittableEnemies;
        if (hittableEnemies.Count != 0)
        {
            Flash();
            Creature target = base.Owner.Player.RunState.Rng.CombatTargets.NextItem(hittableEnemies);
            await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), target, base.DynamicVars["AttackDamage"].BaseValue, ValueProp.Unpowered, base.Owner, null);
        }
        base.DynamicVars["AttackDamage"].BaseValue = 0;
        base.DynamicVars["AttackDamage"].PreviewValue = base.DynamicVars["AttackDamage"].BaseValue;
    }
}
