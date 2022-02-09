
local actorAttribute = { 
    [1] = { id=1, hp=10, moveSpeed=5, chaseMaxDistanceWithInitPos=10, chaseIgnoreOffset=2, idleSleepTime=2, warningRange=5, beHitBackWeights=50, hpResume=2  }, 
    [1001] = { id=1001, hp=10, moveSpeed=5, chaseMaxDistanceWithInitPos=10, chaseIgnoreOffset=2, idleSleepTime=2, warningRange=5, beHitBackWeights=50, hpResume=0  }, 
    [2001] = { id=2001, hp=10, moveSpeed=5, chaseMaxDistanceWithInitPos=20, chaseIgnoreOffset=2, idleSleepTime=2, warningRange=20, beHitBackWeights=50, hpResume=0  }, 
    [10001] = { id=10001, hp=5, moveSpeed=4, chaseMaxDistanceWithInitPos=10, chaseIgnoreOffset=2, idleSleepTime=2, warningRange=5, beHitBackWeights=100, hpResume=0  }, 
    [10002] = { id=10002, hp=5, moveSpeed=4, chaseMaxDistanceWithInitPos=10, chaseIgnoreOffset=2, idleSleepTime=2, warningRange=5, beHitBackWeights=100, hpResume=0  }, 
    [10003] = { id=10003, hp=7, moveSpeed=4, chaseMaxDistanceWithInitPos=10, chaseIgnoreOffset=2, idleSleepTime=2, warningRange=5, beHitBackWeights=50, hpResume=0  }, 
    [10004] = { id=10004, hp=30, moveSpeed=2.5, chaseMaxDistanceWithInitPos=6, chaseIgnoreOffset=2, idleSleepTime=2, warningRange=4, beHitBackWeights=0, hpResume=0  } 
}

local mt = {}
mt.__index = function(t,k)
    loge("不能发现配置表项actorAttribute:"..k)
end
setmetatable(actorAttribute,mt)

return actorAttribute
