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

    public override decimal ModifyPowerAmountGivenMultiplicative(PowerModel power, Creature giver, decimal amount, Creature? target, CardModel? cardSource)
    {
        if (!(power is NanaKarma))
        {
            return amount;
        }
        if (giver != base.Owner)
        {
            return amount;
        }
        if (target == base.Owner)
        {
            return amount;
        }
        if (cardSource is NanaShiftTheBlame)
        {
            return Math.Min(amount * 2, amount + 4);
        }
        return Math.Min(amount * 2, amount + 4);
    }
    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (applier == base.Owner && !(amount <= 0m) && power is NanaKarma && !(cardSource is NanaShiftTheBlame) && !(cardSource is NanaMurder) && !(cardSource is NanaPf666))
        {
            await PowerCmd.Apply<NanaKarma>(new ThrowingPlayerChoiceContext(), applier, 1, null, null);
        }
    }
    
}
