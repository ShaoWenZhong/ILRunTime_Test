
local Sound = { 
    [1001] = { id=1001, clipPath="Audio/motorcollision", delay=0, volume=0.5, IsLoop=false  }, 
    [1002] = { id=1002, clipPath="Audio/carexplosion", delay=0, volume=1, IsLoop=false  }, 
    [1003] = { id=1003, clipPath="Audio/kick", delay=0, volume=0.9, IsLoop=false  }, 
    [1004] = { id=1004, clipPath="Audio/hurt", delay=0, volume=1, IsLoop=false  }, 
    [1005] = { id=1005, clipPath="Audio/dead", delay=0, volume=1, IsLoop=false  }, 
    [1006] = { id=1006, clipPath="Audio/levelgetcoin", delay=0, volume=1, IsLoop=false  }, 
    [1007] = { id=1007, clipPath="Audio/getdiamond", delay=0, volume=1, IsLoop=false  }, 
    [1008] = { id=1008, clipPath="Audio/clickbutton", delay=0, volume=1, IsLoop=false  }, 
    [1009] = { id=1009, clipPath="Audio/accelerators", delay=0, volume=1, IsLoop=false  }, 
    [1010] = { id=1010, clipPath="Audio/pickup", delay=0, volume=1, IsLoop=false  }, 
    [1011] = { id=1011, clipPath="Audio/blast", delay=0, volume=1, IsLoop=false  }, 
    [1012] = { id=1012, clipPath="Audio/land", delay=0, volume=0.1, IsLoop=false  }, 
    [1013] = { id=1013, clipPath="Audio/drive2", delay=0, volume=0.06, IsLoop=true  }, 
    [1014] = { id=1014, clipPath="Audio/train", delay=0, volume=1, IsLoop=false  }, 
    [1015] = { id=1015, clipPath="Audio/transforming", delay=0.1, volume=0.4, IsLoop=false  }, 
    [1016] = { id=1016, clipPath="Audio/fashepaodan", delay=0, volume=0.7, IsLoop=false  }, 
    [1017] = { id=1017, clipPath="Audio/zhishengji", delay=0, volume=0.4, IsLoop=true  }, 
    [1018] = { id=1018, clipPath="Audio/zhishengjisaoshe", delay=0, volume=1, IsLoop=false  }, 
    [1019] = { id=1019, clipPath="Audio/bazookashoot", delay=0, volume=1, IsLoop=false  }, 
    [1020] = { id=1020, clipPath="Audio/bazookahit", delay=0, volume=1.5, IsLoop=false  }, 
    [1021] = { id=1021, clipPath="Audio/gun_dahuangfeng", delay=0, volume=0.8, IsLoop=false  }, 
    [1022] = { id=1022, clipPath="Audio/gun_gaoda", delay=0, volume=0.4, IsLoop=false  }, 
    [1023] = { id=1023, clipPath="Audio/gun_05", delay=0, volume=0.8, IsLoop=false  }, 
    [1024] = { id=1024, clipPath="Audio/attack_TSL", delay=0, volume=1, IsLoop=true  }, 
    [2001] = { id=2001, clipPath="Audio/mainbg", delay=0, volume=0.2, IsLoop=true  }, 
    [2002] = { id=2002, clipPath="Audio/citybg", delay=0, volume=0.2, IsLoop=true  } 
}

local mt = {}
mt.__index = function(t,k)
    loge("不能发现配置表项Sound:"..k)
end
setmetatable(Sound,mt)

return Sound
