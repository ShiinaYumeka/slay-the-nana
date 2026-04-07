using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaTricolorBone : CardModel
{

    public bool IsNanaFcMove = true;
    public NanaTricolorBone()
		: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        CardModel card = CardFactory.GetDistinctForCombat(base.Owner, [(ModelDb.Card<NanaBlueBone>()), (ModelDb.Card<NanaOrangeBone>()), (ModelDb.Card<NanaPurpleBone>()), (ModelDb.Card<NanaRedBone>()), (ModelDb.Card<NanaBoneStorm>()), (ModelDb.Card<NanaBoneCombo>()), (ModelDb.Card<NanaBoneReturn>()), (ModelDb.Card<NanaBoneSpike>())], 1, base.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault();

        //if (base.IsUpgraded)
        //{
        //    CardCmd.Upgrade(card);
        //}
        card.SetToFreeThisTurn();
        card.AddKeyword(CardKeyword.Ethereal);

        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, addedByPlayer: true);

    }


    protected override void OnUpgrade()
	{
        base.EnergyCost.UpgradeBy(-1);

    }
}
