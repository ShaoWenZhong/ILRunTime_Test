
---@class actorAttributeBean 
---@field id int @id 
---@field hp int @血量 
---@field moveSpeed float @移动速度 
---@field chaseMaxDistanceWithInitPos float @追击最大距离 
---@field chaseIgnoreOffset float @追击超过距离返回途中的无视再次追击偏移 
---@field idleSleepTime float @追击超过距离返回途中的无视再次追击偏移 
---@field warningRange float @警戒范围 
---@field beHitBackWeights int @被击退概率(0-100) 
---@field hpResume float @回血速率 
local m={}
