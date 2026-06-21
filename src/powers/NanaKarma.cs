using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaKarma : PowerModel
{

	public override PowerType Type => PowerType.Debuff;

	public override PowerStackType StackType => PowerStackType.Counter;

    public override Color AmountLabelColor => StsColors.purple;

    public int CalculateDamageNextTurn()
	{
		ICombatState combatState = base.Owner.CombatState;
		if (combatState == null)
		{
			return (int)base.Amount;
		}

		IEnumerable<AbstractModel> modifiers;
		return (int)Hook.ModifyDamage(
			combatState.RunState,
			combatState,
			base.Owner,
			null,
			base.Amount,
			ValueProp.Unblockable | ValueProp.Unpowered,
			null,
			ModifyDamageHookType.All,
			CardPreviewMode.None,
			out modifiers);
	}

	public int CalculateTotalDamageNextTurn()
	{
		return CalculateDamageNextTurn();
	}

	public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
	{
		if (side != base.Owner.Side || !base.Owner.IsAlive)
		{
			return;
		}

		ICombatState ownerState = base.Owner.CombatState;
		decimal damage = base.Amount;
		if (ownerState != null)
		{
			IEnumerable<AbstractModel> modifiers;
			damage = Hook.ModifyDamage(
				ownerState.RunState,
				ownerState,
				base.Owner,
				null,
				base.Amount,
				ValueProp.Unblockable | ValueProp.Unpowered,
				null,
				ModifyDamageHookType.All,
				CardPreviewMode.None,
				out modifiers);
		}

		await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, damage, ValueProp.Unblockable | ValueProp.Unpowered, null, null);

		if (!base.Owner.IsAlive)
		{
			await Cmd.CustomScaledWait(0.1f, 0.25f);
			return;
		}

		if (NanaFinalTrialPower.ShouldPreventEnemyKarmaDecay(base.Owner, combatState))
		{
			return;
		}

		decimal amountToRemove = 4;
		await PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(), this, -amountToRemove, null, null);
	}
}
