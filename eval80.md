# Évaluation intermédiaire (80 %) – Noé

## Synthèse

Côté jeu, l'avancement est bon : 22 ennemis (11 démons, 11 squelettes) qui descendent et tirent, 6 défenses destructibles, des tirs avec cooldown, un score et des vies. **Mais la défaite bloque l'application** (boucle infinie), il n'y a pas de victoire, et le code repose massivement sur les **exceptions comme mécanisme de contrôle**. Côté documentation, **le rapport et le journal de travail ont été supprimés du dépôt le 04.09** (« je n'avais pas compris ce que l'on devait mettre ») et n'ont jamais été remis. Le dossier `Doc` ne contient que le cahier des charges.

## Points à améliorer d'ici au rendu final (30.10)

1. **Remettre un rapport et un journal de travail dans `doc/`** (exigence du CdC et du handbook).
2. Remplacer les exceptions par des booléens ou des propriétés (`IsOutOfScreen`, `IsDestroyed`) et supprimer avec une boucle à l'envers.
3. Écran de fin (victoire et défaite) sans boucle bloquante : arrêter le timer et afficher un message.
4. DRY ! Comparez vos classes `Demon` et `Skeleton` ainsi que `Demonatk` et `Skeletonatk`. Trouvez une manière de fusionner ces paires en une seule classe pour éliminer les répétitions.
5. Renommer `Drone` en `Player`, plus généralement tout ce qui reste du code de base, et appliquer les conventions C#.
6. Ajouter des tests unitaires
