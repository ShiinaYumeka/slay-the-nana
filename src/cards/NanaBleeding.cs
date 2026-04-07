using Godot;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaBleeding: CardModel
{
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<NanaKarma>()),(HoverTipFactory.FromPower<NanaBleedingPower>())];

	public NanaBleeding()
		: base(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{

		if (cardPlay.Target.HasPower<NanaKarma>())
		{
			await PowerCmd.Apply<NanaBleedingPower>(cardPlay.Target, 3 * cardPlay.Target.GetPowerAmount<NanaKarma>(), base.Owner.Creature, this);
			await PowerCmd.Remove<NanaKarma>(cardPlay.Target);
		}
	}


	protected override void OnUpgrade()
	{
		base.EnergyCost.UpgradeBy(-1);
	}
}
