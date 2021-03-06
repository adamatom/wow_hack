-- 
-- Face Smasher
-- An addon by Falie, aka Aytherine of Maelstrom
-- Let's smash some faces in
--


local chatbuf;
local chatcount;
local lasttell;
local totalcount;



-- Our base array
FaceSmasher = {}

-- Face Melter variables NOT SAVED

FaceSmasher.versionNumber = 1.3
FaceSmasher.currentTarget = ""
FaceSmasher.currentSpell = ""
FaceSmasher.lastTarget = ""
FaceSmasher.npcList = {} -- {guid, name}
FaceSmasher.plagueList = {}  -- {guid, GetTime}
FaceSmasher.icyList = {} -- {guid, GetTime}
FaceSmasher.blightTime = 0
FaceSmasher.lockRunic = false
FaceSmasher.runetapTime = 0
FaceSmasher.iceboundTime = 0
FaceSmasher.lichborneTime = 0
FaceSmasher.shieldTime = 0
FaceSmasher.shieldDown = true
FaceSmasher.DnDTime = 0
FaceSmasher.pestTime = 0
FaceSmasher.freezingFog = false
FaceSmasher.howlingTime = 0
FaceSmasher.rsTime = 0
FaceSmasher.horn = false
FaceSmasher.mindfreezetime = 0
FaceSmasher.strangulateTime = 0
FaceSmasher.badbad = 0
FaceSmasher.bloodTapTime = 0
FaceSmasher.gargTime = 0
FaceSmasher.OpenRunic = 0
FaceSmasher.suddenDoom = 0
FaceSmasher.fsCost = 40
FaceSmasher.ps = false
FaceSmasher.it = false
FaceSmasher.playerLevel = 0



FaceSmasher.timeSinceLastUpdate = 0


FaceSmasher.playerName = UnitName("player");
FaceSmasher.spellHaste = GetCombatRatingBonus(20)

FaceSmasher.textureList = {
  ["last"] = nil,
  ["current"] = nil,
  ["next"] = nil,
  ["misc"] = nil,
  ["int"] = nil,
  }
  
FaceSmasher.SL = {
  ["Blood Boil"] = GetSpellInfo(48721),
  ["Blood Strike"] = GetSpellInfo(45902),
  ["Blood Tap"] = GetSpellInfo(45529),
  ["Pestilence"] = GetSpellInfo(50842),
  ["Strangulate"] = GetSpellInfo(47476),
  ["Horn of Winter"] = GetSpellInfo(57330),
  ["Icebound Fortitude"] = GetSpellInfo(48792),
  ["Icy Touch"] = GetSpellInfo(45477),
  ["Mind Freeze"] = GetSpellInfo(47528),
  ["Obliterate"] = GetSpellInfo(49020),
  ["Rune Strike"] = GetSpellInfo(56815),
  ["Bone Shield"] = GetSpellInfo(49222),
  ["Death and Decay"] = GetSpellInfo(43265),
  ["Death Coil"] = GetSpellInfo(52375),
  ["Death Strike"] = GetSpellInfo(49998),
  ["Plague Strike"] = GetSpellInfo(45462),
  ["Scourge Strike"] = GetSpellInfo(55265),
  ["Unholy Blight"] = GetSpellInfo(51376),
  ["Rune Tap"] = GetSpellInfo(48982),
  ["Vampiric Blood"] = GetSpellInfo(55233),
  ["Heart Strike"] = GetSpellInfo(55258),
  ["Lichborne"] = GetSpellInfo(49039),
  ["Howling Blast"] = GetSpellInfo(49184),
  ["Unbreakable Armor"] = GetSpellInfo(51271),
  ["Frost Strike"] = GetSpellInfo(51416),
  ["Frost Fever"] = GetSpellInfo(55095),
  ["Blood Plague"] = GetSpellInfo(55078),
  ["Summon Gargoyle"] = GetSpellInfo(49206),
  ["Freezing Fog"] = GetSpellInfo(59052),
  ["Dancing Rune Weapon"] = GetSpellInfo(49028),
  ["Gnaw"] = GetSpellInfo(47481),
  ["Leap"] = GetSpellInfo(47482),
  ["Deserter"] = GetSpellInfo(26013),
  
  

}


-- Our sneaky frame to watch for events ... checks FaceSmasher.events[] for the function.  Passes all args.
FaceSmasher.eventFrame = CreateFrame("Frame")
FaceSmasher.eventFrame:SetScript("OnEvent", function(this, event, ...)
  FaceSmasher.events[event](...)
end)

FaceSmasher.eventFrame:RegisterEvent("ADDON_LOADED")
FaceSmasher.eventFrame:RegisterEvent("PLAYER_LOGIN")
FaceSmasher.eventFrame:RegisterEvent("PLAYER_ALIVE")


-- Define our Event Handlers here
FaceSmasher.events = {}

function FaceSmasher.events.PLAYER_ALIVE()
	FaceSmasher:CheckStuff()
	FaceSmasher.eventFrame:UnregisterEvent("PLAYER_ALIVE")
end

function FaceSmasher.events.PLAYER_LOGIN()
  FaceSmasher.playerName = UnitName("player");

  FaceSmasher.spellHaste = GetCombatRatingBonus(20)
 
  
end

function FaceSmasher.events.ADDON_LOADED(addon)
  if addon ~= "FaceSmasher" then return end
  if FaceSmasher.an ~= "Face" .. "Smasher" then return end
  local _,playerClass = UnitClass("player")
  if playerClass ~= "DEATHKNIGHT" then
	FaceSmasher.eventFrame:UnregisterEvent("PLAYER_ALIVE")
	return 
  end
  
  FaceSmasher.playerLevel = UnitLevel("player")
  
  
  -- Default saved variables
  if not FaceSmasherdb then 
	FaceSmasherdb = {} -- fresh start
  end
  if not FaceSmasherdb.scale then FaceSmasherdb.scale = 1 end
  if FaceSmasherdb.locked == nil then FaceSmasherdb.locked = false end
  if not FaceSmasherdb.x then FaceSmasherdb.x = 100 end
  if not FaceSmasherdb.y then FaceSmasherdb.y = 100 end
  if FaceSmasherdb.modeSingle == nil then FaceSmasherdb.modeSingle = true end
  if FaceSmasherdb.modeAoE == nil then FaceSmasherdb.modeAoE = true end
  if FaceSmasherdb.modeDefensive == nil then FaceSmasherdb.modeDefensive = true end
  if FaceSmasherdb.modeMisc == nil then FaceSmasherdb.modeMisc = true end
  if FaceSmasherdb.modeInt == nil then FaceSmasherdb.modeInt = true end
  if FaceSmasherdb.horn == nil then FaceSmasherdb.horn = true end
  if not FaceSmasherdb.healthPercent then FaceSmasherdb.healthPercent = 75 end
  if FaceSmasherdb.bloodfirst == nil then FaceSmasherdb.bloodfirst = true end
  if FaceSmasherdb.DSMid == nil then FaceSmasherdb.DSMid = false end
  if FaceSmasherdb.HowlingFirst == nil then FaceSmasherdb.HowlingFirst = false end
  if FaceSmasherdb.usePS == nil then FaceSmasherdb.usePS = true end
  if FaceSmasherdb.useITD == nil then FaceSmasherdb.useITD = false end
  
  
  -- Create GUI
  FaceSmasher:CreateGUI()
  FaceSmasher.displayFrame:SetScale(FaceSmasherdb.scale)
  
  
  -- Create Options Frame
  FaceSmasher:CreateOptionFrame()
  if FaceSmasherdb.locked then
    FaceSmasher.displayFrame:SetScript("OnMouseDown", nil)
    FaceSmasher.displayFrame:SetScript("OnMouseUp", nil)
    FaceSmasher.displayFrame:SetScript("OnDragStop", nil)
    FaceSmasher.displayFrame:SetBackdropColor(0, 0, 0, 0)
	FaceSmasher.displayFrame:EnableMouse(false)
  else
    FaceSmasher.displayFrame:SetScript("OnMouseDown", function(self) self:StartMoving() end)
    FaceSmasher.displayFrame:SetScript("OnMouseUp", function(self) self:StopMovingOrSizing() end)
    FaceSmasher.displayFrame:SetScript("OnDragStop", function(self) self:StopMovingOrSizing() end)
    FaceSmasher.displayFrame:SetBackdropColor(0, 0, 0, .4)
	FaceSmasher.displayFrame:EnableMouse(true)
  end
  
  -- Register for Slash Commands
  SlashCmdList["FaceSmasher"] = FaceSmasher.Options
  SLASH_FaceSmasher1 = "/FaceSmasher"
  SLASH_FaceSmasher2 = "/fs"
  
  -- Register for Function Events
  FaceSmasher.eventFrame:RegisterEvent("COMBAT_LOG_EVENT_UNFILTERED")
  FaceSmasher.eventFrame:RegisterEvent("COMBAT_RATING_UPDATE") -- Monitor the all-mighty haste
  FaceSmasher.eventFrame:RegisterEvent("PLAYER_TARGET_CHANGED")
  FaceSmasher.eventFrame:RegisterEvent("PLAYER_REGEN_ENABLED") -- Left combat, clean up all enemy GUIDs
  FaceSmasher.eventFrame:RegisterEvent("UNIT_INVENTORY_CHANGED")
  FaceSmasher.eventFrame:RegisterEvent("CHARACTER_POINTS_CHANGED")
  FaceSmasher.eventFrame:RegisterEvent("RUNE_POWER_UPDATE") -- hey, we're a deathknight
  FaceSmasher.eventFrame:RegisterEvent("RUNE_TYPE_UPDATE")
  FaceSmasher.eventFrame:RegisterEvent("GLYPH_ADDED")
  FaceSmasher.eventFrame:RegisterEvent("GLYPH_REMOVED")
  FaceSmasher.eventFrame:RegisterEvent("GLYPH_UPDATED")
  FaceSmasher.eventFrame:RegisterEvent("ZONE_CHANGED_NEW_AREA")
  
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

function FaceSmasher.events.COMBAT_LOG_EVENT_UNFILTERED(timestamp, event, srcGUID, srcName, srcFlags, dstGUID, dstName, dstFlags, ...)
  if(chatcount >= 20) then
		chatcount = 0;
		local l1;
	  for l1=4,24,1 do
	    chatbuf[l1]="EMPTY";
	  end
	end
	
	chatbuf[chatcount+4] = format("%d:Location--%s", chatcount, GetZoneText());
	chatbuf[3] = chatcount; -- store the slot we just wrote to
	chatcount = chatcount+1;
  if srcName == FaceSmasher.playerName then
    if event == "SPELL_CAST_SUCCESS" then
        local sid, spellName = ...
        if spellName == FaceSmasher.SL["Blood Strike"] then
          FaceSmasher.currentSpell = "Blood Strike"
          FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Unholy Blight"] then
          FaceSmasher.currentSpell = "Unholy Blight"
		  FaceSmasher.blightTime = GetTime()
          FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Death Coil"] then
          FaceSmasher.currentSpell = "Death Coil"
		  FaceSmasher.suddenDoom = 0
          FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Rune Tap"] then
			FaceSmasher.runetapTime = GetTime()
			FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Blood Tap"] then
			FaceSmasher.bloodTapTime = GetTime()
			FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Vampiric Blood"] or spellName == FaceSmasher.SL["Unbreakable Armor"] or spellName == FaceSmasher.SL["Bone Shield"] then
			FaceSmasher.shieldTime = GetTime()
			FaceSmasher.shieldDown = false;
			FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Death and Decay"] then
			FaceSmasher.DnDTime = GetTime()
			FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Howling Blast"] then
			FaceSmasher.freezingFog = false
			FaceSmasher.howlingTime = GetTime()
			FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Rune Strike"] then
			FaceSmasher.rsTime = 0
			FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Mind Freeze"] then
			FaceSmasher.mindfreezetime = GetTime()
			FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Strangulate"] then
			FaceSmasher.strangulateTime = GetTime()
			FaceSmasher:DecideSpells()
		end
	elseif event == "SPELL_DAMAGE" then
		 local sid, spellName = ...
		 if spellName == FaceSmasher.SL["Plague Strike"] then
          FaceSmasher.currentSpell = "Plague Strike"
		  FaceSmasher.plagueList[dstGUID] = GetTime()
          FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Icy Touch"] then
          FaceSmasher.currentSpell = "Icy Touch"
		  FaceSmasher.icyList[dstGUID] = GetTime()
          FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Pestilence"] then
			FaceSmasher.pestTime = GetTime()
			if FaceSmasher.currentTarget ~= dstGUID then
				FaceSmasher.icyList[dstGUID] = GetTime()
				FaceSmasher.plagueList[dstGUID] = GetTime()
			end
			FaceSmasher:DecideSpells()
		 elseif spellName == FaceSmasher.SL["Scourge Strike"] then
          FaceSmasher.currentSpell = "Scourge Strike"
          FaceSmasher:DecideSpells()
		end
	elseif event == "SPELL_AURA_APPLIED" then
		local sid, spellName = ...
		if spellName == FaceSmasher.SL["Frost Fever"] then
			FaceSmasher.icyList[dstGUID] = GetTime()
		elseif spellName == FaceSmasher.SL["Blood Plague"] then
			FaceSmasher.plagueList[dstGUID] = GetTime()
		elseif sid == 43680 then
			chatbuf[chatcount+4] = format("%d:QUIT", chatcount);
			chatbuf[3] = chatcount; -- store the slot we just wrote to
			chatcount = chatcount+1;
		elseif spellName == FaceSmasher.SL["Summon Gargoyle"] then
			if dstName == FaceSmasher.playerName then
				FaceSmasher.lockRunic = true
			end	
		elseif spellName == FaceSmasher.SL["Lichborne"] then
			FaceSmasher.lichborneTime = GetTime()
			FaceSmasher.shieldDown = false
			FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Icebound Fortitude"] then
			FaceSmasher.iceboundTime = GetTime()
			FaceSmasher.shieldDown = false
			FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Freezing Fog"] then
			FaceSmasher.freezingFog = true;
			FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Horn of Winter"] then
			FaceSmasher.horn = true
			FaceSmasher:DecideSpells()
		end
	elseif event == "SPELL_AURA_REFRESH" then
		local sid, spellName = ...
		if spellName == FaceSmasher.SL["Frost Fever"] then
			FaceSmasher.icyList[dstGUID] = GetTime()
		elseif spellName == FaceSmasher.SL["Blood Plague"] then
			FaceSmasher.plagueList[dstGUID] = GetTime()
		end
	elseif event == "SPELL_AURA_REMOVED" then
		local sid, spellName = ...
		if spellName == FaceSmasher.SL["Frost Fever"] then
			FaceSmasher.icyList[dstGUID] = 0
		elseif spellName == FaceSmasher.SL["Blood Plague"] then
			FaceSmasher.plagueList[dstGUID] = 0
		elseif sid == 50514 then -- Summon Gargoyle buff that matters
			if dstName == FaceSmasher.playerName then
				FaceSmasher.lockRunic = false
			end
		elseif spellName == FaceSmasher.SL["Vampiric Blood"] or spellName == FaceSmasher.SL["Unbreakable Armor"] or spellName == FaceSmasher.SL["Bone Shield"] or spellName == FaceSmasher.SL["Lichborne"] or spellName == FaceSmasher.SL["Icebound Fortitude"] then
			FaceSmasher.shieldDown = true;
			FaceSmasher:DecideSpells()
		elseif spellName == FaceSmasher.SL["Horn of Winter"] then
			FaceSmasher.horn = false
			FaceSmasher:DecideSpells()
		end
	end
  elseif event == "SWING_MISSED" and dstName == FaceSmasher.playerName then
		local missName = ...
		if missName == "PARRY" or missName == "DODGE" then
			FaceSmasher.rsTime = GetTime()
		end
		FaceSmasher:DecideSpells()
  
  end
  
end

function FaceSmasher.events.COMBAT_RATING_UPDATE(unit)
  if unit == "player" then
    FaceSmasher.spellHaste = GetCombatRatingBonus(20) -- update spell haste
  end
end

function FaceSmasher.events.PLAYER_TARGET_CHANGED(...) 
  -- target changed, set last target, update current target, will be nil if no target

  FaceSmasher.lastTarget = FaceSmasher.currentTarget
  FaceSmasher.currentTarget = UnitGUID("target")
  if UnitName("target") == nil or UnitIsFriend("player","target") ~= nil or UnitHealth("target") == 0 then
    FaceSmasher.displayFrame_last:Hide()
    FaceSmasher.displayFrame_current:Hide()
    FaceSmasher.displayFrame_next:Hide()
	FaceSmasher.displayFrame_misc:Hide()
	FaceSmasher.displayFrame_int:Hide()
  else
    FaceSmasher.displayFrame_last:Show()
    FaceSmasher.displayFrame_current:Show()
    FaceSmasher.displayFrame_next:Show()
	FaceSmasher.displayFrame_misc:Show()
	FaceSmasher.displayFrame_int:Show()
  end
  FaceSmasher:DecideSpells()
end

function FaceSmasher.events.PLAYER_REGEN_ENABLED(...)
  -- We have left combat, clean up GUIDs
  FaceSmasher.plagueList = {}  -- {guid, GetTime}
  FaceSmasher.icyList = {} -- {guid, GetTime}
end


function FaceSmasher.events.UNIT_INVENTORY_CHANGED(name)
  --if name == "player" then
  --  FaceSmasher:CheckStuff()
  --end
end

function FaceSmasher.events.CHARACTER_POINTS_CHANGED()
    FaceSmasher:CheckStuff()
end
function FaceSmasher.events.GLYPH_ADDED()
    FaceSmasher:CheckStuff()
end
function FaceSmasher.events.GLYPH_REMOVED()
    FaceSmasher:CheckStuff()
end
function FaceSmasher.events.GLYPH_CHANGED()
    FaceSmasher:CheckStuff()
end
function FaceSmasher.events.GLYPH_UPDATED()
	FaceSmasher:CheckStuff()
end
function FaceSmasher.events.ZONE_CHANGED_NEW_AREA()
  if(chatcount >= 20) then
		chatcount = 0;
		local l1;
    for l1=4,24,1 do
      chatbuf[l1]="EMPTY";
	  end
	end
	
	local zoneName = GetZoneText();
	--message(zoneName);
	chatbuf[chatcount+4] = format("%d:Location--%s", chatcount,zoneName);
	chatbuf[3] = chatcount; -- store the slot we just wrote to
	chatcount = chatcount+1;
end


function FaceSmasher.events.RUNE_POWER_UPDATE(rid, rstatus)
	if rstatus == true then
		if rid ~= 7 and rid ~= 8 then
		FaceSmasher:DecideSpells()
		end
	end
	--DEFAULT_CHAT_FRAME:AddMessage("Update: " .. rid .. "  Status: " .. tostring(rstatus))

end

function FaceSmasher.events.RUNE_TYPE_UPDATE(rid)
	
	local start, duration, runeReady = GetRuneCooldown(rid)
	if runeReady == true then
		FaceSmasher:DecideSpells()
	end
	
	--local rtype = GetRuneType(rid)
	--DEFAULT_CHAT_FRAME:AddMessage("Change: " .. rid .. "  Type: " .. rtype .. "  Cooldown: S-" .. start .. " D- " .. duration .. " R- " .. tostring(runeReady))
end

-- End Event Handlers




function FaceSmasher:CreateGUI()

  local displayFrame = CreateFrame("Frame","FaceSmasherDisplayFrame",UIParent)
  displayFrame:SetFrameStrata("BACKGROUND")
  displayFrame:SetWidth(250)
  displayFrame:SetHeight(90)
  displayFrame:SetBackdrop({
          bgFile = "Interface\\Tooltips\\UI-Tooltip-Background", tile = true, tileSize = 32,
  			})
  displayFrame:SetBackdropColor(0, 0, 0, .4)
  displayFrame:EnableMouse(true)
  displayFrame:SetMovable(true)
  --displayFrame:RegisterForDrag("LeftButton")  --causes right buttont to go crazy, go figure
  displayFrame:SetClampedToScreen(true)
  displayFrame:SetScript("OnMouseDown", function(self) self:StartMoving() end)
  displayFrame:SetScript("OnMouseUp", function(self) self:StopMovingOrSizing() end)
  displayFrame:SetScript("OnDragStop", function(self) self:StopMovingOrSizing() end)

  displayFrame:SetPoint("CENTER",-200,-200)
  
  local displayFrame_last = CreateFrame("Frame","$parent_last", FaceSmasherDisplayFrame)
  local displayFrame_current = CreateFrame("Frame","$parent_current", FaceSmasherDisplayFrame)
  local displayFrame_next = CreateFrame("Frame","$parent_next", FaceSmasherDisplayFrame)
  local displayFrame_misc = CreateFrame("Frame","$parent_misc", FaceSmasherDisplayFrame)
  local displayFrame_int = CreateFrame("Frame","$parent_int", FaceSmasherDisplayFrame)
  
  displayFrame_last:SetWidth(50)
  displayFrame_current:SetWidth(70)
  displayFrame_next:SetWidth(50)
  displayFrame_misc:SetWidth(40)
  displayFrame_int:SetWidth(40)
  
  displayFrame_last:SetHeight(50)
  displayFrame_current:SetHeight(70)
  displayFrame_next:SetHeight(50)
  displayFrame_misc:SetHeight(40)
  displayFrame_int:SetHeight(40)
  
  
  displayFrame_last:SetPoint("TOPLEFT", 0, -40)
  displayFrame_current:SetPoint("TOPLEFT", 90, -10)
  displayFrame_next:SetPoint("TOPLEFT", 200, -40)
  displayFrame_misc:SetPoint("TOPLEFT", 45, 0)
  displayFrame_int:SetPoint("TOPLEFT", 165, 0)
  
    
  local t = displayFrame_last:CreateTexture(nil,"BACKGROUND")
  t:SetTexture(nil)
  t:SetAllPoints(displayFrame_last)
  t:SetAlpha(.8)
  displayFrame_last.texture = t
  FaceSmasher.textureList["last"] = t
  
  t = displayFrame_current:CreateTexture(nil,"BACKGROUND")
  t:SetTexture(nil)
  t:ClearAllPoints()
  t:SetAllPoints(displayFrame_current)
  displayFrame_current.texture = t
  FaceSmasher.textureList["current"] = t


  
  t = displayFrame_next:CreateTexture(nil,"BACKGROUND")
  t:SetTexture(nil)
  t:SetAllPoints(displayFrame_next)
  t:SetAlpha(.8)
  displayFrame_next.texture = t
  FaceSmasher.textureList["next"] = t
  
  t = displayFrame_misc:CreateTexture(nil,"BACKGROUND")
  t:SetTexture(nil)
  t:SetAllPoints(displayFrame_misc)
  t:SetAlpha(.8)
  displayFrame_misc.texture = t
  FaceSmasher.textureList["misc"] = t
  
  t = displayFrame_int:CreateTexture(nil,"BACKGROUND")
  t:SetTexture(nil)
  t:SetAllPoints(displayFrame_int)
  t:SetAlpha(.8)
  displayFrame_int.texture = t
  FaceSmasher.textureList["int"] = t
  

  
  displayFrame:SetScript("OnUpdate", function(this, elapsed)
    FaceSmasher:OnUpdate(elapsed)
  end)
  
  local cooldownFrame = CreateFrame("Cooldown","$parent_cooldown", FaceSmasherDisplayFrame_current)
  cooldownFrame:SetHeight(70)
  cooldownFrame:SetWidth(70)
  cooldownFrame:ClearAllPoints()
  cooldownFrame:SetPoint("CENTER", displayFrame_current, "CENTER", 0, 0)
  
  FaceSmasher.displayFrame = displayFrame
  FaceSmasher.displayFrame_last = displayFrame_last
  FaceSmasher.displayFrame_current = displayFrame_current
  FaceSmasher.displayFrame_next = displayFrame_next
  FaceSmasher.displayFrame_misc =  displayFrame_misc
  FaceSmasher.displayFrame_int =  displayFrame_int
  FaceSmasher.cooldownFrame = cooldownFrame
  
  
end



function FaceSmasher:OnUpdate(elapsed)
  FaceSmasher.timeSinceLastUpdate = FaceSmasher.timeSinceLastUpdate + elapsed; 
  
  if (FaceSmasher.timeSinceLastUpdate > (1.5 - (1.5 * FaceSmasher.spellHaste * .01)) * 0.3) then
    FaceSmasher:DecideSpells()
  end

end


function FaceSmasher:DecideSpells()

  -- clear our array if the buffer is full
  if(chatcount >= 20) then
		chatcount = 0;
		local l1;
    for l1=4,24,1 do
      chatbuf[l1]="EMPTY";
	  end
	end

  FaceSmasher.timeSinceLastUpdate = 0;
  local guid = UnitGUID("target")
  if  UnitName("target") == nil or UnitIsFriend("player","target") ~= nil or UnitHealth("target") == 0 then

	return -- ignore the dead and friendly
  end
  
  
  if guid == nil then
    FaceSmasher.textureList["last"]:SetTexture(nil)
    FaceSmasher.textureList["current"]:SetTexture(nil)
    FaceSmasher.textureList["next"]:SetTexture(nil)
	FaceSmasher.textureList["misc"]:SetTexture(nil)
	FaceSmasher.textureList["int"]:SetTexture(nil)

    return
  end  
  
  local runes = {0,0,0,0}
	 for i=1,6,1 do 
		local start, duration, runeReady = GetRuneCooldown(i)
		local runeType = GetRuneType(i)
		if runeReady then
			runes[runeType] = runes[runeType] + 1;
		end
	 end
  local runic = UnitPower("Player");
 
  local spell = ""
  local defspell = ""
  local aoespell = ""
  local miscspell = ""
  local intspell = ""
  
	if FaceSmasherdb.modeSingle then
		spell = FaceSmasher:NextSpell(runes, runic)
	end
	if FaceSmasherdb.modeDefensive then
		defspell = FaceSmasher:DefSpell(runes, runic)
	end
	if FaceSmasherdb.modeAoE then
		aoespell = FaceSmasher:AoESpell(runes, runic)
	end
	if FaceSmasherdb.modeMisc then
		miscspell = FaceSmasher:MiscSpell(runes, runic)
	end
	if FaceSmasherdb.modeInt then
		intspell = FaceSmasher:IntSpell(runes, runic)
	end
	
	if FaceSmasherdb.DSMid and defspell == FaceSmasher.SL["Death Strike"] then
		defspell = spell
		spell = FaceSmasher.SL["Death Strike"]
	end
	

   
   FaceSmasher.textureList["current"]:SetTexture(GetSpellTexture(spell))
   FaceSmasher.textureList["last"]:SetTexture(GetSpellTexture(defspell))
   FaceSmasher.textureList["misc"]:SetTexture(GetSpellTexture(miscspell))
   FaceSmasher.textureList["int"]:SetTexture(GetSpellTexture(intspell))
   
    if spell == FaceSmasher.SL["Plague Strike"] or spell == FaceSmasher.SL["Icy Touch"] then
		if aoespell == FaceSmasher.SL["Unholy Blight"] or aoespell == FaceSmasher.SL["Death and Decay"] then
			FaceSmasher.textureList["next"]:SetTexture(GetSpellTexture(aoespell))
		else
			FaceSmasher.textureList["next"]:SetTexture(nil)
		end
	else
		FaceSmasher.textureList["next"]:SetTexture(GetSpellTexture(aoespell))
	end
   
	if spell ~= "" and spell ~= nil then
	   local start, dur = GetSpellCooldown(spell)
		if dur == 0 then
			FaceSmasher.cooldownFrame:SetAlpha(0)
			if miscspell == FaceSmasher.SL["Horn of Winter"] then
				chatbuf[chatcount+4] = format("%d:%s", chatcount,miscspell);
			elseif intspell == FaceSmasher.SL["Mind Freeze"] or intspell == FaceSmasher.SL["Strangulate"] or intspell == FaceSmasher.SL["Gnaw"] then
				chatbuf[chatcount+4] = format("%d:%s", chatcount,intspell);
			else
				chatbuf[chatcount+4] = format("%d:%s", chatcount,spell);
			end
	    chatbuf[3] = chatcount; -- store the slot we just wrote to
	    chatcount = chatcount+1;
		else
			if start ~= nil and dur ~= nil then
				FaceSmasher.cooldownFrame:SetAlpha(1)
				FaceSmasher.cooldownFrame:SetCooldown(start, dur)
			end
		end
	else

		chatbuf[chatcount+4] = format("%d:Location--%s", chatcount, GetZoneText());
		chatbuf[3] = chatcount; -- store the slot we just wrote to
		chatcount = chatcount+1;
	end
 
    
     
end

function FaceSmasher:NextSpell(runes, runic)

	local guid = UnitGUID("target")
	local currentTime = GetTime()
	local GCD = 1.5 - (1.5 * FaceSmasher.spellHaste * .01)
	
	-- check our runes yo
	--start, duration, runeReady = GetRuneCooldown(id)
	--runeType = GetRuneType(id)  BUFD

    FaceSmasher.badbad = 0;
	
	if FaceSmasher.freezingFog and IsSpellInRange(FaceSmasher.SL["Howling Blast"], "target") == 1 then
			return FaceSmasher.SL["Howling Blast"]
		end

		
		
	-- Every spec wants diseases up always... I'm fairly sure ;)

	
	FaceSmasher.ps = false
	if FaceSmasherdb.usePS == false then
		FaceSmasher.ps = false;
	elseif FaceSmasher.plagueList[guid] == nil then
		FaceSmasher.ps = true;
	elseif FaceSmasherdb.dDuration - (currentTime - FaceSmasher.plagueList[guid]) < GCD then
		FaceSmasher.ps = true;
	end
	if FaceSmasher.ps and IsSpellInRange(FaceSmasher.SL["Plague Strike"], "target") == 1 then
		if IsUsableSpell(FaceSmasher.SL["Plague Strike"]) then
			return FaceSmasher.SL["Plague Strike"]
		else
			-- uh oh, can't apply PS and it's down... we need to death rune something and then ps... right.
			FaceSmasher.badbad = FaceSmasher.badbad + 1;
		end
	end
	
	FaceSmasher.it = false
	if FaceSmasher.icyList[guid] == nil then
		FaceSmasher.it = true;
	elseif FaceSmasherdb.dDuration - (currentTime - FaceSmasher.icyList[guid]) < GCD then
		FaceSmasher.it = true;
	end
	if FaceSmasher.it and IsSpellInRange(FaceSmasher.SL["Icy Touch"], "target") == 1 then
		if IsUsableSpell(FaceSmasher.SL["Icy Touch"]) then
			return FaceSmasher.SL["Icy Touch"]
		else
			-- uh oh, can't apply IT and it's down... we need to death rune something and then IT... right.
			FaceSmasher.badbad = FaceSmasher.badbad + 1;
		end
	end
	
	
	if FaceSmasher.badbad == 1 then
		-- Bleh I say bleh.  Not working the best we can.  Convert to death rune if we can
		if FaceSmasher.playerLevel > 63 and IsUsableSpell(FaceSmasher.SL["Blood Tap"]) then
			return FaceSmasher.SL["Blood Tap"]
		end
	end
	
	
	if runic > 99 and not FaceSmasher.lockRunic then
		if FaceSmasherdb.unholyblight  then
			if (currentTime - FaceSmasher.blightTime) > 20 then
				return FaceSmasher.SL["Unholy Blight"]
			end
		end
		if FaceSmasherdb.spec == FaceSmasher.SL["Frost Strike"] and IsSpellInRange(FaceSmasher.SL["Frost Strike"], "target") == 1 then
			return FaceSmasher.SL["Frost Strike"]
		--elseif IsSpellInRange(FaceSmasher.SL["Death Coil"], "target") == 1 then
		--	return FaceSmasher.SL["Death Coil"]
		end
	end
	
	
	if FaceSmasherdb.bloodfirst then
		-- Use our blood runes
		if runes[1] > 0 then
			if FaceSmasherdb.spec == FaceSmasher.SL["Heart Strike"] and IsSpellInRange(FaceSmasher.SL["Heart Strike"], "target") == 1 then
				return FaceSmasher.SL["Heart Strike"]
			elseif IsSpellInRange(FaceSmasher.SL["Blood Strike"], "target") == 1 then
				return FaceSmasher.SL["Blood Strike"]
			end
		end	
		-- If we're heart strike spec, we should use our death runes here too for heart strikes
		if FaceSmasherdb.spec == FaceSmasher.SL["Heart Strike"] and IsSpellInRange(FaceSmasher.SL["Heart Strike"], "target") == 1 then
			if runes[4] > 0 then
				return FaceSmasher.SL["Heart Strike"]
			end
		end
	end
	
	
	if FaceSmasherdb.HowlingFirst and IsUsableSpell(FaceSmasher.SL["Howling Blast"]) then
		if FaceSmasherdb.howling and IsSpellInRange(FaceSmasher.SL["Howling Blast"], "target") == 1 then
			local s, d, e = GetSpellCooldown(FaceSmasher.SL["Howling Blast"])
			if d <= 1.5 then
				return FaceSmasher.SL["Howling Blast"]
			end
		end
	end
	
	if IsSpellInRange(FaceSmasherdb.uf, "target") == 1 and IsUsableSpell(FaceSmasherdb.uf)then
		return FaceSmasherdb.uf
	end
	
	if not FaceSmasherdb.HowlingFirst and IsUsableSpell(FaceSmasher.SL["Howling Blast"]) then
		-- in case Oblit is out of range, but we have the runes for UF strike, and we have howling blast, and hb is in range.... ya...
		if FaceSmasherdb.howling and IsSpellInRange(FaceSmasher.SL["Howling Blast"], "target") == 1 then
			local s, d, e = GetSpellCooldown(FaceSmasher.SL["Howling Blast"])
			if d <= 1.5 then
				return FaceSmasher.SL["Howling Blast"]
			end
		end
	end
	
	if not FaceSmasherdb.bloodfirst then
		-- Use our blood runes
		if runes[1] > 0 then
			if FaceSmasherdb.spec == FaceSmasher.SL["Heart Strike"] and IsSpellInRange(FaceSmasher.SL["Heart Strike"], "target") == 1 then
				return FaceSmasher.SL["Heart Strike"]
			elseif IsSpellInRange(FaceSmasher.SL["Blood Strike"], "target") == 1 then
				return FaceSmasher.SL["Blood Strike"]
			end
		end	
		-- If we're heart strike spec, we should use our death runes here too for heart strikes
		if FaceSmasherdb.spec == FaceSmasher.SL["Heart Strike"] and IsSpellInRange(FaceSmasher.SL["Heart Strike"], "target") == 1 then
			if runes[4] > 0 then
				return FaceSmasher.SL["Heart Strike"]
			end
		end
	end
	if FaceSmasherdb.unholyblight and runic > 39 and not FaceSmasher.lockRunic then
		if (currentTime - FaceSmasher.blightTime) > 20 then
			return FaceSmasher.SL["Unholy Blight"]
		end
	end
	if runic >= FaceSmasher.fsCost and not FaceSmasher.lockRunic then
		if FaceSmasherdb.spec == FaceSmasher.SL["Frost Strike"] and IsSpellInRange(FaceSmasher.SL["Frost Strike"], "target") == 1 then
			return FaceSmasher.SL["Frost Strike"]
		--elseif IsSpellInRange(FaceSmasher.SL["Death Coil"], "target") == 1 and runic >= 40 then
		--	return FaceSmasher.SL["Death Coil"]
		end
	end
	
	
	return ""

end

function FaceSmasher:DefSpell(runes, runic)
	
	local currentTime = GetTime()
	
	if not FaceSmasher.it and not FaceSmasher.ps and UnitHealth("player") / UnitHealthMax("player") * 100 <= FaceSmasherdb.healthPercent then 
		if IsUsableSpell(FaceSmasher.SL["Death Strike"]) and IsSpellInRange(FaceSmasher.SL["Death Strike"], "target") == 1  then
			return FaceSmasher.SL["Death Strike"]
		elseif FaceSmasherdb.runetap then
			if IsUsableSpell(FaceSmasher.SL["Rune Tap"]) then
				local s, d, e = GetSpellCooldown(FaceSmasher.SL["Rune Tap"])
				if d <= 1.5 then
					return FaceSmasher.SL["Rune Tap"]
				end
			end
		end
	end
		
 
	
	if FaceSmasher.shieldDown then
		if IsUsableSpell(FaceSmasherdb.shield) then
			local s, d, e = GetSpellCooldown(FaceSmasherdb.shield)
			if d <= 1.5 then
				return FaceSmasherdb.shield
			end
		end

		if IsUsableSpell(FaceSmasher.SL["Icebound Fortitude"]) then
			local s, d, e = GetSpellCooldown(FaceSmasher.SL["Icebound Fortitude"])
			if d <= 1.5 then
				return FaceSmasher.SL["Icebound Fortitude"]
			end
		end

		if FaceSmasherdb.lichborne and IsUsableSpell(FaceSmasher.SL["Lichborne"]) then
			local s, d, e = GetSpellCooldown(FaceSmasher.SL["Lichborne"])
			if d <= 1.5 then
				return FaceSmasher.SL["Lichborne"]
			end
		end
	end
	
	-- guess we got nothing
	return ""

end

function FaceSmasher:AoESpell(runes, runic)

	local guid = UnitGUID("target")
	local currentTime = GetTime()
	local GCD = 1.5 - (1.5 * FaceSmasher.spellHaste * .01)
	
	if IsUsableSpell(FaceSmasher.SL["Unholy Blight"]) and not FaceSmasher.lockRunic then
		if (currentTime - FaceSmasher.blightTime) > 20 then
			return FaceSmasher.SL["Unholy Blight"]
		end
	end
	
	-- THERE HAS TO BE A BETTER WAY TO FIGURE OUT IF WE HAVE THE RUNES FOR DND... -_-
	if not FaceSmasherdb.howling then
		if IsUsableSpell(FaceSmasher.SL["Death and Decay"]) then
			return FaceSmasher.SL["Death and Decay"]
		end
	end
	-- THAT WAS FUN
	
	if IsUsableSpell(FaceSmasher.SL["Howling Blast"]) and IsSpellInRange(FaceSmasher.SL["Howling Blast"], "target") == 1 then
		local s, d, e = GetSpellCooldown(FaceSmasher.SL["Howling Blast"])
			if d <= 1.5 then
				return FaceSmasher.SL["Howling Blast"]
			end
	end

	-- If the target needs IT or PS, this will get filtered out in the DecideSpells.  We use dDuration since we should only pest once per disease application optimally
	if IsSpellInRange(FaceSmasher.SL["Pestilence"], "target") == 1 then
		if runes[1] > 0 or runes[4] > 0 then
			return FaceSmasher.SL["Pestilence"]
		end
	end
	
	-- Blood boil... I guess
	if runes[1] > 0 or runes[4] > 0 then
		if IsSpellInRange(FaceSmasher.SL["Blood Boil"], "target") == 1 then
			return FaceSmasher.SL["Blood Boil"]
		end
	end
	
	return ""

end

function FaceSmasher:MiscSpell(runes, runic)

	local guid = UnitGUID("target")
	local currentTime = GetTime()
	local GCD = 1.5 - (1.5 * FaceSmasher.spellHaste * .01)

		
	-- Let's dance... if we're blood.
	if IsUsableSpell(FaceSmasher.SL["Dancing Rune Weapon"]) then
		local s, d, e = GetSpellCooldown(FaceSmasher.SL["Dancing Rune Weapon"])
		if d <= 1.5 then
			if runic >= 100 then
				FaceSmasher.lockRunic = false;
				return FaceSmasher.SL["Dancing Rune Weapon"]
			else
				FaceSmasher.lockRunic = true;
			end
		end
	end
	
	-- check if rune strike is up cause it's awsome
	if IsUsableSpell(FaceSmasher.SL["Rune Strike"]) then
		return FaceSmasher.SL["Rune Strike"]
	end
	
	if FaceSmasherdb.horn and IsUsableSpell(FaceSmasher.SL["Horn of Winter"]) and not FaceSmasher.horn then
		local s, d, e = GetSpellCooldown(FaceSmasher.SL["Horn of Winter"])
			if d <= 1.5 then
				return FaceSmasher.SL["Horn of Winter"]
			end
	end
	
	return ""
	
end

function FaceSmasher:IntSpell(runes, runic)

	local guid = UnitGUID("target")
	local currentTime = GetTime()
	local GCD = 1.5 - (1.5 * FaceSmasher.spellHaste * .01)

	local spell = UnitCastingInfo("target")
	local channel = UnitChannelInfo("target")

	
	if spell or channel then 
		if IsUsableSpell(FaceSmasher.SL["Mind Freeze"]) and IsSpellInRange(FaceSmasher.SL["Mind Freeze"], "target") == 1 then
			local s, d, e = GetSpellCooldown(FaceSmasher.SL["Mind Freeze"])
			if d == 0 then
				return FaceSmasher.SL["Mind Freeze"]
			end
		elseif IsUsableSpell(FaceSmasher.SL["Strangulate"]) and IsSpellInRange(FaceSmasher.SL["Strangulate"], "target") == 1 then
			local s, d, e = GetSpellCooldown(FaceSmasher.SL["Strangulate"])
			if d <= 1.5 then
				return FaceSmasher.SL["Strangulate"]
			end
		end
	end


	local petspell = UnitCastingInfo("pettarget")
	local petchannel = UnitChannelInfo("pettarget")
	if petspell or petchannel then
		if IsUsableSpell(FaceSmasher.SL["Leap"]) and IsSpellInRange(FaceSmasher.SL["Leap"], "pettarget") == 1 then
			local s, d, e = GetSpellCooldown(FaceSmasher.SL["Gnaw"])
			if d == 0 then
				return FaceSmasher.SL["Gnaw"]
			end
		elseif IsUsableSpell(FaceSmasher.SL["Gnaw"]) and IsSpellInRange(FaceSmasher.SL["Gnaw"], "pettarget") == 1 then
			local s, d, e = GetSpellCooldown(FaceSmasher.SL["Gnaw"])
			if d == 0 then
				return FaceSmasher.SL["Gnaw"]
			end
		end		
	end

	return ""

end

function FaceSmasher:CheckStuff()

	FaceSmasherdb.talentCount = {}
	
	local name,_,pointsSpent,_ = GetTalentTabInfo(1)
	FaceSmasherdb.talentCount["Blood"] = pointsSpent
	name,_,pointsSpent,_ = GetTalentTabInfo(2)
	FaceSmasherdb.talentCount["Frost"] = pointsSpent
	name,_,pointsSpent,_ = GetTalentTabInfo(3)
	FaceSmasherdb.talentCount["Unholy"] = pointsSpent
	
	if FaceSmasherdb.talentCount["Blood"] > 40 then
		local _, _, _, _, currentRank = GetTalentInfo(1, 25); -- check for Heart Strike
		if currentRank == 1 then
			FaceSmasherdb.spec = FaceSmasher.SL["Heart Strike"]
		end
	elseif FaceSmasherdb.talentCount["Frost"] > 40 then
		local _, _, _, _, currentRank = GetTalentInfo(2, 25); -- check for Frost Strike
		if currentRank == 1 then
			FaceSmasherdb.spec = FaceSmasher.SL["Frost Strike"]
		end
	elseif FaceSmasherdb.talentCount["Unholy"] > 40 then
		local _, _, _, _, currentRank = GetTalentInfo(3, 28); -- check for Scourge Strike
		if currentRank == 1 then
			FaceSmasherdb.spec = FaceSmasher.SL["Scourge Strike"]
		end
	else
		FaceSmasherdb.spec = "Default"
	end
	
	if FaceSmasherdb.spec == FaceSmasher.SL["Scourge Strike"] then
		FaceSmasherdb.uf = FaceSmasher.SL["Scourge Strike"]
	elseif FaceSmasherdb.spec == FaceSmasher.SL["Heart Strike"] then
		FaceSmasherdb.uf = FaceSmasher.SL["Death Strike"]
	else
		FaceSmasherdb.uf = FaceSmasher.SL["Obliterate"]
	end
	
	-- check for epidemic
	_, _, _, _, currentRank = GetTalentInfo(3, 4);
	FaceSmasherdb.dDuration = 15 + (currentRank * 3)
		
	
	-- check for a spec shield
	FaceSmasherdb.shield = ""
	_, _, _, _, currentRank = GetTalentInfo(1, 23);
	if currentRank == 1 then
		FaceSmasherdb.shield = FaceSmasher.SL["Vampiric Blood"]
	end
	_, _, _, _, currentRank = GetTalentInfo(2, 23);
	if currentRank == 1 then
		FaceSmasherdb.shield = FaceSmasher.SL["Unbreakable Armor"]
	end
	_, _, _, _, currentRank = GetTalentInfo(3, 25);
	if currentRank == 1 then
		FaceSmasherdb.shield = FaceSmasher.SL["Bone Shield"]
	end
	
	-- check for Lichborne as many specs can get it
	_, _, _, _, currentRank = GetTalentInfo(2, 8);
	if currentRank == 1 then
		FaceSmasherdb.lichborne = true
	else
		FaceSmasherdb.lichborne = false
	end
	
	-- check for Rune Tap as many specs can get it
	_, _, _, _, currentRank = GetTalentInfo(1, 7);
	if currentRank == 1 then
		FaceSmasherdb.runetap = true
		_, _, _, _, currentRank = GetTalentInfo(1, 10);
		FaceSmasherdb.runetapcd = 60 - (currentRank * 10)
	else
		FaceSmasherdb.runetap = false
	end
	
	-- check for DnD time reduction
	_, _, _, _, currentRank = GetTalentInfo(3, 5);
	FaceSmasherdb.dndcd = 30 - (currentRank * 5)
	
	-- check for Unholy Blight
	_, _, _, _, currentRank = GetTalentInfo(3, 14);
	if currentRank == 1 then
		FaceSmasherdb.unholyblight = true
	else
		FaceSmasherdb.unholyblight = false
	end
	
	-- check for Howling Blast
	_, _, _, _, currentRank = GetTalentInfo(2, 28);
	if currentRank == 1 then
		FaceSmasherdb.howling = true
	else
		FaceSmasherdb.howling = false
	end
	
	-- check for glyph of rune strike 58669
	if FaceSmasher:hasGlyph(58669) then
		FaceSmasherdb.rsCost = 25
	else
		FaceSmasherdb.rsCost = 20
	end
	
	-- check for glyph of frost strike
	if FaceSmasher:hasGlyph(58647) then
		FaceSmasher.fsCost = 32
	else
		FaceSmasher.fsCost = 40
	end
	
	--check for endless winter talent 18, out of 2, 10 sec reduce each
	_, _, _, _, currentRank = GetTalentInfo(2, 12);
	FaceSmasherdb.mindfreezeCost = 20 - (currentRank * 10)
	
end

-- Options Panel

function FaceSmasher:GetLocked() 
  return FaceSmasherdb.locked
end

function FaceSmasher:ToggleLocked()
  if FaceSmasherdb.locked then
    FaceSmasherdb.locked = false
    FaceSmasher.displayFrame:SetScript("OnMouseDown", function(self) self:StartMoving() end)
    FaceSmasher.displayFrame:SetScript("OnMouseUp", function(self) self:StopMovingOrSizing() end)
    FaceSmasher.displayFrame:SetScript("OnDragStop", function(self) self:StopMovingOrSizing() end)
    FaceSmasher.displayFrame:SetBackdropColor(0, 0, 0, .4)
	FaceSmasher.displayFrame:EnableMouse(true)
  else
    FaceSmasherdb.locked = true
    FaceSmasher.displayFrame:SetScript("OnMouseDown", nil)
    FaceSmasher.displayFrame:SetScript("OnMouseUp", nil)
    FaceSmasher.displayFrame:SetScript("OnDragStop", nil)
    FaceSmasher.displayFrame:SetBackdropColor(0, 0, 0, 0)
	FaceSmasher.displayFrame:EnableMouse(false)
  end
end
function FaceSmasher:GetModeSingle() 
  return FaceSmasherdb.modeSingle
end

function FaceSmasher:ToggleModeSingle()
  if FaceSmasherdb.modeSingle then
    FaceSmasherdb.modeSingle = false
  else
    FaceSmasherdb.modeSingle = true
  end
end
function FaceSmasher:GetModeAoE() 
  return FaceSmasherdb.modeAoE
end

function FaceSmasher:ToggleModeAoE()
  if FaceSmasherdb.modeAoE then
    FaceSmasherdb.modeAoE = false
  else
    FaceSmasherdb.modeAoE = true
  end
end
function FaceSmasher:GetModeDefensive() 
  return FaceSmasherdb.modeDefensive
end

function FaceSmasher:ToggleModeDefensive()
  if FaceSmasherdb.modeDefensive then
    FaceSmasherdb.modeDefensive = false
  else
    FaceSmasherdb.modeDefensive = true
  end
end

function FaceSmasher:GetModeMisc() 
  return FaceSmasherdb.modeMisc
end

function FaceSmasher:ToggleModeMisc()
  if FaceSmasherdb.modeMisc then
    FaceSmasherdb.modeMisc = false
  else
    FaceSmasherdb.modeMisc = true
  end
end

function FaceSmasher:GetModeInt() 
  return FaceSmasherdb.modeInt
end

function FaceSmasher:ToggleModeInt()
  if FaceSmasherdb.modeInt then
    FaceSmasherdb.modeInt = false
  else
    FaceSmasherdb.modeInt = true
  end
end

function FaceSmasher:GetHorn() 
  return FaceSmasherdb.horn
end

function FaceSmasher:ToggleHorn()
  if FaceSmasherdb.horn then
    FaceSmasherdb.horn = false
  else
    FaceSmasherdb.horn = true
  end
end
function FaceSmasher:GetBlood() 
  return FaceSmasherdb.bloodfirst
end

function FaceSmasher:ToggleBlood()
  if FaceSmasherdb.bloodfirst then
    FaceSmasherdb.bloodfirst = false
  else
    FaceSmasherdb.bloodfirst = true
  end
end

function FaceSmasher:GetDSMid() 
  return FaceSmasherdb.DSMid
end

function FaceSmasher:ToggleDSMid()
  if FaceSmasherdb.DSMid then
    FaceSmasherdb.DSMid = false
  else
    FaceSmasherdb.DSMid = true
  end
end

function FaceSmasher:GetHowlingFirst() 
  return FaceSmasherdb.HowlingFirst
end

function FaceSmasher:ToggleHowlingFirst()
  if FaceSmasherdb.HowlingFirst then
    FaceSmasherdb.HowlingFirst = false
  else
    FaceSmasherdb.HowlingFirst = true
  end
end

function FaceSmasher:GetPS() 
  return FaceSmasherdb.usePS
end

function FaceSmasher:TogglePS()
  if FaceSmasherdb.usePS then
    FaceSmasherdb.usePS = false
  else
    FaceSmasherdb.usePS = true
  end
end

function FaceSmasher:GetITD() 
  return FaceSmasherdb.useITD
end

function FaceSmasher:ToggleITD()
  if FaceSmasherdb.useITD then
    FaceSmasherdb.useITD = false
  else
    FaceSmasherdb.useITD = true
  end
end
FaceSmasher.an = "FaceSmasher"
function FaceSmasher:GetScale()
  return FaceSmasherdb.scale
end
function FaceSmasher:SetScale(num)
  FaceSmasherdb.scale = num
  FaceSmasher.displayFrame:SetScale(FaceSmasherdb.scale)
  FaceSmasher.cooldownFrame:SetScale(FaceSmasherdb.scale)
end
function FaceSmasher:GetHealthPercent()
  return FaceSmasherdb.healthPercent
end
function FaceSmasher:SetHealthPercent(num)
  FaceSmasherdb.healthPercent = num
end

function FaceSmasher:CreateOptionFrame()
  local panel = CreateFrame("FRAME", "FaceSmasherOptions");
  panel.name = "Face Smasher";
  local fstring1 = panel:CreateFontString("FaceSmasherOptions_string1","OVERLAY","GameFontNormal")
  local fstring5 = panel:CreateFontString("FaceSmasherOptions_string4","OVERLAY","GameFontNormal")
  fstring1:SetText("Lock")
  fstring5:SetText("GUI Scale")
  fstring1:SetPoint("TOPLEFT", 10, -10)
  fstring5:SetPoint("TOPLEFT", 10, -40)
  local checkbox1 = CreateFrame("CheckButton", "$parent_cb1", panel, "OptionsCheckButtonTemplate")
  checkbox1:SetWidth(18)
  checkbox1:SetHeight(18)
  checkbox1:SetScript("OnClick", function() FaceSmasher:ToggleLocked() end)
  checkbox1:SetPoint("TOPRIGHT", -10, -10)
  checkbox1:SetChecked(FaceSmasher:GetLocked())
  local slider2 = CreateFrame("Slider", "$parent_sl2", panel, "OptionsSliderTemplate")
  slider2:SetMinMaxValues(.5, 1.5)
  slider2:SetValue(FaceSmasher:GetScale())
  slider2:SetValueStep(.05)
  slider2:SetScript("OnValueChanged", function(self) FaceSmasher:SetScale(self:GetValue()); getglobal(self:GetName() .. "Text"):SetText(self:GetValue())  end)
  getglobal(slider2:GetName() .. "Low"):SetText("0.5")
  getglobal(slider2:GetName() .. "High"):SetText("1.5")
  getglobal(slider2:GetName() .. "Text"):SetText(FaceSmasher:GetScale())
  slider2:SetPoint("TOPRIGHT", -10, -40)
  
  local fstring2 = panel:CreateFontString("FaceSmasherOptions_string2","OVERLAY","GameFontNormal")
  local fstring3 = panel:CreateFontString("FaceSmasherOptions_string3","OVERLAY","GameFontNormal")
  local fstring4 = panel:CreateFontString("FaceSmasherOptions_string4","OVERLAY","GameFontNormal")
  local fstring6 = panel:CreateFontString("FaceSmasherOptions_string6","OVERLAY","GameFontNormal")
  local fstring7 = panel:CreateFontString("FaceSmasherOptions_string7","OVERLAY","GameFontNormal")
  fstring2:SetText("Single Target Enabled")
  fstring3:SetText("AoE Enabled")
  fstring4:SetText("Defensive Enabled")
  fstring6:SetText("Misc Enabled")
  fstring7:SetText("Interrupt Enabled")
  fstring2:SetPoint("TOPLEFT", 10, -70)
  fstring3:SetPoint("TOPLEFT", 10, -100)
  fstring4:SetPoint("TOPLEFT", 10, -130)
  fstring6:SetPoint("TOPLEFT", 10, -160)
  fstring7:SetPoint("TOPLEFT", 10, -190)
  local checkbox2 = CreateFrame("CheckButton", "$parent_cb2", panel, "OptionsCheckButtonTemplate")
  checkbox2:SetWidth(18)
  checkbox2:SetHeight(18)
  checkbox2:SetScript("OnClick", function() FaceSmasher:ToggleModeSingle() end)
  checkbox2:SetPoint("TOPRIGHT", -10, -70)
  checkbox2:SetChecked(FaceSmasher:GetModeSingle())
  local checkbox3 = CreateFrame("CheckButton", "$parent_cb3", panel, "OptionsCheckButtonTemplate")
  checkbox3:SetWidth(18)
  checkbox3:SetHeight(18)
  checkbox3:SetScript("OnClick", function() FaceSmasher:ToggleModeAoE() end)
  checkbox3:SetPoint("TOPRIGHT", -10, -100)
  checkbox3:SetChecked(FaceSmasher:GetModeAoE())
  local checkbox4 = CreateFrame("CheckButton", "$parent_cb4", panel, "OptionsCheckButtonTemplate")
  checkbox4:SetWidth(18)
  checkbox4:SetHeight(18)
  checkbox4:SetScript("OnClick", function() FaceSmasher:ToggleModeDefensive() end)
  checkbox4:SetPoint("TOPRIGHT", -10, -130)
  checkbox4:SetChecked(FaceSmasher:GetModeDefensive())
  local checkbox5 = CreateFrame("CheckButton", "$parent_cb5", panel, "OptionsCheckButtonTemplate")
  checkbox5:SetWidth(18)
  checkbox5:SetHeight(18)
  checkbox5:SetScript("OnClick", function() FaceSmasher:ToggleModeMisc() end)
  checkbox5:SetPoint("TOPRIGHT", -10, -160)
  checkbox5:SetChecked(FaceSmasher:GetModeMisc())
  local checkbox6 = CreateFrame("CheckButton", "$parent_cb6", panel, "OptionsCheckButtonTemplate")
  checkbox6:SetWidth(18)
  checkbox6:SetHeight(18)
  checkbox6:SetScript("OnClick", function() FaceSmasher:ToggleModeInt() end)
  checkbox6:SetPoint("TOPRIGHT", -10, -190)
  checkbox6:SetChecked(FaceSmasher:GetModeInt())
  
  
  local fstring5 = panel:CreateFontString("FaceSmasherOptions_string5","OVERLAY","GameFontNormal")
  fstring5:SetText("Health % for DS and RT")
  fstring5:SetPoint("TOPLEFT", 10, -220)
  
  local slider1 = CreateFrame("Slider", "$parent_sl1", panel, "OptionsSliderTemplate")
  slider1:SetMinMaxValues(0, 100)
  slider1:SetValue(FaceSmasher:GetHealthPercent())
  slider1:SetValueStep(1)
  slider1:SetScript("OnValueChanged", function(self) FaceSmasher:SetHealthPercent(self:GetValue()); getglobal(self:GetName() .. "Text"):SetText(self:GetValue()) end)
   getglobal(slider1:GetName() .. "Low"):SetText("1")
  getglobal(slider1:GetName() .. "High"):SetText("100")
  getglobal(slider1:GetName() .. "Text"):SetText(FaceSmasher:GetHealthPercent())
  slider1:SetPoint("TOPRIGHT", -10, -220)
  
  local fstring8 = panel:CreateFontString("FaceSmasherOptions_string8","OVERLAY","GameFontNormal")
  fstring8:SetText("Suggest Horn of Winter")
  fstring8:SetPoint("TOPLEFT", 10, -250)
  
  local checkbox8 = CreateFrame("CheckButton", "$parent_cb8", panel, "OptionsCheckButtonTemplate")
  checkbox8:SetWidth(18)
  checkbox8:SetHeight(18)
  checkbox8:SetScript("OnClick", function() FaceSmasher:ToggleHorn() end)
  checkbox8:SetPoint("TOPRIGHT", -10, -250)
  checkbox8:SetChecked(FaceSmasher:GetHorn())
  
  
  local fstring9 = panel:CreateFontString("FaceSmasherOptions_string9","OVERLAY","GameFontNormal")
  fstring9:SetText("Prioritize Blood over Unholy + Frost")
  fstring9:SetPoint("TOPLEFT", 10, -280)
  
  local checkbox9 = CreateFrame("CheckButton", "$parent_cb9", panel, "OptionsCheckButtonTemplate")
  checkbox9:SetWidth(18)
  checkbox9:SetHeight(18)
  checkbox9:SetScript("OnClick", function() FaceSmasher:ToggleBlood() end)
  checkbox9:SetPoint("TOPRIGHT", -10, -280)
  checkbox9:SetChecked(FaceSmasher:GetBlood())
  
  local fstring10 = panel:CreateFontString("FaceSmasherOptions_string10","OVERLAY","GameFontNormal")
  fstring10:SetText("Suggest DS in the Middle")
  fstring10:SetPoint("TOPLEFT", 10, -310)
  
  local checkbox10 = CreateFrame("CheckButton", "$parent_cb10", panel, "OptionsCheckButtonTemplate")
  checkbox10:SetWidth(18)
  checkbox10:SetHeight(18)
  checkbox10:SetScript("OnClick", function() FaceSmasher:ToggleDSMid() end)
  checkbox10:SetPoint("TOPRIGHT", -10, -310)
  checkbox10:SetChecked(FaceSmasher:GetDSMid())
 
  local fstring11 = panel:CreateFontString("FaceSmasherOptions_string11","OVERLAY","GameFontNormal")
  fstring11:SetText("Prioritize Howling Blast over other UF")
  fstring11:SetPoint("TOPLEFT", 10, -340)
  
  local checkbox11 = CreateFrame("CheckButton", "$parent_cb11", panel, "OptionsCheckButtonTemplate")
  checkbox11:SetWidth(18)
  checkbox11:SetHeight(18)
  checkbox11:SetScript("OnClick", function() FaceSmasher:ToggleHowlingFirst() end)
  checkbox11:SetPoint("TOPRIGHT", -10, -340)
  checkbox11:SetChecked(FaceSmasher:GetHowlingFirst())
  
  local fstring12 = panel:CreateFontString("FaceSmasherOptions_string12","OVERLAY","GameFontNormal")
  fstring12:SetText("Use PS (if not, use blood tap as frost)")
  fstring12:SetPoint("TOPLEFT", 10, -370)
  
  local checkbox12 = CreateFrame("CheckButton", "$parent_cb12", panel, "OptionsCheckButtonTemplate")
  checkbox12:SetWidth(18)
  checkbox12:SetHeight(18)
  checkbox12:SetScript("OnClick", function() FaceSmasher:TogglePS() end)
  checkbox12:SetPoint("TOPRIGHT", -10, -370)
  checkbox12:SetChecked(FaceSmasher:GetPS())
  
  local fstring13 = panel:CreateFontString("FaceSmasherOptions_string13","OVERLAY","GameFontNormal")
  fstring13:SetText("Use IT for Death Runes (for DW)")
  fstring13:SetPoint("TOPLEFT", 10, -400)
  
  local checkbox13 = CreateFrame("CheckButton", "$parent_cb13", panel, "OptionsCheckButtonTemplate")
  checkbox13:SetWidth(18)
  checkbox13:SetHeight(18)
  checkbox13:SetScript("OnClick", function() FaceSmasher:ToggleITD() end)
  checkbox13:SetPoint("TOPRIGHT", -10, -400)
  checkbox13:SetChecked(FaceSmasher:GetITD())
  
  InterfaceOptions_AddCategory(panel); 
end

-- Slash Command
function FaceSmasher.Options()
  InterfaceOptionsFrame_OpenToCategory(getglobal("FaceSmasherOptions"))
end

function FaceSmasher:hasGlyph(id)
	for i = 1, 6 do
		local _, _, glyphSpell = GetGlyphSocketInfo(i)
		if glyphSpell == id then
			return true
		end
	end
end
	
	


