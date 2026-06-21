using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Models.PotionPools;
using BaseLib.Abstracts;
using System;

namespace MegaCrit.Sts2.Core.Models.PotionPools;

/// <summary>
/// Nana 专用药水池：药水内容与铁甲战士池一致，能量图标使用角色资源。
/// </summary>
public sealed class NanaDummyPotionPool : CustomPotionPoolModel
{
    public override string? TextEnergyIconPath => "res://images/packed/sprite_fonts/nana_dummy_energy_icon.png";

    public override string? BigEnergyIconPath => "res://images/packed/sprite_fonts/nana_dummy_energy_icon.png";

    protected override IEnumerable<PotionModel> GenerateAllPotions()
    {
        PotionPoolModel ironclad = ModelDb.PotionPool<IroncladPotionPool>();
        foreach (PotionModel p in EnumeratePotionsFromPool(ironclad))
        {
            yield return p;
        }
    }

    private static IEnumerable<PotionModel> EnumeratePotionsFromPool(PotionPoolModel pool)
    {
        Type t = pool.GetType();
        PropertyInfo? prop =
            t.GetProperty("AllPotions")
            ?? t.GetProperty("Potions")
            ?? t.GetProperty("AllModels");

        object? val = prop?.GetValue(pool);
        if (val is IEnumerable<PotionModel> typed)
        {
            foreach (PotionModel p in typed)
            {
                yield return p;
            }

            yield break;
        }

        if (val is IEnumerable raw)
        {
            foreach (object? o in raw)
            {
                if (o is PotionModel pm)
                {
                    yield return pm;
                }
            }
        }
    }
}
