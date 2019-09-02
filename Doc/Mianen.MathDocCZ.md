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

### Ukázka použití operací
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

### Konstrukce nad netradičním objektem
Uživatel může začít připravovat svojí matematickou třídu od základu pomocí interface `INumber` a zajistit tak, že finální produkt může využívat stále rozsáhlejší matematickou knihovnu.

>Konstukce nad stringem
>```cs
>   public class SNumber : INumber<string>
>   {
>       public string Value { get; set; }
>
>       public SNumber(string Value)
>       {
>           this.Value = Value;
>       }
>
>       public INumber<string> Add(INumber<string> Number)
>       {
>           if (this.Value.Length != Number.Value.Length)
>               throw new ArgumentException();
>           StringBuilder bld = new StringBuilder();
>
>           for (int i = 0; i < this.Value.Length; i++)
>           {
>             bld.Append((char)((this.Value[i] + Number.Value[i]) % 256));
>           }
>           return new SNumber(bld.ToString());
>       }
>   ...
>   }
>```
>Kód:
>```cs
>   SNumber s1 = new SNumber("ahoj");
>   SNumber s2 = new SNumber("ahoj");
>   var res = s1.Add(s2);
>   Console.WriteLine(res.Value.ToString());
>```
>Vrátí výsledek: `BP^T`
>|  | a | h | o | j |
>|----|---|---|---|---|
>|  ASCII  | 97  | 104  | 111  | 106  |
>| s1 + s2 | 194 | 208 | 222 | 212 |
>|  %126 ASCII  |  66 | 80  | 94  | 84  |
>| Result:  |  B |  p | ^  |  T |




### Konstrukce nad existujicími čísly

Druhá možnost je za `<T>` zvolit již existující impmenetaci čísla. Knihovna sama obsahuje implementaci
[Decimal - NDecimal](https://github.com/MianenCZ/Mianen/blob/Credit/Mianen/Matematics.Numerics/INumber_ExplicitDefinition/NDecimal.cs "GitHub - Mianen[Credit]"),
[Double - NDouble](https://github.com/MianenCZ/Mianen/blob/Credit/Mianen/Matematics.Numerics/INumber_ExplicitDefinition/NDouble.cs "GitHub - Mianen[Credit]"),
[Float - NFloat](https://github.com/MianenCZ/Mianen/blob/Credit/Mianen/Matematics.Numerics/INumber_ExplicitDefinition/NFloat.cs "GitHub - Mianen[Credit]"),
[Int - NInt](https://github.com/MianenCZ/Mianen/blob/Credit/Mianen/Matematics.Numerics/INumber_ExplicitDefinition/NInt.cs "GitHub - Mianen[Credit]") a
[Long - NLong](https://github.com/MianenCZ/Mianen/blob/Credit/Mianen/Matematics.Numerics/INumber_ExplicitDefinition/NLong.cs "GitHub - Mianen[Credit]").

Zcela jednoduše lze vytvořit například nad [BigInteger](https://docs.microsoft.com/cs-cz/dotnet/api/system.numerics.biginteger?view=netframework-4.8 "Microsoft - .Net dokumentace"), nebo implmentací fixed point [Long - NLong](https://github.com/MianenCZ/Mianen/blob/Credit/Mianen/Matematics.Numerics/INumber_ExplicitDefinition/NLong.cs "GitHub - Mianen[Credit]")




# Generické maticové počítání

Za použití implementace `INumber` implementuje knihovna [Mianen.Matematics.LinearAlgebra](https://github.com/MianenCZ/Mianen/tree/Credit/Mianen/Matematics.LinearAlgebra "GitHub - Mianen[Credit]") základní maticové počítání a některé pokročilé funkce a operace

>Pro použití této knihovny je třeba dodržovat pravidla knihovny `INumber<T>`

## Implementace
```cs
namespace Mianen.Matematics.LinearAlgebra
{
    public class Matrix<T> {...}
    public static class Matrix {...}
    public static class MatrixMT {...}
    public class VirtualSubMatrix<T> : Matrix<T> {...}

    public class Vector<T> {...}
    public static class Vector {...}

    public static class LinearMath {...}
}
```

Implementace je dělena na třídu `Matrix<T>`, která implementuje samotnou instanci Matice a metody instanci vlastní. Dále statická třída `Matrix` implementuje statické generické metody maticových operací.
>Implementace pomocí statické metody `Matrix` umožňuje využít syntaktické zkratky implicitního definování generiky pomocí argumentů metody

Třída `VirtualSubMatrix<T>` je "virtuální rozhraní" umožňující podhled na matici jako na její submatici. Na této virtuální matici lze provádět jakékoliv operace, jako na základní matici. Pomocí virtuální podmatice statická třída `MatrixMT` implementuje některé metody v multivláknové podobě.

### Bližší pohled na generickou matici
```cs
public class Matrix<T>
{
    public int RowCount { get; internal set; }
    public int ColumnCount { get; internal set; }
    internal INumber<T>[,] Data;

    public virtual INumber<T> this[int i, int j, bool IndexFromZero = true]
		{
			get => Data[(IndexFromZero) ? i : i - 1, (IndexFromZero) ? j : j - 1];
			set => Data[(IndexFromZero) ? i : i - 1, (IndexFromZero) ? j : j - 1] = value;
		}
    ...
}
```
Věnujme pozornost tomu, že `<T>` je bez jakékoliv restrikce
>Tady by se dost hodilo říct: "`Matrix<T>` existuje právě tehdy pokud existuje`INumber<T>` a právě tato konkterétní implementace `INumber<T>` bude použita v `Matrix<T>`.

#### Matice lze pak použít následovně:
Mějme třídy `NumDouble1` a `NumDouble2` implementující `INumber<double>`. Pak může dojít i k následujcímu podivnému použití:
```cs
                                //RowCount, ColumnCount
  Matrix<double> t = new Matrix<double>(2,2);
  NumDouble1 a = new NumDouble1(1);
  NumDouble2 b = new NumDouble2(1);
  t[1, 1] = a;
  t[2, 2] = b;
```
>Přestože obě třídy mohou implementovat zcela jiné chování, obě imlementují `INumber<double>` a tedy existují mezi nimi operace. Programově je takovýto kód v pořádku. Významově pak záleží čistě na uživateli/programátorovi, který knihovnu používá.
