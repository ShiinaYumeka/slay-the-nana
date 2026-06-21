using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.Cards;
using System.Text;

namespace SlayTheNANA;

/// <summary>
/// 统一卡图：<c>res://images/packed/card_portraits/nana_dummy/{蛇形类名}.png</c>。
/// 共用素材时在子类中重写 <see cref="PortraitFileStem"/>。
/// </summary>
public abstract class NanaCardModel : CustomCardModel
{
	public override string PortraitPath =>
		$"res://images/packed/card_portraits/nana_dummy/{PortraitFileStem}.png";

	/// <summary>默认由类型名转为蛇形文件名（与 Godot 资源 <c>nana_*.png</c> 对齐）。</summary>
	protected virtual string PortraitFileStem => TypeNameToSnakeCase(GetType().Name);

	protected NanaCardModel(int energyCost, CardType type, CardRarity rarity, TargetType targetType)
		: base(energyCost, type, rarity, targetType)
	{
	}

	static string TypeNameToSnakeCase(string typeName)
	{
		var sb = new StringBuilder(typeName.Length + 8);
		for (var i = 0; i < typeName.Length; i++)
		{
			var c = typeName[i];
			if (char.IsUpper(c) && i > 0)
				sb.Append('_');
			sb.Append(char.ToLowerInvariant(c));
		}

		return sb.ToString();
	}
}
