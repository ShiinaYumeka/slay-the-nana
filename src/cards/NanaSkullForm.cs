using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.GameInfo.Objects;
using SlayTheNANA.src.cardtags;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaSkullForm : CardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DynamicVar("StrengthLoss", 2m),
        new IntVar("Replay", 1m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        (HoverTipFactory.FromPower<StrengthPower>()),
        HoverTipFactory.Static(StaticHoverTip.ReplayStatic)
    ];

    public NanaSkullForm()
        : base(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, -base.DynamicVars["StrengthLoss"].BaseValue, base.Owner.Creature, this);

        int replayAdd = base.DynamicVars["Replay"].IntValue;
        foreach (PileType pileType in new[] { PileType.Hand, PileType.Draw, PileType.Discard, PileType.Exhaust })
        {
            var pile = pileType.GetPile(base.Owner);
            if (pile?.Cards == null || pile.Cards.Count == 0)
            {
                continue;
            }

            foreach (CardModel card in pile.Cards.ToList())
            {
                if (!card.Tags.Contains(CustomCardTag.Bone))
                {
                    continue;
                }

                card.BaseReplayCount += replayAdd;
                CardCmd.Preview(card);
            }
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["StrengthLoss"].UpgradeValueBy(-1m);
    }
}
