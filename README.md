# Dokumentation der PasswortHashing-Bibliothek

---

## Table of Content:

- [�bersicht]
- [Konstanten]
- [Eigenschaften]
- [Methoden]
    - [Hash(string password, int iterations)]
    - [Hash(string password)]
    - [IsHashSupported(string hashString)]
    - [Verify(string password, string hashedPassword)]
- [Nutzung]
    - [Ein Passwort hashen]
    - [Ein Passwort verifizieren]
    - [Hash-Unterst�tzung pr�fen]
- [Sicherheits�berlegungen]

---

## �bersicht

Der Namespace *PasswortHashing* bietet Funktionen zum sicheren Hashing und �berpr�fen von Passw�rtern unter 
Verwendung von **PBKDF2** (Password-Based Key Derivation Function 2). Dies stellt sicher, dass Passw�rter sicher 
gespeichert werden, was es Angreifern erschwert, die urspr�nglichen Passw�rter selbst dann zu ermitteln, 
wenn sie Zugriff auf die gehashten Passwortdaten erhalten.

---

## Konstanten
- SaltSize: Gr��e des Salt in Bytes (16 Bytes).
- HashSize: Gr��e des Hash in Bytes (20 Bytes).
- HashPrefix: Pr�fix, das im formatierten Hash-String verwendet wird *("$FAMDTL$V1$")*.

---

## Eigenschaften
- SaltSize1: �ffentlicher Getter f�r die Konstante SaltSize.
- HashSize1: �ffentlicher Getter f�r die Konstante HashSize.

---

## Methoden

### Hash(string password, int iterations)

Erstellt einen Hash aus dem gegebenen Passwort mit einer angegebenen Anzahl von Iterationen.

**Parameter:**
- password (string): Das zu hashende Passwort.
- iterations (int): Die Anzahl der Iterationen f�r den PBKDF2-Algorithmus.

**R�ckgabewert:**
- string: Der formatierte Hash, der das Salt und die Iterationen enth�lt.

**Beispiel:**
```cs
    string gehashtesPasswort = PasswortHasher.Hash("meinPasswort", 15000);
```

---

### Hash(string password)

Erstellt einen Hash aus dem gegebenen Passwort unter Verwendung von standardm��ig *10.000* Iterationen.

**Parameter:**
- password (string): Das zu hashende Passwort.

**R�ckgabewert:**
- string: Der formatierte Hash, der das Salt und die Iterationen enth�lt.

**Beispiel:**
```cs
    string gehashtesPasswort = PasswortHasher.Hash("meinPasswort");
```

---

### IsHashSupported(string hashString)

�berpr�ft, ob der gegebene Hash von dieser Implementierung unterst�tzt wird.

**Parameter:**
- hashString (string): Der zu �berpr�fende Hash.

**R�ckgabewert:**
- bool: true, wenn der Hash unterst�tzt wird; andernfalls false.

**Beispiel:**
```cs
    bool wirdUnterst�tzt = PasswortHasher.IsHashSupported(gehashtesPasswort);
```

---

### Verify(string password, string hashedPassword)

�berpr�ft das gegebene Passwort gegen das gehashte Passwort.

**Parameter:**
- password (string): Das zu �berpr�fende Passwort.
- hashedPassword (string): Das zu �berpr�fende gehashte Passwort.

**R�ckgabewert:**
- bool: true, wenn das Passwort mit dem Hash �bereinstimmt; andernfalls false.

**Ausnahmen:**
- *NotSupportedException:* Wird ausgel�st, wenn der Hash-Typ nicht unterst�tzt wird.

**Beispiel:**
```cs
    bool istG�ltig = PasswortHasher.Verify("meinPasswort", gehashtesPasswort);
    if (istG�ltig)
    {
        Console.WriteLine("Passwort ist korrekt.");
    }
    else
    {
        Console.WriteLine("Passwort ist inkorrekt.");
    }
```

--- 

## Nutzung

### Ein Passwort hashen

Um ein Passwort mit einer benutzerdefinierten Anzahl von Iterationen zu hashen:

```cs
    string gehashtesPasswort = PasswortHasher.Hash("meinPasswort", 15000);
```

Um ein Passwort mit der standardm��igen Anzahl von Iterationen (10.000) zu hashen:
```cs
    string gehashtesPasswort = PasswortHasher.Hash("meinPasswort");
```

---

### Ein Passwort verifizieren

Um ein Passwort gegen ein gehashtes Passwort zu verifizieren:
```cs
    bool istG�ltig = PasswortHasher.Verify("meinPasswort", gehashtesPasswort);
    if (istG�ltig)
    {
        // Aktion, falls Hash g�ltig ist
    }
    else
    {
        // Aktion, falls Hash ung�ltig ist
    }
```

---

### Hash-Unterst�tzung pr�fen

Um zu �berpr�fen, ob ein Hash unterst�tzt wird:
```cs
    bool wirdUnterst�tzt = PasswortHasher.IsHashSupported(gehashtesPasswort);
    if (wirdUnterst�tzt)
    {
        // Falls Hash unterst�tzt wird
    }
    else
    {
        // Falls Hash nicht unterst�tzt wird
    }
```

Dies wird beim verifizieren automatisch gemacht und muss nicht manuell abgefragt werden.

## Sicherheits�berlegungen
- Die Anzahl der Iterationen sollte hoch angesetzt sein. Mit 10.000 Iterationen als Standardwert 
liegt man schon recht gut, abh�ngig der Rechenleistung des Backends sollte diese Zahl aber hochgesetzt werden. 
Dies erschwert Brute-Force Angriffe.
- Das Salt sollte f�r jeden Hash einzigartig sein, um Rainbow-Table Angriffe zu erschweren.
- Gehashte Passw�rter sollten selbstverst�ndlich sicher in der Datenbank gespeichert werden und der 
Hashprozess sollte im Backend laufen, um den Code nicht �ffentlich zug�nglich zu machen