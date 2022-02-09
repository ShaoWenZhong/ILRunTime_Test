
local item = { 
    [101] = { id=101, nameId=0, icon="", bgIcon="", showInBackage=false, connectPotName="HeadPoint", prefabPath="equip/Armour_Helmet_lv1", addHp=5, eftPrefab=""  }, 
    [102] = { id=102, nameId=0, icon="", bgIcon="", showInBackage=false, connectPotName="HeadPoint", prefabPath="equip/Armour_Helmet_lv2", addHp=10, eftPrefab=""  }, 
    [103] = { id=103, nameId=0, icon="", bgIcon="", showInBackage=false, connectPotName="HeadPoint", prefabPath="equip/Armour_Helmet_lv3", addHp=20, eftPrefab=""  }, 
    [201] = { id=201, nameId=0, icon="", bgIcon="", showInBackage=false, connectPotName="ArmourPoint", prefabPath="equip/Armour_lv1", addHp=5, eftPrefab=""  }, 
    [202] = { id=202, nameId=0, icon="", bgIcon="", showInBackage=false, connectPotName="ArmourPoint", prefabPath="equip/Armour_lv2", addHp=10, eftPrefab=""  }, 
    [203] = { id=203, nameId=0, icon="", bgIcon="", showInBackage=false, connectPotName="ArmourPoint", prefabPath="equip/Armour_lv3", addHp=20, eftPrefab=""  }, 
    [1] = { id=1, nameId=0, icon="itemicon_gem_blue", bgIcon="", showInBackage=false, connectPotName="", prefabPath="", addHp=0, eftPrefab="effect/Diamonds/Diamonds"  }, 
    [2] = { id=2, nameId=0, icon="itemicon_coin_gold_star", bgIcon="", showInBackage=false, connectPotName="", prefabPath="", addHp=0, eftPrefab="effect/Diamonds/Diamonds"  } 
}

local mt = {}
mt.__index = function(t,k)
    loge("不能发现配置表项item:"..k)
end
setmetatable(item,mt)

return item
