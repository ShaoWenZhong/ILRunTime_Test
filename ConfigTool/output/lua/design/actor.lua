
local actor = { 
    [1] = { id=1, model="", weaponId=1, attributeIds={1}  }, 
    [1001] = { id=1001, model="", weaponId=1001, attributeIds={1001}  }, 
    [2001] = { id=2001, model="", weaponId=2001, attributeIds={2001}  }, 
    [10001] = { id=10001, model="", weaponId=10001, attributeIds={10001}  }, 
    [10002] = { id=10002, model="", weaponId=10002, attributeIds={10002}  }, 
    [10003] = { id=10003, model="", weaponId=10003, attributeIds={10003}  }, 
    [10004] = { id=10004, model="", weaponId=10004, attributeIds={10004}  } 
}

local mt = {}
mt.__index = function(t,k)
    loge("不能发现配置表项actor:"..k)
end
setmetatable(actor,mt)

return actor
