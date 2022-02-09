
local Anything = { 
    [1] = { id=1, intValue=0, intArray={10001,10002,10003,10004,10005,10006,10007,10008,10009,10010}, floatValue=0, floatArray={}, stringValue="", stringArray={}, boolValue=false  }, 
    [2] = { id=2, intValue=0, intArray={10001}, floatValue=0, floatArray={}, stringValue="", stringArray={}, boolValue=false  }, 
    [3] = { id=3, intValue=0, intArray={1,2}, floatValue=0, floatArray={}, stringValue="", stringArray={}, boolValue=false  } 
}

local mt = {}
mt.__index = function(t,k)
    loge("不能发现配置表项Anything:"..k)
end
setmetatable(Anything,mt)

return Anything
