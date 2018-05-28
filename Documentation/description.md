#Aihemäärittely

**Project:** This project was very challenging indeed. It consisted of four major parts. 

First thing was construct a rag-doll-type four-legged creature with Unity joints. But it had to be steerable with motors so no ordinary ragdoll could be used but I had to construct it with hinge-joints. 

The next thing to do was to implement the motor-system which could be given just an array of real-numbers and it would use them as impulses to the motors. However this proved to be very challenging and I quickly noticed that giving straight motor-speed and force orders couldn't do the task. That was because they were impossible to force to be within limits. I wanted that the machine could take input orders from -1 to 1 and react so that it would never try to exceed the joint limits. I finally found a way to interpret the input so that it was not used as force or speed, but a relative position of the joint. So number 1 would be the order to bend the joint all the way up but not further.

The third phase was to implement an easily extendable neural network which would have many functions for mutating and breeding. This was actually the easiest task. I made a Monobehaviour class Mind2 which had two of these networks and switched between them based on some sensorinfo. Mind2 also had acces to the mechanical Monobehaviour class "Doggy" containing the actual sensorinfo and the motorsystem.

Last but not least I had to design the actual game logic consisting of tuning the parameters and the shape of the networks, choosing the amount of the doggys and adjusting the eyesensors in the beginning of the game. After that started the actual game where I had to implement some kind of training tools. At the moment they are randomizing a new network, mutating an existing one with tunable parameters, cross breeding two or more units and finally generating the next generation.

##Class diagram of the interesting parts of the game.

![luokkadiagrammi](ClassDiagram.JPG)


