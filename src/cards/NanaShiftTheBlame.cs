using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MegaCrit.Sts2.Core.Models.CardPools;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaShiftTheBlame : NanaCardModel
{

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<NanaKarma>())];


    protected override bool IsPlayable => base.Owner.Creature.GetPowerAmount<NanaKarma>() >= 1;

    public NanaShiftTheBlame()
        : base(0, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int num = base.Owner.Creature.GetPowerAmount<NanaKarma>();
        await PowerCmd.Remove<NanaKarma>(base.Owner.Creature);

        await PowerCmd.Apply<NanaKarma>(choiceContext, cardPlay.Target, num, base.Owner.Creature, this);
    }

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
    {
        if (player == base.Owner && CombatManager.Instance.History.CardPlaysFinished.Any((CardPlayFinishedEntry e) => e.HappenedLastPlayerTurn(base.Owner) && e.CardPlay.Card == this))
        {
            CardPile? pile = base.Pile;
            if (pile == null || pile.Type != PileType.Hand)
            {
                await CardPileCmd.Add(this, PileType.Hand);
            }
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
