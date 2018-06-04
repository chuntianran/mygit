
local MgsBase = {}


--定义消息对象
function MgsBase:new(mgsId)

    local o = {};
    setmetatable(o, self)
    self.__index = self
    o.mgsId = mgsId;
    return o;

end
--获取对应的管理器
function MgsBase:GetManager()
    
    --识别是哪个管理器
    local id = math.floor(  self.mgsId / 3000 ) * 3000;
    id = math.floor( id );

    for i,v in pairs(ManageId) do 
        if id == v then
            return v;
        end
    end
    return nil;
end

return MgsBase;