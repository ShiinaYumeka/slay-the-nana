using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheNANA;


[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaChocolate : NanaCardModel
{

	static LocString DrawSelectionPrompt =>
		new("cards", "SLAYTHENANA-NANA_CHOCOLATE.drawSelectionScreenPrompt");
	protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1)];
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [base.EnergyHoverTip, (HoverTipFactory.FromPower<NanaPlantMagic>())];

	public NanaChocolate()
		: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);

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
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars.Energy.UpgradeValueBy(1);
	}
}
