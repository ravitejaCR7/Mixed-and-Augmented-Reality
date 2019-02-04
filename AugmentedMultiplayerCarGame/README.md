AugmentedMultiplayerCarGame
===========================

In this project, the user will be able to play a car racing game with other users by connecting over the internet. 

Things used:
* I've used ARCore and integrated with Unity in order to detect the ground plane and then the user can place a car over the detected ground plane.
* Joystick to control the car prefab.
* Unity Network Manager and Network Manager HUD to connect the players and coordinate their movements over the network.

How to use:
1) Initially, point the phone camera towards the ground and move the camera for 3-5 seconds so that ARCore will detect the ground plane.
2) Once the ground plane gets detected, touch anywhere on the detected ground plane. You will see a car prefab being instantiated. This is done by using RayCast. The user can also move the car using the joystick.
3) After finishing the 2nd step. Now the user has to select the network options present over the top left corner. The user has to select the "Enable MatchMaking" option. This option is used to connect the user to the unity matchmaking server.
Here, one player has to host the match and other players can connect to this match which is created by the host. This one who is creating the match will act as both the client and the server and other players will act only like a client.
4) The one who is acting as a server needs to click on the option "create internet match". This will initialize the game over the network.
5) Now the clients need to connect to this game using the option "find internet match" -> "join default game".
6) After all the players connected to the default server, the users can now see other player's cars in their mobile phone.

Extensions:
This project can further be improved such that the users can feel more realistic about playing in the multiplayer mode.
