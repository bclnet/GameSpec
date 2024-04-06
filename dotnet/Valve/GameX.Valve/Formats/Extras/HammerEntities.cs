namespace GameX.Valve.Formats.Extras
{
    //was:Utils/HammerEntities
    public static class HammerEntities
    {
        public static string GetToolModel(string classname)
            => classname switch
            {
                "ai_attached_item_manager" => "materials/editor/info_target.vmat",
                "ai_battle_line" => "models/pigeon.vmdl",
                "ai_goal_fightfromcover" => "materials/editor/ai_goal_follow.vmat",
                "ai_goal_follow" => "materials/editor/ai_goal_follow.vmat",
                "ai_goal_injured_follow" => "materials/editor/ai_goal_follow.vmat",
                "ai_goal_lead" => "materials/editor/ai_goal_lead.vmat",
                "ai_goal_lead_weapon" => "materials/editor/ai_goal_lead.vmat",
                "ai_goal_police" => "materials/editor/ai_goal_police.vmat",
                "ai_goal_standoff" => "materials/editor/ai_goal_standoff.vmat",
                "ai_relationship" => "materials/editor/ai_relationship.vmat",
                "ai_scripted_abilityusage" => "materials/editor/aiscripted_schedule.vmat",
                "ai_scripted_idle" => "materials/editor/aiscripted_schedule.vmat",
                "ai_scripted_moveto" => "materials/editor/ai_scripted_moveto.vmat",
                "ai_sound" => "materials/editor/ai_sound.vmat",
                "aiscripted_schedule" => "materials/editor/aiscripted_schedule.vmat",
                "ambient_generic" => "materials/editor/ambient_generic.vmat",
                "assault_assaultpoint" => "materials/editor/assault_point.vmat",
                "assault_rallypoint" => "materials/editor/assault_rally.vmat",
                "beam_spotlight" => "models/editor/cone_helper.vmdl",
                "color_correction" => "materials/editor/color_correction.vmat",
                "combine_attached_armor_prop" => "models/characters/combine_soldier_heavy/combine_hand_shield.vmdl",
                "combine_mine" => "models/props_combine/combine_mine/combine_mine.vmdl",
                "commentary_auto" => "materials/editor/commentary_auto.vmat",
                "devtest_hierarchy2" => "models/survivors/survivor_gambler.vmdl",
                "dota_color_correction" => "materials/editor/color_correction.vmat",
                "dota_world_particle_system" => "models/editor/cone_helper.vmdl",
                "env_clock" => "materials/editor/logic_timer.vmat",
                "env_combined_light_probe_volume" => "models/editor/env_cubemap.vmdl",
                "env_cubemap" => "models/editor/env_cubemap.vmdl",
                "env_cubemap_box" => "models/editor/env_cubemap.vmdl",
                "env_cubemap_fog" => "materials/editor/env_cubemap_fog.vmat",
                "env_decal" => "models/editor/axis_helper_thick.vmdl",
                "env_deferred_spot_light" => "models/editor/cone_helper.vmdl",
                "env_dof_controller" => "materials/editor/env_dof_controller.vmat",
                "env_explosion" => "materials/editor/env_explosion.vmat",
                "env_fade" => "materials/editor/env_fade.vmat",
                "env_fire" => "materials/editor/env_fire.vmat",
                "env_firesource" => "materials/editor/env_firesource.vmat",
                "env_fog_controller" => "materials/editor/env_fog_controller.vmat",
                "env_global_light" => "models/editor/spot.vmdl",
                "env_gradient_fog" => "materials/editor/env_fog_controller.vmat",
                "env_headcrabcanister" => "models/props_combine/headcrabcannister01b.vmdl",
                "env_instructor_hint" => "materials/editor/env_instructor_hint.vmat",
                "env_instructor_vr_hint" => "materials/editor/env_instructor_hint.vmat",
                "env_light_probe_volume" => "models/editor/iv_helper.vmdl",
                "env_lightglow" => "models/editor/axis_helper_thick.vmdl",
                "env_microphone" => "materials/editor/env_microphone.vmat",
                "env_physexplosion" => "materials/editor/env_physexplosion.vmat",
                "env_physimpact" => "materials/editor/env_physexplosion.vmat",
                "env_projectedtexture" => "models/editor/spot.vmdl",
                "env_rotorshooter" => "materials/editor/env_shooter.vmat",
                "env_shake" => "materials/editor/env_shake.vmat",
                "env_shooter" => "materials/editor/env_shooter.vmat",
                "env_sky" => "materials/editor/env_sky.vmat",
                "env_soundscape" => "materials/editor/env_soundscape.vmat",
                "env_soundscape_proxy" => "materials/editor/env_soundscape.vmat",
                "env_soundscape_triggerable" => "materials/editor/env_soundscape.vmat",
                "env_spark" => "materials/editor/env_spark.vmat",
                "env_speaker" => "materials/editor/ambient_generic.vmat",
                "env_spherical_vignette" => "materials/editor/env_fog_controller.vmat",
                "env_sun" => "models/editor/light_environment.vmdl",
                "env_texturetoggle" => "materials/editor/env_texturetoggle.vmat",
                "env_tilt" => "models/editor/axis_helper_thick.vmdl",
                "env_time_of_day" => "models/editor/sky_helper.vmdl",
                "env_tonemap_controller" => "materials/editor/env_tonemap_controller.vmat",
                "env_volumetric_fog_volume" => "materials/editor/fog_volume.vmat",
                "env_wind" => "materials/editor/env_wind.vmat",
                "env_wind_clientside" => "materials/editor/env_wind.vmat",
                "env_world_lighting" => "materials/editor/light_env.vmat",
                "filter_activator_attribute_int" => "materials/editor/filter_class.vmat",
                "filter_activator_class" => "materials/editor/filter_class.vmat",
                "filter_activator_context" => "materials/editor/filter_name.vmat",
                "filter_activator_mass_greater" => "materials/editor/filter_class.vmat",
                "filter_activator_model" => "materials/editor/filter_name.vmat",
                "filter_activator_name" => "materials/editor/filter_name.vmat",
                "filter_combineball_type" => "materials/editor/filter_class.vmat",
                "filter_damage_type" => "materials/editor/filter_type.vmat",
                "filter_enemy" => "materials/editor/filter_class.vmat",
                "filter_los" => "materials/editor/filter_class.vmat",
                "filter_multi" => "materials/editor/filter_multiple.vmat",
                "filter_proximity" => "materials/editor/filter_class.vmat",
                "filter_vr_grenade" => "materials/editor/filter_name.vmat",
                "game_end" => "materials/editor/game_end.vmat",
                "game_text" => "materials/editor/game_text.vmat",
                "ghost_actor" => "materials/editor/ghost_actor.vmat",
                "ghost_speaker" => "materials/editor/ghost_speaker.vmat",
                "gibshooter" => "materials/editor/gibshooter.vmat",
                "grenade_helicopter" => "models/combine_helicopter/helicopter_bomb01.vmdl",
                "haptic_relay" => "materials/editor/haptic_relay.vmat",
                "hlvr_weapon_energygun" => "models/weapons/vr_alyxgun/vr_alyxgun.vmdl",
                "info_cull_triangles" => "materials/editor/info_cull_triangles.vmat",
                "info_hint" => "models/editor/node_hint.vmdl",
                "info_hlvr_equip_player" => "materials/editor/info_hlvr_equip_player.vmat",
                "info_hlvr_holo_hacking_plug" => "models/props_combine/combine_doors/combine_hacking_interact_point.vmdl",
                "info_hlvr_offscreen_particle_texture" => "materials/editor/info_hlvr_offscreen_particle_texture.vmat",
                "info_hlvr_toner_path" => "materials/editor/info_hlvr_toner_path.vmat",
                "info_landmark" => "materials/editor/info_landmark.vmat",
                "info_lighting" => "materials/editor/info_lighting.vmat",
                "info_node" => "models/editor/ground_node.vmdl",
                "info_node_air" => "models/editor/air_node.vmdl",
                "info_node_air_hint" => "models/editor/air_node_hint.vmdl",
                "info_node_climb" => "models/editor/climb_node.vmdl",
                "info_node_hint" => "models/editor/ground_node_hint.vmdl",
                "info_notepad" => "materials/editor/info_notepad.vmat",
                "info_npc_spawn_destination" => "materials/editor/info_target.vmat",
                "info_overlay" => "models/editor/overlay_helper.vmdl",
                "info_particle_system" => "models/editor/cone_helper.vmdl",
                "info_particle_target" => "models/editor/cone_helper.vmdl",
                "info_player_start" => "models/editor/playerstart.vmdl",
                "info_player_start_badguys" => "models/editor/playerstart.vmdl",
                "info_player_start_dota" => "models/editor/playerstart.vmdl",
                "info_player_start_goodguys" => "models/editor/playerstart.vmdl",
                "info_projecteddecal" => "models/editor/axis_helper_thick.vmdl",
                "info_radar_target" => "materials/editor/info_target.vmat",
                "info_roquelaire_perch" => "materials/editor/info_target.vmat",
                "info_snipertarget" => "materials/editor/info_target.vmat",
                "info_spawngroup_landmark" => "materials/editor/info_target.vmat",
                "info_spawngroup_load_unload" => "materials/editor/info_target.vmat",
                "info_target" => "materials/editor/info_target.vmat",
                "info_target_advisor_roaming_crash" => "materials/editor/info_target.vmat",
                "info_target_gunshipcrash" => "materials/editor/info_target.vmat",
                "info_target_helicopter_crash" => "materials/editor/info_target.vmat",
                "info_target_vehicle_transition" => "materials/editor/info_target.vmat",
                "info_teleport_destination" => "models/editor/playerstart.vmdl",
                "info_teleporter_countdown" => "materials/editor/info_target.vmat",
                "info_visibility_box" => "materials/editor/info_visibility_box.vmat",
                "info_world_layer" => "materials/editor/info_world_layer.vmat",
                "infodecal" => "models/editor/axis_helper_thick.vmdl",
                "item_ammo_357" => "models/items/357ammo.vmdl",
                "item_ammo_357_large" => "models/items/357ammobox.vmdl",
                "item_ammo_ar2" => "models/items/combine_rifle_cartridge01.vmdl",
                "item_ammo_ar2_altfire" => "models/items/combine_rifle_ammo01.vmdl",
                "item_ammo_ar2_large" => "models/items/combine_rifle_cartridge01.vmdl",
                "item_ammo_crate" => "models/items/ammocrate_rockets.vmdl",
                "item_ammo_crossbow" => "models/items/crossbowrounds.vmdl",
                "item_ammo_pistol" => "models/items/boxsrounds.vmdl",
                "item_ammo_pistol_large" => "models/items/boxsrounds.vmdl",
                "item_ammo_smg1" => "models/items/boxmrounds.vmdl",
                "item_ammo_smg1_grenade" => "models/items/ar2_grenade.vmdl",
                "item_ammo_smg1_large" => "models/items/boxmrounds.vmdl",
                "item_battery" => "models/items/battery.vmdl",
                "item_box_buckshot" => "models/items/boxbuckshot.vmdl",
                "item_dynamic_resupply" => "models/items/healthkit.vmdl",
                "item_healthcharger_DEPRECATED" => "models/props_combine/health_charger001.vmdl",
                "item_healthkit" => "models/items/healthkit.vmdl",
                "item_healthvial_DEPRECATED" => "models/items/healthvial/healthvial.vmdl",
                "item_hlvr_combine_console_tank" => "models/props_combine/combine_consoles/glass_tank_01.vmdl",
                "item_hlvr_grenade_xen" => "models/weapons/vr_xen_grenade/vr_xen_grenade.vmdl",
                "item_hlvr_headcrab_gland" => "models/props/headcrab_guts/headcrab_gland.vmdl",
                "item_hlvr_weapon_energygun" => "models/weapons/vr_alyxgun/vr_alyxgun.vmdl",
                "item_hlvr_weapon_generic_pistol" => "models/weapons/vr_alyxgun/vr_alyxgun.vmdl",
                "item_hlvr_weapon_rapidfire" => "models/weapons/vr_ipistol/vr_ipistol.vmdl",
                "item_hlvr_weapon_shotgun" => "models/weapons/vr_shotgun/vr_shotgun_b.vmdl",
                "item_rpg_round" => "models/weapons/w_missile_closed.vmdl",
                "item_suit" => "models/items/hevsuit.vmdl",
                "item_suitcharger" => "models/props_combine/suit_charger001.vmdl",
                "keyframe_rope" => "models/editor/axis_helper_thick.vmdl",
                "light_ambient" => "materials/editor/light_ambient.vmat",
                "light_ambientocclusion" => "materials/editor/light_ambientocclusion.vmat",
                "light_barn" => "models/editor/spot.vmdl",
                "light_dynamic" => "materials/editor/light.vmat",
                "light_environment" => "models/editor/sun.vmdl",
                "light_importance_volume" => "materials/editor/light_importance_volume.vmat",
                "light_irradvolume" => "models/editor/iv_helper.vmdl",
                "light_omni" => "models/editor/spot.vmdl",
                "light_ortho" => "models/editor/spot.vmdl",
                "light_rect" => "models/editor/spot.vmdl",
                "light_sky" => "materials/editor/light_sky.vmat",
                "light_spot" => "models/editor/spot.vmdl",
                "logic_achievement" => "materials/editor/logic_achievement.vmat",
                "logic_auto" => "materials/editor/logic_auto.vmat",
                "logic_autosave" => "materials/editor/logic_autosave.vmat",
                "logic_branch" => "materials/editor/logic_branch.vmat",
                "logic_case" => "materials/editor/logic_case.vmat",
                "logic_choreographed_scene" => "materials/editor/choreo_scene.vmat",
                "logic_compare" => "materials/editor/logic_compare.vmat",
                "logic_distance_autosave" => "materials/editor/logic_autosave.vmat",
                "logic_door_barricade" => "materials/editor/logic_door_barricade.vmat",
                "logic_gameevent_listener" => "materials/editor/game_event_listener.vmat",
                "logic_handsup_listener" => "materials/editor/logic_hands_up.vmat",
                "logic_multicompare" => "materials/editor/logic_multicompare.vmat",
                "logic_npc_counter_aabb" => "materials/editor/math_counter.vmat",
                "logic_npc_counter_obb" => "materials/editor/math_counter.vmat",
                "logic_npc_counter_radius" => "materials/editor/math_counter.vmat",
                "logic_random_outputs" => "materials/editor/logic_random_outputs.vmat",
                "logic_relay" => "materials/editor/logic_relay.vmat",
                "logic_scene_list_manager" => "materials/editor/choreo_manager.vmat",
                "logic_script" => "materials/editor/logic_script.vmat",
                "logic_scripted_scenario" => "materials/editor/choreo_scene.vmat",
                "logic_timer" => "materials/editor/logic_timer.vmat",
                "math_counter" => "materials/editor/math_counter.vmat",
                "move_rope" => "models/editor/axis_helper.vmdl",
                "npc_antlion_grub" => "models/antlion_grub.vmdl",
                "npc_apcdriver" => "models/roller.vmdl",
                "npc_barney" => "models/characters/barney/barney.vmdl",
                "npc_bullseye" => "materials/editor/bullseye.vmat",
                "npc_clawscanner" => "models/shield_scanner.vmdl",
                "npc_combine_advisor_roaming" => "models/advisor.vmdl",
                "npc_combine_camera" => "models/combine_camera/combine_camera.vmdl",
                "npc_combine_cannon" => "models/combine_soldier.vmdl",
                "npc_combinedropship" => "models/combine_dropship.vmdl",
                "npc_combinegunship" => "models/creatures/gunship/gunship.vmdl",
                "npc_crabsynth" => "models/Synth.vmdl",
                "npc_cranedriver" => "models/roller.vmdl",
                "npc_crow" => "models/creatures/crow/crow.vmdl",
                "npc_cscanner" => "models/creatures/scanner/combine_scanner.vmdl",
                "npc_dog" => "models/dog.vmdl",
                "npc_enemyfinder" => "materials/editor/enemyfinder.vmat",
                "npc_fastzombie" => "models/Zombie/fast.vmdl",
                "npc_fastzombie_torso" => "models/Zombie/Fast_torso.vmdl",
                "npc_fisherman" => "models/Barney.vmdl",
                "npc_gman" => "models/gman.vmdl",
                "npc_grenade_frag" => "models/Weapons/w_grenade.vmdl",
                "npc_headcrab_armored" => "models/creatures/headcrab_armored/headcrab_armored.vmdl",
                "npc_heli_avoidsphere" => "materials/editor/env_firesource.vmat",
                "npc_helicopter" => "models/creatures/combine_helicopter/combine_helicopter.vmdl",
                "npc_hunter" => "models/hunter.vmdl",
                "npc_hunter_maker" => "materials/editor/npc_maker.vmat",
                "npc_ichthyosaur" => "models/ichthyosaur.vmdl",
                "npc_launcher" => "models/junk/w_traffcone.vmdl",
                "npc_maker" => "materials/editor/npc_maker.vmat",
                "npc_manhack" => "models/creatures/manhack/manhack.vmdl",
                "npc_metropolice" => "models/characters/metrocop/metrocop.vmdl",
                "npc_missiledefense" => "models/missile_defense.vmdl",
                "npc_monk" => "models/Monk.vmdl",
                "npc_mortarsynth" => "models/mortarsynth.vmdl",
                "npc_mossman" => "models/mossman.vmdl",
                "npc_pigeon" => "models/creatures/pigeon/pigeon.vmdl",
                "npc_poisonzombie" => "models/Zombie/Poison.vmdl",
                "npc_rollermine" => "models/roller.vmdl",
                "npc_seagull" => "models/creatures/seagull/seagull.vmdl",
                "npc_sniper" => "models/combine_soldier.vmdl",
                "npc_stalker" => "models/Stalker.vmdl",
                "npc_strider" => "models/creatures/strider/combine_strider.vmdl",
                "npc_template_maker" => "materials/editor/npc_maker.vmat",
                "npc_turret_citizen" => "models/props/citizen_battery_turret.vmdl",
                "npc_turret_floor" => "models/combine_turrets/floor_turret.vmdl",
                "npc_turret_ground" => "models/combine_turrets/ground_turret.vmdl",
                "npc_vehicledriver" => "models/roller.vmdl",
                "npc_zombie_blind" => "models/creatures/zombie_blind/zombie_blind.vmdl",
                "npc_zombie_torso" => "models/creatures/zombie_classic/zombie_classic_torso.vmdl",
                "npc_zombine" => "models/Zombie/zombie_soldier.vmdl",
                "phys_ballsocket" => "materials/editor/phys_ballsocket.vmat",
                "phys_constraint" => "models/editor/axis_helper_thick.vmdl",
                "phys_genericconstraint" => "models/editor/axis_helper.vmdl",
                "phys_hinge" => "models/editor/axis_helper.vmdl",
                "phys_hinge_local" => "models/editor/axis_helper.vmdl",
                "phys_lengthconstraint" => "models/editor/axis_helper.vmdl",
                "phys_pulleyconstraint" => "models/editor/axis_helper.vmdl",
                "phys_ragdollconstraint" => "models/editor/axis_helper.vmdl",
                "phys_ragdollmagnet" => "materials/editor/info_target.vmat",
                "phys_slideconstraint" => "models/editor/axis_helper.vmdl",
                "phys_splineconstraint" => "models/editor/axis_helper_thick.vmdl",
                "phys_spring" => "models/editor/axis_helper.vmdl",
                "point_aimat" => "models/editor/point_aimat.vmdl",
                "point_camera" => "models/editor/camera.vmdl",
                "point_camera_vertical_fov" => "models/editor/camera.vmdl",
                "point_devshot_camera" => "models/editor/camera.vmdl",
                "point_grabbable" => "materials/editor/point_grabbable.vmat",
                "point_hlvr_player_input_modifier" => "materials/editor/point_hlvr_player_input_modifier.vmat",
                "point_hlvr_strip_player" => "materials/editor/point_hlvr_strip_player.vmat",
                "point_instructor_event" => "materials/editor/env_instructor_hint.vmat",
                "point_soundevent" => "materials/editor/ambient_generic.vmat",
                "point_spotlight" => "models/editor/cone_helper.vmdl",
                "point_teleport" => "models/editor/axis_helper_thick.vmdl",
                "point_template" => "materials/editor/point_template.vmat",
                "point_value_remapper" => "models/editor/axis_helper_thick.vmdl",
                "point_viewcontrol" => "models/editor/camera.vmdl",
                "point_workplane" => "models/editor/axis_helper_thick.vmdl",
                "point_worldtext" => "models/editor/axis_helper_thick.vmdl",
                "postprocess_controller" => "materials/editor/postprocess_controller.vmat",
                "prop_coreball" => "models/props_combine/coreball.vmdl",
                "prop_reviver_heart" => "models/creatures/headcrab_reviver/reviver_heart.vmdl",
                "save_photogrammetry_anchor" => "materials/editor/save_photogrammetry_anchor.vmat",
                "scripted_sentence" => "materials/editor/scripted_sentence.vmat",
                "scripted_sequence" => "models/editor/scripted_sequence.vmdl",
                "scripted_target" => "materials/editor/info_target.vmat",
                "shadow_control" => "materials/editor/shadow_control.vmat",
                "snd_event_alignedbox" => "materials/editor/snd_event.vmat",
                "snd_event_param" => "materials/editor/snd_opvar_set.vmat",
                "snd_event_path_corner" => "materials/editor/snd_event.vmat",
                "snd_event_point" => "materials/editor/snd_event.vmat",
                "snd_opvar_set" => "materials/editor/snd_opvar_set.vmat",
                "snd_opvar_set_aabb" => "materials/editor/snd_opvar_set.vmat",
                "snd_opvar_set_obb" => "materials/editor/snd_opvar_set.vmat",
                "snd_opvar_set_path_corner" => "materials/editor/snd_opvar_set.vmat",
                "snd_opvar_set_point" => "materials/editor/snd_opvar_set.vmat",
                "snd_opvar_set_wind_obb" => "materials/editor/snd_opvar_set.vmat",
                "snd_sound_area_obb" => "materials/editor/snd_opvar_set.vmat",
                "snd_sound_area_sphere" => "materials/editor/snd_opvar_set.vmat",
                "snd_soundscape" => "materials/editor/env_soundscape.vmat",
                "snd_soundscape_proxy" => "materials/editor/env_soundscape.vmat",
                "snd_soundscape_triggerable" => "materials/editor/env_soundscape.vmat",
                "snd_stack_save" => "materials/editor/snd_event.vmat",
                "sound_opvar_set" => "materials/editor/ambient_generic.vmat",
                "tanktrain_ai" => "materials/editor/tanktrain_ai.vmat",
                "tanktrain_aitarget" => "materials/editor/tanktrain_aitarget.vmat",
                "tutorial_npc_blocker" => "materials/editor/info_target.vmat",
                "vgui_movie_display" => "models/editor/axis_helper_thick.vmdl",
                "vgui_slideshow_display" => "models/editor/axis_helper_thick.vmdl",
                "visibility_hint" => "materials/editor/visibility_hint.vmat",
                "vr_teleport_marker" => "models/effects/teleport/teleport_marker.vmdl",
                "water_lod_control" => "materials/editor/waterlodcontrol.vmat",
                "weapon_357" => "models/weapons/w_357.vmdl",
                "weapon_alyxgun" => "models/weapons/W_Alyx_Gun.vmdl",
                "weapon_annabelle" => "models/weapons/W_annabelle.vmdl",
                "weapon_ar2" => "models/weapons/vr_irifle/vr_irifle.vmdl",
                "weapon_bugbait" => "models/spore.vmdl",
                "weapon_crossbow" => "models/weapons/w_crossbow.vmdl",
                "weapon_crowbar" => "models/weapons/w_crowbar.vmdl",
                "weapon_frag" => "models/weapons/w_grenade.vmdl",
                "weapon_physcannon" => "models/weapons/w_physics.vmdl",
                "weapon_pistol" => "models/weapons/vr_pistol/vr_pistol.vmdl",
                "weapon_rpg" => "models/weapons/w_rocket_launcher.vmdl",
                "weapon_shotgun" => "models/weapons/vr_shotgun/vr_shotgun.vmdl",
                "weapon_smg1" => "models/weaponsmodels/weapons/vr_smg/vr_smg.vmdl",
                "weapon_striderbuster" => "models/magnusson_device.vmdl",
                "weapon_stunstick" => "models/weapons/vr_baton/vr_baton.vmdl",
                "weapon_zipline" => "models/weapons/w_crossbow.vmdl",
                "xen_flora_animatedmover" => "models/props/xen_infestation_v2/xen_v2_floater_jellybobber.vmdl",
                "xen_foliage_bloater" => "models/props/xen_infestation/boomerplant_01.vmdl",
                "xen_foliage_grenade_spawner" => "models/props/xen_infestation/xen_grenade_plant.vmdl",
                "xen_foliage_turret" => "models/props/xen_infestation/xen_grenade_plant.vmdl",
                _ => "materials/editor/obsolete.vmat",
            };
    }
}