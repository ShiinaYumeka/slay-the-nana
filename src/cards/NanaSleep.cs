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
public sealed class NanaSleep: NanaCardModel
{


    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(10m), new DynamicVar("DexterityLoss", 2m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<DexterityPower>())]; 
    public NanaSleep()
        : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<DexterityPower>(choiceContext, base.Owner.Creature, -base.DynamicVars["DexterityLoss"].BaseValue, base.Owner.Creature, this);
        await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["DexterityLoss"].UpgradeValueBy(-1m);
    }
}
