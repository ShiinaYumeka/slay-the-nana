using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MegaCrit.Sts2.Core.Models.CardPools;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaBladeSlash : NanaCardModel
{
	static LocString DrawSelectionPrompt =>
		new("cards", "SLAYTHENANA-NANA_BLADE_SLASH.drawSelectionScreenPrompt");

	protected override IEnumerable<DynamicVar> CanonicalVars =>
		[new DamageVar(9m, ValueProp.Move | ValueProp.Unblockable)];

	public NanaBladeSlash()
		: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
		await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
			.WithHitFx("vfx/vfx_attack_slash")
			.Execute(choiceContext);

		CardSelectorPrefs drawPrefs = new(DrawSelectionPrompt, 1);
		CardModel fromDraw = (await CardSelectCmd.FromCombatPile(
			choiceContext,
			PileType.Draw.GetPile(base.Owner),
			base.Owner,
			drawPrefs)).FirstOrDefault();
		if (fromDraw?.Pile?.Type == PileType.Draw)
		{
			await CardPileCmd.Add(fromDraw, PileType.Discard);
		}
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars.Damage.UpgradeValueBy(3m);
	}
}
