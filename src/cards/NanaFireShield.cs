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

namespace SlayTheNANA;

public sealed class NanaFireShield: CardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1), new BlockVar(12m, ValueProp.Move), new PowerVar<DexterityPower>(1m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [base.EnergyHoverTip, (HoverTipFactory.FromPower<DexterityPower>())];

    public NanaFireShield()
        : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
        await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, -base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, this);
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);

    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Block.UpgradeValueBy(4m);
        base.DynamicVars.Energy.UpgradeValueBy(1m);
    }
}
