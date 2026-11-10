# Évaluation finale (100 %) – Noé Béran

## Synthèse

Les deux problèmes majeurs de l'éval 80 % sont résolus.

Surtout: les exceptions ne servent plus au contrôle de flux

** : des listes « à supprimer » sont traitées dans `removeobj()`. La défaite ne bloque plus l'application. Un écran d'accueil et une esquive ont été ajoutés, et une interface `IMonster` commence à factoriser démons et squelettes. La suite de tests est la plus fournie de la classe (32 tests). Il manque la **victoire**, les conventions de nommage C# sont **largement ignorées\*\*, et il reste des traces `AIRSPACE`. La release « rendu » date du 31.10.

## Suivi des points de l'évaluation à 80 %

| #   | Point demandé                                        | Statut | Commentaire                                                                                                                                                                                                                                                                                                                                                 |
| --- | ---------------------------------------------------- | ------ | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 1   | Remettre rapport et journal                          | ✅     | Les deux sont livrés et complets.                                                                                                                                                                                                                                                                                                                           |
| 2   | Remplacer les exceptions et supprimer à l'envers     | ✅     | Il n'y a plus de `try/catch`. La suppression passe par des listes tampon, ce qui est une bonne solution.                                                                                                                                                                                                                                                    |
| 4   | DRY `Demon` / `Skeleton`, `Demonatk` / `Skeletonatk` | ⚠️     | L'interface `IMonster` est un bon début. Mais les deux classes restent quasi identiques, et `demonatk` et `skeletonatk` ne sont pas fusionnées. La dette est identifiée dans le rapport et par un `// TODO`.                                                                                                                                                |
| 5   | Renommer `Drone` et appliquer les conventions C#     | ⚠️     | `Player` est fait. Restent `Config.AIRSPACEWIDTH` et `AIRSPACEHEIGHT` ([Config.cs:5](code/projet/Helpers/Config.cs#L5)), `AirSpace_KeyDown` et les commentaires « drone » et « espace aérien ». Les classes sont en minuscules (`skeleton`, `demon`, `bone_pile`, `demonatk`, `player`), ainsi que les méthodes (`update`, `shoot`, `checkhit`, `newwave`). |

## Évaluation par critère

| Critère                           | Niveau          | Justification                                                                                                                                                                                                                                                                                                                                                                                        |
| --------------------------------- | --------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Rythme de travail                 | **Acquis**      | Les points de base sont réalisés (22 ennemis, tirs, 6 défenses, score, vies, défaite), avec des ajouts (accueil, esquive). La **victoire** (US 9) n'est pas faite.                                                                                                                                                                                                                                   |
| Conscience professionnelle        | **Insuffisant** | Les conventions C# ne sont pas appliquées (casse des classes et des méthodes). L'état est en `public static` (`right`, `left`, `up`, `space`, `score`… [GameSpace.cs:26](code/projet/View/GameSpace.cs#L26)). Les fautes de frappe persistent dans les identifiants (`cooldawn`, `wavecooldawn`) et le fichier est en Latin-1. La dette technique est en revanche bien identifiée (TODO et rapport). |
| Connaissances professionnelles    | **Acquis**      | Progrès nets : interface, suppression différée, `OfType<T>()`. La duplication démon/squelette reste importante.                                                                                                                                                                                                                                                                                      |
| Processus de travail              | **Acquis**      | Le rapport est complet, avec une comparaison planification/réalité et un exemple IA reconstitué. Le journal Excel est régulier mais ne référence pas les commits. Livraison le 31.10.                                                                                                                                                                                                                |
| Expression orale et écrite        | —               | Évaluée lors de la livraison du 04.09, non réévaluée.                                                                                                                                                                                                                                                                                                                                                |
| Approche écologique et économique | **Acquis**      | Dépôt léger (4,9 Mo), sans binaires.                                                                                                                                                                                                                                                                                                                                                                 |
| Travail en équipe                 | _À compléter_   | Observation en classe.                                                                                                                                                                                                                                                                                                                                                                               |
| Autonomie                         | _À compléter_   | Observation en classe.                                                                                                                                                                                                                                                                                                                                                                               |

## Points forts

- Les retours critiques de l'éval 80 % ont été réellement pris en compte.
- 32 tests unitaires couvrent presque toutes les classes.
- Le bilan est lucide sur la planification trop optimiste.

## À retenir pour les prochains projets

1. Conventions C# : PascalCase pour les classes, méthodes et propriétés, camelCase pour les variables locales.
2. Aller au bout du DRY : une classe `Monster` paramétrée (PV, image, vitesse, points) remplace `demon` et `skeleton`.
3. Prévoir la victoire en même temps que la défaite : ce sont les deux faces d'une même US.
