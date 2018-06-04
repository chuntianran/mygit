

MgsId = 3000;
--从上往下执行
function CreateEnumTable(tbl, index,number)   
    local enumtbl = {}   
    local enumindex = index or 0   
    for i, v in pairs(tbl) do   
        enumtbl[v] = enumindex + i *  number;
    end   
    return enumtbl   
end 


--声明所有管理器的消息区域，3000-3999区间
local Table ={ "UIManager","GameManager" }
ManageId =  CreateEnumTable(Table,0,MgsId);

--声明UI管理器的消息
local uiT = {"Load","Refresh"}
UIID = CreateEnumTable(uiT,ManageId.UIManager,1)


local gameT = {"Start","End"};
GAMEID = CreateEnumTable(gameT,ManageId.GameManager,1)
--lua类定义，与继承
-- Mgs = {};
-- function Mgs:new(o)

--     local o = o or {}
--     setmetatable(o,self)
--     self.__index = self
--     o.lenght = 20
--     return o
-- end

-- temp = Mgs:new();
-- print(temp.lenght)


-- function temp:new()

--     local o = o or {}
--     setmetatable(o,self)
--     self.__index = self
--     o.width = 50

--     return o
-- end
-- --如果temp2查询不到，就从Mgs查询，Mgs对象是个o，它有设计一个元表，从元表查询，直到Mgs，由于Mgs没有设置元表，不查询
-- temp2 = temp:new();
-- print(temp2.lenght)
-- print(temp2.width)
