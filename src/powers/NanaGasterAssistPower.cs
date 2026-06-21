using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaGasterAssistPower : PowerModel
{
    public override PowerType Type => PowerType.Debuff;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<FrailPower>()), (HoverTipFactory.FromPower<WeakPower>())];
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == base.Owner.Side)
        {
            Flash();
            await PowerCmd.Apply<FrailPower>(choiceContext, base.Owner, base.Amount, base.Owner, null);
            await PowerCmd.Apply<WeakPower>(choiceContext, base.Owner, base.Amount, base.Owner, null);
            await PowerCmd.Remove(this);
        }
    }
}
