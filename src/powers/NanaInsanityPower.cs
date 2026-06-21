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
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaInsanityPower : PowerModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("NanaInsanityDamage", 0m), new DynamicVar("NanaInsanityBlock", 0m)];


    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
    {
        if (dealer == base.Owner && props.IsPoweredAttack_() && result.UnblockedDamage > 0)
        {
            await PowerCmd.Apply<NanaInsanityPower>(choiceContext, base.Owner, 1, base.Owner, null);
            UpdateData();
        }
    }
    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (side != base.Owner.Side)
        {
            return;
        }
        await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount / 10, ValueProp.Unpowered, null, null);
        if (base.Owner.IsAlive)
        {
            await PowerCmd.Apply<VigorPower>(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount / 4, base.Owner, null);
            await CreatureCmd.GainBlock(base.Owner, base.Amount / 4, ValueProp.Unpowered, null);
        }

    }
    public void UpdateData()
    {
        AssertMutable();
        base.DynamicVars["NanaInsanityDamage"].BaseValue = base.Amount / 10;
        base.DynamicVars["NanaInsanityBlock"].BaseValue = base.Amount / 4;
    }
}
