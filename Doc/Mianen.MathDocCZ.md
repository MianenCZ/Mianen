# Plně generická matematická knihovna
## Generická reprezentace čísel
Knihovna [Mianen.Matematics.Numerics](https://github.com/MianenCZ/Mianen/tree/Credit/Mianen/Matematics.Numerics "GitHub - Mianen[Credit]") implementuje interface pro generické počítání.
Umožňuje implementaci základních matematických operací se zaměřením na libovolný datový typ.

### Interface `INumber<T>`
Implementuje možnost vytvářet genericky číleslné typy, které splňují axiomy Těles.
Knihovna pak implementuje různé matematické funkce, které lze zaměřit na libovnolný datový typ.
Generický typ `<T>` proto můžeme považovat za definici přesnosti matematických funkcí.

>Jak víme C# neumožňuje vynucení statických metod. Proto vynutit operátory +,-,\*... není možné.
Matematické operace jsou proto vynuceny za pomoci instančních metod.

### Implementace
```cs
public interface INumber<T> : IComparable<INumber<T>>, IEquatable<INumber<T>>
{
    T Value { get; set; }
    INumber<T> Add(INumber<T> Number);
    INumber<T> Subtract(INumber<T> Number);
    INumber<T> Multiply(INumber<T> Number);
    INumber<T> Divide(INumber<T> Number);
    INumber<T> Negative();
    INumber<T> Power(INumber<T> Exponent);
    bool IsGreaterThan(INumber<T> Number);
    bool IsGreaterOrEqualThan(INumber<T> Number);
    bool IsLowerThan(INumber<T> Number);
    bool IsLowerOrEqualThan(INumber<T> Number);
    bool IsEqual(INumber<T> Number);
    bool IsNotEqual(INumber<T> Number);

    INumber<T> GetZero();
    INumber<T> GetOne();

    string ToString(IFormatProvider IformatProvider);
    string ToString(string Format);
    string ToString(string Format, IFormatProvider IformatProvider);
}
```

>Hlavní "nepřehledností" je získávání konstant z instance.
Metodu `GetZero` bychom v kalsickém kódu imlementovali staticky.
Jak bylo již smíněno, statické metody nelze vynutit a proto je získávání
konstant nutno implementovat tímto způsobem.


### Uživatelská zodpovědnost

Implementace pomocí metod umožňuje programátorovi libovolné chování.
Nicméně knihovny, které využívají implementace `INumber` očekávají určité
invariantní chování. Toto chování nelze vynutit ani jednoduše kontrolovat.


### "Invarianty" pro používání

 - Metody vracející novou instanci `INumber`, by měl vracet  výsledek operace,
 ktrerou reprezentují na daném datovém typu
 - Metody (Vyjma konstruktoru a `Value Get`) nijak neupravují instanci `INumber`
 - Metody zachovávají svůj obecný význam vůči datovému typu, který instance reprezentuje
 - Metody zachovávají svou obecnou časovou a prostorovou složitost vůči
 datovému typu, který instance reprezentuje
 - Metody implementující matematické operace zachávájí axiomy Těles
    - Komutativita sčítání
    - Asociativita sčítání
    - Existence nulového prvku
    - Existence opačného prvku
    - Existence jednotkového prvku
    - Komutativita násobení
    - Asociativita násobení
    - Existence inverzního prvku
    - Netrivialita



### Základní použití
Mějme například následující implementaci `INumber` zaměřenou na `double`.
Implementace samotná je prostá a pouze přepisuje základní statické operátory na metody.

>K implementaci je vhodné doplnit základní konverze pro snadnější manipulaci se vstupy.

```cs
public class NDouble : INumber<double>
{
      public double Value { get; set; }
      public NDouble(double Value)
      {
          this.Value = Value;
      }
      public bool Equals(INumber<double> Number) => this.Value == Number.Value;
      public INumber<double> Add(INumber<double> Number) => new NDouble(this.Value + Number.Value);
      public INumber<double> Multiply(INumber<double> Number) => new NDouble(this.Value * Number.Value);
      public override string ToString() => this.Value.ToString();
      public INumber<double> GetOne() => new NDouble(1);
      public static implicit operator NDouble(double Value)
      {
          return new NDouble(Value);
      }
      ...
}
```

#### Ukázka použití
```cs
  NDouble d1 = new NDouble(4.13159);
  NDouble d2 = new NDouble(4);
  NDouble d3 = 4.13159;
  NDouble d4 = 4;

  var r1 = d1.Divide(d2);
  var r2 = d3.Add(d4);
  var r3 = d2.Subtract(d2);
  bool b1 = d1.IsEqual(d4);
  bool b3 = d3.IsLowerThan(d2);

  List<NDouble> l1 = new List<NDouble>() {.1, .2, .3, .4};

  NDouble Sum = l1[0];
  for (int i = 1; i < 4; i++)
  {
      Sum.Add(l1[i]);
  }
```

## Generické maticové počítání

Za použití implementace `INumber` implementuje knihovna [Mianen.Matematics.LinearAlgebra](https://github.com/MianenCZ/Mianen/tree/Credit/Mianen/Matematics.LinearAlgebra "GitHub - Mianen[Credit]") základní maticové počítání a některé pokročilé funkce a operace

>Pro použití této knihovny je třeba dodržovat pravidla knihovny `INumber<T>`

Knihovna se skládá z několika základních tříd:
```cs
namespace Mianen.Matematics.LinearAlgebra
{
    public static class Matrix {...}
    public static class LinearMath<T> {...}
}
```

## Implementace
