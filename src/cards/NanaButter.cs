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

public sealed class NanaButter: CardModel
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<WeakPower>(1m), new DynamicVar("DexterityLoss", 3m)];
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<DexterityPower>())];

	public NanaButter()
		: base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
	{
	}

	public PowerVar<NanaPlantMagic> NanaPlantMagic => (PowerVar<NanaPlantMagic>)base.DynamicVars["NanaPlantMagic"];

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{

		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
		await PowerCmd.Apply<WeakPower>(cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this);
		await PowerCmd.Apply<DexterityPower>(cardPlay.Target, -base.DynamicVars["DexterityLoss"].BaseValue, base.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars["DexterityLoss"].UpgradeValueBy(2m);
	}
}
