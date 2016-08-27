using ld36Game.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld36Game.Managers
{
    public class Bullet
    {
        public Vector2 position;
        public Vector2 velocity;
        public int damage;

        public Bullet(Vector2 p, Vector2 v, int d)
        {
            position = p;
            velocity = v;
            damage = d;
        }
    }

    public class Tower
    {
        public Vector2 position;
        public double fireRate;
        public int damage;
        public int id;
        public float range;
        public double cooldown;

        public Tower(Vector2 p, double f, int d, int i, float r)
        {
            position = p;
            fireRate = f;
            damage = d;
            id = i;
            range = r;
            cooldown = 0;
        }
    }

    public class TowerManager
    {
        List<Tower> towers;
        List<Bullet> bullets;
        Tower[] towerTypes;
        int selectedTowerType = -1;
        MainGameState parent;

        public TowerManager(MainGameState p)
        {
            towers = new List<Tower>();
            bullets = new List<Bullet>();

            parent = p;

            towerTypes = new Tower[2];
            towerTypes[0] = new Tower(new Vector2(0, 0), 1000, 1, 0, 100.0f);
            towerTypes[1] = new Tower(new Vector2(0, 0), 500, 2, 1, 100.0f);
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

        public Bullet getBullet(int i)
        {
            return bullets[i];
        }

        public int getBulletCount()
        {
            return bullets.Count;
        }

        public void update(GameTime time)
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
                Vector2 position = tilePosFromMouse(ms);
                double fireRate = towerTypes[selectedTowerType].fireRate;
                int damage = towerTypes[selectedTowerType].damage;
                int id = towerTypes[selectedTowerType].id;
                float range = towerTypes[selectedTowerType].range;

                Tower newTower = new Tower(position, fireRate, damage, id, range);
                towers.Add(newTower);
                selectedTowerType = -1;
            }

            if(!parent.eManager.isPaused())
            {
                for (int i = 0; i < towers.Count; ++i)
                {
                    Tower t = towers[i];
                    if (t.cooldown <= 0)
                    {
                        //get target, and fire

                        Vector2 vel = getTargetFromEnemies(t.range, t.position);
                        if(vel != Vector2.Zero)
                        {
                            Bullet b = new Bullet(t.position + new Vector2(16, 16), vel * 10.0f, 1);
                            bullets.Add(b);
                            towers[i].cooldown = t.fireRate;
                        }
                    }
                    towers[i].cooldown -= time.ElapsedGameTime.Milliseconds;

                }

                for (int i = 0; i < bullets.Count; ++i)
                {
                    bullets[i].position += bullets[i].velocity;
                    Bullet b = bullets[i];
                    for (int j = 0; j < parent.eManager.getCount(); ++j)
                    {
                        //check for collisions
                        Entity e = parent.eManager.getEntity(j);
                        if (e == null) continue;
                        float ex = e.position.X;
                        float ey = e.position.Y;

                        float bx = b.position.X;
                        float by = b.position.Y;

                        if (bx > ex && bx < ex + 32 && by > ey && by < ey + 32)
                        {
                            //collision
                            parent.eManager.kill(j);
                            bullets.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }
        }

        private Vector2 getTargetFromEnemies(float range, Vector2 position)
        {
            for(int i = 0; i < parent.eManager.getCount(); ++i)
            {
                Entity e = parent.eManager.getEntity(i);
                if (e == null) continue;
                Vector2 distanceVec = e.position - position;
                float distance = distanceVec.Length();

                if(distance <= range) //this is our target
                {
                    distanceVec.Normalize();
                    return distanceVec;
                }
            }
            return Vector2.Zero;
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
