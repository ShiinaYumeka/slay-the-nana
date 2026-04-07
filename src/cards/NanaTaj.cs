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
using static Godot.HttpRequest;

namespace SlayTheNANA;

public sealed class NanaTaj : CardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<WeakPower>(3m), new DynamicVar("NanaFcGain", 25m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<WeakPower>())];

    public NanaTaj()
        : base(0, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<WeakPower>(base.Owner.Creature, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this);
        (await PowerCmd.Apply<NanaFc>(base.Owner.Creature, base.DynamicVars["NanaFcGain"].BaseValue, base.Owner.Creature, this))?.NanaFcGain();

    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["NanaFcGain"].UpgradeValueBy(8m);
    }
}
