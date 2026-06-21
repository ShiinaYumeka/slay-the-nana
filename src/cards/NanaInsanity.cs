using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MegaCrit.Sts2.Core.Models.CardPools;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaInsanity: NanaCardModel
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("NanaInsanityPower", 12m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<NanaInsanityPower>())];
    public NanaInsanity()
        : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        (await PowerCmd.Apply<NanaInsanityPower>(choiceContext, base.Owner.Creature, base.DynamicVars["NanaInsanityPower"].BaseValue, base.Owner.Creature, this))?.UpdateData();
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["NanaInsanityPower"].UpgradeValueBy(8m);
    }
}
