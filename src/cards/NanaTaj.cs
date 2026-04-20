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
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("NanaKarma", 10m), new DynamicVar("NanaFcGain", 99m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<NanaKarma>())];

    public NanaTaj()
        : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<NanaKarma>(base.Owner.Creature, base.DynamicVars["NanaKarma"].BaseValue, base.Owner.Creature, this);
        (await PowerCmd.Apply<NanaFc>(base.Owner.Creature, base.DynamicVars["NanaFcGain"].BaseValue, base.Owner.Creature, this))?.NanaFcGain();

    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}
