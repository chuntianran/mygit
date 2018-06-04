local monoBase = require("Lua/MonoBase.lua")

local ManageBase = monoBase:new();

ManageBase.MgsDic = {};

function ManageBase:new()

    local o = {}
    setmetatable(o,self)
    self.__index = self
    return o;
end

--注册事件
function ManageBase:Register(mgsId,mono)

    local isHave = self:CheckHaveKey(mgsId);
    if not isHave then
        local tbl = {};
        table.insert( tbl,mono );
        self.MgsDic[mgsId] = tbl;
    else

        local tbl = self.MgsDic[mgsId];
        table.insert( tbl,mono );
    end
end

--检测是否存在该key
function ManageBase:CheckHaveKey(key)
    if self.MgsDic[key] then 
        return true;
    end
    return false;
end

--撤销某个监听者
function ManageBase:UnRegister(mono)

    for i,v in pairs(self.MgsDic) do
        if v ~= nil then 
            local temp = nil;
            for h,j in pairs(v) do
                if j == mono then    
                    temp = h;
                    break;
                end
            end
         
            if temp ~= nil then 
                table.remove( v,temp )    
            end
        end
    end
end

--撤销某一事件的所有监听者
function ManageBase:UnRegisterAll(key)

    if self.MgsDic[key] ~= nil then
        self.MgsDic[key] = nil;
    end

end

--处理信息
function ManageBase:ProcessEvent(MgsBase)

    if MgsBase ~= nil then
            
            local tbl = self.MgsDic[MgsBase.mgsId];
            if tbl ~= nil then
                for h,j in pairs(tbl) do
                    j:ProcessEvent(MgsBase);
                end
            end
    end

end


return ManageBase;