local manageBase = require("Lua/Manager/ManagerBase.lua");

local GameManager = manageBase:new();

GameManager.UIDir = {}

function GameManager:new()

    local o = {}
    setmetatable(o, self)
    self.__index = self;
    return o;

end



function GameManager:SendMgs(MgsBase)

    if not MgsBase then return end

    local mgsId = MgsBase:GetManager();

    if mgsId == ManageId.GameManager then 
        --如果是本身本模块调用
        self:ProcessEvent(MgsBase);

    else

        MgsCenter.SendMgs(MgsBase)

    end

end
return GameManager;