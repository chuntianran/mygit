
MgsCenter = {}

UIManager = (require "Lua/Manager/UIManager.lua"):new(); 
MgsCenter[ManageId.UIManager] = UIManager;

GameManager = (require "Lua/Manager/GameManager.lua"):new();
MgsCenter[ManageId.GameManager] = GameManager;

--发送信息
function MgsCenter.SendMgs(MgsBase)

    if not MgsBase then return end
    
    local mgsId = MgsBase:GetManager()

    local manage = MgsCenter[mgsId];

    if manage ~= nil then 

        manage:ProcessEvent(MgsBase);

    end

end