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
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<DexterityPower>(1m),new DynamicVar("NanaPlantMagic", 1m)];
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<DexterityPower>()),(HoverTipFactory.FromPower<NanaPlantMagic>())];

	public NanaChocolate()
		: base(0, CardType.Skill, CardRarity.Common, TargetType.AllEnemies)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, this);
		await PowerCmd.Apply<NanaPlantMagic>(base.CombatState.HittableEnemies, base.DynamicVars["NanaPlantMagic"].BaseValue, base.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars["NanaPlantMagic"].UpgradeValueBy(1m);
	}
}
