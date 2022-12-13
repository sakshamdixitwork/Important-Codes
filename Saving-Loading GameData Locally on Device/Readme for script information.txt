The game data class is set static to be accessible everywhere in the project and is not geeting from monobehaviour,
and the gamedatasaverscript access the game data and saves the data in the serializable classes in form of class objects and 
is then saved locally through application.persistentdatapath for android, ios, windows, mac, webgl (all platforms).