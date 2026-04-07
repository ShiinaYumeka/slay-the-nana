using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
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
using System.Threading.Tasks;
using System.Linq;

namespace SlayTheNANA;

public sealed class NanaReality: CardModel
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(5m, ValueProp.Move), new DynamicVar("NanaFcCost", 30m), new RepeatVar(4)];

    protected override bool IsPlayable => base.Owner.Creature.GetPowerAmount<NanaFc>() >= base.DynamicVars["NanaFcCost"].BaseValue;
    protected override bool ShouldGlowGoldInternal => IsPlayable;

    public bool IsNanaFcMove = true;
    public NanaReality()
		: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        await PowerCmd.Apply<NanaFc>(base.Owner.Creature, -base.DynamicVars["NanaFcCost"].BaseValue, base.Owner.Creature, this);

        CardModel card = CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
                where c.Rarity == CardRarity.Rare
                select c, 1, base.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault();

        if (base.IsUpgraded)
        {
            CardCmd.Upgrade(card);
        }
        card.SetToFreeThisCombat();

        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, addedByPlayer: true);

    }


    protected override void OnUpgrade()
	{
        base.DynamicVars["NanaFcCost"].UpgradeValueBy(-5m);

    }
}
