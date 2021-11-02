local Original_ChatFrame_OnEvent;
local chatbuf;
local chatcount;
local lasttell;
local totalcount;
function AdamAtom_OnLoad()
	-- Hook ChatFrame_OnEvent so we can hook AddMessage
	
	this:RegisterEvent("COMBAT_LOG_EVENT_UNFILTERED");

	
	Original_ChatFrame_OnEvent = ChatFrame_OnEvent;
	ChatFrame_OnEvent = AdamAtom_ChatFrame_OnEvent;
	
	
	
	--display AddOn loaded message at startup
	if(DEFAULT_CHAT_FRAME) then
		DEFAULT_CHAT_FRAME:AddMessage("AdamAtom Loaded");
	end
	UIErrorsFrame:AddMessage("AdamAtom loaded!", 1.0, 1.0, 1.0, 1.0, UIERRORS_HOLD_TIME);
	
	lasttell = "";
	 chatcount = 0;
	 totalcount = 0;
	 chatbuf={};
   chatbuf[200]=0;
   local l1;
    for l1=1,200,1 do
      chatbuf[l1]=0;
	 end
	 chatbuf[2]=1234.65;
   chatbuf[1]=1234.56;
end

function AdamAtom_OnEvent()
end

function AdamAtom_ChatFrame_OnEvent(event)
	Original_ChatFrame_OnEvent(event); --call the real ChatFrame_OnEvent function
	--if we haven't already done so, hook the AddMessage function
	if(not this.Original_AddMessage) then
		this.Original_AddMessage = this.AddMessage;
		this.AddMessage = AdamAtom_AddMessage;
	end
end

function AdamAtom_AddMessage(this, msg, r, g, b, id)
	local locmsg = "";
	if(not msg) then msg = ""; end

	if((r == 0.340000 and g == 0.6400000 and b==1.000000) or
		(r == 0.7 and g == 0.7 and b==0.7) or
		(r == 0.75 and g == 0.05 and b==0.05) or
		(r == 1.0 and g == 1.0 and b == 1.0 and id == 31)) then
	this:Original_AddMessage(msg, r, g, b, id);
	else
	
			if(chatcount >= 20) then
				chatcount = 0;
				local l1;
	    	for l1=4,24,1 do
		      chatbuf[l1]="EMPTY";
				end
			end
			
			
			localmsg = msg;
			if(id == 10 or id == 8) then
				local thisispm = "THISISAPM"; 
				localmsg = format(":%s:%s", thisispm, msg);
			end
			if(id == 2) then
				local thisispm = "THISISASAY"; 
				localmsg = format(":%s:%s", thisispm, msg);
			end
			if(id == 7) then
				local thisispm = "THISISAYELL"; 
				localmsg = format(":%s:%s", thisispm, msg);
			end
			if(id == 5) then
				local thisispm = "THISISAGUILD"; 
				localmsg = format(":%s:%s", thisispm, msg);
			end
			
			
			chatbuf[chatcount+4] = format("%d:%s", chatcount,localmsg);
			chatbuf[3] = chatcount; -- store the slot we just wrote to
			chatcount = chatcount+1;
			
			
			-- use this to get color code and id to create filter for messages (see beginning of this func)	
			-- local newmsg = format("[%f:%f:%f:%d] %s",r, g, b,id, msg); --format the output
			-- this:Original_AddMessage(newmsg, r, g, b, id); --call the real AddMessage function
			this:Original_AddMessage(msg, r, g, b, id); --call the real AddMessage function
	end
end
