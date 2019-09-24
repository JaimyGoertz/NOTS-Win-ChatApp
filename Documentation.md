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

Een generic is een klasse die het mogelijk maakt om klasse en methodes te definiëren met een placeholder. Generics zijn toegevoegd in versie 2.0 van de C# programmeertaal en de CLR (Common Language Runtime). CLR zorgt voor het uitvoeren van code in verschillende ondersteunde programmeertalen. Generics zorgen ervoor dat het mogelijk is om geen type aan te geven. Dit kan later door de clients code bepaald worden. De parameter die gebruikt wordt is: "T". Generics zorgt ervoor dat boxing overbodig wordt (boxing wordt in het volgende hoofdstuk uitgelegd). 

Generics zijn herbruikbare en efficiënt in tegenstelling tot niet generieke alternatieven. Een voorbeeld van zo'n alternatief is ArrayList. In de meeste gevallen is het verstandig om ```List<T>``` te gebruiken in plaats van het zelf maken van een klasse.

Er is een mogelijkheid om het aantal types dat gebruikt wordt te beperken. Dit kan met behulp van constraints. Een constraint wordt als volgende gebruikt:
```csharp
public class GenericList<T> where T : class
{
}
 ```
Hierin kan class iedere klasse zijn.

### Code voorbeeld

Een generic wordt gedefinieerd door het gebruik van de punthaken (<>) en de parameter "T". Dit is hoe een generic klasse aangemaakt wordt:
 ```csharp
public class GenericList<T>
{
    public void Add(T input) { }
}
 ```

Het type van bovenstaande code (```<T>```) kan gedeclareerd worden door de volgende code aan te roepen:
```csharp
GenericList<int> list1 = new GenericList<int>();
        list1.Add(1);
 ```
In deze code hierboven wordt als type een integer gebruikt. Je kunt hier ook andere types gebruiken zoals bijvoorbeeld een string of een eigen klasse.

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

### Authentieke en gezaghebbende bronnen

-	C# programming guide (windows docs): https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
-	Windows docs CLR: https://docs.microsoft.com/en-us/dotnet/standard/clr 
- Windows docs constraints: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
- 
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
- https://docs.microsoft.com/en-us/dotnet/framework/performance/performance-tips 

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
