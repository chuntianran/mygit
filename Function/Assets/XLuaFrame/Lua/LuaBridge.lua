

function start()

    require("Lua/FrameTools.lua");
    require("Lua/Sign.lua");
    require("Lua/MgsCenter.lua");


    local monoBase = require("Lua/MonoBase.lua");

    --相当于窗体
    local test1 = monoBase:new();
    local test2 = monoBase:new();
    local test3 = monoBase:new();
    UIManager:Register(UIID.Load,test1);
    UIManager:Register(UIID.Refresh,test2);

    --相当于游戏物体
    GameManager:Register(GAMEID.Start,test3);

    local MgsBase = require("Lua/Message/MgsBase.lua");
    local message = MgsBase:new(UIID.Load);
    local message2 = MgsBase:new(GAMEID.Start);
    message2.message = "调用GameManager";
    message.message = "我调用自身啦";
    UIManager:SendMgs(message);
    UIManager:SendMgs(message2);

end

function Test()

    local monoBase = require("Lua/MonoBase.lua")
    local test1 = monoBase:new();
    local test2 = monoBase:new();
    local test3 = monoBase:new();
    local test = require("Lua/Manager/ManagerBase.lua"):new();
    test:Register(ManageId.UIManager,test1);
    test:Register(ManageId.GameManager,test2);
    test:Register(ManageId.GameManager,test3);
    test:ProcessEvent();
    test:UnRegisterAll(ManageId.GameManager);
    test:ProcessEvent();

end

function update()

	--print("luaUpdate");
end

function ondestroy()
    print("lua destroy")
end


