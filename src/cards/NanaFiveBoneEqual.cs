using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using SlayTheNANA.src.cardtags;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaFiveBoneEqual : NanaCardModel
{
	
	protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CustomCardTag.Bone };
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("WhiteBoneCount", 5m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => (HoverTipFactory.FromCardWithCardHoverTips<NanaWhiteBone>());

    public NanaFiveBoneEqual()
		: base(3, CardType.Skill, CardRarity.Rare, TargetType.Self)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		int count = (int)base.DynamicVars["WhiteBoneCount"].BaseValue;
		for (int i = 0; i < count; i++)
		{
			CardModel card = base.CombatState.CreateCard(ModelDb.Card<NanaWhiteBone>(), base.Owner);
			card.SetToFreeThisTurn();
            if (base.IsUpgraded)
            {
                CardCmd.Upgrade(card);
            }
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, base.Owner);
		}
    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }

}
