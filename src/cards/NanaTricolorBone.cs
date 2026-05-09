using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using SlayTheNANA.src.cardtags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaTricolorBone : CardModel
{
    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CustomCardTag.Bone };

    public bool IsNanaFcMove = true;
    public NanaTricolorBone()
		: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
	{
	}

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromKeyword(CardKeyword.Ethereal))];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        IEnumerable<CardModel> boneCandidates =
            from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
            where c.Tags.Contains(CustomCardTag.Bone) && c.GetType() != typeof(NanaSkullForm)
            select c;

        CardModel? card = CardFactory.GetDistinctForCombat(base.Owner, boneCandidates, 1, base.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault();
        if (card == null)
        {
            return;
        }

        CardCmd.Upgrade(card);
        card.SetToFreeThisTurn();
        card.AddKeyword(CardKeyword.Ethereal);

        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, addedByPlayer: true);

    }


    protected override void OnUpgrade()
	{
        base.EnergyCost.UpgradeBy(-1);

    }
}
