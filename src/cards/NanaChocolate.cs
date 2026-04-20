using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaChocolate : CardModel
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1), new DynamicVar("NanaPlantMagic", 1m)];
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [base.EnergyHoverTip, (HoverTipFactory.FromPower<NanaPlantMagic>())];

	public NanaChocolate()
		: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
		await PowerCmd.Apply<NanaPlantMagic>(base.CombatState.HittableEnemies, base.DynamicVars["NanaPlantMagic"].BaseValue, base.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars.Energy.UpgradeValueBy(1);
	}
}
