#define MyAppName "Fusion Client"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Fusion GmbH"
#define MyAppExeName "Fusion Client.exe"

[Setup]
AppId={{D15A9D2A-21D3-4B85-9A77-1E3C1B77F210}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\Fusion Client
DefaultGroupName=Fusion Client
UninstallDisplayIcon={app}\{#MyAppExeName}
OutputDir=release
OutputBaseFilename=FusionClient-Setup
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=lowest
ArchitecturesInstallIn64BitMode=x64

[Files]
Source: "publish\Fusion Client.exe"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\Fusion Client"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\Fusion Client"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Desktop-Verknüpfung erstellen"; GroupDescription: "Zusätzliche Symbole:"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Fusion Client starten"; Flags: postinstall nowait skipifsilent
