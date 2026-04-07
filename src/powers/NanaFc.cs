using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Godot.HttpRequest;

namespace SlayTheNANA;

public sealed class NanaFc: PowerModel
{
    public override PowerType Type => PowerType.Buff;
    //protected override bool IsVisibleInternal => false;
    public override bool ShouldPlayVfx => false;

    public override PowerStackType StackType => PowerStackType.Counter;
    public async void NanaFcGain()
    {
        if (base.Owner.GetPowerAmount<NanaFc>() > 99)
        {
            await PowerCmd.SetAmount<NanaFc>(base.Owner ,99 , base.Owner ,null);
        }
    }
}
