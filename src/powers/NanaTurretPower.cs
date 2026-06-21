using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.ValueProps;

namespace SlayTheNANA;

public sealed class NanaTurretPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
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
                await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), target, base.Amount, ValueProp.Unpowered, base.Owner, null);

        }
    }
}
