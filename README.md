# Dokumentation der PasswortHashing-Bibliothek

---

## Table of Content:

- [Übersicht]
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
    - [Hash-Unterstützung prüfen]
- [Sicherheitsüberlegungen]

---

## Übersicht

Der Namespace *PasswortHashing* bietet Funktionen zum sicheren Hashing und Überprüfen von Passwörtern unter 
Verwendung von **PBKDF2** (Password-Based Key Derivation Function 2). Dies stellt sicher, dass Passwörter sicher 
gespeichert werden, was es Angreifern erschwert, die ursprünglichen Passwörter selbst dann zu ermitteln, 
wenn sie Zugriff auf die gehashten Passwortdaten erhalten.

---

## Konstanten
- SaltSize: Größe des Salt in Bytes (16 Bytes).
- HashSize: Größe des Hash in Bytes (20 Bytes).
- HashPrefix: Präfix, das im formatierten Hash-String verwendet wird *("$FAMDTL$V1$")*.

---

## Eigenschaften
- SaltSize1: Öffentlicher Getter für die Konstante SaltSize.
- HashSize1: Öffentlicher Getter für die Konstante HashSize.

---

## Methoden

### Hash(string password, int iterations)

Erstellt einen Hash aus dem gegebenen Passwort mit einer angegebenen Anzahl von Iterationen.

**Parameter:**
- password (string): Das zu hashende Passwort.
- iterations (int): Die Anzahl der Iterationen für den PBKDF2-Algorithmus.

**Rückgabewert:**
- string: Der formatierte Hash, der das Salt und die Iterationen enthält.

**Beispiel:**
```cs
    string gehashtesPasswort = PasswortHasher.Hash("meinPasswort", 15000);
```

---

### Hash(string password)

Erstellt einen Hash aus dem gegebenen Passwort unter Verwendung von standardmäßig *10.000* Iterationen.

**Parameter:**
- password (string): Das zu hashende Passwort.

**Rückgabewert:**
- string: Der formatierte Hash, der das Salt und die Iterationen enthält.

**Beispiel:**
```cs
    string gehashtesPasswort = PasswortHasher.Hash("meinPasswort");
```

---

### IsHashSupported(string hashString)

Überprüft, ob der gegebene Hash von dieser Implementierung unterstützt wird.

**Parameter:**
- hashString (string): Der zu überprüfende Hash.

**Rückgabewert:**
- bool: true, wenn der Hash unterstützt wird; andernfalls false.

**Beispiel:**
```cs
    bool wirdUnterstützt = PasswortHasher.IsHashSupported(gehashtesPasswort);
```

---

### Verify(string password, string hashedPassword)

Überprüft das gegebene Passwort gegen das gehashte Passwort.

**Parameter:**
- password (string): Das zu überprüfende Passwort.
- hashedPassword (string): Das zu überprüfende gehashte Passwort.

**Rückgabewert:**
- bool: true, wenn das Passwort mit dem Hash übereinstimmt; andernfalls false.

**Ausnahmen:**
- *NotSupportedException:* Wird ausgelöst, wenn der Hash-Typ nicht unterstützt wird.

**Beispiel:**
```cs
    bool istGültig = PasswortHasher.Verify("meinPasswort", gehashtesPasswort);
    if (istGültig)
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

Um ein Passwort mit der standardmäßigen Anzahl von Iterationen (10.000) zu hashen:
```cs
    string gehashtesPasswort = PasswortHasher.Hash("meinPasswort");
```

---

### Ein Passwort verifizieren

Um ein Passwort gegen ein gehashtes Passwort zu verifizieren:
```cs
    bool istGültig = PasswortHasher.Verify("meinPasswort", gehashtesPasswort);
    if (istGültig)
    {
        // Aktion, falls Hash gültig ist
    }
    else
    {
        // Aktion, falls Hash ungültig ist
    }
```

---

### Hash-Unterstützung prüfen

Um zu überprüfen, ob ein Hash unterstützt wird:
```cs
    bool wirdUnterstützt = PasswortHasher.IsHashSupported(gehashtesPasswort);
    if (wirdUnterstützt)
    {
        // Falls Hash unterstützt wird
    }
    else
    {
        // Falls Hash nicht unterstützt wird
    }
```

Dies wird beim verifizieren automatisch gemacht und muss nicht manuell abgefragt werden.

## Sicherheitsüberlegungen
- Die Anzahl der Iterationen sollte hoch angesetzt sein. Mit 10.000 Iterationen als Standardwert 
liegt man schon recht gut, abhängig der Rechenleistung des Backends sollte diese Zahl aber hochgesetzt werden. 
Dies erschwert Brute-Force Angriffe.
- Das Salt sollte für jeden Hash einzigartig sein, um Rainbow-Table Angriffe zu erschweren.
- Gehashte Passwörter sollten selbstverständlich sicher in der Datenbank gespeichert werden und der 
Hashprozess sollte im Backend laufen, um den Code nicht öffentlich zugänglich zu machen