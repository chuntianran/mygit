local manageBase = require("Lua/Manager/ManagerBase.lua");

local UIManager = manageBase:new();

UIManager.UIDir = {}

function UIManager:new()

    local o = {}
    setmetatable(o, self)
    self.__index = self;
    return o;

end


--待定
function UIManager:CreateWindow()


end

function UIManager:GetWindow(sign)



end

function UIManager:SendMgs(MgsBase)

    if not MgsBase then return end

    local mgsId = MgsBase:GetManager();

    if mgsId == ManageId.UIManager then 
        --如果是本身本模块调用
        self:ProcessEvent(MgsBase);

    else

        MgsCenter.SendMgs(MgsBase)

    end

end
return UIManager;