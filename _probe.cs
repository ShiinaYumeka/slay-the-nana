using System;
using System.Linq;
using System.Reflection;
class P {
  static void Main() {
    var asm = Assembly.LoadFrom(@"libs\sts2.dll");
    foreach (var n in new[]{"MegaCrit.Sts2.Core.Combat.ICombatState","MegaCrit.Sts2.Core.Entities.Creatures.Creature"}) {
      var t = asm.GetType(n);
      Console.WriteLine("== "+n);
      foreach (var p in t.GetProperties().OrderBy(x=>x.Name)) Console.WriteLine("P "+p.Name+" : "+p.PropertyType.Name);
      foreach (var m in t.GetMethods(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly).OrderBy(x=>x.Name)) if(!m.IsSpecialName) Console.WriteLine("M "+m.Name);
    }
  }
}
