
local weapon = { 
    [1] = { id=1, prefabPath="equip/Axe_lv0", connectName="weaponPoint", time=0.5, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.05,0.35}, hurtValue=1  }, 
    [101] = { id=101, prefabPath="equip/Pickaxe_lv0", connectName="weaponPoint", time=0.5, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.05,0.35}, hurtValue=1  }, 
    [201] = { id=201, prefabPath="equip/Sword_lv0", connectName="weaponPoint", time=0.5, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.05,0.35}, hurtValue=1  }, 
    [202] = { id=202, prefabPath="equip/Sword_lv1", connectName="weaponPoint", time=0.5, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.05,0.35}, hurtValue=2  }, 
    [203] = { id=203, prefabPath="equip/Sword_lv2", connectName="weaponPoint", time=0.5, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.05,0.35}, hurtValue=4  }, 
    [204] = { id=204, prefabPath="equip/Sword_lv3", connectName="weaponPoint", time=0.5, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.05,0.35}, hurtValue=6  }, 
    [1001] = { id=1001, prefabPath="equip/Sword_lv0", connectName="weaponPoint", time=0.5, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.05,0.35}, hurtValue=1  }, 
    [2001] = { id=2001, prefabPath="equip/Sword_lv0", connectName="weaponPoint", time=0.5, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.05,0.35}, hurtValue=1  }, 
    [10001] = { id=10001, prefabPath="equip/DiverSword", connectName="weaponPoint", time=0.5, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.05,0.35}, hurtValue=1  }, 
    [10002] = { id=10002, prefabPath="equip/JetiStick", connectName="weaponPoint", time=0.5, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.05,0.35}, hurtValue=1  }, 
    [10003] = { id=10003, prefabPath="equip/KnightSword", connectName="weaponPoint", time=0.5, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.05,0.35}, hurtValue=2  }, 
    [10004] = { id=10004, prefabPath="equip/SpacesuitSword", connectName="weaponPoint", time=1.667, attackAnims="PawnChop", fireNodes={}, bulletAngles={}, bulletIds={}, attackApplyTimes={0.16,1.16}, hurtValue=9  } 
}

local mt = {}
mt.__index = function(t,k)
    loge("不能发现配置表项weapon:"..k)
end
setmetatable(weapon,mt)

return weapon
