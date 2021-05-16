/*
_________________________________________________

Les murs ont pour element :
    - NavMesvhObstacle
_________________________________________________

Le sol a pour element :
    - NavMesvhSurface
_________________________________________________

La boue a pour elements :
    - NavMesvhSurface

    - NavMesvhModifier
        Ignore From Build = false
        Overridre Area = true
        AreaType = SlowArea (cost 5)
        Affected Agent = all

    - Slow Area Handler
        Script = SlowAreaHandler
        Agent --> Drag n Drop Agent GameObject
        LowSpeed = 2
_________________________________________________

Les chausse-trappes a pour elements :
    - NavMesvhSurface

    - NavMesvhModifier
        Ignore From Build = false
        Overridre Area = true
        AreaType = Caltrop (cost 2)
        Affected Agent = all

    - FailSimulation
        Script = FailSimulation
        Agent --> Drag n Drop Agent GameObject
_________________________________________________

Le NavMeshBaker (GameObject vide) a pour element :
   - Script =  NavMeshBaker

   - Nav Mesh Surface (Drag n drop du GameObject sur le titre "Nav Mesh Surface")
        [Floor] 
_________________________________________________

L'agent a pour element :
    - Rigidbody

    - Nav Mesh Agent
        speed = 0
        angular speed = 120
        acceleration = 8
        atopping distance = 0
        auto braking = true
    
    - NPC Move
        script = NPCMove
        Agent --> Drag n Drop Agent GameObject

*/