using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace Engine
{
    public class RoomCont
    {
        public const uint RM_1 = 0;
        public const uint RM_2 = 1;
        public const uint RM_3 = 2;
        public const uint RM_4 = 3;
        public const uint RM_5 = 4;
        public const uint RM_6 = 5;

        public const char T_PLAYER = 'P';
        public const char T_WALL_BASIC = 'W';

        public GameHandler gameHandler;

        public uint currentRoom;
        public void Init()
        {
            currentRoom = RM_1;
            LoadRoom(currentRoom);
        }
        public void Update()
        {

        }
        public void LoadRoom(uint rm)
        {
            switch (rm)
            {
                case RM_1:
                    Room1 _room = new Room1();
                    List<String> _data=_room.data;
                    int _width=_room.roomWidth;
                    int _height=_room.roomHeight;

                    gameHandler = new GameHandler();

                    for (int i = _data.Count - 1; i >= 0; i--)
                    {
                        for (int y = 0; y < _height; y++)
                        {
                            for (int x = 0; x < _width; x++)
                            {
                                switch (_data[i][y * _width + x])
                                {
                                    case T_PLAYER:
                                        new Player(x * 32, y * 32);
                                        break;
                                    case T_WALL_BASIC:
                                        new WallBasic(x * 32, y * 32);
                                        break;
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}