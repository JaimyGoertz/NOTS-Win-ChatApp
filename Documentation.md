Studentnaam: Jaimy Göertz

Studentnummer: 597339

---
# Algemene beschrijving applicatie

De chat app is een applicatie waarbij meerdere gebruikers berichten met elkaar kunnen uitwisselen. Dit doen ze door een aantal gegevens in te vullen om een connectie te maken met een server. Als de juiste gegevens zijn ingevuld kan de gebruiker (client) chatten met andere gebruikers die dezelfde gegevens hebben ingevuld. De benodigde gegevens zijn: poort, ip adres en buffergrootte. Alle clients praten via de server met elkaar. De gehele applicatie is voorzien van error handling. Hierdoor worden alle verkeerde user input en andere fouten goed afgehandeld. 

Voor dit project wordt windows forms gebruikt. De connecties worden gedaan op basis van het tcp protocol. Windows forms is de basis van de applicatie. De gebruikte programmeertaal van windows forms is C#. Ook wordt het .NET framework gebruikt.
Een aantal van de belangrijkste van technieken van c# en .net worden in de andere hoofdstukken van dit verslag behandelt.

---

## Generics

### Beschrijving van concept in eigen woorden

Een generic is een klasse die het mogelijk maakt om klasse en methodes te definiëren met een placeholder. Generics zijn toegevoegd in versie 2.0 van de C# programmeertaal en de CLR (Common Language Runtime). CLR zorgt voor het uitvoeren van code in verschillende ondersteunde programmeertalen. Generics zorgen ervoor dat het mogelijk is om geen type aan te geven. Dit kan later door de clients code bepaald worden. De parameter die gebruikt wordt is: "T". Generics zorgt ervoor dat boxing overbodig wordt (boxing wordt in het volgende hoofdstuk uitgelegd). Doordat het type later pas gekozen hoeft te worden. Code kan dan makkelijk worden hergebruikt.

Generics zijn herbruikbare en efficiënt in tegenstelling tot niet generieke alternatieven. Een voorbeeld van zo'n alternatief is ArrayList. In de meeste gevallen is het verstandig om ```List<T>``` te gebruiken in plaats van het zelf maken van een klasse. Er zijn verschillende soorten generics. De soorten zijn: interfaces, klassen, methodes, events en delegates. Generics worden voornamelijk gebruikt bij het maken van collection classes. Dit zijn klassen die fungeren als lijst. Met een generic delegate kan een delegate verschillende waardes krijgen. Net zoals bij een normale generic. Hieronder staat een voorbeeld van zo'n delegate: 
```csharp
public delegate void Del<T>(T item);
 ```
(delegates komen in een later hoofdstuk aan bod).

Er is een mogelijkheid om het aantal types dat gebruikt wordt te beperken. Dit kan met behulp van constraints. Constraints kunnen alleen klasses bevatten die erven van System.Object. Een constraint wordt als volgende gebruikt:
```csharp
public class GenericList<T> where T : class
{
}
 ```
Hierin kan class iedere klasse zijn. Het is ook mogelijk om meerdere types te gebruiken in een constraint. Dit doe je door achter de class met komma's meerdere types op te sommen.

### Code voorbeeld

Een generic wordt gedefinieerd door het gebruik van de punthaken (<>) en de parameter "T". Dit is te zien bij punt 1. Het type van "T" wordt bepaald bij punt 2, 3 en 4. Er zijn verschillende types mogelijk. Bij 2 en 3 zijn het value types (int en string). Je kunt ook een klasse gebruiken als type. Dat gebeurt bij punt 4.

Dit is hoe een generic generic klasse werkt:
 ```csharp
 // 1
public class GenericList<T>
{
    public void Add(T input) { }
}
class TestGenericList
{
    private class ExampleClass { }
    static void Main()
    {
        // 2
        GenericList<int> list1 = new GenericList<int>();
        list1.Add(1);

        // 3
        GenericList<string> list2 = new GenericList<string>();
        list2.Add("");

        // 4
        GenericList<ExampleClass> list3 = new GenericList<ExampleClass>();
        list3.Add(new ExampleClass());
    }
}
 ```

In de gehele klasse kan de ```<T>``` parameter gebruikt worden. Als je dit doet wordt de "T" verandert door het type dat gekozen is. Dit gebeurt ook in de volgende code:
```csharp
 public Node(T t)
        {
            data = t;
        }
 ```
of
```csharp
private T data;
 ```

### Alternatieven & adviezen
Het tegenovergestelde van een generic is een non-generic. Dit is ook het meest voorkomende alternatief. Een voorbeeld van zo'n non-generic is de ArrayList. Een groot verschil met een generic is dat je in een ArrayList meerdere verschillende types kunt toevoegen. Arraylists worden niet vaak meer gebruikt, omdat deze veel performance kosten. Microsoft (maker c#) raad zelf aan om geen arraylists meer te gebruiken, maar om generics te gebruiken. Het is dus aan te raden om generics te gebruiken.

### Authentieke en gezaghebbende bronnen

-	C# programming guide (windows docs): https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
-	Windows docs CLR: https://docs.microsoft.com/en-us/dotnet/standard/clr 
- Windows docs constraints: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
- Windows docs arraylists: https://docs.microsoft.com/en-us/dotnet/api/system.collections.arraylist?view=netframework-4.8
- Windows docs generic delegates: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-delegates

---


## Boxing & Unboxing

### Beschrijving van concept in eigen woorden

Boxing is het omzetten van een value type naar het object type of een ander type die geïmplementeerd wordt door dit type. Als een type geboxt wordt komt hij op de heap te staan. Unboxen is het tegenovergestelde. Bij unboxing haal je een value type uit een object. 

Boxing wordt vaak gebruikt om een variabele op te slaan op de heap. Een normale value type staat namelijk op de stack. Dit komt is het geval, omdat een object een reference type is. Dit betekent dat de waarde van een object op de heap wordt opgeslagen. Hiernaar wordt verwezen vanuit de stack. Het voordeel van de heap is dat daar een garbage collector actief is. Deze haalt alle onnodige data (garbage) weg.

Unboxing wordt gebruikt om een variabele van de heap op te slaan op de stack. Van de waarde in een object wordt een value type gemaakt (bijvoorbeeld een integer). Unboxing kan alleen plaatsvinden als er eerst een value type geboxt is. Er kan dus nooit alleen unboxing plaatsvinden. 

### Code voorbeeld

Een simpel voorbeeld van boxing is:

 ```csharp
int variable = 123;
object o = (object)variable;
```
In dit voorbeeld wordt een int geboxt. De waarde van de variabele wordt hier opgeslagen in een object. Het is belangrijk dat de variabele geconverteerd wordt naar een object. Dit is het stukje: "(object)". Als je dit vergeet geeft dit een error. 

Een simpel voorbeeld van unboxing is:

 ```csharp
int variable = 123;      
object o = variable;  

int j = (int)o;
 ```

In het code voorbeeld wordt eerst een variabele opgeslagen in een object. In de laatste regel wordt het object teruggezet naar een integer variabele. De laatste regel is unboxing.

### Alternatieven & adviezen

Boxing en unboxing vraagt veel performance. Als performance een grote rol speelt (dit is vaak in kleinere applicaties) is dit niet wenselijk. Als een value type geboxt wordt moet er een nieuw type toegewezen en gemaakt worden. Unboxing vraagt iets minder performance, maar nog steeds genoeg om hier hinder van te ondervinden. Het is dus aan te raden om het aantal keren dat je deze technieken gebruikt zo beperkt mogelijk te houden. Probeer geen value type waardes te geven aan een object als dit niet hoeft, want het heeft een significante impact op de snelheid van de code.

### Authentieke en gezaghebbende bronnen

-	Microsoft docs: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing
- Microsoft docs performance: https://docs.microsoft.com/en-us/dotnet/framework/performance/performance-tips 

---

## Delegates & Invoke

### Beschrijving van concept in eigen woorden

### Code voorbeeld

### Alternatieven & adviezen

### Authentieke en gezaghebbende bronnen


---

## Threading & Async
### Beschrijving van concept in eigen woorden
### Code voorbeeld
### Alternatieven & adviezen
### Authentieke en gezaghebbende bronnen
