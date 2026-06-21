using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System.Collections.Generic;
using System.Linq;

namespace SlayTheNANA;

public sealed class NanaFinalTrialPower : PowerModel
{
	public override PowerType Type => PowerType.Buff;

	public override PowerStackType StackType => PowerStackType.Single;

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<NanaKarma>()];

	internal static bool ShouldPreventEnemyKarmaDecay(Creature karmaOwner, ICombatState combatState)
	{
		if (karmaOwner.IsPlayer)
		{
			return false;
		}

		return combatState.RunState.Players.Any(player => player.Creature.HasPower<NanaFinalTrialPower>());
	}
}
