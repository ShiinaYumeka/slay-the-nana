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
using static Godot.HttpRequest;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SlayTheNANA;

public sealed class NanaCruelPower : PowerModel
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, Creature? target, CardModel? cardSource)
    {
        if (!(power is NanaKarma))
        {
            return amount;
        }
        if (giver != base.Owner)
        {
            return amount;
        }
        if (cardSource is NanaShiftTheBlame)
        {
            return amount;
        }
        return amount * 2;
    }
    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (applier == base.Owner && !(amount <= 0m) && power is NanaKarma && !(cardSource is NanaShiftTheBlame))
        {
            await PowerCmd.Apply<NanaKarma>(applier, amount/2, null, null);
        }
    }
}
