using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

using MegaCrit.Sts2.Core.Models.CardPools;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaSkullForm : NanaCardModel
{
	protected override IEnumerable<DynamicVar> CanonicalVars =>
	[
		new DynamicVar("StrengthLoss", 1m),
		new DynamicVar("ExtraPlays", 1m)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips =>
	[
		HoverTipFactory.FromPower<StrengthPower>(),
		HoverTipFactory.FromPower<NanaSkullFormPower>()
	];

	public NanaSkullForm()
		: base(3, CardType.Power, CardRarity.Rare, TargetType.Self)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		await PowerCmd.Apply<StrengthPower>(choiceContext, base.Owner.Creature, -base.DynamicVars["StrengthLoss"].BaseValue, base.Owner.Creature, this);
		await PowerCmd.Apply<NanaSkullFormPower>(choiceContext, base.Owner.Creature, base.DynamicVars["ExtraPlays"].BaseValue, base.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		base.AddKeyword(CardKeyword.Retain);
	}
}
