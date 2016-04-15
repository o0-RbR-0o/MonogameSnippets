using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


/*

	Simple Menu generator taking Entry-Strings and actions which are invoked on click.
	Version: 0.5
	Author : 2016 by Kim Oliver Schweikert
	License: CC-by

*/

namespace UserInterfaces
{
    //A Menu :)
    class Menu
    {
        //Current menu entry index; -1=no active entry
        int current_entry = -1;
        //Mouse states
        MouseState mstate, mstate_old;

        //List holding all menu entries
        private List<Menu_Entry> menu_entries = new List<Menu_Entry>();
        public List<Menu_Entry> Menu_entries
        {
            get { return menu_entries; }
            set { menu_entries = value; }
        }

        //Is the menu drawn centered?
        bool centered = true;
        public bool Centered
        {
            get { return centered; }
            set { centered = value; }
        }

        //Font for the menu
        SpriteFont font;
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        //Colors: Idle and active color
        Color color, color_active;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        //Bounds of the menu
        Rectangle bounds;
        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        //Creates an empty menu with the given Bounds, font, Colors and either centers the entries or renders them left-aligned
        public Menu(Rectangle bounds, SpriteFont font, Color active, Color color, bool centered)
        {
            this.centered = centered;
            this.bounds = bounds;
            this.color = color;
            this.color_active = active;
            this.font = font;
        }

        //Add an entry to the menu with given name and onclick action
        public void addEntry(string name, Action action){
            int entrynum=this.menu_entries.Count;
            Point entry_size=this.font.MeasureString(name.ToUpper()).ToPoint();

            this.menu_entries.Add(new Menu_Entry(action, name));
            
            //Calculate the bounding rectangle for this menu entry.
            if (centered)
            {
                //Fill with center-Stuff. But i guess i could just also do that with the drawing origin...
            }
            else
            {
                this.menu_entries[entrynum].Collision_rect = new Rectangle(
                        (this.bounds.Location.ToVector2() + new Vector2(0, entrynum * entry_size.Y)).ToPoint(), entry_size
                );
            }
        }

        //Remove a menu entry
        public void removeEntry(string name)
        {
            foreach(Menu_Entry m in this.menu_entries)
                if (m.Name == name) {
                    this.menu_entries.Remove(m);
                    break;
                }
        }

        //Checks for mouse click and invokes the method of the selected menu point
        public void work()
        {
            this.mstate = Mouse.GetState();
            this.current_entry = -1;

            //Find the colliding menu entry
            for (int i = 0; i < this.menu_entries.Count; i++) { 
                
                //Mouse collides with menu entry so choose this one as active.
                if(this.menu_entries[i].Collision_rect.Intersects(new Rectangle(this.mstate.Position,new Point(1,1)))){
                    this.current_entry=i;
                    break;
                }
            }

            //No intersection found at all, so the mouse is somewhere else and we don't need to do anything else and return.
            if (this.current_entry==-1) 
                return;
            
            //Intersection found - Invoke menu entry action if mouse has been pressed.
            if (this.mstate.LeftButton == ButtonState.Pressed &&this.mstate!=this.mstate_old)
            {
                this.menu_entries[this.current_entry].Action.Invoke();
            }
            this.mstate_old = this.mstate;
        }


        //Draw the menu - This doesn't take care of the centered option yet. Depends of where i put that part...
        public void draw(SpriteBatch sb) {
            
            //Draw all the entries
            for(int i=0;i<this.menu_entries.Count;i++){
                Color current_color = this.color;
                float scale = 1.0f;

                //If we draw the active entry, change size and color
                if (i == this.current_entry) {
                    scale = 1.01f;
                    current_color = this.color_active;
                }

                //Shadow for testing. Replace with effect later...
                sb.DrawString(this.font, this.menu_entries[i].Name.ToUpper(), this.menu_entries[i].Collision_rect.Location.ToVector2() + new Vector2(4, 4), Color.Black * 0.4f, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 1.0f);
                //Draw the font
                sb.DrawString(this.font, this.menu_entries[i].Name.ToUpper(), this.menu_entries[i].Collision_rect.Location.ToVector2(), current_color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 1.0f);
            }
        }

        //Menu entry subclass
        public class Menu_Entry
        {
            Rectangle collision_rect;
            public Rectangle Collision_rect
            {
                get { return collision_rect; }
                set { collision_rect = value; }
            }

            Action action;
            public Action Action
            {
                get { return action; }
                set { action = value; }
            }
            
            string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public Menu_Entry(Action action, string name)
            {
                this.action = action;
                this.name = name;
            }
        }
    }
}

