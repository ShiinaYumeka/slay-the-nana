using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MegaCrit.Sts2.Core.Models.CardPools;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaVoidVirus : NanaCardModel
{
	public override bool GainsBlock => true;

	protected override IEnumerable<DynamicVar> CanonicalVars =>
		[new BlockVar(8m, ValueProp.Move), new DynamicVar("NanaFcCost", 40m)];

	protected override bool IsPlayable =>
		base.Owner.Creature.GetPowerAmount<NanaFc>() >= base.DynamicVars["NanaFcCost"].BaseValue;

	protected override bool ShouldGlowGoldInternal => IsPlayable;

	public bool IsNanaFcMove = true;

	static LocString DrawSelectionPrompt =>
		new("cards", "SLAYTHENANA-NANA_VOID_VIRUS.drawSelectionScreenPrompt");

	public NanaVoidVirus()
		: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PowerCmd.Apply<NanaFc>(choiceContext, base.Owner.Creature, -base.DynamicVars["NanaFcCost"].BaseValue, base.Owner.Creature, this);
		await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);

		CardSelectorPrefs discardPrefs = new(SelectionScreenPrompt, 1);
		CardModel fromDiscard = (await CardSelectCmd.FromCombatPile(
			choiceContext,
			PileType.Discard.GetPile(base.Owner),
			base.Owner,
			discardPrefs)).FirstOrDefault();
		if (IsInDrawOrDiscardPile(fromDiscard))
		{
			await CardPileCmd.Add(fromDiscard, PileType.Draw, CardPilePosition.Top);
		}

		CardSelectorPrefs drawPrefs = new(DrawSelectionPrompt, 1);
		CardModel fromDraw = (await CardSelectCmd.FromCombatPile(
			choiceContext,
			PileType.Draw.GetPile(base.Owner),
			base.Owner,
			drawPrefs)).FirstOrDefault();
		if (fromDraw?.Pile?.Type == PileType.Draw)
		{
			await CardPileCmd.Add(fromDraw, PileType.Hand);
		}

		CardModel toExhaust = (await CardSelectCmd.FromHand(
			prefs: new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1),
			context: choiceContext,
			player: base.Owner,
			filter: null,
			source: this)).FirstOrDefault();
		if (toExhaust != null)
		{
			await CardCmd.Exhaust(choiceContext, toExhaust);
		}
	}

	protected override void OnUpgrade()
	{
		base.AddKeyword(CardKeyword.Retain);
	}

	static bool IsInDrawOrDiscardPile(CardModel? card) =>
		card?.Pile?.Type is PileType.Draw or PileType.Discard;
}
