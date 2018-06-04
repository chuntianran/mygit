
--基础类型
local  MonoBase = {};
function MonoBase:new()
    local o = {};
    setmetatable(o, self)
    self.__index = self
    return o
end

--执行方法
function MonoBase:ProcessEvent(MgsBase)

    print(MgsBase.message);

end

return MonoBase;