# G5v2-HowToInfocrazy

ReadMe als PDF für schönere Formatierung: https://htlsalzburg.sharepoint.com/:b:/s/MTIN_HowToInfokratie/IQCMjk9nSglpRpU2fNtGxHwhAYOuY9JrAfH_Ra8F0jEFA-w?e=5Pqac0
Playthrough Video besser aufgelöst: https://htlsalzburg.sharepoint.com/:v:/s/MTIN_HowToInfokratie/IQDhWJXAQIvmRImV6jcw-L5fAQcz8QyUGHRErSN27t4AA3c?e=e7Q7T2

Read Me
Fuchs Paul, Ziegler Jakob, Aichhorn Tom

Arbeitsaufteilung:
Tom:
1) Vorher: Es sollen nur die ersten Antworten zählen
2) Verlustbedingung: „Wenn ein Wert 0 erreicht, bist du verloren“
Wenn einer der zentralen Werte (z. B. Bevölkerungszufriedenheit, Budget, Funktionalität, Opposition) auf 0 fällt, endet das Spiel sofort mit einer Niederlage. 
Umsetzung: Permanente Prüfung der Metriken nach jeder Entscheidung; bei <= 0 GameOver auslösen und Endscreen zeigen. 
3) IN PROGRESS: Sieg & Reflektion: Endbildschirm über die eigenen infokratischen Entscheidungen
Beim Spielende (Sieg) bekommt der/die Spieler:in einen reflektierenden Endscreen: 
Zusammenfassung aller wichtigen Entscheidungen, die man selbst getroffen hat und die zeigen, dass wir bereits anfangen im Stil der Infokratie zu denken 
Umsetzung: Die jeweiligen Antworten abspeichern und die, die Antworten einer Infokratie entsprechen, am Ende auswerten und als Grafiken/Text darstellen. 
4) Leak-Event im Internet: Schnellentscheidung (1–10) innerhalb 10 Sekunden
Ein Leak-Event wird zufällig ausgelöst. Etwas über dich oder deine Regierung wurde geleaked. Je schneller und besser du reagierst, desto weniger bekommen etwas davon mit und desto besser wirst du dastehen. Das Leak hat eine Gewichtung von 1–10 und der Spieler muss innerhalb von 10 Sekunden reagieren (z. B. Optionen: „Leugnen“, „Ablenken“, „Unterdrücken“, „Transparenz“). Nur die infokratische Reaktion hat den gewünschten Effekt. Auswirkungen sind viel stärker, bei Wertveränderung.  
Umsetzung: Event-Coroutine erzeugt das Leak-Problem (1–10), zeigt Countdown-UI (10s). Bei Timeout gilt: keine Aktion → stärkster Negativeffekt.  

Paul:
5) Fixen des Scores und hinzufügen von 2 neuen Werten.
Bestehende Werte
STAATSKASSE) Dieser beschreibt wie viel geld der spieler noch hat
HAPPIENESS) Dieser zeigt dem Spieler wie glücklich die Bevölkerung ist
Neue Werte
OPPOSITION) Opposition beschreibt wie populär die Opposition ist, und wie viel politischen Druck dein Charakter aushalten muss. Functionalität) zeigt wie gut dein Land Funktioniert. Infrastruktur, Gesundheitssystem, Kriminalitätsrate etc.
Dises Werte werden mit jeder Frage und Leakevent bearbeitet und verändert(je nachdem wie sich der Spieler entscheidet). 

A:
Unser Projekt versucht auf spielerischer Weise auf reale Probleme aufmerksam zu machen. Als Inspiration für unsere interaktive Anwendung dient zu einem großen Teil das Buch „Infokratie“ von Byung-Chul Han. In dem Buch beschreibt er wie man mit der Kontrolle von Informationen die Kontrolle über die Menschen gewinnt und somit den Willen der Menschen beeinflussen und missbrauchen kann. Wir greifen diese Thematik auf und wollen sie mit einem interaktiven Medium einem breiteren Publikum näherbringen.
In unserem Spiel ist der Spieler der fiktive Herrscher über ein Land. Im Spielverlauf wird der Spieler immer wieder mit Problemen in seinem Staat konfrontiert. Verschiedene Lösungsmöglichkeiten stehen ihm zur Auswahl, um seine Macht und den Staat zu sichern. Die beste Möglichkeit zur Sicherung der Macht ist die, die den Spieler immer weiter zu einer infokratischen Führungsperson macht

B:
1)	Interaktion mit dem Projekt:
•	Startscene: 2 Optionen: 1. Starte das Spiel und 2. Starte das Tutorial
    o	Spielstart: Du kommst in die Office Szene. Hier kannst du dich frei in deinem Büro als Präsident bewegen. Rechts oben siehst du deine Regierungswerte: Grün ist der Wert der Bevölkerungszufriedenheit, Braun ist dein Staatsbudget, Lila ist der Wert der Opposition (also wie sehr die Bevölkerung zur Opposition tendiert) und Blau der Wert, wie gut deine Antworten funktionieren (Funktionalität). Bei dem Klick auf “Start” kommt dein/e Sekretär/in mit einem Problem in dein Büro. Mit einem Klick auf „Answer” kommen deine 4 Berater mit jeweils einem Lösungsvorschlag hinein. Aber: Die Lösungsvorschläge unterscheiden sich in dem, wie sehr sie die Bevölkerung zufriedenstellen, wie viel Geld sie kosten, wie sehr die Opposition dadurch an Aufschwung und Abschwung erhält und wie gut dieser Vorschlag funktioniert. Du musst dich als Präsident für einen dieser Vorschläge entscheiden (durch Klicken auf die Antwort). Danach siehst du, ob du dich richtig oder falsch entschieden hast. Je nachdem verändern sich deine Werte rechts oben und bei der richtigen Antwort bekommst du ein Video gezeigt, warum es die richtige Antwort ist bzw. warum diese Antwort als infokratisch gilt, der Sinn: Es soll gezeigt werden, wie sehr wir bereits in der Infokratie leben. Beim Wählen der falschen Antwort kommst du so oft zur gleichen Frage, bis du die richtige Antwort wählst. Die Werte verändern sich aber nur bei der ersten Antwort. Nachdem du die Richtige gefunden hast, kommst du wieder in dein Büro zurück. Während du im Büro bist, kann es passieren, dass Informationen über dich geleakt werden. Da poppt plötzlich ein Fenster auf und je nach Stufe (1-3) ist die Gewichtung des Leaks unterschiedlich stark, also es hat unterschiedlich starke Auswirkungen und du hast unterschiedlich lange Zeit. Du musst dich schnell für die richtige Antwort entscheiden, je nachdem, was du wählst, hat es unterschiedliche Auswirkungen.
    o	Tutorial: Dir wird in 4 Abschnitten erklärt, wie das Spiel funktioniert, was die Werte bedeuten, und es werden Tipps zum Spiel gegeben. Im Office werden beispielsweise die Steuerung des Charakters, die verschiedenen Werte und das Ziel erläutert. Sobald man genügend Informationen erhalten hat, kann man das Spiel starten.

2)  Inputs für das Game 
Die Interaktion mit dem Spiel erfolgt über Maus und Tastatur. 

Maus 
Navigation durch Menüs und UI-Elemente 
Auswahl von Dialogoptionen und Entscheidungsmöglichkeiten 
Interaktion mit Buttons (z. B. „Start“, „Answer“, Auswahl der Berater) 
Bedienung des Videoplayers bei erklärenden Videosequenzen 

Tastatur 
W / A / S / D – Bewegung des Charakters im Büro (vorwärts, links, rückwärts, rechts) 
Q / E – Wechseln zwischen Szenen im Tutorial 
Alle spielrelevanten Entscheidungen werden bewusst über einfache Eingabemethoden getroffen, um die Aufmerksamkeit des Spielers auf die Inhalte, 
Entscheidungen und deren Konsequenzen zu lenken und nicht auf komplexe Steuerungsmechaniken. 

3)	Als Nutzer/in befindest du dich als Präsident/in in deinem Büro, kannst durch Knopfdruck deine/n Sekretär/in hineinlassen, die hat immer eine gewisse Problemstellung, die es zu lösen gilt, aber als Präsident musst du natürlich nicht alles selbst entscheiden, du kannst also danach Assistenten hineinlassen, mit Lösungsvorschlägen. Danach befindest du dich wieder im Büro. Dort kann es zufällig passieren, dass ein Leak Event passiert, also etwas über deine Regierung wurde geleakt und du musst in kurzer Zeit je nach Intensität bzw. Härte/Level des Leaks das Problem schnell lösen. 

Deine hauptsächlichen Tätigkeiten im Spiel sind also das Problemlösen, das Bekennen und Leben mit den Auswirkungen deiner Entscheidungen und am Ende das Reflektieren über deine Entscheidungen.


C: Screenshots aus dem Spiel
<img width="2837" height="1603" alt="Bildschirmfoto 2026-01-18 um 15 19 24" src="https://github.com/user-attachments/assets/93460c3a-4b4e-46aa-8b5c-7a42685f2afe" />
<img width="2848" height="1607" alt="Bildschirmfoto 2026-01-18 um 15 20 11" src="https://github.com/user-attachments/assets/1b6c3525-48e4-419b-b3b9-1916d54db6f4" />
<img width="2849" height="1596" alt="Bildschirmfoto 2026-01-18 um 15 20 02" src="https://github.com/user-attachments/assets/d592d06b-a1f7-438a-9890-835e17941fed" />
<img width="2849" height="1603" alt="Bildschirmfoto 2026-01-18 um 15 19 59" src="https://github.com/user-attachments/assets/c1287aee-1ef0-4b21-bfa7-c7d61db8a0d7" />
<img width="2933" height="1602" alt="Bildschirmfoto 2026-01-18 um 15 19 35" src="https://github.com/user-attachments/assets/f1c69a97-2ed8-4d5f-9f4a-4a2b8bb8742d" />
<img width="2849" height="1602" alt="Bildschirmfoto 2026-01-18 um 15 19 32" src="https://github.com/user-attachments/assets/f90c7cad-62a3-4a6a-9089-2baf1b1d5376" />
<img width="2853" height="1607" alt="Bildschirmfoto 2026-01-18 um 15 20 25" src="https://github.com/user-attachments/assets/97427c0e-e335-4899-a77a-81b431b66a5c" />
<img width="2853" height="1606" alt="Bildschirmfoto 2026-01-18 um 15 20 22" src="https://github.com/user-attachments/assets/92ab76ac-dc22-4752-8cb2-f631a0becdf1" />
<img width="2845" height="1601" alt="Bildschirmfoto 2026-01-18 um 15 20 14" src="https://github.com/user-attachments/assets/4171483e-f4d9-4918-b544-d57296969789" />
<img width="2850" height="1468" alt="Bildschirmfoto 2026-01-18 um 15 53 50" src="https://github.com/user-attachments/assets/20215a63-bece-472f-b5d6-c32107524829" />
<img width="2852" height="1606" alt="Bildschirmfoto 2026-01-18 um 15 52 59" src="https://github.com/user-attachments/assets/c67d941a-e012-4882-b74b-44489cdc021b" />
<img width="2843" height="1601" alt="Bildschirmfoto 2026-01-18 um 15 19 27" src="https://github.com/user-attachments/assets/485cf1c3-05fb-443b-ac5b-b2a7edbc0ec7" />


Playthrough Video:
https://github.com/user-attachments/assets/51d89087-2dcf-43e5-86e7-e6402eeeb8f5

D:
Entwicklungsplattform:
    •	Unity Version: 6000.0.57f
    •	Programmiersprache: C#
    •	Betriebssystem (z. B. Windows 11, macOS Sequoia 15.6.1)

E:
Zielplattformen:
    •	Windows Standalone
    •	WebGL
