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
public sealed class NanaGasterAssist : NanaCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(12m, ValueProp.Move), new EnergyVar(3), new DynamicVar("NanaFcCost", 36m), new CardsVar(3), new DynamicVar("FrailPower", 3m)];

    protected override bool IsPlayable => base.Owner.Creature.GetPowerAmount<NanaFc>() >= base.DynamicVars["NanaFcCost"].BaseValue;
    protected override bool ShouldGlowGoldInternal => IsPlayable;

    public bool IsNanaFcMove = true;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<NanaGasterAssistPower>()),base.EnergyHoverTip];
    public NanaGasterAssist()
        : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<NanaFc>(choiceContext, base.Owner.Creature, -base.DynamicVars["NanaFcCost"].BaseValue, base.Owner.Creature, this);
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
        await PowerCmd.Apply<NanaGasterAssistPower>(choiceContext, base.Owner.Creature, base.DynamicVars["FrailPower"].BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
