using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyRelicPool))]
public sealed class NanaRedTridentRelic : RelicModel
{
	public override RelicRarity Rarity => RelicRarity.Common;
	protected override IEnumerable<DynamicVar> CanonicalVars => [(new CardsVar(1))];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromCardWithCardHoverTips<NanaRedTrident>();

	public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
	{
		if (player == base.Owner && combatState.RoundNumber == 1)
		{
			CardModel card = combatState.CreateCard(ModelDb.Card<NanaRedTrident>(),base.Owner);
			await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, player);
		}
	}
}
