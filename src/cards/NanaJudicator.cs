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
public sealed class NanaJudicator : NanaCardModel
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("NanaJudicatorKarma", 4m)];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<NanaJudicatorPower>())];

	/// <summary>与审判共用卡图，直至有独立立绘。</summary>
	protected override string PortraitFileStem => "nana_trial";

	public NanaJudicator()
		: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PowerCmd.Apply<NanaJudicatorPower>(choiceContext,
            base.Owner.Creature,
			base.DynamicVars["NanaJudicatorKarma"].BaseValue,
			base.Owner.Creature,
			this);
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars["NanaJudicatorKarma"].UpgradeValueBy(2m);
	}
}
