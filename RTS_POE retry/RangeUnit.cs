using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS_POE
{
    class RangeUnit : Unit
    {


        public RangeUnit(string name, int xPos, int yPos, int health, int speed, int attack, int attackRange, int team, string symbol, bool isAttacking) : base(xPos, yPos, 70, 1, attack, 2, team, "U", false)
        {
            HEALTH_MAX = health;
            this.name = name;
            this.xPos = xPos;
            this.yPos = yPos;
            this.health = health;
            this.speed = speed;
            this.attack = attack;
            this.attackRange = attackRange;
            this.team = team;
            this.symbol = symbol;
            this.isAttacking = isAttacking;
        }

        public int XPos
        { get { return this.xPos; } set { xPos = value; } }

        public int YPos
        { get { return this.yPos; } set { yPos = value; } }

        public int Health
        {
            get { return this.health; }

            set
            {
                if (value < 0)
                {
                    health = 0;
                    this.death();
                }
                else { health = value; }
            }
        }

        public int Speed
        {
            get { return this.speed; }

            /* set
             {
                 if (value < 0)
                 {
                     speed = 0;
                 }
                 else { speed = value; }
             } */
        }

        public int Attack
        {
            get { return this.attack; }

            set
            {
                if (value < 1)
                {
                    attack = 1;
                }
                else { attack = value; }
            }
        }

        public int AttackRange
        {
            get { return this.attackRange; }

            /* set
             {
                 if (value < 1)
                 {
                     attackRange = 1;
                 }
                 else { attackRange = value; }
             } */
        }

        public int Team
        {
            get { return team; }
        }

        public string Symbol
        {
            get { return this.symbol; }

            /* set
            {
                if ((value.Trim()).Equals(""))
                {
                    symbol = "O";
                }
                else if (value.Length > 1)
                {
                    symbol = value[0] + "";
                }
                else { symbol = value; }
                
            } */
        }

        public bool IsAttacking
        {
            get { return this.isAttacking; }

            set { isAttacking = value; }
        }

        public int HealthMax
        {
            get { return this.HEALTH_MAX; }
        }






        public override void move(int Direction, int Diagonal)
        {
            Random rnd = new Random();
            XPos += rnd.Next(-1, 1);
            YPos += rnd.Next(-1, 1);
        }

        public override void combat( Unit enemy)
        {
            enemy.Health = enemy.Health - this.attack;
        }

        public override bool inRange(Unit enemy)
        {


            double distance = Math.Sqrt(Math.Pow(Math.Abs(enemy.XPos - this.XPos), 2) + Math.Pow(Math.Abs(enemy.YPos - this.YPos), 2));
            if (distance <= AttackRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Unit nearby(Unit[] units)
        {
            Unit closestUnit = this;
            double closeestDistance = Int32.MaxValue;

            foreach (Unit u in units)
            {
                if (u.Team != this.Team)
                {
                    double distance = Math.Sqrt(Math.Pow(Math.Abs(u.XPos - this.XPos), 2) + Math.Pow(Math.Abs(u.YPos - this.YPos), 2));

                    if (distance < closeestDistance)
                    {
                        closeestDistance = distance;
                        closestUnit = u;
                    }
                }
            }
       return closestUnit;

            
        }

        public override void death()
        {

        }

        public override string ToString()
        {
            return Symbol + "  "+name+Symbol+"\nTeam: " + team + "\nPosition:  X:" + XPos + ", Y:" + YPos + "\nHP: " + Health + "\nAttack: " + Attack;

            
        }
    }
}
