using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    enum StateMachine
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

    enum Screen
    {
        main_menu,
        level1,
        game_over
    }
}
