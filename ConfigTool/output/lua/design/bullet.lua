
local bullet = { 
    [1] = { id=1, prefabPath="Model/Bullet/bullet01", pathType=0, speed=8, maxLifeTime=2  } 
}

local mt = {}
mt.__index = function(t,k)
    loge("不能发现配置表项bullet:"..k)
end
setmetatable(bullet,mt)

return bullet
