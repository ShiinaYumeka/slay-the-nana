using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace SlayTheNANA;

public sealed class NanaWhirlwind : CardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [(CardKeyword.Exhaust), (CardKeyword.Ethereal)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        (HoverTipFactory.FromKeyword(CardKeyword.Exhaust)),
        (HoverTipFactory.FromKeyword(CardKeyword.Ethereal)),
        (HoverTipFactory.FromPower<DexterityPower>())
    ];

    public NanaWhirlwind()
        : base(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);

        int dex = cardPlay.Target.GetPowerAmount<DexterityPower>();
        if (dex != 0)
        {
            await PowerCmd.Apply<DexterityPower>(cardPlay.Target, -dex, base.Owner.Creature, this);
        }

        int gain = Math.Abs(dex);
        if (gain != 0)
        {
            await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, gain, base.Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Ethereal);
    }
}
