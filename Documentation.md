Studentnaam: Jaimy Göertz

Studentnummer: 597339

---
# Algemene beschrijving applicatie

De chat app is een applicatie waarbij meerdere gebruikers berichten met elkaar kunnen uitwisselen. Dit doen ze door een aantal gegevens in te vullen om een connectie te maken met een server. Als de juiste gegevens zijn ingevuld kan de gebruiker (client) chatten met andere gebruikers die dezelfde gegevens hebben ingevuld. De benodigde gegevens zijn: poort, ip adres en buffergrootte. Alle clients praten via de server met elkaar. De gehele applicatie is voorzien van error handling. Hierdoor worden alle verkeerde user input en andere fouten goed afgehandeld. 

Voor dit project wordt Windows Forms gebruikt. De connecties worden gedaan op basis van het tcp protocol. Windows Forms is de basis van de applicatie. De gebruikte programmeertaal van Windows Forms is C#. Ook wordt het .NET framework gebruikt.
Een aantal van de belangrijkste van technieken van C# en .NET worden in de andere hoofdstukken van dit verslag behandelt.

---

## Generics

### Beschrijving van concept in eigen woorden

Een generic is een klasse die het mogelijk maakt om klassen en methodes te definiëren met een placeholder. Generics zijn toegevoegd in versie 2.0 van de C# programmeertaal en de CLR (Common Language Runtime). CLR zorgt voor het uitvoeren van code in verschillende ondersteunde programmeertalen. Generics zorgen ervoor dat het mogelijk is om geen type aan te geven. Dit kan later door de clients code bepaald worden. De parameter die gebruikt wordt is: "T". Generics zorgt ervoor dat boxing overbodig wordt (boxing wordt in het volgende hoofdstuk uitgelegd). Doordat het type later pas gekozen hoeft te worden. Code kan dan makkelijk worden hergebruikt.

Generics zijn herbruikbare en efficiënt in tegenstelling tot niet generieke alternatieven. Een voorbeeld van zo'n alternatief is ArrayList. In de meeste gevallen is het verstandig om ```List<T>``` te gebruiken in plaats van het zelf maken van een klasse. Er zijn verschillende soorten generics. De soorten zijn: interfaces, klassen, methodes, events en delegates. Generics worden voornamelijk gebruikt bij het maken van collection classes. Dit zijn klassen die fungeren als lijst. Met een generic delegate kan een delegate verschillende waardes krijgen. Net zoals bij een normale generic. Hieronder staat een voorbeeld van zo'n delegate: 
```csharp
public delegate void Del<T>(T item);
 ```
(delegates komen in een later hoofdstuk aan bod).

Er is een mogelijkheid om het aantal types dat gebruikt wordt te beperken. Dit kan met behulp van constraints. Constraints kunnen alleen klasses bevatten die erven van System.Object. Een constraint wordt als volgende gebruikt:
```csharp
public class GenericList<T> where T : class {}
 ```
Hierin kan class iedere klasse zijn. Het is ook mogelijk om meerdere types te gebruiken in een constraint. Dit doe je door achter de class met komma's meerdere types op te sommen.

### Code voorbeeld

Een generic wordt gedefinieerd door het gebruik van de punthaken (<>) en de parameter "T".  Dit is te zien bij punt 1 in onderstaande code. Het type van "T" wordt bepaald bij punt 2, 3 en 4. Er zijn verschillende types mogelijk. Bij 2 en 3 zijn het value types (int en string). Je kunt ook een klasse gebruiken als type. Dat gebeurt bij punt 4.

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
Het tegenovergestelde van een generic is een non-generic. Dit is ook het meest voorkomende alternatief. Een voorbeeld van zo'n non-generic is de ArrayList. Een groot verschil met een generic is dat je in een ArrayList meerdere verschillende types kunt toevoegen. Arraylists worden niet vaak meer gebruikt, omdat deze veel performance kosten. Microsoft (maker C#) raad zelf aan om geen arraylists meer te gebruiken, maar om generics te gebruiken. Het is dus aan te raden om generics te gebruiken.

### Authentieke en gezaghebbende bronnen

-	Windows docs over generics: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
-	Windows docs over CLR: https://docs.microsoft.com/en-us/dotnet/standard/clr 
- Windows docs over constraints: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
- Windows docs over arraylists: https://docs.microsoft.com/en-us/dotnet/api/system.collections.arraylist?view=netframework-4.8
- Windows docs over generic delegates: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-delegates

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
In dit voorbeeld wordt een integer geboxt. De waarde van de variabele wordt hier opgeslagen in een object. Het is belangrijk dat de variabele geconverteerd wordt naar een object. Dit is het stukje: "(object)". Als je dit vergeet geeft dit een error. 

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

-	Microsoft docs over boxing/unboxing: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing
- Microsoft docs over performance: https://docs.microsoft.com/en-us/dotnet/framework/performance/performance-tips 

---

## Delegates & Invoke

### Beschrijving van concept in eigen woorden
Een delegate is een type die een referentie naar een methode bevat. Hij heeft 1 of meerdere parameters en een return type. Alle methodes met hetzelfde aantal parameters en return type kunnen verbonden worden aan een delegate. Je kunt deze methodes dan via de delegate aanroepen. Dit zit ook in de naam: "To delegate" betekent in het Nederlands delegeren. Dit is ook wat een delegate doet. Hij geeft een aanroep door aan de juiste methode die op deze delegate lijkt. Methodes waar delegates naar verwijzen kunnen static of een instance method (methode zonder het static keyword) zijn. Dit maakt het makkelijk om de aanroep van een methode te veranderen of om nieuwe code toe te voegen aan bestaande klassen. Een andere belangrijke functie van delegates is het doorgeven van methodes als parameters.  

De naam van een delegate hoeft niet overeen te komen met de methode die wordt aangewezen. De namen van de parameters hoeven ook niet overeen te komen. Als de types en hoeveelheden parameters maar overeen komen. Delegates zijn de basis van events in windows forms. Voor een windows forms applicatie zijn delegates dus vaak noodzakelijk.

Een invoke voert een delegate uit op een specifieke thread in Windoes Forms. Een invoke kun op twee manieren implementeren (zie code voorbeeld). Een invoke retouneert altijd een object.

### Code voorbeeld
Een simpel voorbeeld van een delegate is:
```csharp
public delegate int DoSomethingImportant(int x, int y);
 ```

Een functie die aangeroepen zou kunnen worden met behulp van bovenstaande delegate is:
```csharp
public int AddTwoNumbers(int variable1, int variable2)
{
    int count = variable1 + variable2;
    Console.WriteLine(count);
    return count
}
 ```
 Het belangrijkste is dat alle types overeen komen met die van de delegate.

De twee manieren van het implementeren van een invoke zijn:
```csharp
Invoke(Delegate)
 ```
 en
 ```csharp
 ```Invoke(Delegate, Object[])```
 ```
 Er zit een subtiel verschil tussen beide implementaties. Het enige verschil is de lijst van objecten die meegenomen worden als parameter.


### Alternatieven & adviezen
Delegates worden binnen C# vaak gebruikt. Het is ook een unieke functie van C#. Zoals eerder beschreven zijn delegates de basis van events in windows forms. Je zal ze dus vaak moeten gebruiken in windows forms. Je komt er bijna niet om heen. Ik zou dit ook niet proberen, omdat delegates mits gebruikt met goede reden prima zijn. Een interface zou een alternatief zijn. Als een klasse 1 implementatie nodig heeft van een methode kun je een interface gebruiken. Als het er meer moeten zijn is een delegate beter. Voor invoke geld hetzelfde als bij de delegate. Er is geen reden om invoke te vermijden. Vooral in combinatie met de delegate kan een invoke zeker de juiste oplossing zijn.

### Authentieke en gezaghebbende bronnen
- Windows docs over delegates: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/
- Windows docs over invoke: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.invoke?view=netframework-4.8
- Windows docs over delegates vs interfaces: https://docs.microsoft.com/nl-nl/previous-versions/visualstudio/visual-studio-2010/ms173173(v=vs.100)
- Windows docs over het gebruik van delegates: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/using-delegates

---

## Threading & Async

### Beschrijving van concept in eigen woorden
Threading is het parallel uitvoeren van code. Deze threads zorgen ervoor dat er meerdere code tegelijk uitgevoerd kan worden. Een "normaal" programma werkt met één thread. Deze thread heet de primaire thread.  Je kunt naast deze standaard thread zelf nog meerdere threads openen. Deze heten: "worker threads". Met meerdere threads kun je code tegelijkertijd uitvoeren door iedere thread een ander stuk code te laten uitvoeren. Als er meerdere threads geopend zijn heet dit multithreading. Meerdere threads kunnen bijvoorbeeld de user interface optimaliseren door de website responsive te houden terwijl er op de achtergrond andere dingen gebeuren.  

Threads werken aan de hand van scheduling priority. Dit betekent dat iedere thread een nummer krijgt gebaseerd op hoe belangrijk ze zijn. Een nummer wordt gebaseerd op de klasse waarin hij staat en de prioriteit. Threads worden ingeplanned op basis van belangrijkheid. Hierin is 0 het minst belangrijk en 31 het meest belangrijk.

De tegenhanger van threading is async. Async zorgt er ook voor dat code tegelijk kan runnen. Een belangrijke functie van async is het responsive houden van een applicatie, want het zorgt ervoor dat de code niet vastloopt en niet op elkaar hoeft te wachten. Dit zorgt voor een betere ervaring voor de gebruiker. Dit komt, omdat alle UI gerelateerde zaken op één thread (de UI thread) uitgevoerd worden. 

Een ander belangrijk aspect van asynchronous programming is de await operator. De await operator zorgt ervoor dat de code niet verder mag tot de regel waar het keyword klaar is met uitvoeren. Een ander belangrijk aspect van asynchronous programming zijn taks. Dit is het returntype van een methode die async is. Je kunt ook een void returnen, maar dit is alleen aan te raden bij event handlers.

### Code voorbeeld
Een thread wordt op de volgende manier aangemaakt en uitgevoerd:
```csharp
var th = new Thread(new ThreadStart(FunctionThatExecutesOnThread()));
 ```
 De "new Thread" maakt een thread aan en de "new ThreadStart" zorgt ervoor dat de thread gestart word.

 Een async methode ziet er als volgt uit:
```csharp
async Task<int> GetTaskOfTResultAsync()
{
    int hours = 0;
    await Task.Delay(0);
    return hours;
}
 ```
 Let hierbij op het async keyword aan het begin van de methode en het Task returntype. In deze functie wordt een integere geretouneerd deze staat tussen de haakjes achter de task. Ook het await keyword valt op in deze code. Deze is in de beschrijving al beschreven. Een functie kan ook niks retouneren. Dan kun je de integer en de return weglaten.

### Alternatieven & adviezen
In de nieuwe versies van c# en .NET gaat bij de meeste mensen de voorkeur uit naar async. Async is een simpele oplossing in tegenstelling tot oudere technieken. Het is relatief makkelijk om async en await te gebruiken in C#. Threading is vaak ingewikkelder. Threading is ook moeilijker te onderhouden en te debuggen. Het kost ook veel meer code om hetzelfde te bereiken. Async is veel moderner en makkelijker in gebruik. Daarom is het beter om asynchronous programming te gebruiken. 

### Authentieke en gezaghebbende bronnen
- Windows docs over threading: https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread?view=netframework-4.8
- Windows docs over scheduling priority: https://docs.microsoft.com/en-us/windows/win32/procthread/scheduling-priorities
- Windows forms over async programming: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/task-asynchronous-programming-model
- Windows forms over async en await: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/

