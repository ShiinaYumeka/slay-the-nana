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

using MegaCrit.Sts2.Core.Models.CardPools;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaFireShield: NanaCardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [(CardKeyword.Retain)];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1), new BlockVar(12m, ValueProp.Move), new PowerVar<DexterityPower>(1m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [base.EnergyHoverTip, (HoverTipFactory.FromPower<DexterityPower>())];

    public NanaFireShield()
        : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext, base.Owner.Creature, base.DynamicVars.Energy.IntValue, base.Owner.Creature, this);
        await PowerCmd.Apply<DexterityPower>(choiceContext, base.Owner.Creature, -base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, this);

    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Energy.UpgradeValueBy(1);
        base.DynamicVars.Block.UpgradeValueBy(6m);
    }
}
