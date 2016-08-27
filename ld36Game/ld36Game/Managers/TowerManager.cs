using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld36Game.Managers
{
    public struct Tower
    {
        public Vector2 position;
        public double fireRate;
        public int damage;
        public int id;

        public Tower(Vector2 p, double f, int d, int i)
        {
            position = p;
            fireRate = f;
            damage = d;
            id = i;
        }
    }

    public class TowerManager
    {
        List<Tower> towers;
        Tower[] towerTypes;
        int selectedTowerType = -1;

        public TowerManager()
        {
            towers = new List<Tower>();
            towerTypes = new Tower[2];

            towerTypes[0] = new Tower(new Vector2(0, 0), 1000, 1, 0);
            towerTypes[1] = new Tower(new Vector2(0, 0), 500, 2, 1);
        }

        public int getTowerTypeCount()
        {
            return 2;
        }

        public int getSelectedTowerType()
        {
            return selectedTowerType;
        }

        public int getTowerCount()
        {
            return towers.Count;
        }

        public Tower getTower(int i)
        {
            return towers[i];
        }

        public void update()
        {
            MouseState ms = Mouse.GetState();

            for (int i = 0; i < 2; ++i)
            {
                float xPos = 700.0f;
                float yPos = 100.0f + 100.0f * i;

                if (ms.X > xPos && ms.X < xPos + 64 && ms.Y > yPos && ms.Y < yPos + 64 && ms.LeftButton == ButtonState.Pressed)
                {
                    selectedTowerType = i;
                    break;
                }
            }

            if (selectedTowerType >= 0 && ms.LeftButton == ButtonState.Released)
            {
                //set the tower
                Tower newTower = towerTypes[selectedTowerType];
                newTower.position = tilePosFromMouse(ms);
                towers.Add(newTower);
                selectedTowerType = -1;
            }
        }

        private Vector2 tilePosFromMouse(MouseState ms)
        {
            Vector2 result = new Vector2();
            result.X = (ms.X / 32) * 32.0f;
            result.Y = (ms.Y / 32) * 32.0f;
            return result;
        }
    }
}
