# Présentation
Projet réalisé dans le cadre de l'UE Environnements virtuels hautement interactifs à Sorbonne Université.  
NoWayHome est un jeu 2D visant à plaire aux joueurs killer et socializer selon la typologie de Bartle.  
Inspiré par [Modeling Player Experience for Content Creation](https://www.researchgate.net/publication/224118298_Modeling_Player_Experience_for_Content_Creation), le jeu tente
également de prédire l'émotion du joueur afin d'adapter l'environnement automatiquement.

Le jeu devait donc initialement contenir un scénario pour les killers un scénario pour les socializers. Le joueur devait passer une phase de calibration où il est lâché dans
un environnement inconnu où divers actions sont possibles comme par exemple combattre des ennemis, dialoguer avec des PNJ, ramasser des objets etc...  
A la fin de la calibration, le jeu regarde les actions réalisées et dirige le joueur vers le scénario le plus adapté.  
Par manque de temps, seul le scénario killer a été réalisé. Il n'y a donc qu'un scénario possible après la calibration.

Tout au long du scénario et ce depuis la calibration, on trace chez le joueur :
* un compteur de tirs
* un compteur de coup de couteau
* un compteur de morts
* un compteur de frames à rester immobile
* un compteur de frames à sprinter
* un compteur d’interaction (ouvrir un coffre, parler à un pnj…)
* un compteur de loot
* un compteur de dialogue
* un compteur d’ouverture d’inventaire
* un compteur de saut de dialogue

Ces compteurs (appellés features dans l'article) sont ensuite périodiquement envoyés à un serveur qui nous retourne l'émotion la plus probable que ressent le joueur parmi fun, 
frustration et ennui. Cette prédiction est faite grâce à un arbre de décision. Le code de cette fonctionnalité est disponible à https://github.com/ohouens/no_way_home

L'adaptation est pensée comme ceci :
1. Si on pense que le joueur ressent du fun, on ne fait rien
2. Si on pense que le joueur ressent de la frustration :
    * dégâts des ennemis réduit de 1
    * puissance des soins augmentés de 1
    * bonus de dégâts pour le joueur augmenté de 1
3. Si on pense que le joueur ressent de l'ennui :
    * afficher les dialogues raccourcis plutôt que les dialogues complets
    * nombre d’ennemis par zone augmenté de 3
    * puissance des soins diminué de 1
    * points de vie des ennemis augmentés de 1

En jeu, on peut retrouver des PNJ rouges représentant les ennemis, des PNJ beiges représentant des habitants dont le seul comportement est de se déplacer aléatoirement
et des PNJ bleus représentant les marchands.  
Un système de commerce est commencé mais non fini, cependant l'essentiel est déjà fait : dialogues avec choix, objets, argent, inventaire...  
Un scénario socializer serait d'ailleurs facilement faisable avec tous les éléments déjà présents.

Il existe pour l'instant 2 types d'armes, le pistolet et le couteau. Au début du jeu, le joueur n'a aucune arme et doit en trouver dans des coffres. Une fois trouvé, il faut
passer par l'inventaire et l'équiper. Le joueur pourra alors attaquer en faisant un clic gauche. Le personnage attaque vers la direction de la souris. Pour attaquer avec le pistolet, des munitions sont nécessaires. Les ennemis générés automatiquement ont tout comme le joueur soit un couteau soit un pistolet.

Le jeu sauvegarde périodiquement des données comme la position du joueur dans un fichier binaire à [Utilisateur]/AppData/LocalLow/DefaultCompany/NoWayHome/saveSlot

Le code tire grandement avantage des Scriptable Objects et de signaux. Les classes communiquent donc entre elles sans dépendre les unes des autres. On peut donc facilement rajouter et retirer des classes sans tout casser. Par exemple quand le joueur démarre un dialogue avec un PNJ, le PNJ ne sait pas que c'est le joueur qui a initié le dialogue et la classe Joueur ne sait même pas qu'il est dans un dialogue, il est juste privé de mouvement le temps que le texte s'affiche à l'écran. Cela permettrait donc de faire des choses comme 2 PNJ qui dialoguent.

Pistes d'amélioration :
* Améliorer l’accuracy de l’arbre de décision
* Ajouter un autre dispositif de trace (un téléphone physique avait été l'idée de départ)


# Commandes
ZQSD pour se déplacer  
I pour ouvrir l'inventaire  
Clic gauche pour attaquer  
Espace pour interagir avec un coffre/PNJ/avancer dans un dialogue  
Echap en plein dialogue pour sauter le dialogue  
X à tout moment pour fermer le jeu

# Informations utiles
Provenance des sprites : https://opengameart.org/content/zelda-like-tilesets-and-sprites  
Version d'Unity : 2020.3.19f1  
Path des sauvegardes : [Utilisateur]/AppData/LocalLow/DefaultCompany/NoWayHome/saveSlot  
Packages utilisés : [Cinemachine](https://unity.com/fr/unity/features/editor/art-and-design/cinemachine), [SavingSystem](https://github.com/GameDevExperiments/SavingSystem)
