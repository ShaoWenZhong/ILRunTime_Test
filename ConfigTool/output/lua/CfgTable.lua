
CfgTable = {}

setmetatable(
    CfgTable,
    {
        __index = function(t, k)
            local v = require(k)
            if type(v) == "table" then
                CfgTable[k] = v
                return v
            else
				CfgTable[k] = nil
                return nil
            end
        end
    }
)
