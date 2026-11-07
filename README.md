# Fusion Client 1.0 – C# / Windows

Dies ist die native C#/.NET-Neufassung. Der Spieler braucht **weder Python noch das .NET SDK**.
Das Release wird als selbstenthaltende Windows-x64-App veröffentlicht und anschließend mit
Inno Setup in `FusionClient-Setup.exe` verpackt.

## Auf deinem Entwicklungs-PC
Installiere:
- .NET 8 SDK (nur zum Bauen)
- Inno Setup 6 (zum Erstellen des Setup-Programms)

Dann `build_release.bat` starten. Ergebnis:
- `publish\Fusion Client.exe` – eigenständige App, enthält .NET-Laufzeit
- `release\FusionClient-Setup.exe` – der Installer für Spieler (wenn ISCC.exe im PATH ist;
  alternativ `Installer\FusionClient.iss` mit Inno Setup Compiler öffnen und kompilieren)

## Spieler
Spieler laden nur `FusionClient-Setup.exe` herunter und führen sie aus. Kein Python,
kein .NET SDK und keine separate .NET-Laufzeit erforderlich. Minecraft benötigt dennoch
eine passende Java-Laufzeit; diese erste Version installiert Java noch nicht automatisch.

## Status und wichtige Einschränkungen
- WPF-Oberfläche und Installer-Skript sind enthalten.
- Die Integration verwendet CmlLib.Core und dessen Microsoft-Login-Flow.
- Paketversionen und API-Kompatibilität müssen beim ersten Windows-Build gegen NuGet
  geprüft werden; falls NuGet eine Version oder API-Änderung meldet, ist die Projektdatei
  bzw. der Auth-Aufruf anzupassen.
- Die App wurde in dieser Umgebung nicht kompiliert oder auf Windows getestet.
- Noch nicht enthalten: automatischer Java-Runtime-Download, Fabric/Forge/NeoForge,
  Auto-Updater, Code-Signatur, Branding-Icon, Crash-Reporter.
- Vor Veröffentlichung vollständige Login-/Installations-/Starttests durchführen.

## Veröffentlichung
Verteile nur die fertige `release\FusionClient-Setup.exe`. Signiere den Installer mit
einem gültigen Code-Signing-Zertifikat, ergänze Datenschutz/Impressum/Support und
veröffentliche Prüfsummen/Versionshinweise. Fusion Client ist unabhängig und nicht
mit Mojang/Microsoft verbunden. Minecraft-Marken und Spielinhalte bleiben bei ihren Inhabern.
