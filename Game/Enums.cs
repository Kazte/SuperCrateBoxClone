﻿namespace Game
{
    public enum StateMachine
    {
        idle_left,
        idle_right,
        walk_left,
        walk_right,
        jump_left,
        jump_right,
        death_left,
        death_right
    }

    public enum Screen
    {
        main_menu,
        select_level,
        level1,
        level2,
        level3,
        game_over
    }
}
